using Chess.Models.Interfaces;

namespace Chess.Models.SpecificPieces;

internal class Bishop : IChessPiece
{
    public string Name { get; private set; }
    public string Color { get; private set; }
    public Coordinate Position { get; set; }

    public Bishop(string name, string color, Coordinate position)
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
        int j = 7;
        for (int i = -7; i < 7; i++, j--)
        {
            possibleMoves.Add(new(position.X + i, position.Y + i));
            possibleMoves.Add(new(position.X + i, position.Y + j));
        }
        possibleMoves.Remove(position);
        return possibleMoves;
    }

    public void Show()
    {
        throw new NotImplementedException();
    }
}
