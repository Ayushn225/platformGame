using System.Numerics;
using Raylib_cs;
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

    public void Draw(int TILE_SIZE)
    {
        player = myWorld.GetRect();
        Raylib.ClearBackground(Color.RayWhite);
        levelManager.Draw(TILE_SIZE);
        drawPlayer(TILE_SIZE);
    }

    void drawPlayer(int TILE_SIZE)
    {
        Raylib.DrawRectangle((int)player.getX(), (int)player.getY(), TILE_SIZE, TILE_SIZE, Color.DarkBlue);
        Raylib.DrawRectangleLines((int)player.getX(), (int)player.getY(), TILE_SIZE, TILE_SIZE, Color.White);

    }
}