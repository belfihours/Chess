using Chess.Models.Interfaces;

namespace Chess.Models;

internal abstract class ChessPiece(Color color, Coordinate position) : IChessPiece
{
    public Color Color { get; private set; } = color;
    public Coordinate Position { get; set; } = position;
    public char Letter => this.GetType().Name[0];

    public abstract IEnumerable<Coordinate> GetPossibleMoves();
}

