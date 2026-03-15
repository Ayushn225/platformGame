using static Constants;
public class CollisionDetection
{

    public static bool detectCollWithLevel(Rect player, float x, float y)
    {
        float left = player.getX() + x;
        float right = player.getX() + x + player.getWidth();
        float top = player.getY() + y;
        float bottom = player.getY() + y + player.getHeight();
        if (!isSolid(left, top))
            if (!isSolid(right-1, top))
                if (!isSolid(left, bottom-1))
                    if (!isSolid(right-1, bottom-1))
                        return false;
        return true;
    }

    private static bool isSolid(float x, float y)
    {
        int xIndex = (int)(x / TILE_SIZE);
        int yIndex = (int)(y / TILE_SIZE);
        if (xIndex < 0 || xIndex >= LEVEL_WIDTH || yIndex < 0 || yIndex >= LEVEL_HEIGHT)
        {
            return true;
        }
        int c = LevelManager.getLevel(1, xIndex, yIndex);
        if (c == 0) return false;
        return true;
    }

    private static bool isEmpty(float x, float y)
    {
        int xIndex = (int)(x/TILE_SIZE);
        int yIndex = (int)(y/TILE_SIZE) + 1;
        if (xIndex < 0 || xIndex >= LEVEL_WIDTH || yIndex < 0 || yIndex >= LEVEL_HEIGHT)
        {
            return true;
        }
        int c = LevelManager.getLevel(1, xIndex, yIndex);
        if(c==0) return true;
        return false;
    }

    public static bool canWalk(float x, float y)
    {
        //check front if solid
        if(isSolid(x, y)) return false;
        //check down if empty
        if(isEmpty(x, y )) return false;

        return true;
    }
}