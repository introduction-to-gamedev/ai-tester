namespace Quoridor.Core.Game
{
    using Field;

    public class QuoridorGame
    {
        private IQuoridorField field;
        

        public QuoridorGame(IQuoridorField field)
        {
            this.field = field;
        }
    }
}