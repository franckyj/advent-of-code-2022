var s = new Solution();
s.puzzle2();

public class Solution
{
    public void puzzle1()
    {
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        var ht = new HeadTail();
        while (!file.EndOfStream)
        {
            var direction = (char)file.Read();
            file.Read();

            int steps = 0;
            var i1 = (char)file.Read();
            var i2 = (char)file.Read();

            if (char.IsDigit(i2))
            {
                steps = int.Parse($"{i1}{i2}");
                if (!file.EndOfStream)
                    file.Read();
            }
            else
                steps = int.Parse($"{i1}");

            if (!file.EndOfStream)
                file.Read();

            ht.MoveHead(direction, steps);
        }
        Console.WriteLine($"day9-1: {ht.GetDistinctLocations()}");
    }

    public void puzzle2()
    {
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        var ht = new HeadTailN();
        while (!file.EndOfStream)
        {
            var direction = (char)file.Read();
            file.Read();

            int steps = 0;
            var i1 = (char)file.Read();
            var i2 = (char)file.Read();

            if (char.IsDigit(i2))
            {
                steps = int.Parse($"{i1}{i2}");
                if (!file.EndOfStream)
                    file.Read();
            }
            else
                steps = int.Parse($"{i1}");

            if (!file.EndOfStream)
                file.Read();

            ht.MoveHead(direction, steps);
        }
        Console.WriteLine($"day9-2: {ht.GetDistinctLocations()}");
    }

    public class HeadTail
    {
        public (int, int) Head { get; private set; }
        public (int, int) Tail { get; private set; }

        private readonly HashSet<(int, int)> _visited = new();

        public HeadTail()
        {
            _visited.Add((0, 0));
            Head = (0, 0);
            Tail = (0, 0);
        }

        public int GetDistinctLocations()
        {
            return _visited.Count;
        }

        public void MoveHead(char direction, int steps)
        {
            (int xDirection, int yDirection) = direction switch
            {
                'U' => (0, 1),
                'R' => (1, 0),
                'D' => (0, -1),
                'L' => (-1, 0),
                _ => (0, 0)
            };

            for (int i = 0; i < steps; ++i)
            {
                Head = (Head.Item1 + xDirection, Head.Item2 + yDirection);
                var diff = (Head.Item1 - Tail.Item1, Head.Item2 - Tail.Item2);

                var tailMove = diff switch
                {
                    (0, 2) => (0, 1),
                    (1, 2) => (1, 1),
                    (2, 1) => (1, 1),
                    (2, 0) => (1, 0),
                    (2, -1) => (1, -1),
                    (1, -2) => (1, -1),
                    (0, -2) => (0, -1),
                    (-1, -2) => (-1, -1),
                    (-2, -1) => (-1, -1),
                    (-2, 0) => (-1, 0),
                    (-2, 1) => (-1, 1),
                    (-1, 2) => (-1, 1),
                    _ => (0, 0)
                };

                Tail = (Tail.Item1 + tailMove.Item1, Tail.Item2 + tailMove.Item2);
                _visited.Add((Tail.Item1, Tail.Item2));
            }
        }
    }

    public class HeadTailN
    {
        public (int, int) Head { get; private set; }
        public (int, int)[] Tails { get; private set; }

        private readonly HashSet<(int, int)> _visited = new();

        public HeadTailN()
        {
            _visited.Add((0, 0));
            Head = (0, 0);
            Tails = new (int, int)[9];
        }

        public int GetDistinctLocations()
        {
            return _visited.Count;
        }

        public void MoveHead(char direction, int steps)
        {
            (int xDirection, int yDirection) = direction switch
            {
                'U' => (0, 1),
                'R' => (1, 0),
                'D' => (0, -1),
                'L' => (-1, 0),
                _ => (0, 0)
            };

            // do through all the knots until the tail
            for (int i = 0; i < steps; ++i)
            {
                Head = (Head.Item1 + xDirection, Head.Item2 + yDirection);
                var diff = (Head.Item1 - Tails[0].Item1, Head.Item2 - Tails[0].Item2);

                var move = diff switch
                {
                    (0, 2) => (0, 1),
                    (1, 2) => (1, 1),
                    (2, 1) => (1, 1),
                    (2, 0) => (1, 0),
                    (2, -1) => (1, -1),
                    (1, -2) => (1, -1),
                    (0, -2) => (0, -1),
                    (-1, -2) => (-1, -1),
                    (-2, -1) => (-1, -1),
                    (-2, 0) => (-1, 0),
                    (-2, 1) => (-1, 1),
                    (-1, 2) => (-1, 1),
                    _ => (0, 0)
                };

                Tails[0] = (Tails[0].Item1 + move.Item1, Tails[0].Item2 + move.Item2);

                Move(1);
                Move(2);
                Move(3);
                Move(4);
                Move(5);
                Move(6);
                Move(7);
                Move(8);

                _visited.Add((Tails[8].Item1, Tails[8].Item2));
            }
        }

        private void Move(int index)
        {
            var diff = (Tails[index - 1].Item1 - Tails[index].Item1, Tails[index - 1].Item2 - Tails[index].Item2);
            var move = diff switch
            {
                (0, 2) => (0, 1),
                (1, 2) => (1, 1),
                (2, 2) => (1, 1),
                (2, 1) => (1, 1),
                (2, 0) => (1, 0),
                (2, -1) => (1, -1),
                (2, -2) => (1, -1),
                (1, -2) => (1, -1),
                (0, -2) => (0, -1),
                (-1, -2) => (-1, -1),
                (-2, -2) => (-1, -1),
                (-2, -1) => (-1, -1),
                (-2, 0) => (-1, 0),
                (-2, 1) => (-1, 1),
                (-2, 2) => (-1, 1),
                (-1, 2) => (-1, 1),
                _ => (0, 0)
            };

            Tails[index] = (Tails[index].Item1 + move.Item1, Tails[index].Item2 + move.Item2);
        }
    }
}