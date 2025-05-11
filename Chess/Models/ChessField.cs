namespace Chess.Models;
internal class ChessField
{
    private readonly int _width = 8;
    private readonly int _height = 8;

    public Dictionary<Coordinate, FieldPos> Table { get; private set; }

    public ChessField()
    {
        Table = [];
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var backColor = (i + j) % 2 == 0 ? ConsoleColor.Yellow : ConsoleColor.DarkYellow;
                Table.Add(new(i, j), new(backColor));
            }
        }
    }

}
