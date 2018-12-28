using System;

namespace BlightController
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string username = "claypooljake%40gmail.com"; // todo: urlEncode the username;
            string password = "pizza";
            Blight blight = new Blight(username, password);
            string game = "6495019052564480";
            var gameState = blight.togglePause(game);
            
//            Console.WriteLine(gameState);

        }
    }
}
