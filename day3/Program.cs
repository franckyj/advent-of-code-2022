// Lowercase item types a through z have priorities 1 through 26.
// Uppercase item types A through Z have priorities 27 through 52.

var s = new Solution();
s.puzzle2();

public class Solution
{
    public void puzzle1()
    {
        int score = 0;
        var left = new HashSet<char>();
        var right = new HashSet<char>();
        using var fs = new FileStream("input-1.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                var len = line.Length;
                var half = len / 2;
                for (int i = 0; i < half; ++i)
                {
                    left.Add(line[i]);
                }
                for (int i = half; i < len; ++i)
                {
                    right.Add(line[i]);
                }
                left.IntersectWith(right);
                foreach (var el in left)
                {
                    if (el >= 97 && el <= 122)
                        score += (el - 96);
                    else if (el >= 65 && el <= 90)
                        score += (el - 38);
                }

                left.Clear();
                right.Clear();
            }
        }

        Console.WriteLine($"score day3-1: {score}");
    }

    public void puzzle2()
    {
        int score = 0;
        var left = new HashSet<char>();
        var right = new HashSet<char>();
        using var fs = new FileStream("input-2.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            while (!file.EndOfStream)
            {
                var line1 = file.ReadLine();
                var line2 = file.ReadLine();
                var line3 = file.ReadLine();
                for (int i = 0; i < line1.Length; ++i)
                {
                    left.Add(line1[i]);
                }
                for (int i = 0; i < line2.Length; ++i)
                {
                    right.Add(line2[i]);
                }
                left.IntersectWith(right);
                right.Clear();
                for (int i = 0; i < line3.Length; ++i)
                {
                    right.Add(line3[i]);
                }
                left.IntersectWith(right);
                right.Clear();
                foreach (var el in left)
                {
                    if (el >= 97 && el <= 122)
                        score += (el - 96);
                    else if (el >= 65 && el <= 90)
                        score += (el - 38);
                }

                left.Clear();
            }
        }

        Console.WriteLine($"score day3-2: {score}");
    }
}