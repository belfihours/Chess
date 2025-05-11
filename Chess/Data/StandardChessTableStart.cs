using Chess.Models;
using Chess.Models.SpecificPieces;

namespace Chess.Data;

internal static class StandardChessTableStart
{
    public static ChessField Get() => BuildStartField();

    private static ChessField BuildStartField()
    {
        ChessField chessField = new();
        var pieces = GetWhites().Concat(GetBlacks());
        foreach (var piece in pieces)
        {
            chessField.Table[piece.Position].PlacePiece(piece);
        }
        return chessField;
    }

    private static List<ChessPiece> GetWhites()
    {
        List<ChessPiece> result = [];
        for (int i = 0; i < 8; i++)
        {
            result.Add(new Pawn(Color.White, new(i, 1)));
        }
        result.AddRange(
                [
                    new Tower(Color.White, new(0, 0)),
                    new Horse(Color.White, new(1, 0)),
                    new Bishop(Color.White, new(2, 0)),
                    new King(Color.White, new(3, 0)),
                    new Queen(Color.White, new(4, 0)),
                    new Bishop(Color.White, new(5, 0)),
                    new Horse(Color.White, new(6, 0)),
                    new Tower(Color.White, new(7, 0)),
                ]
            );
        return result;
    }

    private static List<ChessPiece> GetBlacks()
    {
        List<ChessPiece> result = [];
        for (int i = 0; i < 8; i++)
        {
            result.Add(new Pawn(Color.Black, new(i, 6)));
        }
        result.AddRange(
                [
                    new Tower(Color.Black, new(0, 7)),
                    new Horse(Color.Black, new(1, 7)),
                    new Bishop(Color.Black, new(2, 7)),
                    new King(Color.Black, new(3, 7)),
                    new Queen(Color.Black, new(4, 7)),
                    new Bishop(Color.Black, new(5, 7)),
                    new Horse(Color.Black, new(6, 7)),
                    new Tower(Color.Black, new(7, 7)),
                ]
            );
        return result;
    }
}
