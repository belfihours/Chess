namespace Chess.Models;
internal class ChessField
{
    public int Turn = 0;
    private readonly int _width = 8;
    private readonly int _height = 8;

    public List<FieldPos> Table { get; private set; }

    public ChessField()
    {
        Table = [];
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var backColor = (i + j) % 2 == 0 ? ConsoleColor.Yellow : ConsoleColor.DarkYellow;
                Table.Add(new(new(i, j), backColor));
            }
        }
    }

    public IEnumerable<Coordinate> GetPossibleMoves(Coordinate pos)
    {
        var targetField = Table.FirstOrDefault(p =>
        {
            var piece = p.GetPiece();
            if (piece is not null
                && piece.Position.Equals(pos))
            {
                return true;
            }
            return false;
        }) ?? throw new ArgumentException("No piece found at the given position.");

        var possibleMoves = targetField.GetPiece()!.GetPossibleMoves();
        return possibleMoves
            .Where(IsIn());
    }

    public int GetPiecesCount(Color color)
    {
        return Table.Count(f => f.GetPiece() is not null && f.GetPiece()!.Color == color);
    }

    public Color GetTurn()
    {
        return Turn % 2 == 0 ? Color.White : Color.Black;
    }

    private Func<Coordinate, bool> IsIn()
    {
        return move => move.X >= 0 && move.X < _width && move.Y >= 0 && move.Y < _height;
    }
}
