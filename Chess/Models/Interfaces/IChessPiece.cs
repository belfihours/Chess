namespace Chess.Models.Interfaces;
internal interface IChessPiece
{
    Color Color { get; }
    Coordinate Position { get; }
    IEnumerable<Coordinate> GetPossibleMoves();
}
