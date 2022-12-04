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
            int overlaps = 0;
            var sb = new StringBuilder();
            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                var lineValues = new LineValues();
                foreach (char c in line)
                {
                    if (c == '-')
                    {
                        lineValues.Set(int.Parse(sb.ToString()));
                        sb.Clear();
                    }
                    else if (c == ',')
                    {
                        lineValues.Set(int.Parse(sb.ToString()));
                        sb.Clear();
                    }
                    else
                        sb.Append(c);
                }
                lineValues.Set(int.Parse(sb.ToString()));
                sb.Clear();

                if (lineValues.FullyOverlaps()) overlaps++;
            }

            Console.WriteLine($"day4-1: {overlaps}");
        }
    }

    public void puzzle2()
    {
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            int overlaps = 0;
            var sb = new StringBuilder();
            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                var lineValues = new LineValues();
                foreach (char c in line)
                {
                    if (c == '-')
                    {
                        lineValues.Set(int.Parse(sb.ToString()));
                        sb.Clear();
                    }
                    else if (c == ',')
                    {
                        lineValues.Set(int.Parse(sb.ToString()));
                        sb.Clear();
                    }
                    else
                        sb.Append(c);
                }
                lineValues.Set(int.Parse(sb.ToString()));
                sb.Clear();

                if (lineValues.Overlaps()) overlaps++;
            }

            Console.WriteLine($"day4-1: {overlaps}");
        }
    }

    public record struct LineValues()
    {
        public int FirstMinValue { get; set; } = -1;
        public int FirstMaxValue { get; set; } = -1;
        public int SecondMinValue { get; set; } = -1;
        public int SecondMaxValue { get; set; } = -1;

        public void Set(int value)
        {
            if (FirstMinValue == -1) FirstMinValue = value;
            else if (FirstMaxValue == -1) FirstMaxValue = value;
            else if (SecondMinValue == -1) SecondMinValue = value;
            else if (SecondMaxValue == -1) SecondMaxValue = value;
        }

        public bool FullyOverlaps()
        {
            return (SecondMinValue >= FirstMinValue && SecondMaxValue <= FirstMaxValue)
                || (FirstMinValue >= SecondMinValue && FirstMaxValue <= SecondMaxValue);
        }

        public bool Overlaps()
        {
            return (FirstMinValue <= SecondMaxValue && SecondMinValue <= FirstMaxValue)
                || (SecondMinValue <= FirstMaxValue && FirstMinValue <= SecondMaxValue);
        }
    }
}