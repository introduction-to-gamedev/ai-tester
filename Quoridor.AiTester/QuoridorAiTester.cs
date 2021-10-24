namespace Quoridor.AiTester
{
    using System;
    using IntroToGameDev.AiTester;

    public class QuoridorAiTester : AiTester<QuoridorGameRunner>
    {
        static void Main(string[] args)
        {
            ParseArgsAndStart(args);
        }
    }
}