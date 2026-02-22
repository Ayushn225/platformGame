using Raylib_cs;
using static Constants;
public class LevelManager
{

    private static int[,] levelOne;
    public LevelManager(int row, int col)
    {
        Level level = new Level(1);
        levelOne = level.getLevelScema();
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

                if (IntToColorMap.ContainsKey(tileType))
                {
                    Raylib.DrawRectangle(xCord- xlvlOffset, yCord, TILE_SIZE, TILE_SIZE, IntToColorMap[tileType]);
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
}