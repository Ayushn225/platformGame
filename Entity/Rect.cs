using System.Numerics;
using Raylib_cs;
using static Constants;

public class Rect
{
    private Rectangle rect;
    private float x, y;
    private int width, height;
    private float speedX = 200.0f * SCALE;

    //y speeds
    private float speedY = 0 * SCALE;
    private bool isAir = true;
    private float gravity = 900.0f * SCALE;
    private float jumpSpeed = -400.0f * SCALE;

    //coyote time
    private float coyoteTime = 0.08f;
    private float coyoteTimer;

    public Rect(float x, float y)
    {
        this.x = x;
        this.y = y;
        width = TILE_SIZE - 4;
        height = TILE_SIZE - 4;

        coyoteTimer = coyoteTime;
    }

    public void update(float deltaTime)
    {
        float moveDist = speedX * deltaTime;
        float dx = 0;
        float dy = 0;

        // 1. Gather input
        if (Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A)) dx -= moveDist;
        if (Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D)) dx += moveDist;
        if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up))
        {
            if (coyoteTimer > 0)
            {
                speedY = jumpSpeed;
                isAir = true;
                coyoteTimer = 0;
            }
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

        speedY += gravity*deltaTime;
        dy += speedY * deltaTime;

        if (isAir == true)
        {
            coyoteTimer -= deltaTime;
            if(coyoteTimer<0) coyoteTimer = 0;
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
    }

    public float getX() { return x; }
    public float getY() { return y; }
    public int getHeight() { return height; }
    public int getWidth() { return width; }

}
