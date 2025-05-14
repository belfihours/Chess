using Chess.Models;

namespace Chess.Utils;

internal class Printer
{
    private readonly int _offsetX = 20;
    private readonly int _offsetY = 5;
    private readonly int _multiplierX = 3;
    private readonly int _multiplierY = 1;
    private readonly IEnumerable<char> _letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];
    private readonly ConsoleColor _defaultBackColor = ConsoleColor.Black;
    private readonly ConsoleColor _defaultForegroundColor = ConsoleColor.White;

    public Printer(int offsetX, int offsetY, int multiplierX, int multiplierY)
    {
        _offsetX = offsetX;
        _offsetY = offsetY;
        _multiplierX = multiplierX;
        _multiplierY = multiplierY;
    }
    public Printer() { }

    public void PrintChessGame(ChessField field)
    {
        Console.Clear();
        PrintField(field);
        PrintScore(field);
    }

    public void PrintPossibleMoves(ChessField field, IEnumerable<Coordinate> coordinates)
    {
        RestorePrintColors();
        foreach (var coord in coordinates)
        {
            SetPositionByCoordinate(coord);
            Console.BackgroundColor = ConsoleColor.Green;
            PrintChar(' ');
        }
    }

    public Coordinate GetAlteredPosition(Coordinate key)
    {
        return new
            (
                _offsetX + (key.X * _multiplierX),
                _offsetY + (key.Y * _multiplierY)
            );
    }

    public Coordinate GetAbsolutePosition(Coordinate key)
    {
        return new
            (
                (key.X - _offsetX) / _multiplierX,
                (key.Y - _offsetY) / _multiplierY
            );
    }

    private void PrintScore(ChessField field)
    {
        RestorePrintColors();
        SetPositionByCoordinate(new(0, _offsetY+11), true);
        Console.WriteLine($"White pieces: {field.GetPiecesCount(Color.White)}");
        Console.WriteLine($"Black pieces: {field.GetPiecesCount(Color.Black)}");
        var nextToMove = field.GetTurn();
        Console.WriteLine($"Turn {field.Turn}: moves {nextToMove}");
    }

    private void RestorePrintColors()
    {
        Console.ForegroundColor = _defaultForegroundColor;
        Console.BackgroundColor = _defaultBackColor;
    }

    public void PrintField(ChessField field)
    {
        RestorePrintColors();
        PrintBorders();
        foreach (var fieldPos in field.Table)
        {
            SetPositionByCoordinate(fieldPos.Position);
            SetColor(fieldPos);
            PrintChar(fieldPos.GetLetter());
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

    private void PrintFieldNumbers()
    {
        PrintLeftSideNumbers();
        PrintRightSideNumbers();
    }

    private void PrintRightSideNumbers()
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

    private void PrintLeftSideNumbers()
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

    private void SetPositionByCoordinate(Coordinate coordinate, bool absolute = false)
    {
        if (absolute)
        {
            Console.SetCursorPosition(coordinate.X, coordinate.Y);
            return;
        }
        Coordinate pos = GetAlteredPosition(coordinate);
        Console.SetCursorPosition(pos.X, pos.Y);
    }

    private static void SetColor(FieldPos fieldPos)
    {
        Console.BackgroundColor = fieldPos.BackColor;
        SetForegroundColor(fieldPos);
    }

    private static void SetForegroundColor(FieldPos fieldPos)
    {
        if (!fieldPos.IsEmpty())
        {
            Console.ForegroundColor = fieldPos.GetPiece()!.Color.Equals(Color.White)
                ? ConsoleColor.Gray
                : ConsoleColor.Black;
        }
    }
}
