using System.Numerics;
using Raylib_cs;
using static Constants;
public class GameWorld
{
    private Rect player;
    private int xlvlOffset = 0;
    private int leftBorder = (int)(0.4 * GAME_WIDTH);
    private int rightBorder = (int)(0.6 * GAME_WIDTH);
    private int maxTileOffset;
    private int maxLevelOffset = 0;
    private LevelManager levelManager;
    private List<Enemy> enemies;
    public GameWorld()
    {
        levelManager = new LevelManager();
        Vector2 spawnCords = levelManager.getSpawnPoint();
        Vector2 flagCords = levelManager.getCompletelvlFlag();
        player = new Rect(spawnCords.X, spawnCords.Y, flagCords);

        maxTileOffset = Math.Max(LEVEL_WIDTH - TILE_COL, 0);
        maxLevelOffset = (int)(maxTileOffset * TILE_SIZE);
        
        enemies = levelManager.GetEnemies();
    }

    public void update(float deltaTime)
    {

        player.update(deltaTime, enemies);
        checkCloseToBorder();
        foreach (Enemy enemy in enemies)
        {
            if(enemy.isAlive==true) enemy.update(deltaTime, player, enemies);
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            GameStates.setGameState(GameState.Paused);
        }

    }

    private void checkCloseToBorder()
    {
        int playerX = (int)player.getX();
        int diff = playerX - xlvlOffset;

        if (diff > rightBorder)
        {
            xlvlOffset += diff - rightBorder;
        }
        else if (diff < leftBorder)
        {
            xlvlOffset += diff - leftBorder;
        }

        if (xlvlOffset > maxLevelOffset) xlvlOffset = maxLevelOffset;
        else if (xlvlOffset < 0) xlvlOffset = 0;
    }

    public Rect GetRect()
    {
        return player;
    }

    public int getXlvlOffset()
    {
        return xlvlOffset;
    }

    public LevelManager GetLevelManager()
    {
        return levelManager;
    }

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }
}