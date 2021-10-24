namespace Quoridor.Core.Game
{
    using Field;
    using Moves;

    public class QuoridorGame
    {
        public IQuoridorField Field { get; }

        public Color ActiveColor { get; private set; } = Color.White;

        public QuoridorGame(IQuoridorField field)
        {
            Field = field;
        }

        public bool IsOver => GetWinnerColor().HasValue;

        public void ExecuteMove(Move move)
        {
            move.Execute(Field);
            SwitchActiveColor();
        }

        private void SwitchActiveColor()
        {
            ActiveColor = ActiveColor == Color.Black ? Color.White : Color.Black;
        }

        public Color? GetWinnerColor()
        {
            if (Field.GetCellWithPawn(Color.White).Position.Row == 0)
            {
                return Color.White;
            }
            
            if (Field.GetCellWithPawn(Color.Black).Position.Row == 8)
            {
                return Color.Black;
            }

            return null;
        }
    }
}