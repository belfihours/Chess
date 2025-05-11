namespace Chess.Models;

internal record struct Coordinate(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}
