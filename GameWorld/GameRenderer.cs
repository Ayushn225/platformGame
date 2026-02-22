using System.Numerics;
using Raylib_cs;
using static Constants;
public class GameRenderer
{

    private GameWorld myWorld;
    private Rect player;
    private LevelManager levelManager;
    public GameRenderer(GameWorld world, LevelManager lm)
    {
        myWorld = world;
        levelManager = lm;
    }

    public void Draw()
    {
        player = myWorld.GetRect();
        Raylib.ClearBackground(Color.RayWhite);
        levelManager.Draw(TILE_SIZE);
        drawPlayer(TILE_SIZE);
    }

    void drawPlayer(int TILE_SIZE)
    {
        Raylib.DrawRectangle((int)player.getX(), (int)player.getY(), player.getWidth(), player.getHeight(), Color.DarkBlue);
        Raylib.DrawRectangleLines((int)player.getX(), (int)player.getY(), player.getWidth(), player.getHeight(), Color.Black);

    }
}