namespace Chess.Models;

internal class FieldPos
{
    private bool _empty = true;
    private ChessPiece? _piece;
    public ConsoleColor BackColor { get; }
    public FieldPos() { }
    public FieldPos(ConsoleColor backColor)
    {
        BackColor = backColor;
    }
    public string GetLetter() => _piece?.Letter.ToString() ?? "\u00A0";
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
