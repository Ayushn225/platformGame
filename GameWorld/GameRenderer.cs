using System.Numerics;
using Raylib_cs;
using static Constants;
public class GameRenderer
{

    private GameWorld myWorld;
    private Rect player;
    private LevelManager lm;
    int xlvlOffset;
    public GameRenderer(GameWorld world)
    {
        myWorld = world;
        lm = myWorld.GetLevelManager();
    }
    public void Draw()
    {
        xlvlOffset = myWorld.getXlvlOffset();
        player = myWorld.GetRect();
        Raylib.ClearBackground(Color.RayWhite);
        Raylib.DrawRectangleGradientV(0, 0, GAME_WIDTH, GAME_HEIGHT, Color.SkyBlue, Color.DarkBlue);
        lm.Draw(TILE_SIZE, xlvlOffset);
        drawEnemies();
        drawPlayer();
    }

    void drawPlayer()
    {
        Raylib.DrawRectangle((int)player.getX() - xlvlOffset, (int)player.getY(), player.getWidth(), player.getHeight(), Color.LightGray);
        Raylib.DrawRectangleLines((int)player.getX() - xlvlOffset, (int)player.getY(), player.getWidth(), player.getHeight(), Color.White);
    }

    void drawEnemies()
    {
        List<Enemy> enemies = myWorld.GetEnemies();
        foreach(Enemy enemy in enemies)
        {
            enemy.draw(Color.Red, xlvlOffset);
        }
    }
}