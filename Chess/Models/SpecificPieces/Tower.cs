using Chess.Models.Interfaces;

namespace Chess.Models.SpecificPieces;

internal class Tower : IChessPiece
{
    public string Name { get; private set; }
    public string Color { get; private set; }
    public Coordinate Position { get; set; }

    public Tower(string name, string color, Coordinate position)
    {
        Name = name;
        Color = color;
        Position = position;
    }

    public IEnumerable<Coordinate> GetPossibleMoves()
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

    public void Show()
    {
        throw new NotImplementedException();
    }
}
