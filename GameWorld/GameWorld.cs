using Raylib_cs;
public class GameWorld
{
    private Rect player;
    public GameWorld()
    {
        player = new Rect(Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2);
    }

    public void update(float deltaTime)
    {
        if(Raylib.IsKeyDown(KeyboardKey.Left))  player.moveLeft(deltaTime);
        if(Raylib.IsKeyDown(KeyboardKey.Right)) player.moveRight(deltaTime);
        if(Raylib.IsKeyDown(KeyboardKey.Up)) player.moveUp(deltaTime);
        if(Raylib.IsKeyDown(KeyboardKey.Down)) player.moveDown(deltaTime);
    }

    public Rect GetRect()
    {
        return player;
    }
}