using Raylib_cs;

public class Enemy
{
    private Rectangle enemy;
    private Rectangle baseBounds; // Stores the original, unmodified position and size

    // Animation controls
    private float animationTime = 0f;
    private float squishSpeed = 5f;  // How fast it breathes (higher = faster)
    private float squishAmount = 4f;
    public Enemy(float x, float y, int width, int height)
    {
        enemy = new Rectangle((int)x, (int)y, (int)width, (int)height);
        baseBounds = enemy;
    }

    public void update(float deltaTime)
    {
        squishAndSquashAnimation(deltaTime);

    }

    private void squishAndSquashAnimation(float deltaTime)
    {
        animationTime += deltaTime;

        float wave = (float)Math.Sin(animationTime * squishSpeed);
        float offset = wave * squishAmount;

        enemy.Width = baseBounds.Width + offset;
        enemy.Height = baseBounds.Height - offset;

        enemy.Y = baseBounds.Y + offset;

        enemy.X = baseBounds.X - (offset / 2f);
    }

    public void draw(Color color, int xlvlOffset)
    {
        int drawX = (int)enemy.X - xlvlOffset;
        // int drawX = (int)enemy.X;

        Raylib.DrawRectangle(drawX, (int)enemy.Y, (int)enemy.Width, (int)enemy.Height, color);

        Rectangle drawRect = enemy;
        drawRect.X = drawX;
        Raylib.DrawRectangleLinesEx(drawRect, 4, Color.White);
    }

    public Rectangle getEnemy()
    {
        return enemy;
    }
}