using System.Numerics;
using Raylib_cs;

public class Button
{
    private Rectangle button;
    private string title;

    Color primary, secondary;
    public bool onButton = false;
    private bool isClicked = false;

    private Action onButtonAction;

    public Button(int x, int y, int width, int height, string title, Color color, Action action)
    {
        this.title = title;
        primary = color;
        button = new Rectangle(x, y, width, height);

        float factor = 0.5f;
        byte r = (byte)(primary.R * factor);
        byte g = (byte)(primary.G * factor);
        byte b = (byte)(primary.B * factor);
        secondary = new Color(r, g, b, primary.A);

        onButtonAction = action;
    }

    public void draw()
    {
        if (!onButton)
        {
            Raylib.DrawRectangleLinesEx(button, 4, primary);
            Raylib.DrawText(title, (int)(button.X + button.Width / 2 - 4 * 6), (int)(button.Y + button.Height / 2 - 20), 32, primary);
        }
        else
        {
            if (isClicked)
            {
                Raylib.DrawRectangle((int)button.X, (int)button.Y, (int)button.Width, (int)button.Height, secondary);
                Raylib.DrawText(title, (int)(button.X + button.Width / 2 - 4 * 6), (int)(button.Y + button.Height / 2 - 20), 32, Color.White);
            }
            else
            {
                Raylib.DrawRectangle((int)button.X, (int)button.Y, (int)button.Width, (int)button.Height, primary);
                Raylib.DrawText(title, (int)(button.X + button.Width / 2 - 4 * 6), (int)(button.Y + button.Height / 2 - 18), 32, Color.White);
            }

        }

    }

    public void update()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        if (
            mousePos.X <= button.X + button.Width &&
            mousePos.X >= button.X &&
            mousePos.Y <= button.Y + button.Height &&
            mousePos.Y >= button.Y
        )
        {
            onButton = true;
        }
        else
        {
            onButton = false;
        }

        if (onButton && Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            isClicked = true;
            onClick();
        }
        else
        {
            isClicked = false;
        }
    }

    public void onClick()
    {

        //placeholder for another function that will be defined elsewhere
        onButtonAction?.Invoke();
    }

    public float getBottomPos()
    {
        return button.Y + button.Height;
    }
}