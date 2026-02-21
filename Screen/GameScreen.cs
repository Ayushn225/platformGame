using Raylib_cs;

public class GameScreen
{
    public GameWorld world;
    public GameRenderer render;
    private const float targetUPS = 60f;
    private const float timePerUpdate = 1.0f / targetUPS;

    private const int TILE_DEFAULT_SIZE = 32;
    private const float SCALE = 1.5f;
    private const int TILE_ROW = 14;
    private const int TILE_COL = 26;
    private const int TILE_SIZE = (int)(TILE_DEFAULT_SIZE * SCALE);
    private const int GAME_WIDTH = TILE_SIZE * TILE_COL;
    private const int GAME_HEIGHT = TILE_SIZE*TILE_ROW;

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