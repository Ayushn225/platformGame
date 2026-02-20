using Raylib_cs;

public class GameScreen{
    public GameWorld world;
    public GameRenderer render;
    public GameScreen(){
        world = new GameWorld();
        render = new GameRenderer(world);
    }

    public void Run(){
        // 1. Initialize the Window (Width, Height, Title)
        Raylib.InitWindow(800, 800, "Raylib C# Practice");

        // 2. Set the frame rate (60 FPS is standard)
        Raylib.SetTargetFPS(60);

        // 3. Main Game Loop
        while (!Raylib.WindowShouldClose())
        {
            world.update();

            Raylib.BeginDrawing();
            render.Draw();
            Raylib.EndDrawing();

        }

        // 4. Close the Window
        Raylib.CloseWindow();
    }

}