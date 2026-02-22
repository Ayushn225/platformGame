using Raylib_cs;

public static class AssetLoader
{
    private static string basePath = "/Users/ayushnegi/Desktop/Gaming/PlatformerGame/assets/";

    public static Image loadMapImage(string path)
    {
        string fullname = basePath + path + ".png";
        if (!System.IO.File.Exists(fullname))
        {
            Console.WriteLine("Error! no image found in path: " + fullname);
            return new Image();
        }
        return Raylib.LoadImage(fullname);
    }

    public static void UnloadMapImage(Image image) { Raylib.UnloadImage(image); }
}