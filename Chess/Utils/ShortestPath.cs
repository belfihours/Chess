using Chess.Models;

namespace Chess.Utils;
internal static class ShortestPath
{
    public static List<Coordinate> FindShortestPath(List<Coordinate> possibleMoves, Coordinate start, Coordinate end)
    {
        int rows = possibleMoves.Max(g=>g.X);
        int cols = possibleMoves.Max(g => g.Y);
        bool[,] visited = new bool[rows, cols];
        Queue<List<Coordinate>> queue = new();

        queue.Enqueue(possibleMoves);
        visited[start.X, start.Y] = true;

        List<Coordinate> directions =
        [
            new(0, 1), // right
            new(1, 1), // right-down
            new(1, 0), // down
            new(1, -1), // down-left
            new(0, -1), // left
            new(-1, -1), // left-up
            new(-1, 0), // up
            new(-1, 1) // up-right
        ];

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var current = path[path.Count - 1];

            if (current == end)
            {
                return path;
            }

            foreach (var direction in directions)
            {
                int newRow = current.Item1 + direction[0];
                int newCol = current.Item2 + direction[1];

                if (newRow >= 0;
                var newPath = new List<Coordinate>(path) { (newRow, newCol) };
                queue.Enqueue(newPath);
            }
        }
        return null; // No path found
    }

    public static void Main()
    {
        int[,] grid = new int[1, 6] { { 0, 0, 0, 0, 0, 0 } };
        var path = FindShortestPath(grid, (0, 0), (0, 5));

        if (path != null)
        {
            foreach (var point in path)
            {
                Console.Write($"({point.Item1}, {point.Item2}) ");
            }
        }
        else
        {
            Console.WriteLine("No path found.");
        }
    }
}

