using Raylib_cs;
using static Constants;
public class GameScreen
{
    public GameWorld world;
    private GameRenderer render;
    private MenuScreen menuScreen;
    private PausedScreenOverlay pausedScreen;
    private GameOverOverlay gameOverScreen;
    private LevelCompleted levelCompleted;
    private const float targetUPS = 120f;
    private const float timePerUpdate = 1.0f / targetUPS;
    public static bool ShouldQuit = false;

    int updates = 0;
    float deltaU = 0.0f;
    double lastLogTime = Raylib.GetTime();
    public GameScreen()
    {
        Raylib.InitWindow(GAME_WIDTH, GAME_HEIGHT, "Platformer");
        Raylib.SetExitKey(KeyboardKey.Null);
        Raylib.SetTargetFPS(120);

        world = new GameWorld();
        render = new GameRenderer(world);

        menuScreen = new MenuScreen();
        pausedScreen = new PausedScreenOverlay();
        gameOverScreen = new GameOverOverlay();
        levelCompleted = new LevelCompleted();
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
                case GameState.Reset:
                    applyReset(GameState.Playing);
                    break;
                case GameState.ResetToMenu:
                    applyReset(GameState.Menu);
                    break;
                case GameState.GameOver:
                    applyGameOver();
                    break;
                case GameState.LevelCompleted:
                    applyLevelCompleted();
                    break;
                case GameState.NextLevel:
                    CURRENT_LEVEL++ ;
                    if(CURRENT_LEVEL>TOTAL_LEVELS) CURRENT_LEVEL = 1;
                    applyReset(GameState.Playing);
                    levelCompleted = new LevelCompleted();
                    break;
            }
        }

        // 4. Close the Window
        Raylib.CloseWindow();
    }

    private void applyLevelCompleted()
    {
        //update level completed screen
        levelCompleted.update();
        //draw it
        Raylib.BeginDrawing();
        levelCompleted.draw();
        Raylib.EndDrawing();
    }

    private void applyGameOver()
    {
        gameOverScreen.update();
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.RayWhite);

        render.Draw();
        gameOverScreen.draw();

        Raylib.EndDrawing();
    }

    private void applyReset(GameState newState)
    {
        //when textures are added manually unload them as well here
        world = new GameWorld();
        render = new GameRenderer(world);
        deltaU = 0.0f;

        GameStates.setGameState(newState);
    }

    private void paused()
    {
        pausedScreen.update();
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.RayWhite);

        render.Draw();
        pausedScreen.draw();

        Raylib.EndDrawing();
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