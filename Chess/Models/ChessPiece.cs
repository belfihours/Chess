using Chess.Models.Interfaces;

namespace Chess.Models;

internal abstract class ChessPiece : IChessPiece
{
    public string Name { get; set; }
    public string Color { get; set; }
    public Coordinate Position { get; set; }
    public ChessPiece(string name, string color, Coordinate position)
    {
        Name = name;
        Color = color;
        Position = position;
    }

    public virtual void Show()
    {
        throw new NotImplementedException();
    }

    public virtual void Eat()
    {
        throw new NotImplementedException();
    }

    public virtual IEnumerable<Coordinate> GetPossibleMoves()
    {
        throw new NotImplementedException();
    }
}

