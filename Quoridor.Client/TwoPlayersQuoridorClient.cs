namespace Quoridor.Client
{
    using System;
    using Core.Field;

    class TwoPlayersQuoridorClient
    {
        static void Main(string[] args)
        {
            new OnePlayerQuoridorCommandLineRunner().RunSingleGame();
        }

    
    }
}