using Raylib_cs;

public class LevelManager
{

    private int[,] levelOne;
    private int row, col;
    public LevelManager(int row, int col)
    {
        this.row = row;
        this.col = col;
        Level level = new Level(row, col);
        levelOne = level.getLevelScema(1);
    }

    public void Draw(int TILE_SIZE)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {

                if (levelOne[i, j]==1)
                {
                    int xCord = j * TILE_SIZE;
                    int yCord = i * TILE_SIZE;
                    Raylib.DrawRectangle(xCord, yCord, TILE_SIZE, TILE_SIZE, Color.DarkGray);
                }

            }
        }
    }

    public void Update()
    {

    }
}