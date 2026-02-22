using Raylib_cs;

public static class Constants
{
    public const int TILE_DEFAULT_SIZE = 32;
    public const float SCALE = 2.0f;
    public const int TILE_ROW = 14;
    public const int TILE_COL = 26;
    public const int TILE_SIZE = (int)(TILE_DEFAULT_SIZE * SCALE);
    public const int GAME_WIDTH = TILE_SIZE * TILE_COL;
    public const int GAME_HEIGHT = TILE_SIZE * TILE_ROW;
    public static int LEVEL_WIDTH = 0;
    public static int LEVEL_HEIGHT = 0;

    public static Dictionary<Color, int> ColorToIntMap = new Dictionary<Color, int>()
    {
        {new Color(0,   0,  0, 255),        1},//solid black
        {new Color(255, 255, 255, 255),     0}, //air white
        {new Color(20, 45, 20, 255),         2}, //grass
        {new Color(102, 104, 102, 255),     3}, //wall
        {new Color(50, 94, 50, 255),        4}, //wall and grass

    };

    public static Dictionary<int, Color> IntToColorMap = new Dictionary<int, Color>()
    {
        {1, Color.DarkGray    },//solid black
        {0, Color.White   }, //air white
        {2, Color.DarkGreen       }, //grass
        {3, new Color(102, 104, 102, 255)   }, //wall
        {4, new Color(50, 94, 50, 255)      }, //wall and grass
    };

}