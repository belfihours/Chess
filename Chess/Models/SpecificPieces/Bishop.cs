namespace Chess.Models.SpecificPieces;

internal class Bishop(Color color, Coordinate position) : ChessPiece(color, position)
{
    public override IEnumerable<Coordinate> GetPossibleMoves()
    {
        return GetPossibleMoves(Position);
    }

    public static IEnumerable<Coordinate> GetPossibleMoves(Coordinate position)
    {
        List<Coordinate> possibleMoves = [];
        int j = 7;
        for (int i = -7; i < 7; i++, j--)
        {
            possibleMoves.Add(new(position.X + i, position.Y + i));
            possibleMoves.Add(new(position.X + i, position.Y + j));
        }
        possibleMoves.Remove(position);
        return possibleMoves;
    }
}
