var s = new Solution();
s.puzzle2();

public class Solution
{
    public void puzzle1()
    {
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);

        var monkeys = new MonkeyList();
        var monkeyIndex = 0;
        while (!file.EndOfStream)
        {
            _ = file.ReadLine();
            var startingItems = file.ReadLine()["  Starting items: ".Length..]
                .Split(", ")
                .Select(long.Parse);
            var operationLine = file.ReadLine()["  Operation: new = old ".Length..].AsSpan();
            Func<long, long> operation;
            if (!long.TryParse(operationLine[2..], out long op))
                operation = i => i * i;
            else
                operation = operationLine[0] switch
                {
                    '*' => i => i * op,
                    '/' => i => i / op,
                    '+' => i => i + op,
                    '-' => i => i - op,
                    _ => i => i,
                };
            var div = long.Parse(file.ReadLine()["  Test: divisible by ".Length..].AsSpan());
            Func<long, bool> test = i => i % div == 0;
            var trueMonkey = int.Parse(file.ReadLine()["    If true: throw to monkey ".Length..]);
            var falseMonkey = int.Parse(file.ReadLine()["    If false: throw to monkey ".Length..]);

            var monkey = new Monkey(monkeyIndex++, startingItems, operation, test, trueMonkey, falseMonkey, div);
            monkeys.AddMonkey(monkey);

            file.ReadLine();
        }

        for (long i = 0; i < 20; i++)
            monkeys.Inspect1();

        Console.WriteLine($"day11-1: {monkeys.GetMonkeyBusiness()}");
    }

    public void puzzle2()
    {
        using var fs = new FileStream("input.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);

        var monkeys = new MonkeyList();
        var monkeyIndex = 0;
        while (!file.EndOfStream)
        {
            _ = file.ReadLine();
            var startingItems = file.ReadLine()["  Starting items: ".Length..]
                .Split(", ")
                .Select(long.Parse);
            var operationLine = file.ReadLine()["  Operation: new = old ".Length..].AsSpan();
            Func<long, long> operation;
            if (!long.TryParse(operationLine[2..], out long op))
                operation = i => i * i;
            else
                operation = operationLine[0] switch
                {
                    '*' => i => i * op,
                    '/' => i => i / op,
                    '+' => i => i + op,
                    '-' => i => i - op,
                    _ => i => i,
                };
            var div = long.Parse(file.ReadLine()["  Test: divisible by ".Length..].AsSpan());
            Func<long, bool> test = i => i % div == 0;
            var trueMonkey = int.Parse(file.ReadLine()["    If true: throw to monkey ".Length..]);
            var falseMonkey = int.Parse(file.ReadLine()["    If false: throw to monkey ".Length..]);

            var monkey = new Monkey(monkeyIndex++, startingItems, operation, test, trueMonkey, falseMonkey, div);
            monkeys.AddMonkey(monkey);

            file.ReadLine();
        }

        for (long i = 0; i < 10000; i++)
            monkeys.Inspect2();

        Console.WriteLine($"day11-2: {monkeys.GetMonkeyBusiness()}");
    }

    class MonkeyList
    {
        public IList<Monkey> Monkeys { get; init; }

        public long Modulo { get; private set; }

        public MonkeyList()
        {
            Monkeys = new List<Monkey>();
            Modulo = 1;
        }

        private long _mostSeenItem1;
        private long _mostSeenItem2;

        public void AddMonkey(Monkey monkey)
        {
            Monkeys.Add(monkey);
            Modulo *= monkey.Div;
        }

        public void Inspect1()
        {
            foreach (Monkey monkey in Monkeys)
                monkey.Inspect1(this);
        }

        public void Inspect2()
        {
            foreach (Monkey monkey in Monkeys)
                monkey.Inspect2(this);
        }

        public long GetMonkeyBusiness()
        {
            foreach (Monkey monkey in Monkeys)
            {
                if (monkey.Inspected > _mostSeenItem1)
                {
                    (_mostSeenItem1, _mostSeenItem2) = (_mostSeenItem2, _mostSeenItem1);
                    _mostSeenItem1 = monkey.Inspected;
                }
                else if (monkey.Inspected > _mostSeenItem2)
                {
                    _mostSeenItem2 = monkey.Inspected;
                }
            }

            return _mostSeenItem1 * _mostSeenItem2;
        }
    }

    class Monkey
    {
        public long Index { get; init; }
        public Queue<long> Items { get; }
        public long Inspected { get; private set; }
        public long Div { get; private set; }

        private readonly Func<long, long> _operation;
        private readonly Func<long, bool> _test;
        private readonly int _trueMonkey;
        private readonly int _falseMonkey;

        public Monkey(
            long index,
            IEnumerable<long> items,
            Func<long, long> operation,
            Func<long, bool> test,
            int trueMonkey,
            int falseMonkey,
            long div)
        {
            Index = index;
            Items = new Queue<long>(items);

            _operation = operation;
            _test = test;
            _trueMonkey = trueMonkey;
            _falseMonkey = falseMonkey;
            Div = div;
        }

        public void Inspect1(MonkeyList monkeys)
        {
            while (Items.Count > 0)
            {
                long item = Items.Dequeue();
                long newItem = _operation(item);
                newItem /= 3;
                if (_test(newItem))
                    monkeys.Monkeys[_trueMonkey].Items.Enqueue(newItem);
                else
                    monkeys.Monkeys[_falseMonkey].Items.Enqueue(newItem);

                Inspected++;
            }
        }

        public void Inspect2(MonkeyList monkeys)
        {
            while (Items.Count > 0)
            {
                long item = Items.Dequeue();
                long newItem = _operation(item);
                newItem %= monkeys.Modulo;
                if (_test(newItem))
                    monkeys.Monkeys[_trueMonkey].Items.Enqueue(newItem);
                else
                    monkeys.Monkeys[_falseMonkey].Items.Enqueue(newItem);

                Inspected++;
            }
        }
    }
}