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

    public int GetPiecesCount(Color color)
    {
        return Table.Count(f => f.GetPiece() is not null && f.GetPiece()!.Color == color);
    }

    public Color GetTurn()
    {
        return Turn % 2 == 0 ? Color.White : Color.Black;
    }
}
