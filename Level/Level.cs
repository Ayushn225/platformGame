using Raylib_cs;

public class Level
{
    private int[,] levelData;
    public Level(int level)
    {
        Image levelImage = AssetLoader.loadMapImage("level" + level.ToString());

        int height = levelImage.Height;
        int width = levelImage.Width;
        if(height == 0 && width == 0)
        {
            Console.WriteLine("Failed to load image");
            levelData = new int[14, 26];
        }
        else
        {
            levelData = new int[height, width];
            Constants.LEVEL_WIDTH = width;
            Constants.LEVEL_HEIGHT = height;

            for(int i = 0; i<height; i++)
            {
                for(int j = 0; j<width; j++)
                {
                    Color pixelColor = Raylib.GetImageColor(levelImage, j, i);
                    if(Constants.ColorToIntMap.ContainsKey(pixelColor))
                    {
                        levelData[i, j] = Constants.ColorToIntMap[pixelColor];
                    }
                    else
                    {
                        levelData[i, j] = 0;
                    }
                }
            }
        }

        AssetLoader.UnloadMapImage(levelImage);
    }

    public int[,] getLevelScema()
    {
        return levelData;
    }

}