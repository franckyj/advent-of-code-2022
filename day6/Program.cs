using System.Text;

var s = new Solution();
s.puzzle2();

public class Solution
{
    public void puzzle1()
    {
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            int[] chars = new int[4];
            chars[0] = file.Read();
            chars[1] = file.Read();
            chars[2] = file.Read();
            chars[3] = file.Read();

            int index = 0;
            int readChars = 4;
            while (!file.EndOfStream)
            {
                var c = file.Read();
                readChars++;
                chars[index++] = c;

                if (chars[0] != chars[1] && chars[0] != chars[2] && chars[0] != chars[3]
                    && chars[1] != chars[2] && chars[1] != chars[3]
                    && chars[2] != chars[3])
                    break;

                if (index == 4) index = 0;
            }
            Console.WriteLine($"day6-1: {readChars}");
        }
    }

    public void puzzle2()
    {
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            var chars = new Dictionary<int, int>();
            var queue = new Queue<int>();
            for (int i = 0; i < 14; ++i)
            {
                var read = file.Read();
                if (chars.ContainsKey(read))
                    chars[read]++;
                else
                    chars.Add(read, 1);
                queue.Enqueue(read);
            }

            int readChars = 14;
            while (!file.EndOfStream)
            {
                var c = file.Read();
                readChars++;
                var remove = queue.Dequeue();
                if (chars.ContainsKey(remove))
                {
                    chars[remove]--;
                    if (chars[remove] == 0)
                        chars.Remove(remove);
                }
                if (chars.ContainsKey(c))
                    chars[c]++;
                else
                    chars.Add(c, 1);

                if (chars.Count == 14)
                    break;

                queue.Enqueue(c);
            }
            Console.WriteLine($"day6-2: {readChars}");
        }
    }
}