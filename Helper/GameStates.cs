public enum GameState
{
    Menu,
    Playing,
    Paused,
    Reset,
}

public static class GameStates
{
    private static GameState currentState = GameState.Menu;

    public static void setGameState(GameState newState)
    {
        currentState = newState;
    }

    public static GameState getGameState()
    {
        return currentState;
    }
}