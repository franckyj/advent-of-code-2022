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
            var lists = new List<Stack<int>>(9);
            for (int i = 0; i < 9; i++)
                lists.Add(new Stack<int>(9));

            int charIndex = 0;
            while (!file.EndOfStream)
            {
                var c1 = file.Read();
                if (c1 == '\r')
                {
                    if (charIndex == 0)
                        break;

                    charIndex = 0;

                    // \n
                    file.Read();
                    continue;
                }

                if (c1 == '[')
                {
                    var crate = file.Read();
                    var index = charIndex / 4;
                    lists[index].Push(crate);
                    charIndex++;
                }
                charIndex++;
            }

            // could be better memory wise (2 lists of stacks...)
            var stacks = new List<Stack<int>>(9);
            for (int i = 0; i < 9; i++)
            {
                var list = lists[i];
                var stack = new Stack<int>(20);
                foreach (var ch in list)
                    stack.Push(ch);

                stacks.Add(stack);
            }

            var c = file.Read();

            // word number word number word number
            var sb = new StringBuilder();
            while (!file.EndOfStream)
            {
                var step = new Step();
                while (c != ' ')
                    c = file.Read();

                c = file.Read();
                while (c != ' ')
                {
                    sb.Append((char)c);
                    c = file.Read();
                }
                step.Many = int.Parse(sb.ToString());
                sb.Clear();

                c = file.Read();
                while (c != ' ')
                    c = file.Read();

                c = file.Read();
                while (c != ' ')
                {
                    sb.Append((char)c);
                    c = file.Read();
                }
                step.From = int.Parse(sb.ToString());
                sb.Clear();

                c = file.Read();
                while (c != ' ')
                    c = file.Read();

                c = file.Read();
                while (c != ' ' && c != '\r' && c != -1)
                {
                    sb.Append((char)c);
                    c = file.Read();
                }
                step.To = int.Parse(sb.ToString());
                sb.Clear();

                c = file.Read();

                // execute the steps
                for (int i = 0; i < step.Many; ++i)
                {
                    int tmp = stacks[step.From - 1].Pop();
                    stacks[step.To - 1].Push(tmp);
                }
            }

            sb.Clear();
            foreach (var stack in stacks)
                sb.Append((char)stack.Peek());

            Console.WriteLine($"day5-1: {sb}");
        }
    }

    public void puzzle2()
    {
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            var lists = new List<Stack<int>>(9);
            for (int i = 0; i < 9; i++)
                lists.Add(new Stack<int>(9));

            int charIndex = 0;
            while (!file.EndOfStream)
            {
                var c1 = file.Read();
                if (c1 == '\r')
                {
                    if (charIndex == 0)
                        break;

                    charIndex = 0;

                    // \n
                    file.Read();
                    continue;
                }

                if (c1 == '[')
                {
                    var crate = file.Read();
                    var index = charIndex / 4;
                    lists[index].Push(crate);
                    charIndex++;
                }
                charIndex++;
            }

            // could be better memory wise (2 lists of stacks...)
            var stacks = new List<Stack<int>>(9);
            for (int i = 0; i < 9; i++)
            {
                var list = lists[i];
                var stack = new Stack<int>(20);
                foreach (var ch in list)
                    stack.Push(ch);

                stacks.Add(stack);
            }

            var c = file.Read();

            // word number word number word number
            var sb = new StringBuilder();
            var tmpStack = new Stack<int>(20);
            while (!file.EndOfStream)
            {
                var step = new Step();
                while (c != ' ')
                    c = file.Read();

                c = file.Read();
                while (c != ' ')
                {
                    sb.Append((char)c);
                    c = file.Read();
                }
                step.Many = int.Parse(sb.ToString());
                sb.Clear();

                c = file.Read();
                while (c != ' ')
                    c = file.Read();

                c = file.Read();
                while (c != ' ')
                {
                    sb.Append((char)c);
                    c = file.Read();
                }
                step.From = int.Parse(sb.ToString());
                sb.Clear();

                c = file.Read();
                while (c != ' ')
                    c = file.Read();

                c = file.Read();
                while (c != ' ' && c != '\r' && c != -1)
                {
                    sb.Append((char)c);
                    c = file.Read();
                }
                step.To = int.Parse(sb.ToString());
                sb.Clear();

                c = file.Read();

                // execute the steps
                for (int i = 0; i < step.Many; ++i)
                {
                    int tmp = stacks[step.From - 1].Pop();
                    tmpStack.Push(tmp);
                }

                for (int i = 0; i < step.Many; ++i)
                {
                    int tmp = tmpStack.Pop();
                    stacks[step.To - 1].Push(tmp);
                }
            }

            sb.Clear();
            foreach (var stack in stacks)
                sb.Append((char)stack.Peek());

            Console.WriteLine($"day5-2: {sb}");
        }
    }

    public record struct Step(int Many, int From, int To);
}