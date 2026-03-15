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
        player = myWorld.GetRect();
    }
    public void Draw()
    {
        xlvlOffset = myWorld.getXlvlOffset();
        player = myWorld.GetRect();
        Raylib.ClearBackground(Color.RayWhite);
        Raylib.DrawRectangleGradientV(0, 0, GAME_WIDTH, GAME_HEIGHT, Color.SkyBlue, Color.DarkBlue);
        lm.Draw(TILE_SIZE, xlvlOffset);
        drawEnemies();
        player.draw(xlvlOffset);
    }

    void drawEnemies()
    {
        List<Enemy> enemies = myWorld.GetEnemies();
        foreach (Enemy enemy in enemies)
        {
            if(enemy.isAlive == false) enemy.draw(Color.White, xlvlOffset);
            else enemy.draw(Color.Red, xlvlOffset);
        }
    }
}