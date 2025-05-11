namespace Chess.Models.SpecificPieces;

internal class Queen(Color color, Coordinate position) : ChessPiece(color, position)
{
    public override IEnumerable<Coordinate> GetPossibleMoves()
    {
        var possibleMoves = Tower.GetPossibleMoves(Position).Concat(Bishop.GetPossibleMoves(Position));
        return possibleMoves;
    }
}
