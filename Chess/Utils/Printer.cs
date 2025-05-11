using Chess.Models;

namespace Chess.Utils;

internal class Printer
{
    private readonly IEnumerable<char> _letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];
    public static readonly Printer Instance = new();


    public void PrintChessField(ChessField field)
    {
        Console.Clear();
        foreach (var fieldPos in field.Table)
        {
            var key = fieldPos.Key;
            Console.SetCursorPosition(key.X * 3, key.Y);
            Console.BackgroundColor = fieldPos.Value.BackColor;
            if (!fieldPos.Value.IsEmpty())
            {
                Console.ForegroundColor = fieldPos.Value.GetPiece()!.Color.Equals(Color.White)
                    ? ConsoleColor.Gray
                    : ConsoleColor.Black;
            }
            Console.Write($" {fieldPos.Value.GetLetter()} ");
        }

    }

}
