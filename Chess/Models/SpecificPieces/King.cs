namespace Chess.Models.SpecificPieces;

internal class King(Color color, Coordinate position) : ChessPiece(color, position)
{
    public override IEnumerable<Coordinate> GetPossibleMoves()
    {
        for (int i = -1; i < 1; i++)
        {
            for (int j = -1; j < 1; j++)
            {
                Coordinate possiblePosition = new(Position.X + i, Position.Y + j);
                if (possiblePosition != Position)
                {
                    yield return possiblePosition;
                }
            }
        }
    }
}
