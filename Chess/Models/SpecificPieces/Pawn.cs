using Chess.Models.Interfaces;

namespace Chess.Models.SpecificPieces;

internal class Pawn : IChessPiece
{
    public string Name { get; private set; }
    public string Color { get; private set; }
    public Coordinate Position { get; set; }
    private readonly Coordinate _startPosition;

    public Pawn(string name, string color, Coordinate position)
    {
        Name = name;
        Color = color;
        Position = position;
        _startPosition = position;
    }

    public IEnumerable<Coordinate> GetPossibleMoves()
    {
        List<Coordinate> possibleMoves = [];
        for (int i = -1; i < 1; i++)
        {
            possibleMoves.Add(new(Position.X + i, Position.Y + 1));
        }
        if (Position == _startPosition)
        {
            possibleMoves.Add(new(Position.X, Position.Y + 2));
        }
        return possibleMoves;

    }

    public void Show()
    {
        throw new NotImplementedException();
    }
}
