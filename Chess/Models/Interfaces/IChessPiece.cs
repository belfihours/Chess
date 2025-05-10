namespace Chess.Models.Interfaces;
internal interface IChessPiece
{
    string Name { get; }
    string Color { get; }
    Coordinate Position { get; }

    void Show();
    IEnumerable<Coordinate> GetPossibleMoves();
}
