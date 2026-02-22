using Raylib_cs;
using static Constants;
public class GameScreen
{
    public GameWorld world;
    public GameRenderer render;
    private const float targetUPS = 60f;
    private const float timePerUpdate = 1.0f / targetUPS;

    public GameScreen()
    {
        Raylib.InitWindow(GAME_WIDTH, GAME_HEIGHT, "Platformer");
        LevelManager levelManager = new LevelManager(TILE_ROW, TILE_COL);
        world = new GameWorld();
        render = new GameRenderer(world, levelManager);
    }

    public void Run()
    {

        Raylib.SetTargetFPS(60);

        int updates = 0;
        float deltaU = 0.0f;
        double lastLogTime = Raylib.GetTime();

        // 3. Main Game Loop
        while (!Raylib.WindowShouldClose())
        {
            deltaU += Raylib.GetFrameTime() / timePerUpdate;

            if (deltaU >= 1)
            {
                world.update(timePerUpdate);
                updates++;
                deltaU--;
            }



            Raylib.BeginDrawing();
            render.Draw(TILE_SIZE);
            Raylib.EndDrawing();
            if (Raylib.GetTime() - lastLogTime >= 1.0)
            {
                Console.WriteLine($"FPS: {Raylib.GetFPS()} | UPS: {updates}");
                updates = 0;
                lastLogTime = Raylib.GetTime();
            }
        }

        // 4. Close the Window
        Raylib.CloseWindow();
    }

}