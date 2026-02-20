
using System.Collections;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Raylib_cs;
public class GameWorld
{
    private List<Rect> rects;
    public GameWorld()
    {
        rects = new List<Rect>();
    }

    public void update()
    {
        DetectClick();
        foreach (Rect rect in rects)
        {
            rect.update();
        }

    }

    public List<Rect> GetRects()
    {
        return rects;
    }


    public void DetectClick()
    {
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            Vector2 GlobalPosition = Raylib.GetMousePosition();
            rects.Add(new Rect(GlobalPosition.X, GlobalPosition.Y));
        }
    }
}