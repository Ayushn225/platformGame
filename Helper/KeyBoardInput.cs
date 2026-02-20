using System.Security.Cryptography;
using Raylib_cs;

public class KeyBoardInput
{

    public KeyBoardInput()
    {
        
    }

    public char GetKey()
    {
        if (Raylib.IsKeyDown(KeyboardKey.Right))
        {
            return 'd';
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Left))
        {
            return 'a';
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Up))
        {
            return 'w';
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Down))
        {
            return 's';
        }
        else{
            return ' ';
        }
    }
}