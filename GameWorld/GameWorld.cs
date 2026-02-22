using Raylib_cs;
using static Constants;
public class GameWorld
{
    private Rect player;
    private int xlvlOffset = 0;
    private int leftBorder = (int)(0.2 * GAME_WIDTH);
    private int rightBorder = (int)(0.8*GAME_WIDTH);
    private int maxTileOffset = Math.Max(LEVEL_WIDTH - TILE_COL, 0);
    private int maxLevelOffset = 0;
    public GameWorld()
    {
        player = new Rect(Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2);
        maxLevelOffset = (int)(maxTileOffset*TILE_SIZE);
    }

    public void update(float deltaTime)
    {
        player.update(deltaTime);
        checkCloseToBorder();
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            GameStates.setGameState(GameState.Paused);
        }
        
    }

    private void checkCloseToBorder()
    {
        int playerX = (int)player.getX();
        int diff = playerX - xlvlOffset;

        if(diff > rightBorder)
        {
            xlvlOffset += diff - rightBorder;
        }
        else if(diff < leftBorder)
        {
            xlvlOffset += diff - leftBorder;
        }

        if(xlvlOffset > maxLevelOffset) xlvlOffset = maxLevelOffset;
        else if(xlvlOffset < 0) xlvlOffset = 0;
    }

    public Rect GetRect()
    {
        return player;
    }

    public int getXlvlOffset()
    {
        return xlvlOffset;
    }
}