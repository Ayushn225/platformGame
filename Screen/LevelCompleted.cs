using System.Data;
using Raylib_cs;
using static Constants;

public class LevelCompleted
{
    private Rectangle border;
    private Button nextButton, quitButton;

    public LevelCompleted()
    {
        border = new Rectangle((int)(GAME_WIDTH * 0.1), (int)(GAME_HEIGHT * 0.1), (int)(GAME_WIDTH * 0.8), (int)(GAME_HEIGHT * 0.8));
        int nextLevel = CURRENT_LEVEL + 1;
        string nextText = "Next Level: " + nextLevel;
        nextButton = new Button((int)(GAME_WIDTH * 0.3), (int)(border.Y + 100), (int)(GAME_WIDTH * 0.4), 200, nextText, Color.DarkGreen, () =>
        {
            GameStates.setGameState(GameState.NextLevel);
        });

        quitButton = new Button((int)(GAME_WIDTH * 0.3), (int)(nextButton.getBottomPos() + 100), (int)(GAME_WIDTH * 0.4), 200, "QUIT", Color.Red, () =>
        {
            GameScreen.ShouldQuit = true;
        });
    }

    public void update()
    {
        nextButton.update();
        quitButton.update();
    }

    public void draw()
    {
        Raylib.ClearBackground(Color.White);
        drawTitle();

        Raylib.DrawRectangleLinesEx(border, 4, Color.Blue);

        nextButton.draw();
        quitButton.draw();
    }

    private void drawTitle()
    {
        string title = "Level Completed";
        Raylib.DrawText(title, GAME_WIDTH / 2 - title.Length * 12, 10, 64, Color.Black);
    }
}