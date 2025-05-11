using Chess.Models;

namespace Chess.Utils;

internal class Printer
{
    private const int _offsetX = 20;
    private const int _offsetY = 5;
    private const int _multiplierX = 3;
    private const int _multiplierY = 1;
    private readonly IEnumerable<char> _letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];

    public void PrintChessField(ChessField field)
    {
        Console.Clear();
        PrintBorders();
        foreach (var fieldPos in field.Table)
        {
            SetPositionByCoordinate(fieldPos.Key);
            SetColor(fieldPos);
            PrintChar(fieldPos.Value.GetLetter());
        }
    }

    private static void PrintString(string word)
    {
        Console.Write($" {word} ");
    }

    private static void PrintChar(char letter)
    {
        Console.Write($" {letter} ");
    }

    private void PrintBorders()
    {
        Console.ForegroundColor = ConsoleColor.White;
        SetPositionByCoordinate(new(-2, -2));
        Console.WriteLine("\t________________________________");

        SetPositionByCoordinate(new(0, -1));
        PrintLetters();

        SetPositionByCoordinate(new(0, 8));
        PrintLetters();

        SetPositionByCoordinate(new(-2, 9));
        Console.WriteLine("\t________________________________");
        PrintFieldNumbers();
    }

    private static void PrintFieldNumbers()
    {
        PrintLeftSideNumbers();
        PrintRightSideNumbers();
    }

    private static void PrintRightSideNumbers()
    {
        SetPositionByCoordinate(new(9, -1));
        PrintChar('|');
        for (int i = 0; i < 8; i++)
        {
            SetPositionByCoordinate(new(8, i));
            PrintString((i + 1).ToString());
            PrintChar('|');
        }
        SetPositionByCoordinate(new(9, 8));
        PrintChar('|');
    }

    private static void PrintLeftSideNumbers()
    {
        SetPositionByCoordinate(new(-2, -1));
        PrintChar('|');
        for (int i = 0; i < 8; i++)
        {
            SetPositionByCoordinate(new(-2, i));
            PrintChar('|');
            PrintString((i + 1).ToString());
        }
        SetPositionByCoordinate(new(-2, 8));
        PrintChar('|');
    }

    private void PrintLetters()
    {
        foreach (var l in _letters)
        {
            PrintChar(l);
        }
    }

    private static void SetPositionByCoordinate(Coordinate coordinate)
    {
        Coordinate pos = GetCursorPosition(coordinate);
        Console.SetCursorPosition(pos.X, pos.Y);
    }

    private static Coordinate GetCursorPosition(Coordinate key)
    {
        return new
            (
                _offsetX + (key.X * _multiplierX),
                _offsetY + (key.Y * _multiplierY)
            );
    }

    private static void SetColor(KeyValuePair<Coordinate, FieldPos> fieldPos)
    {
        Console.BackgroundColor = fieldPos.Value.BackColor;
        SetForegroundColor(fieldPos);
    }

    private static void SetForegroundColor(KeyValuePair<Coordinate, FieldPos> fieldPos)
    {
        if (!fieldPos.Value.IsEmpty())
        {
            Console.ForegroundColor = fieldPos.Value.GetPiece()!.Color.Equals(Color.White)
                ? ConsoleColor.Gray
                : ConsoleColor.Black;
        }
    }
}
