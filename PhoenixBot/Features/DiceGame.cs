using System;

namespace PhoenixBot.Features
{
    public class DiceGame
    {
        public static int Roll()
        {
            Random roll = new Random();
            int Roll = roll.Next(1, 6);
            return Roll;
        }
    }
}
