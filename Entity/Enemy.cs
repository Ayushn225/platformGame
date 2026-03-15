using System.Collections;
using System.Reflection.Metadata;
using Raylib_cs;
using static EnemyConstants;

public class Enemy
{
    private Rectangle enemy;
    private Rectangle baseBounds; // Stores the original, unmodified position and size

    private float HealthBarWidth;
    private float HealthBarHeight;
    private int maxHealth = 19;
    private int currHealth = 19;
    private float healthWidth;
    private State currState;

    //Moving
    private float direction = -1.0f;
    private float speedX = 100.0f * Constants.SCALE;
    private float coolDownTimer = 1.5f;
    private bool coolDownRunning = false;

    //attacking
    private float attackTimer = 0.2f;
    private float attackCoolDownTimer = 0.8f;
    private int attackBoxX, attackboxY, attackBoxHeight, attackBoxWidth;
    private bool isAttacking = false;
    public bool isAlive = true;


    public Enemy(float x, float y, int width, int height)
    {
        enemy = new Rectangle((int)x, (int)y, (int)width, (int)height);
        baseBounds = enemy;
        currState = State.IDLE;

        healthInit();
        attackBoxInit();
    }

    private void attackBoxInit()
    {
        attackBoxX = (int)enemy.X;
        attackboxY = (int)enemy.Y;
        attackBoxWidth = (int)(enemy.Width * 3);
        attackBoxHeight = (int)(enemy.Height * 0.6);
    }

    private void healthInit()
    {
        HealthBarWidth = enemy.Width;
        HealthBarHeight = Constants.SCALE * 4;

        healthWidth = (int)((currHealth / (float)maxHealth) * HealthBarWidth);
    }

    private void changeHealth(int val)
    {
        currHealth += val;

        if (currHealth <= 0)
        {
            currHealth = 0;
            // enemy dies
            isAlive = false;
        }
        else if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        healthWidth = (int)((currHealth / (float)maxHealth) * HealthBarWidth);
    }

    public void update(float deltaTime, Rect player, List<Enemy> enemies)
    {
        currState = State.IDLE;
        isAttacking = false;
        float dist = player.getX() - enemy.X;
        if (!coolDownRunning)
        {
            if (Math.Abs(dist) <= SIGHT_DISTANCE) currState = State.RUNNING;
            if (Math.Abs(dist) <= ATTACKING_DISTANCE) currState = State.ATTACKING;
        }

        if (coolDownRunning == true)
        {
            coolDownTimer -= deltaTime;
            if (coolDownTimer <= 0)
            {
                coolDownRunning = false;
                coolDownTimer = 1.0f;
            }
        }
        switch (currState)
        {
            case State.IDLE:
                idleUpdate(deltaTime, enemies);
                break;
            case State.RUNNING:
                runningUpdate(deltaTime, dist, enemies);
                break;
            case State.ATTACKING:
                attackingUpdate(deltaTime, dist, player);
                break;
        }


    }

    private void attackingUpdate(float deltaTime, float dir, Rect player)
    {
        attackTimer -= deltaTime;

        if(attackTimer > 0) return;

        if (dir > 0)
            direction = 1;
        else
            direction = -1;

        attackBoxX = (int)(enemy.X - enemy.Width);
        attackboxY = (int)(enemy.Y + (enemy.Height*0.2));

        Rectangle attackBox = new Rectangle(attackBoxX, attackboxY, attackBoxWidth, attackBoxHeight);

        if(Raylib.CheckCollisionRecs(attackBox, new Rectangle(player.getX(), player.getY(), player.getWidth(), player.getHeight())) && attackTimer<=0)
        {
            player.getHit(AttackPower);
            attackTimer = attackCoolDownTimer;
            isAttacking = true;
        }
    }

    private void runningUpdate(float deltaTime, float dir, List<Enemy> enemies)
    {
        if (dir > 0)
            direction = 1;
        else
            direction = -1;

        float moveDist = 1.5f * speedX * deltaTime;
        float newX;
        if (direction > 0) newX = enemy.X + enemy.Width + (moveDist * direction);
        else newX = enemy.X + (moveDist * direction);

        bool checkWalk = CollisionDetection.canWalk(newX, enemy.Y);

        foreach (Enemy other in enemies)
        {
            if (this == other) continue;

            if (Raylib.CheckCollisionRecs(other.enemy, new Rectangle(newX, enemy.Y, enemy.Width, enemy.Height)))
            {
                coolDownRunning = true;
                return;
            }

        }

        if (checkWalk)
        {
            if (direction > 0) enemy.X = newX - enemy.Width;
            else enemy.X = newX;
        }
        else
        {
            coolDownRunning = true;
        }
    }

    private void idleUpdate(float deltaTime, List<Enemy> enemies)
    {
        float newX;
        float moveDist = speedX * deltaTime;
        if (direction > 0)
        {
            newX = enemy.X + enemy.Width + (moveDist * direction);
        }
        else
        {
            newX = enemy.X + (moveDist * direction);
        }

        foreach (Enemy other in enemies)
        {
            if (this == other) continue;

            if (Raylib.CheckCollisionRecs(other.enemy, new Rectangle(newX, enemy.Y, enemy.Width, enemy.Height)))
            {
                direction *= -1;
                return;
            }

        }

        if (CollisionDetection.canWalk(newX, enemy.Y))
        {
            if (direction > 0) enemy.X = newX - enemy.Width;
            else enemy.X = newX;
        }
        else
        {
            direction *= -1;
        }
    }

    public void draw(Color color, int xlvlOffset)
    {
        int drawX = (int)enemy.X - xlvlOffset;

        Raylib.DrawRectangle(drawX, (int)enemy.Y, (int)enemy.Width, (int)enemy.Height, color);

        Rectangle drawRect = enemy;
        drawRect.X = drawX;
        Raylib.DrawRectangleLinesEx(drawRect, 4, Color.White);

        if (currState == State.RUNNING && (isAlive == true)) Raylib.DrawText("??", (int)(drawX), (int)(enemy.Y - 32 - 2 * HealthBarHeight), 32, Color.White);
        else if (currState == State.ATTACKING && (isAlive == true)) Raylib.DrawText("!!", (int)(drawX), (int)(enemy.Y - 32 - 2 * HealthBarHeight), 32, Color.Red);
        else if (isAlive == false)  Raylib.DrawText("xx", (int)(drawX), (int)(enemy.Y - 32 - 2 * HealthBarHeight), 32, Color.Red);

        drawHealthBar(drawX);
        drawAttackBox(drawX);
    }

    private void drawAttackBox(int drawX)
    {
        if(isAttacking == false) return;
        Raylib.DrawRectangleLinesEx(new Rectangle(drawX - enemy.Width, attackboxY, attackBoxWidth, attackBoxHeight), 4, Color.Red);

    }
    private void drawHealthBar(int drawX)
    {
        Raylib.DrawRectangle((int)drawX, (int)(enemy.Y - 2 * HealthBarHeight), (int)HealthBarWidth, (int)HealthBarHeight, Color.DarkGray);
        Raylib.DrawRectangle((int)drawX, (int)(enemy.Y - 2 * HealthBarHeight), (int)healthWidth, (int)HealthBarHeight, Color.Red);
        Raylib.DrawRectangleLines((int)(drawX - 2), (int)(enemy.Y - 2 * HealthBarHeight - 2), (int)(HealthBarWidth + 4), (int)(HealthBarHeight + 4), Color.White);
    }

    public Rectangle getEnemy()
    {
        return enemy;
    }

    public void getHit(int val)
    {
        changeHealth(-val);
    }
}