namespace Quoridor.Core.Moves
{
    public readonly struct MoveValidationResult
    {
        public bool IsValid { get; }
        
        public string Error { get; }

        public MoveValidationResult(bool isValid) : this()
        {
            IsValid = isValid;
        }

        public MoveValidationResult(string error) : this()
        {
            IsValid = false;
            Error = error;
        }

        public static MoveValidationResult Valid => new(true);
        
        public static MoveValidationResult Invalid(string error) => new(error);
    }
}