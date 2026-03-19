using System.Numerics;
using Raylib_cs;
using static Constants;

public class Rect
{
    private float x, y;
    private int width, height;
    private float speedX = 200.0f * SCALE;
    private bool left = false, right = false;
    private int lastDir = 1;
    //y speeds
    private float speedY = 0 * SCALE;
    private bool isAir = true;
    private float gravity = 900.0f * SCALE;
    private float jumpSpeed = -400.0f * SCALE;

    //coyote time
    private float coyoteTime = 0.08f;
    private float coyoteTimer;
    private float boostTime = 0.125f / 20.0f, resetBoost = 0.25f;

    //health bar
    private int maxHealth = 100;
    private int currHealth = 100;
    private float healthWidth;
    public static float HealthBarWidth = 150 * Constants.SCALE;
    public static float HealthBarHeight = 4 * Constants.SCALE;
    public static float HealthBarX = 34 * Constants.SCALE;
    public static float HealthBarY = 14 * Constants.SCALE;

    //attack
    private float attackTimer = 0.0f;
    private float attackDuration = 0.15f;
    private float attackCoolDown = 0.4f;
    private int attackPower = 8;
    private int attackBoxHeight, attackBoxWidth;

    // Knockback variables
    private float knockbackTimer = 0.0f;
    private float knockbackForce = 200.0f;
    private float knockbackMaxTime = 0.4f;

    //flag
    private Rectangle completeFlag;

    public Rect(float x, float y, Vector2 flagCords)
    {
        this.x = x;
        this.y = y;
        width = TILE_SIZE - 4;
        height = TILE_SIZE - 4;
        completeFlag = new Rectangle(flagCords.X, flagCords.Y, TILE_SIZE, TILE_SIZE);

        coyoteTimer = coyoteTime;

        healthInit();
        attackBoxInit();
    }

    private void healthInit()
    {
        healthWidth = (int)((currHealth / (float)maxHealth) * HealthBarWidth);
    }

    private void attackBoxInit()
    {
        attackBoxHeight = (int)(0.5 * this.height);
        attackBoxWidth = (int)(0.8 * this.width);
    }

    private void changeHealth(int val)
    {
        // Console.WriteLine("ouch: " + val);
        currHealth += val;
        // Console.WriteLine(currHealth);

        if (currHealth <= 0)
        {
            currHealth = 0;
            // change to gameover state
            GameStates.setGameState(GameState.GameOver);
        }
        else if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }

        healthWidth = (int)((currHealth / (float)maxHealth) * HealthBarWidth);
    }

    public void update(float deltaTime, List<Enemy> enemies)
    {
        if (knockbackTimer <= 0)
        {
            speedX = 200.0f * SCALE;
        }
        float moveDist = speedX * deltaTime;
        float dx = 0;
        float dy = 0;

        if (knockbackTimer > 0)
        {
            knockbackTimer -= deltaTime;
            this.x += speedX * deltaTime;
            //add feature to prevent from coliding with solid wall when knockback
        }
        else
        {
            checkCompleteFlag();

            // 1. Gather input
            if (Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A)) left = true;
            if (Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D)) right = true;
            if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up))
            {
                if (coyoteTimer > 0)
                {
                    speedY = jumpSpeed;
                    isAir = true;
                    coyoteTimer = 0;
                }
            }
        }

        if (left)
        {
            dx -= moveDist;
            left = false;
            lastDir = -1;
        }
        if (right)
        {
            dx += moveDist;
            right = false;
            lastDir = 1;
        }

        if (dx != 0)
        {
            this.x += dx;
            if (CollisionDetection.detectCollWithLevel(this, 0, 0))
            {
                if (dx > 0)
                {
                    int tileCol = (int)((this.x + width - 1) / TILE_SIZE);
                    this.x = (tileCol * TILE_SIZE) - width;
                }
                else
                {
                    int tileCol = (int)(this.x / TILE_SIZE);
                    this.x = (tileCol * TILE_SIZE) + TILE_SIZE;
                }
            }
        }

        speedY += gravity * deltaTime;
        dy += speedY * deltaTime;

        if (isAir == true)
        {
            coyoteTimer -= deltaTime;
            if (coyoteTimer < 0) coyoteTimer = 0;
        }

        if (dy != 0)
        {
            this.y += dy;

            // Check if our new Y position overlaps a wall
            if (CollisionDetection.detectCollWithLevel(this, 0, 0))
            {
                if (dy > 0) // Moving Down
                {
                    // Find the top edge of the wall we hit
                    int tileRow = (int)((this.y + height - 1) / TILE_SIZE);
                    this.y = (tileRow * TILE_SIZE) - height;

                    isAir = false;
                    coyoteTimer = coyoteTime;
                    speedY = 0;

                }
                else if (dy < 0) // Moving Up
                {
                    // Find the bottom edge of the wall we hit
                    int tileRow = (int)(this.y / TILE_SIZE);
                    this.y = (tileRow * TILE_SIZE) + TILE_SIZE;

                    speedY = 0;
                }
            }
            else
            {
                isAir = true;
            }


        }


        //attack logic
        if (attackTimer > 0) attackTimer -= deltaTime;

        if (Raylib.IsKeyPressed(KeyboardKey.Space) && attackTimer <= 0)
        {
            attackTimer = attackCoolDown;
            handleAttack(enemies);
        }
    }

    private void handleAttack(List<Enemy> enemies)
    {
        Rectangle attackBox;

        if (lastDir == 1) attackBox = new Rectangle((int)(this.getX() + this.getWidth()), this.getY() + (this.getHeight() - attackBoxHeight) / 2, attackBoxWidth, attackBoxHeight);
        else attackBox = new Rectangle((int)(this.getX() - attackBoxWidth), this.getY() + (this.getHeight() - attackBoxHeight) / 2, attackBoxWidth, attackBoxHeight);

        foreach (Enemy enemy in enemies)
        {
            if (Raylib.CheckCollisionRecs(attackBox, enemy.getEnemy()))
            {
                enemy.getHit(attackPower, this.getX());
            }
        }
    }

    public void draw(int xlvlOffset)
    {
        drawHitBox(xlvlOffset);
        drawHealthBar();
        drawAtttackBox(xlvlOffset);
    }

    private void drawAtttackBox(int xlvlOffset)
    {
        if (attackTimer <= (attackCoolDown - attackDuration)) return;
        Rectangle attackBox;

        if (lastDir == 1) attackBox = new Rectangle((int)(this.getX() - xlvlOffset + this.getWidth()), this.getY() + (int)((this.getHeight() - attackBoxHeight) / 2), attackBoxWidth, attackBoxHeight);
        else attackBox = new Rectangle((int)(this.getX() - attackBoxWidth - xlvlOffset), this.getY() + (int)((this.getHeight() - attackBoxHeight) / 2), attackBoxWidth, attackBoxHeight);

        Raylib.DrawRectangleLinesEx(attackBox, 4, Color.Black);
    }

    private void drawHitBox(int xlvlOffset)
    {

        Raylib.DrawRectangle((int)this.getX() - xlvlOffset, (int)this.getY(), this.getWidth(), this.getHeight(), Color.LightGray);
        Raylib.DrawRectangleLines((int)this.getX() - xlvlOffset, (int)this.getY(), this.getWidth(), this.getHeight(), Color.White);
    }

    private void drawHealthBar()
    {
        Raylib.DrawRectangle((int)HealthBarX, (int)HealthBarY, (int)HealthBarWidth, (int)HealthBarHeight, Color.White);
        Raylib.DrawRectangle((int)HealthBarX, (int)HealthBarY, (int)healthWidth, (int)HealthBarHeight, Color.Pink);
        Raylib.DrawRectangleLines((int)(HealthBarX - 2), (int)(HealthBarY - 2), (int)(HealthBarWidth + 4), (int)(HealthBarHeight + 4), Color.White);
    }

    public float getX() { return x; }
    public float getY() { return y; }
    public int getHeight() { return height; }
    public int getWidth() { return width; }

    public void getHit(int val, float enemyX)
    {
        changeHealth(-val);
        knockbackTimer = knockbackMaxTime;
        speedY = -400f; // Slight pop upwards
        if (this.x < enemyX) speedX = -knockbackForce;
        else speedX = knockbackForce;
    }
    private void checkCompleteFlag()
    {
        if(Raylib.CheckCollisionRecs(new Rectangle(x, y, width, height), completeFlag))
        {
            GameStates.setGameState(GameState.LevelCompleted);
        }
    }

}


