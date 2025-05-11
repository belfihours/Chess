namespace Chess.Models.SpecificPieces;

internal class Horse(Color color, Coordinate position) : ChessPiece(color, position)
{
    private readonly IEnumerable<Coordinate> _possibleOffsets =
        [new(-1, -2), new(-2, -1), new(-1, 2), new(-2, 1), new(1, -2), new(2, -1), new(2, 1), new(1, 2)];

    public override IEnumerable<Coordinate> GetPossibleMoves()
    {
        List<Coordinate> possibleMoves = [];
        foreach (var offset in _possibleOffsets)
        {
            possibleMoves.Add(new(Position.X + offset.X, Position.Y + offset.Y));
        }
        return possibleMoves;
    }
}
