var s = new Solution();
s.puzzle2();

public class Solution
{
    public void puzzle1()
    {
        int score = 0;
        using var fs = new FileStream("input-1.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            while (!file.EndOfStream)
            {
                var hand = ReadHandPuzzle1(file);
                score += GetHandScore(hand.You, hand.Opponent);
            }
        }

        Console.WriteLine($"score-1: {score}");
    }

    private (RPS Opponent, RPS You) ReadHandPuzzle1(StreamReader file)
    {
        char opponent = (char)file.Read();

        // the space
        file.Read();
        char you = (char)file.Read();

        if (!file.EndOfStream)
        {
            // the newline
            file.Read();
            file.Read();
        }

        return ((RPS)(opponent - 'A'), (RPS)(you - 'X'));
    }

    private bool? Beat(RPS you, RPS opponent)
    {
        if (you == opponent) return null;
        return
            you == RPS.Rock && opponent == RPS.Scissors ||
            you == RPS.Paper && opponent == RPS.Rock ||
            you == RPS.Scissors && opponent == RPS.Paper;
    }

    private int GetHandScore(RPS you, RPS opponent)
    {
        int score;
        var beat = Beat(you, opponent);

        if (!beat.HasValue)
            score = 3 + (int)you + 1;
        else if (beat.Value)
            score = 6 + (int)you + 1;
        else
            score = (int)you + 1;

        return score;
    }

    public void puzzle2()
    {
        // X lose
        // Y draw
        // Z win
        int score = 0;
        using var fs = new FileStream("input-2.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            while (!file.EndOfStream)
            {
                var hand = ReadHandPuzzle2(file);
                score += GetScoreForOutcome(hand.Outcome, hand.Opponent);
            }
        }

        Console.WriteLine($"score-2: {score}");
    }

    private (RPS Opponent, RPSOutcome Outcome) ReadHandPuzzle2(StreamReader file)
    {
        char opponent = (char)file.Read();

        // the space
        file.Read();
        char you = (char)file.Read();

        if (!file.EndOfStream)
        {
            // the newline
            file.Read();
            file.Read();
        }

        return ((RPS)(opponent - 'A'), (RPSOutcome)(you - 'X'));
    }

    private int GetScoreForOutcome(RPSOutcome outcome, RPS opponent)
    {
        return (outcome, opponent) switch
        {
            (RPSOutcome.Lose, RPS.Rock) => (int)RPS.Scissors + 1,
            (RPSOutcome.Lose, RPS.Paper) => (int)RPS.Rock + 1,
            (RPSOutcome.Lose, RPS.Scissors) => (int)RPS.Paper + 1,
            (RPSOutcome.Draw, _) => 3 + (int)opponent + 1,
            (RPSOutcome.Win, RPS.Rock) => 6 + (int)RPS.Paper + 1,
            (RPSOutcome.Win, RPS.Paper) => 6 + (int)RPS.Scissors + 1,
            (RPSOutcome.Win, RPS.Scissors) => 6 + (int)RPS.Rock + 1,
            _ => 0
        };
    }

    public enum RPS
    {
        Rock,
        Paper,
        Scissors
    }

    public enum RPSOutcome
    {
        Lose,
        Draw,
        Win
    }
}