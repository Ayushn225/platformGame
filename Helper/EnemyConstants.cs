public static class EnemyConstants
{
    public enum State {
        IDLE,
        RUNNING,
        ATTACKING,
        KNOCKBACK,
    };


    public static int SIGHT_DISTANCE = 10*Constants.TILE_SIZE;
    public static int ATTACKING_DISTANCE = 1*Constants.TILE_SIZE;

    public static int AttackPower = 10;

}