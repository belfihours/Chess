using Chess.Models.Interfaces;

namespace Chess.Models.SpecificPieces;

internal class King : IChessPiece
{
    public string Name { get; private set; }
    public string Color { get; private set; }
    public Coordinate Position { get; set; }

    public King(string name, string color, Coordinate position)
    {
        Name = name;
        Color = color;
        Position = position;
    }

    public IEnumerable<Coordinate> GetPossibleMoves()
    {
        for (int i = -1; i < 1; i++)
        {
            for (int j = -1; j < 1; j++)
            {
                Coordinate possiblePosition = new(Position.X + i, Position.Y + j);
                if (possiblePosition != Position)
                    yield return possiblePosition;
            }
        }
    }

    public void Show()
    {
        throw new NotImplementedException();
    }
}
