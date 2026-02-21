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
        width = 50;
        height = 50;
    }

    public void moveLeft(float deltaTime)
    {
        this.x -= speed*deltaTime;
    }

    public void moveRight(float deltaTime)
    {
        this.x += speed*deltaTime;
    }

    public void moveUp(float deltaTime)
    {
        this.y -= speed*deltaTime;
    }

    public void moveDown(float deltaTime)
    {
        this.y += speed*deltaTime;
    }

    public float getX() { return x; }
    public float getY() { return y; }
    public int getHeight() { return height; }
    public int getWidth() { return width;}

}
