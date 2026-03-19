using System.Collections;
using System.Numerics;
using Raylib_cs;
using static Constants;
public class LevelManager
{

    private static int[,] levelOne;
    public int currLevel;
    private List<Enemy> enemies;
    private Vector2 spawnPoint, completeFlag;
    public LevelManager()
    {
        Level level = new Level(CURRENT_LEVEL);
        levelOne = level.getLevelScema();
        enemies = new List<Enemy>();

        for (int i = 0; i < LEVEL_HEIGHT; i++)
        {
            for (int j = 0; j < LEVEL_WIDTH; j++)
            {
                int xCord = j * TILE_SIZE;
                int yCord = i * TILE_SIZE;
                int tileType = levelOne[i, j];

                switch (tileType)
                {
                    case 5:
                        enemies.Add(new Enemy(xCord, yCord, TILE_SIZE-2, TILE_SIZE-2));
                        break;
                    case 6:
                        spawnPoint = new Vector2(xCord, yCord);
                        break;
                    case 7:
                        completeFlag = new Vector2(xCord, yCord);
                        break;
                }
            }
        }
    }


    public void Draw(int TILE_SIZE, int xlvlOffset)
    {
        for (int i = 0; i < LEVEL_HEIGHT; i++)
        {
            for (int j = 0; j < LEVEL_WIDTH; j++)
            {


                int xCord = j * TILE_SIZE;
                int yCord = i * TILE_SIZE;
                int tileType = levelOne[i, j];
                if (tileType == 0) continue;
                if (tileType == 5 || tileType == 6)
                {
                    continue;
                }
                if (IntToColorMap.ContainsKey(tileType))
                {
                    Raylib.DrawRectangle(xCord - xlvlOffset, yCord, TILE_SIZE, TILE_SIZE, IntToColorMap[tileType]);
                }

            }
        }
    }

    public void Update()
    {

    }

    public static int getLevel(int x, int y)
    {
        return levelOne[y, x];
    }

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }

    public Vector2 getSpawnPoint()
    {
        return spawnPoint;
    }

    public Vector2 getCompletelvlFlag()
    {
        return completeFlag;
    }
}