using Raylib_cs;
using static Constants;
public class GameScreen
{
    public GameWorld world;
    public GameRenderer render;
    public MenuScreen menuScreen;
    private const float targetUPS = 60f;
    private const float timePerUpdate = 1.0f / targetUPS;
    public static bool ShouldQuit = false;

    int updates = 0;
    float deltaU = 0.0f;
    double lastLogTime = Raylib.GetTime();
    public GameScreen()
    {
        Raylib.InitWindow(GAME_WIDTH, GAME_HEIGHT, "Platformer");
        Raylib.SetExitKey(KeyboardKey.Null);
        Raylib.SetTargetFPS(60);

        LevelManager levelManager = new LevelManager(TILE_ROW, TILE_COL);
        world = new GameWorld();
        render = new GameRenderer(world, levelManager);

        menuScreen = new MenuScreen();

    }

    public void Run()
    {

        // 3. Main Game Loop
        while (!Raylib.WindowShouldClose() && !ShouldQuit)
        {
            switch (GameStates.getGameState())
            {
                case GameState.Menu:
                    showMenu();
                    break;
                case GameState.Playing:
                    playing();
                    break;
                case GameState.Paused:
                    paused();
                    break;
            }
        }

        // 4. Close the Window
        Raylib.CloseWindow();
    }

    private void paused()
    {
        
    }

    private void showMenu()
    {
        menuScreen.update();
        Raylib.BeginDrawing();
        menuScreen.draw();
        Raylib.EndDrawing();
    }

    private void playing()
    {

        deltaU += Raylib.GetFrameTime() / timePerUpdate;

        if (deltaU >= 1)
        {
            world.update(timePerUpdate);
            updates++;
            deltaU--;
        }

        Raylib.BeginDrawing();
        render.Draw();
        Raylib.EndDrawing();
        if (Raylib.GetTime() - lastLogTime >= 1.0)
        {
            Console.WriteLine($"FPS: {Raylib.GetFPS()} | UPS: {updates}");
            updates = 0;
            lastLogTime = Raylib.GetTime();
        }
    }

}