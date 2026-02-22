using Microsoft.VisualBasic;
using Raylib_cs;

public class Slider
{
    private Rectangle bar, blob;

    private float val;
    public Slider(float x, float y, float width, float height)
    {
        bar = new Rectangle(x, y, width, height);
        val = 0.5f;
        float blobSize = height*2;
        blob = new Rectangle(bar.X + bar.Width * val - blobSize/2, bar.Height/2 + bar.Y - blobSize/2 , blobSize/2, blobSize);
    }

    public void draw()
    {
        Raylib.DrawRectangle((int)bar.X, (int)bar.Y, (int)bar.Width, (int)bar.Height, Color.Orange);
        Raylib.DrawRectangleLinesEx(bar, 4, Color.Brown);
        Raylib.DrawRectangle((int)blob.X, (int)blob.Y, (int)blob.Width, (int)blob.Height, Color.Brown);
        Raylib.DrawRectangleLinesEx(blob, 4, Color.DarkBrown);
    }

    public void update()
    {
        //will work on the update later
    }
}