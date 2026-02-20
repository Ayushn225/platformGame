using Raylib_cs;

public class Rect
{
    private Rectangle rect;
    public float Speed { get; private set; }
    public int DirectionX { get; private set; }
    public int DirectionY { get; private set; }
    public float Hue { get; set; }

    public Rect(float x, float y)
    {
        int width = Raylib.GetRandomValue(20, 100);
        int height = Raylib.GetRandomValue(20, 100);
        float speed = Raylib.GetRandomValue(2, 7);

        rect = new Rectangle(x, y, width, height);

        Speed = speed;
        
        DirectionX = Random.Shared.Next(0, 2);
        DirectionY = Random.Shared.Next(0, 2);
        if (DirectionX == 0) DirectionX = -1;
        if (DirectionY == 0) DirectionY = -1;

        Hue = Random.Shared.Next(0, 360);
    }

    public void update()
    {
        if (rect.X <= 0 || rect.X + rect.Width >= 800)
        {
            changeDirection('x');
        }
        if (rect.Y <= 0 || rect.Y + rect.Height >= 800)
        {
            changeDirection('y');
        }

        rect.X += Speed * DirectionX;
        rect.Y += Speed * DirectionY;
    }



    public void changeDirection(char axis)
    {
        if (axis == 'x')
        {
            DirectionX *= -1;
        }
        else if (axis == 'y')
        {
            DirectionY *= -1;
        }
        Hue = (Hue + 30f) % 360f;
    }

    public Color GetColor()
    {
        return Raylib.ColorFromHSV(Hue, 1f, 1f);
    }

    public Rectangle GetRect()
    {
        return rect;
    }

}
