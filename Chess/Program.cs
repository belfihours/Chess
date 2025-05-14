
//initialize table and pieces
using Chess;
using Chess.Data;
using Chess.Models;
using Chess.Utils;
using System.ComponentModel;

var game = new ChessGame();

game.Run();










int turnCounter = 0;
List<int[]> possibleMoves = new List<int[]>();
int blackPiecesCounter = 16;
int whitePiecesCounter = 16;
int[] intChoice = new int[2];
int[] previousChoice = { 0, 0 };
bool checkMove = false;
Pieces[,] table = new Pieces[8, 8];
Initialize(table);
char winner = ' ';
bool exit = false;

int[] result = new int[2];
var handle = NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE);

int mode = 0;
if (!(NativeMethods.GetConsoleMode(handle, ref mode))) { throw new Win32Exception(); }

mode |= NativeMethods.ENABLE_MOUSE_INPUT;
mode &= ~NativeMethods.ENABLE_QUICK_EDIT_MODE;
mode |= NativeMethods.ENABLE_EXTENDED_FLAGS;

if (!(NativeMethods.SetConsoleMode(handle, mode))) { throw new Win32Exception(); }

var record = new NativeMethods.INPUT_RECORD();
uint recordLen = 0;


while (!exit)
{
    if (!(NativeMethods.ReadConsoleInput(handle, ref record, 1, ref recordLen))) { throw new Win32Exception(); }
    Console.SetCursorPosition(0, 0);
    if (record.EventType == NativeMethods.MOUSE_EVENT)
    {
        if (record.MouseEvent.dwButtonState == 1)
        {
            result[0] = record.MouseEvent.dwMousePosition.X;
            result[1] = record.MouseEvent.dwMousePosition.Y;
            int bwturn = turnCounter % 2;
            char colorturn;
            if (bwturn == 0) colorturn = 'w';
            else colorturn = 'b';
            if (intChoice[0] != 404)
            {

                if (table[intChoice[0], intChoice[1]] == null)
                {
                    if (possibleMoves.Count > 0)
                    {

                        foreach (var pos in possibleMoves)
                        {
                            if (pos[0].Equals(intChoice[0]) && pos[1].Equals(intChoice[1]))
                            {

                                if (table[previousChoice[0], previousChoice[1]].Color == colorturn)
                                {

                                    if (table[previousChoice[0], previousChoice[1]].Move(previousChoice, intChoice, ref winner))
                                    {
                                        if (colorturn == 'w')
                                        {
                                            blackPiecesCounter--;
                                        }
                                        else whitePiecesCounter--;

                                        if (winner != ' ') exit = true;
                                    }
                                    checkMove = true;
                                    turnCounter++;
                                    break;
                                }

                            }
                        }

                        possibleMoves.Clear();
                    }
                    if (!checkMove) Console.WriteLine("No piece there, try again");
                }
                else
                {

                    if (possibleMoves.Count > 0)
                    {
                        foreach (var pos in possibleMoves)
                        {
                            if (pos[0].Equals(intChoice[0]) && pos[1].Equals(intChoice[1]))
                            {

                                if (table[previousChoice[0], previousChoice[1]].Color.Equals(colorturn))
                                {
                                    if (table[previousChoice[0], previousChoice[1]].Move(previousChoice, intChoice, ref winner))
                                    {
                                        if (colorturn == 'w')
                                        {
                                            blackPiecesCounter--;
                                        }
                                        else whitePiecesCounter--;

                                        if (winner != ' ') exit = true;
                                    }
                                    checkMove = true;
                                    turnCounter++;
                                    break;
                                }
                            }
                        }
                        possibleMoves.Clear();

                    }
                    if (!checkMove)
                    {

                        if (table[intChoice[0], intChoice[1]].Color.Equals(colorturn))
                        {
                            table[intChoice[0], intChoice[1]].Show();
                            previousChoice[0] = intChoice[0];
                            previousChoice[1] = intChoice[1];
                        }

                    }
                }


            }
            checkMove = false;
            Print(table);
            if (exit)
            {
                possibleMoves.Clear();
                Print(table);
                GameOver(winner);
            }
            else
            {
                if (bwturn == 0) Console.WriteLine("White turn: ");
                else Console.WriteLine("Black turn: ");
                Console.WriteLine($"X: {result[0]}, Y:{result[1]}");
                intChoice = FindCorrectPositionByMouse(result);
                Thread.Sleep(100);
            }

        }

    }
}
//---METHODS---

void GameOver(char winner)
{
    Console.SetCursorPosition(34, 15);
    if (winner == 'b')
    {
        Console.WriteLine("Black Wins!");

    }
    else if (winner == 'w')
    {
        Console.WriteLine("White Wins!");

    }
}
int[] FindCorrectPositionByMouse(int[] pos)
{
    int[] result = new int[2];

    switch (pos[0])
    {
        case 12:
        case 14:
        case 13:
            {
                result[0] = 0;
                break;
            }
        case 15:
        case 17:
        case 16:
            {
                result[0] = 1;
                break;
            }
        case 18:
        case 20:
        case 19:
            {
                result[0] = 2;
                break;
            }
        case 21:
        case 23:
        case 22:
            {
                result[0] = 3;
                break;
            }
        case 24:
        case 25:
        case 26:
            {
                result[0] = 4;
                break;
            }
        case 27:
        case 28:
        case 29:
            {
                result[0] = 5;
                break;
            }
        case 30:
        case 31:
        case 32:
            {
                result[0] = 6;
                break;
            }
        case 33:
        case 34:
        case 35:
            {
                result[0] = 7;
                break;
            }
        default:
            {
                result[0] = 404;
                break;
            }
    }
    if (pos[1] < 2 || pos[1] > 9)
    {
        result[0] = 404;
    }
    else
    {
        result[1] = pos[1] - 2;
    }


    return result;

}


void Print(Pieces[,] table)
{
    Console.Clear();
    Console.WriteLine("\t________________________________");
    Console.WriteLine($"\t|    A  B  C  D  E  F  G  H    |\tBlack Pieces: {blackPiecesCounter}");
    int colorcounter = 1;
    for (int i = 0; i < 8; i++)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"\t| {i + 1} ");
        colorcounter--;
        for (int j = 0; j < 8; j++)
        {

            if (colorcounter % 2 == 0) Console.BackgroundColor = ConsoleColor.Yellow;
            else Console.BackgroundColor = ConsoleColor.DarkYellow;

            foreach (var pos in possibleMoves)
            {
                if (pos[0].Equals(j) && pos[1].Equals(i)) Console.BackgroundColor = ConsoleColor.Green;
            }

            if (table[j, i] != null)
            {
                if (table[j, i].Color.Equals('b'))
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write($" {table[j, i].Letter} ");


            }
            else Console.Write("   ");



            colorcounter++;
        }

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" {i + 1} |");
        Console.WriteLine();
        //Console.WriteLine("________________________________");
    }
    Console.WriteLine($"\t|    A  B  C  D  E  F  G  H    |\tWhite Pieces: {whitePiecesCounter}");
    Console.WriteLine("\t--------------------------------");

}



//--MOVE

bool Move(Pieces[,] table)
{
    bool moved = false;

    //  INPUT DA CURSORE E REFRESH DEI CONSOLE.WRITELINE (ti cancella quello che hai fatto prima)

    return moved;
}
//initialize table
void Initialize(Pieces[,] table)
{
    List<Pieces>[] list = InitializePieces();
    List<Pieces> blackPieces = list[0];
    List<Pieces> whitePieces = list[1];

    foreach (var piece in blackPieces)
    {
        piece.Color = 'b';
        piece.CurrentPos = piece.StartPos;
    }
    foreach (var piece in whitePieces)
    {
        piece.Color = 'w';
        piece.CurrentPos = piece.StartPos;
    }

    foreach (var piece in blackPieces)
    {
        var pos = piece.StartPos;
        int pos0 = pos[0];
        int pos1 = pos[1];
        table[pos0, pos1] = piece;
    }
    foreach (var piece in whitePieces)
    {
        var pos = piece.StartPos;
        int pos0 = pos[0];
        int pos1 = pos[1];
        table[pos0, pos1] = piece;
    }


}

List<Pieces>[] InitializePieces()
{
    List<Pieces>[] list = new List<Pieces>[2];
    list[0] = new List<Pieces>(); //blacks
    list[1] = new List<Pieces>(); //whites
    Pieces pieces = new Pieces();
    pieces.Table = table;
    pieces.PossibleMoves = possibleMoves;

    //blacks
    Pieces.Queen bQueen = new Pieces.Queen(); bQueen.StartPos[0] = 4; bQueen.StartPos[1] = 0; ; bQueen.Letter = 'Q'; bQueen.Color = 'b';
    Pieces.King bKing = new Pieces.King(); bKing.StartPos[0] = 3; bKing.StartPos[1] = 0; bKing.Letter = 'K'; bKing.Color = 'b';
    Pieces.Bishop bBishop1 = new Pieces.Bishop(); bBishop1.StartPos[0] = 2; bBishop1.StartPos[1] = 0; bBishop1.Letter = 'B'; bBishop1.Color = 'b';
    Pieces.Bishop bBishop2 = new Pieces.Bishop(); bBishop2.StartPos[0] = 5; bBishop2.StartPos[1] = 0; bBishop2.Letter = 'B'; bBishop2.Color = 'b';
    Pieces.Horse bHorse1 = new Pieces.Horse(); bHorse1.StartPos[0] = 1; bHorse1.StartPos[1] = 0; bHorse1.Letter = 'H'; bHorse1.Color = 'b';
    Pieces.Horse bHorse2 = new Pieces.Horse(); bHorse2.StartPos[0] = 6; bHorse2.StartPos[1] = 0; bHorse2.Letter = 'H'; bHorse2.Color = 'w';
    Pieces.Tower bTower1 = new Pieces.Tower(); bTower1.StartPos[0] = 0; bTower1.StartPos[1] = 0; bTower1.Letter = 'T'; bTower1.Color = 'b';
    Pieces.Tower bTower2 = new Pieces.Tower(); bTower2.StartPos[0] = 7; bTower2.StartPos[1] = 0; bTower2.Letter = 'T'; bTower2.Color = 'w';
    Pieces.Pawn bPawn1 = new Pieces.Pawn(); bPawn1.StartPos[0] = 0; bPawn1.StartPos[1] = 1; bPawn1.Letter = 'P'; bPawn1.Color = 'b';
    Pieces.Pawn bPawn2 = new Pieces.Pawn(); bPawn2.StartPos[0] = 1; bPawn2.StartPos[1] = 1; bPawn2.Letter = 'P'; bPawn2.Color = 'b';
    Pieces.Pawn bPawn3 = new Pieces.Pawn(); bPawn3.StartPos[0] = 2; bPawn3.StartPos[1] = 1; bPawn3.Letter = 'P'; bPawn3.Color = 'b';
    Pieces.Pawn bPawn4 = new Pieces.Pawn(); bPawn4.StartPos[0] = 3; bPawn4.StartPos[1] = 1; bPawn4.Letter = 'P'; bPawn4.Color = 'b';
    Pieces.Pawn bPawn5 = new Pieces.Pawn(); bPawn5.StartPos[0] = 4; bPawn5.StartPos[1] = 1; bPawn5.Letter = 'P'; bPawn5.Color = 'b';
    Pieces.Pawn bPawn6 = new Pieces.Pawn(); bPawn6.StartPos[0] = 5; bPawn6.StartPos[1] = 1; bPawn6.Letter = 'P'; bPawn6.Color = 'b';
    Pieces.Pawn bPawn7 = new Pieces.Pawn(); bPawn7.StartPos[0] = 6; bPawn7.StartPos[1] = 1; bPawn7.Letter = 'P'; bPawn7.Color = 'b';
    Pieces.Pawn bPawn8 = new Pieces.Pawn(); bPawn8.StartPos[0] = 7; bPawn8.StartPos[1] = 1; bPawn8.Letter = 'P'; bPawn8.Color = 'b';
    list[0].Add(bQueen);
    list[0].Add(bKing);
    list[0].Add(bBishop1);
    list[0].Add(bBishop2);
    list[0].Add(bHorse1);
    list[0].Add(bHorse2);
    list[0].Add(bTower1);
    list[0].Add(bTower2);
    list[0].Add(bPawn1);
    list[0].Add(bPawn2);
    list[0].Add(bPawn3);
    list[0].Add(bPawn4);
    list[0].Add(bPawn5);
    list[0].Add(bPawn6);
    list[0].Add(bPawn7);
    list[0].Add(bPawn8);
    //whites
    Pieces.Queen wQueen = new Pieces.Queen(); wQueen.StartPos[0] = 4; wQueen.StartPos[1] = 7; ; wQueen.Letter = 'Q'; wQueen.Color = 'w';
    Pieces.King wKing = new Pieces.King(); wKing.StartPos[0] = 3; wKing.StartPos[1] = 7; wKing.Letter = 'K'; wKing.Color = 'w';
    Pieces.Bishop wBishop1 = new Pieces.Bishop(); wBishop1.StartPos[0] = 2; wBishop1.StartPos[1] = 7; wBishop1.Letter = 'B'; wBishop1.Color = 'w';
    Pieces.Bishop wBishop2 = new Pieces.Bishop(); wBishop2.StartPos[0] = 5; wBishop2.StartPos[1] = 7; wBishop2.Letter = 'B'; wBishop2.Color = 'w';
    Pieces.Horse wHorse1 = new Pieces.Horse(); wHorse1.StartPos[0] = 1; wHorse1.StartPos[1] = 7; wHorse1.Letter = 'H'; wHorse1.Color = 'w';
    Pieces.Horse wHorse2 = new Pieces.Horse(); wHorse2.StartPos[0] = 6; wHorse2.StartPos[1] = 7; wHorse2.Letter = 'H'; wHorse2.Color = 'w';
    Pieces.Tower wTower1 = new Pieces.Tower(); wTower1.StartPos[0] = 0; wTower1.StartPos[1] = 7; wTower1.Letter = 'T'; wTower1.Color = 'w';
    Pieces.Tower wTower2 = new Pieces.Tower(); wTower2.StartPos[0] = 7; wTower2.StartPos[1] = 7; wTower2.Letter = 'T'; wTower2.Color = 'w';
    Pieces.Pawn wPawn1 = new Pieces.Pawn(); wPawn1.StartPos[0] = 0; wPawn1.StartPos[1] = 6; wPawn1.Letter = 'P'; wPawn1.Color = 'w';
    Pieces.Pawn wPawn2 = new Pieces.Pawn(); wPawn2.StartPos[0] = 1; wPawn2.StartPos[1] = 6; wPawn2.Letter = 'P'; wPawn2.Color = 'w';
    Pieces.Pawn wPawn3 = new Pieces.Pawn(); wPawn3.StartPos[0] = 2; wPawn3.StartPos[1] = 6; wPawn3.Letter = 'P'; wPawn3.Color = 'w';
    Pieces.Pawn wPawn4 = new Pieces.Pawn(); wPawn4.StartPos[0] = 3; wPawn4.StartPos[1] = 6; wPawn4.Letter = 'P'; wPawn4.Color = 'w';
    Pieces.Pawn wPawn5 = new Pieces.Pawn(); wPawn5.StartPos[0] = 4; wPawn5.StartPos[1] = 6; wPawn5.Letter = 'P'; wPawn5.Color = 'w';
    Pieces.Pawn wPawn6 = new Pieces.Pawn(); wPawn6.StartPos[0] = 5; wPawn6.StartPos[1] = 6; wPawn6.Letter = 'P'; wPawn6.Color = 'w';
    Pieces.Pawn wPawn7 = new Pieces.Pawn(); wPawn7.StartPos[0] = 6; wPawn7.StartPos[1] = 6; wPawn7.Letter = 'P'; wPawn7.Color = 'w';
    Pieces.Pawn wPawn8 = new Pieces.Pawn(); wPawn8.StartPos[0] = 7; wPawn8.StartPos[1] = 6; wPawn8.Letter = 'P'; wPawn8.Color = 'w';
    list[1].Add(wQueen);
    list[1].Add(wKing);
    list[1].Add(wBishop1);
    list[1].Add(wBishop2);
    list[1].Add(wHorse1);
    list[1].Add(wHorse2);
    list[1].Add(wTower1);
    list[1].Add(wTower2);
    list[1].Add(wPawn1);
    list[1].Add(wPawn2);
    list[1].Add(wPawn3);
    list[1].Add(wPawn4);
    list[1].Add(wPawn5);
    list[1].Add(wPawn6);
    list[1].Add(wPawn7);
    list[1].Add(wPawn8);
    return list;
}