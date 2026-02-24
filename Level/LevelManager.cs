using Raylib_cs;
using static Constants;
public class LevelManager
{

    private static int[,] levelOne;
    List<Enemy> enemies;
    public LevelManager()
    {
        Level level = new Level(1);
        levelOne = level.getLevelScema();
        enemies = level.GetEnemies();
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
                if (tileType == 5)
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

    public static int getLevel(int levelNo, int x, int y)
    {
        return levelOne[y, x];
    }

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }
}