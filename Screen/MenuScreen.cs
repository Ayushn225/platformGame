using System.Numerics;
using System;
using Raylib_cs;
using static Constants;

public class MenuScreen
{
    private Rectangle border;
    private Button playButton, quitButton;
    
    public MenuScreen()
    {
        border = new Rectangle((int)(GAME_WIDTH * 0.1), (int)(GAME_HEIGHT * 0.1), (int)(GAME_WIDTH * 0.8), (int)(GAME_HEIGHT * 0.8));

        playButton = new Button((int)(GAME_WIDTH * 0.3), (int)(border.Y + 100), (int)(GAME_WIDTH * 0.4), 200, "PLAY", Color.DarkGreen, () =>
        {
            GameStates.setGameState(GameState.Playing);
        });

        quitButton = new Button((int)(GAME_WIDTH * 0.3), (int)(playButton.getBottomPos() + 100), (int)(GAME_WIDTH * 0.4), 200, "QUIT", Color.Red, () =>
        {
            GameScreen.ShouldQuit = true;
        });
    }
    public void update()
    {
        playButton.update();
        quitButton.update();
    }
    public void draw()
    {
        Raylib.ClearBackground(Color.RayWhite);
        drawTitle();


        Raylib.DrawRectangleLinesEx(border, 4, Color.Blue);

        playButton.draw();
        quitButton.draw();
    }

    private void drawTitle()
    {
        string title = "Menu Screen";
        Raylib.DrawText(title, GAME_WIDTH / 2 - title.Length * 12, 10, 64, Color.Black);
    }

}