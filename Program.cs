using System;

class Program
{
    static void Main()
    {
        using (var game = new Monogame2.Game1())
        {
            game.Run();
        }
    }
}

