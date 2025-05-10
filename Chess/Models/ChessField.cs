namespace Chess.Models;
internal class ChessField
{
    private readonly int _width = 8;
    private readonly int _height = 8;

    public FieldPos[,] Table { get; private set; }

    FieldPos FieldPos;

    public ChessField()
    {
        Table = new FieldPos[8, 8];
    }

}
