using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

var s = new Solution();
s.puzzle2();

public class Solution
{
    public void puzzle1()
    {
        var sw = Stopwatch.StartNew();
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        StringBuilder sb = new StringBuilder();
        int lineLength = 0;
        using var file = new StreamReader(fs);
        {
            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                lineLength = line.Length;
                sb.Append(line);
            }
        }

        int lineCount = sb.Length / lineLength;
        int[] arr = new int[sb.Length];
        for (int i = 0; i < sb.Length; ++i)
        {
            arr[i] = sb[i] switch
            {
                '0' => 0,
                '1' => 1,
                '2' => 2,
                '3' => 3,
                '4' => 4,
                '5' => 5,
                '6' => 6,
                '7' => 7,
                '8' => 8,
                '9' => 9,
                _ => throw new NotImplementedException()
            };
        }
        var results = new HashSet<ValueTuple<int, int>>();

        // need to cast rays from left, top, right and bottom
        // left
        for (int row = 1; row < lineCount - 1; row++)
        {
            int start = row * lineLength;
            int maxTree = arr[start];
            for (int x = 1; x < lineLength - 1; x++)
            {
                var currentTree = arr[start + x];
                if (currentTree > maxTree)
                {
                    maxTree = currentTree;
                    results.Add((x, row));
                }
            }
        }

        // top
        for (int column = 1; column < lineLength - 1; column++)
        {
            int start = column;
            int maxTree = arr[start];
            for (int y = 1; y < lineCount - 1; y++)
            {
                var index = y * lineLength + column;
                var currentTree = arr[index];
                if (currentTree > maxTree)
                {
                    maxTree = currentTree;
                    results.Add((column, y));
                }
            }
        }

        // right
        for (int row = 1; row < lineCount - 1; row++)
        {
            int start = row * lineLength + lineLength - 1;
            int maxTree = arr[start];
            for (int x = lineLength - 2; x > 0; x--)
            {
                var currentTree = arr[row * lineLength + x];
                if (currentTree > maxTree)
                {
                    maxTree = currentTree;
                    results.Add((x, row));
                }
            }
        }

        // bottom
        for (int column = 1; column < lineLength - 1; column++)
        {
            int start = (lineCount - 1) * lineLength + column;
            int maxTree = arr[start];
            for (int y = lineCount - 2; y > 0; y--)
            {
                var index = y * lineLength + column;
                var currentTree = arr[index];
                if (currentTree > maxTree)
                {
                    maxTree = currentTree;
                    results.Add((column, y));
                }
            }
        }
        int perimeter = lineLength * 2 + lineCount * 2 - 4;
        Console.WriteLine($"day8-1: {results.Count + perimeter}");
    }

    public void puzzle2()
    {
        var sw = Stopwatch.StartNew();
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        StringBuilder sb = new StringBuilder();
        int lineLength = 0;
        using var file = new StreamReader(fs);
        {
            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                lineLength = line.Length;
                sb.Append(line);
            }
        }

        int lineCount = sb.Length / lineLength;
        int[] arr = new int[sb.Length];
        for (int i = 0; i < sb.Length; ++i)
        {
            arr[i] = sb[i] switch
            {
                '0' => 0,
                '1' => 1,
                '2' => 2,
                '3' => 3,
                '4' => 4,
                '5' => 5,
                '6' => 6,
                '7' => 7,
                '8' => 8,
                '9' => 9,
                _ => throw new NotImplementedException()
            };
        }

        var grid = new Node[lineLength * lineCount];
        for (int i = 0; i < lineLength * lineCount; ++i)
            grid[i] = new Node() { Value = arr[i] };

        for (int y = 0; y < lineCount; ++y)
        {
            for (int x = 0; x < lineLength; ++x)
            {
                int index = y * lineLength + x;
                int leftIndex = y * lineLength + x - 1;
                if (x > 0)
                    grid[index].Left = grid[leftIndex];

                int rightIndex = y * lineLength + x + 1;
                if (x < lineLength - 1)
                    grid[index].Right = grid[rightIndex];

                var upIndex = (y - 1) * lineLength + x;
                if (y > 0)
                    grid[index].Up = grid[upIndex];

                var downIndex = (y + 1) * lineLength + x;
                if (y < lineCount - 1)
                    grid[index].Down = grid[downIndex];
            }
        }

        var result = 0;
        for (int y = 1; y < lineCount - 1; ++y)
        {
            for (int x = 1; x < lineLength - 1; ++x)
            {
                int index = y * lineLength + x;
                var points = grid[index].GetPoints();
                if (points > result)
                    result = points;
                //Console.Write("{0,-3}", points);
            }
            //Console.WriteLine();
        }
        Console.WriteLine($"day8-2: {result}");
    }

    public record Node
    {
        public int Value { get; init; }
        public Node Up { get; set; }
        public Node Right { get; set; }
        public Node Down { get; set; }
        public Node Left { get; set; }

        private int? _points;

        public int GetPoints()
        {
            _points ??=
            (
                GetPointsFor(0, Value, false) *
                GetPointsFor(1, Value, false) *
                GetPointsFor(2, Value, false) *
                GetPointsFor(3, Value, false)
            );

            return _points.Value;
        }

        private int GetPointsFor(int direction, int value, bool compare)
        {
            if (value > Value || !compare)
            {
                return direction switch
                {
                    0 => Up?.GetPointsFor(direction, value, true) + 1 ?? 0,
                    1 => Right?.GetPointsFor(direction, value, true) + 1 ?? 0,
                    2 => Down?.GetPointsFor(direction, value, true) + 1 ?? 0,
                    3 => Left?.GetPointsFor(direction, value, true) + 1 ?? 0,
                    _ => 0
                };
            }
            return 0;
        }
    }
}