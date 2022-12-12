using System.Net.Http.Headers;

var s = new Solution();
s.puzzle2();

public class Solution
{
    public void puzzle1()
    {
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);

        int x = 1;
        int cycle = 0;
        int result = 0;

        while (!file.EndOfStream)
        {
            var line = file.ReadLine();
            var command = line[..4];
            if (command == "noop")
            {
                IncrementCycleAndCheck();
            }
            else
            {
                var value = int.Parse(line[5..]);
                IncrementCycleAndCheck();
                IncrementCycleAndCheck();
                x += value;
            }
        }
        Console.WriteLine($"day10-1: {result}");

        void IncrementCycleAndCheck()
        {
            // 20th, 60th, 100th, 140th, 180th, and 220th cycles
            cycle++;
            if (cycle == 20 || cycle == 60 || cycle == 100
                || cycle == 140 || cycle == 180 || cycle == 220)
            {
                result += (cycle * x);
            }
        }
    }

    public void puzzle2()
    {
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);

        int x = 1;
        int beam = 0;

        while (!file.EndOfStream)
        {
            var line = file.ReadLine();
            var command = line[..4];
            if (command == "noop")
            {
                Draw();
            }
            else
            {
                var value = int.Parse(line[5..]);
                Draw();
                Draw();
                x += value;
            }
        }

        void Draw()
        {
            if (beam == x - 1 || beam == x || beam == x + 1)
                Console.Write("# ");
            else
                Console.Write(". ");

            beam++;
            if (beam == 40)
            {
                beam = 0;
                Console.WriteLine();
            }
        }
    }
}
