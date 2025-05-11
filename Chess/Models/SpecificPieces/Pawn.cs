namespace Chess.Models.SpecificPieces;

internal class Pawn(Color color, Coordinate position) : ChessPiece(color, position)
{
    private readonly Coordinate _startPosition = position;

    public override IEnumerable<Coordinate> GetPossibleMoves()
    {
        List<Coordinate> possibleMoves = [];
        for (int i = -1; i < 1; i++)
        {
            possibleMoves.Add(new(Position.X + i, GetYBasedOnColor(1)));
        }
        if (Position == _startPosition)
        {
            possibleMoves.Add(new(Position.X, GetYBasedOnColor(2)));
        }
        return possibleMoves;
    }

    private int GetYBasedOnColor(int n)
    {
        return Color == Color.White
            ? Position.Y + n
            : Position.Y - n;
    }
}
