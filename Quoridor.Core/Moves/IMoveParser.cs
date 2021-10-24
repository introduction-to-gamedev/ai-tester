namespace Quoridor.Core.Moves
{
    public interface IMoveParser
    {
        Move ParseMove(string command); 
    }
}