using Raylib_cs;
public class GameWorld
{
    private Rect player;
    public GameWorld()
    {
        player = new Rect(Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2);
    }

    public void update(float deltaTime)
    {
        player.update(deltaTime);
    }

    public Rect GetRect()
    {
        return player;
    }
}