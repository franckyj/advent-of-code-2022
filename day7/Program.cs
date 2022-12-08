using System.Diagnostics;

var s = new Solution();
s.puzzle2();

public class Solution
{
    public void puzzle1()
    {
        var sw = Stopwatch.StartNew();
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            Folder? currentFolder = null;

            while (!file.EndOfStream)
            {
                var command = file.ReadLine().AsSpan();

                var start = command[..4];
                string name;
                Folder f;
                switch (start)
                {
                    case "$ cd":
                        name = command[5..].ToString();
                        if (name == "..")
                            currentFolder = currentFolder?.Parent;
                        else
                            currentFolder = currentFolder?.GetFolder(name) ?? new Folder(null, name);
                        break;
                    case "$ ls":
                        break;
                    case "dir ":
                        name = command[4..].ToString();
                        new Folder(currentFolder, name);
                        break;
                    default:
                        var size = command[..command.IndexOf(' ')];
                        currentFolder?.SetSize(int.Parse(size));
                        break;
                }
            }

            Folder topMost = currentFolder;
            while (topMost.Parent != null)
                topMost = topMost.Parent;

            var stack = new Stack<Folder>();
            stack.Push(topMost);

            int result = 0;
            while (stack.Count > 0)
            {
                var f = stack.Pop();
                if (f.IsAtMost(100000))
                    result += f.Size;

                foreach (var c in f.Children)
                    stack.Push(c.Value);
            }

            Console.WriteLine($"day7-1: {result} in {sw.Elapsed}");
        }
    }

    public void puzzle2()
    {
        const int TotalDiskSpace = 70000000;
        const int UnusedSpaceNeeded = 30000000;

        var sw = Stopwatch.StartNew();
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            Folder? currentFolder = null;

            while (!file.EndOfStream)
            {
                var command = file.ReadLine().AsSpan();

                var start = command[..4];
                string name;
                Folder f;
                switch (start)
                {
                    case "$ cd":
                        name = command[5..].ToString();
                        if (name == "..")
                            currentFolder = currentFolder?.Parent;
                        else
                            currentFolder = currentFolder?.GetFolder(name) ?? new Folder(null, name);
                        break;
                    case "$ ls":
                        break;
                    case "dir ":
                        name = command[4..].ToString();
                        new Folder(currentFolder, name);
                        break;
                    default:
                        var size = command[..command.IndexOf(' ')];
                        currentFolder?.SetSize(int.Parse(size));
                        break;
                }
            }

            Folder topMost = currentFolder;
            while (topMost.Parent != null)
                topMost = topMost.Parent;

            var stack = new Stack<Folder>();
            stack.Push(topMost);

            int result = 0;
            int unusedSpace = TotalDiskSpace - topMost.Size;
            int missingSpace = UnusedSpaceNeeded - unusedSpace;
            int minDiff = int.MinValue;
            while (stack.Count > 0)
            {
                var f = stack.Pop();
                var diff = missingSpace - f.Size;
                if (diff < 0 && diff > minDiff)
                {
                    minDiff = diff;
                    result = f.Size;
                }
                foreach (var c in f.Children)
                    stack.Push(c.Value);
            }

            Console.WriteLine($"day7-2: {result} in {sw.Elapsed}");
        }
    }

    public class Folder
    {
        public int Size { get; private set; } = 0;

        public Folder? Parent { get; set; }

        public string Name { get; private set; }

        public Dictionary<string, Folder> Children { get; private set; }

        public Folder(Folder? parent, string name)
        {
            Parent = parent;
            Name = name;
            Children = new Dictionary<string, Folder>();

            parent?.Children.Add(Name, this);
        }

        public void SetSize(int size)
        {
            Size += size;
            Parent?.SetSize(size);
        }

        public Folder? GetFolder(string name)
        {
            Children.TryGetValue(name, out Folder? folder);
            return folder;
        }

        public bool IsAtMost(int size) => Size <= size;
    }
}