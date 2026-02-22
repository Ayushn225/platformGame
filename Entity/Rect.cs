using System.Numerics;
using Raylib_cs;

public class Rect
{
    private Rectangle rect;
    private float x, y;
    private int width, height;
    private float speed = 250.0f;

    public Rect(float x, float y)
    {
        this.x = x;
        this.y = y;
        width = Constants.TILE_SIZE - 4;
        height = Constants.TILE_SIZE - 4;
    }

    public void update(float deltaTime)
    {
        float moveDist = speed * deltaTime;
        float dx = 0;
        float dy = 0;

        // 1. Gather input
        if (Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A)) dx -= moveDist;
        if (Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D)) dx += moveDist;
        if (Raylib.IsKeyDown(KeyboardKey.Up) || Raylib.IsKeyDown(KeyboardKey.W)) dy -= moveDist;
        if (Raylib.IsKeyDown(KeyboardKey.Down) || Raylib.IsKeyDown(KeyboardKey.S)) dy += moveDist;

        if (dx != 0)
        {
            this.x += dx;
            if (CollisionDetection.detectCollWithLevel(this, 0, 0))
            {
                if (dx > 0)
                {
                    int tileCol = (int)((this.x + width - 1) / Constants.TILE_SIZE);
                    this.x = (tileCol * Constants.TILE_SIZE) - width;
                }
                else
                {
                    int tileCol = (int)(this.x / Constants.TILE_SIZE);
                    this.x = (tileCol * Constants.TILE_SIZE) + Constants.TILE_SIZE;
                }
            }
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
                    int tileRow = (int)((this.y + height - 1) / Constants.TILE_SIZE);
                    this.y = (tileRow * Constants.TILE_SIZE) - height;
                }
                else if (dy < 0) // Moving Up
                {
                    // Find the bottom edge of the wall we hit
                    int tileRow = (int)(this.y / Constants.TILE_SIZE);
                    this.y = (tileRow * Constants.TILE_SIZE) + Constants.TILE_SIZE;
                }
            }
        }
    }

    public float getX() { return x; }
    public float getY() { return y; }
    public int getHeight() { return height; }
    public int getWidth() { return width; }

}
