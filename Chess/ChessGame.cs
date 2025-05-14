using Chess.Data;
using Chess.Models;
using Chess.Utils;

namespace Chess;

internal class ChessGame
{
    private const int _offsetX = 20;
    private const int _offsetY = 5;
    private const int _multiplierX = 3;
    private const int _multiplierY = 1;
    public void Run()
    {
        var printer = new Printer(_offsetX, _offsetY, _multiplierX, _multiplierY);
        var inputPicker = new MouseInputPicker();
        var chessField = StandardChessTableStart.Get();
        var ext = false;
        var mouseInput = new MouseInput();
        Coordinate inputCoordinate;
        while (!ext)
        {
            printer.PrintChessGame(chessField);
            var exitSelectionLoop = false;
            while (!exitSelectionLoop)
            {
                mouseInput = inputPicker.GetInput();
                if (mouseInput.Click.Equals(MouseClick.LeftClick))
                {
                    var absPos = printer.GetAbsolutePosition(mouseInput.Coordinate);
                    var possibleMoves = chessField.GetPossibleMoves(absPos);
                    printer.PrintPossibleMoves(chessField, possibleMoves);
                    var secondClick = inputPicker.GetInput();
                    if(secondClick.Click.Equals(MouseClick.LeftClick))
                    {
                        var secondAbsPos = printer.GetAbsolutePosition(secondClick.Coordinate);
                        if (possibleMoves.Contains(secondAbsPos))
                        {
                            chessField.Table.First(f=>f.Position.Equals(secondAbsPos))
                                .PlacePiece(chessField.Table.First(f => f.Position.Equals(absPos)).GetPiece()!);
                        }

                    }
                    // Continue Game

                }
                else
                {
                     printer.PrintField(chessField);
                }

            }
            //inputCoordinate = inputPicker.GetInput();
            //Console.WriteLine($"X: {inputCoordinate.X}, Y:{inputCoordinate.Y}");
            ////var absPos = printer.GetAbsolutePosition(inputCoordinate);
            //Console.WriteLine($"Absolute X: {absPos.X}, Y:{absPos.Y}");
            //inputCoordinate = inputPicker.GetInput();
        }
    }
}
