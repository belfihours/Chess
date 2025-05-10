using Chess.Models.Interfaces;

namespace Chess.Models.SpecificPieces;

internal class Horse : IChessPiece
{
    public string Name { get; private set; }
    public string Color { get; private set; }
    public Coordinate Position { get; set; }
    private readonly IEnumerable<Coordinate> _possibleOffsets =
        [new(-1, -2), new(-2, -1), new(-1, 2), new(-2, 1), new(1, -2), new(2, -1), new(2, 1), new(1, 2)];

    public Horse(string name, string color, Coordinate position)
    {
        Name = name;
        Color = color;
        Position = position;
    }

    public IEnumerable<Coordinate> GetPossibleMoves()
    {
        List<Coordinate> possibleMoves = [];
        foreach (var offset in _possibleOffsets)
        {
            possibleMoves.Add(new(Position.X + offset.X, Position.Y + offset.Y));
        }
        return possibleMoves;
    }

    public void Show()
    {
        throw new NotImplementedException();
    }
}
