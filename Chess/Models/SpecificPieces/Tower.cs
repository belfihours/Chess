namespace Chess.Models.SpecificPieces;

internal class Tower(Color color, Coordinate position) : ChessPiece(color, position)
{
    public override IEnumerable<Coordinate> GetPossibleMoves()
    {
        return GetPossibleMoves(Position);
    }

    public static IEnumerable<Coordinate> GetPossibleMoves(Coordinate position)
    {
        List<Coordinate> possibleMoves = [];
        for (int i = -7; i < 7; i++)
        {
            possibleMoves.Add(new(position.X + i, position.Y));
            possibleMoves.Add(new(position.X, position.Y + i));
        }
        possibleMoves.Remove(position);
        return possibleMoves;
    }
}
