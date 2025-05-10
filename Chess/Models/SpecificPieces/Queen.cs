using Chess.Models.Interfaces;

namespace Chess.Models.SpecificPieces;

internal class Queen : IChessPiece
{
    public string Name { get; private set; }
    public string Color { get; private set; }
    public Coordinate Position { get; set; }

    public Queen(string name, string color, Coordinate position)
    {
        Name = name;
        Color = color;
        Position = position;
    }

    public IEnumerable<Coordinate> GetPossibleMoves()
    {
        var possibleMoves = Tower.GetPossibleMoves(Position).Concat(Bishop.GetPossibleMoves(Position));
        return possibleMoves;
    }

    public void Show()
    {
        throw new NotImplementedException();
    }
}
