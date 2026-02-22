using Raylib_cs;
using static Constants;
public class PausedScreenOverlay
{
    Color darkOverlay;
    private Rectangle border;
    private float spacingY = 0.1f, spacingX = 0.3f;
    private float buttonSize = 200;
    Button playButton, retryButton, homeButton, soundButton;
    List<Button> buttons;

    Slider slideBar;

    public PausedScreenOverlay()
    {
        darkOverlay = new Color(0, 0, 0, 128);
        border = new Rectangle((int)(GAME_WIDTH * spacingX), (int)(GAME_HEIGHT * spacingY), (int)(GAME_WIDTH * (1 - 2 * spacingX)), (int)(GAME_HEIGHT * (1 - 2 * spacingY)));

        float marginX = (border.Width - 2 * buttonSize) / 3;
        float marginY = (border.Height - 3 * buttonSize) / 4;
        float offset = 100;

        playButton = new Button((int)(border.X + marginX), (int)(border.Y + offset + marginY), (int)buttonSize, (int)buttonSize, "Play", Color.DarkGreen, () =>
        {
            GameStates.setGameState(GameState.Playing);
        });

        retryButton = new Button((int)(playButton.getBackPos() + marginX), (int)(border.Y + offset + marginY), (int)buttonSize, (int)buttonSize, "Retry", Color.Orange, () =>
        {
            GameStates.setGameState(GameState.Reset);
        });

        homeButton = new Button((int)(border.X + marginX), (int)(playButton.getBottomPos() + marginY), (int)buttonSize, (int)buttonSize, "Home", Color.Red, () =>
        {
            GameStates.setGameState(GameState.Menu);
        });

        soundButton = new Button((int)(homeButton.getBackPos() + marginX), (int)(playButton.getBottomPos() + marginY), (int)buttonSize, (int)buttonSize, "Sound", Color.Brown, () =>
        {
            GameStates.setGameState(GameState.Menu); //just as a placeholder will work on it later
        });

        buttons = new List<Button>() { playButton, retryButton, homeButton, soundButton };

        slideBar = new Slider(
            border.X + marginX, 
            soundButton.getBottomPos() + marginY + 20, 
            buttonSize*2 + marginX, 
            buttonSize/4);

    }
    public void update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            GameStates.setGameState(GameState.Playing);
        }

        foreach (Button b in buttons)
        {
            b.update();
        }

        slideBar.update();
    }

    public void draw()
    {
        Raylib.DrawRectangle(0, 0, GAME_WIDTH, GAME_HEIGHT, darkOverlay);
        Raylib.DrawRectangle((int)border.X, (int)border.Y, (int)border.Width, (int)border.Height, Color.White);
        Raylib.DrawRectangleLinesEx(border, 4, Color.Orange);
        drawTitle();
        foreach (Button b in buttons)
        {
            b.draw();
        }

        slideBar.draw();
    }

    private void drawTitle()
    {
        string title = "Paused Screen";
        Raylib.DrawText(title, GAME_WIDTH / 2 - title.Length * 18, (int)(20 + border.Y), 64, Color.Black);
    }
}