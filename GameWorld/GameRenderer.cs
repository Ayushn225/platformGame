using Raylib_cs;
public class GameRenderer
{

    private GameWorld myWorld;
    private Rectangle rect;
    public GameRenderer(GameWorld world)
    {
        myWorld = world;

    }

    public void Draw()
    {
        Raylib.ClearBackground(Color.RayWhite);
        List<Rect> rects = myWorld.GetRects();
        foreach (Rect rect in rects)
        {
            Raylib.DrawRectangleRec(rect.GetRect(), rect.GetColor());
        }
    }
}