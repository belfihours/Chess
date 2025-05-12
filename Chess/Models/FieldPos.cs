namespace Chess.Models;

internal class FieldPos
{
    public Coordinate Position { get; }
    private bool _empty = true;
    private ChessPiece? _piece;
    public ConsoleColor BackColor { get; }
    public FieldPos(Coordinate coordinate)
    {
        Position = coordinate;
    }
    public FieldPos(Coordinate coordinate, ConsoleColor backColor)
    {
        Position = coordinate;
        BackColor = backColor;
    }
    public char GetLetter() => _piece?.Letter ?? ' ';
    public bool IsEmpty() => _empty;
    public ChessPiece? GetPiece() => _piece;
    public ChessPiece? PlacePiece(ChessPiece piece)
    {
        if (_empty)
        {
            _piece = piece;
            _empty = false;
            return null;
        }
        var killedPiece = _piece;
        _piece = piece;
        return killedPiece;
    }
}
