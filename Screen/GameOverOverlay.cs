using Raylib_cs;
using static Constants;
public class GameOverOverlay
{
    Color darkOverlay;
    private Rectangle border;

    private float buttonSize = 200;
    private float spacingY = 0.1f, spacingX = 0.3f;
    Button homeButton, resetButton;


    public GameOverOverlay()
    {
        darkOverlay = new Color(0, 0, 0, 128);

        border = new Rectangle((int)(GAME_WIDTH * spacingX), (int)(GAME_HEIGHT * spacingY), (int)(GAME_WIDTH * (1 - 2 * spacingX)), (int)(GAME_HEIGHT * (1 - 2 * spacingY)));

        float marginX = (border.Width - 2 * buttonSize) / 3;
        float marginY = (border.Height - 3 * buttonSize) / 4;
        float offset = 100;

        homeButton = new Button((int)(border.X + marginX), (int)((Raylib.GetScreenHeight() - buttonSize)/2), (int)buttonSize, (int)buttonSize, "Home", Color.Red, () =>
        {
            GameStates.setGameState(GameState.ResetToMenu);
        });


        resetButton = new Button((int)(homeButton.getBackPos() + marginX),  (int)((Raylib.GetScreenHeight() - buttonSize)/2), (int)buttonSize, (int)buttonSize, "Reset", Color.Orange, () =>
        {
            GameStates.setGameState(GameState.Reset);
        });

    }
    public void update()
    {
        homeButton.update();
        resetButton.update();
    }

    public void draw()
    {
        Raylib.DrawRectangle(0, 0, GAME_WIDTH, GAME_HEIGHT, darkOverlay);
        Raylib.DrawRectangle((int)border.X, (int)border.Y, (int)border.Width, (int)border.Height, Color.White);
        Raylib.DrawRectangleLinesEx(border, 4, Color.Brown);
        drawTitle();

        homeButton.draw();
        resetButton.draw();
    }

    private void drawTitle()
    {
        string title = "Game Over";
        Raylib.DrawText(title, GAME_WIDTH / 2 - title.Length * 18, (int)(20 + border.Y), 64, Color.Black);
    }
}