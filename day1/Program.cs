var s = new Solution();
s.puzzle2();

public class Solution
{
    public void puzzle1()
    {
        int elfIndex = 1;
        int maxElfCaloriesIndex = 0;
        int maxElfCalories = 0;
        using var fs = new FileStream("input-1.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            var currentMaxCalories = 0;
            var line = file.ReadLine();
            while (line != null)
            {
                if (line == "")
                {
                    if (currentMaxCalories > maxElfCalories)
                    {
                        maxElfCalories = currentMaxCalories;
                        maxElfCaloriesIndex = elfIndex;
                    }
                    elfIndex++;
                    currentMaxCalories = 0;
                }
                else
                {
                    var calories = int.Parse(line);
                    currentMaxCalories += calories;
                }
                line = file.ReadLine();
            }

            if (currentMaxCalories > maxElfCalories)
            {
                maxElfCalories = currentMaxCalories;
                maxElfCaloriesIndex = elfIndex;
            }

            Console.WriteLine($"result day1-1: elf {maxElfCaloriesIndex}, calories {maxElfCalories}");
        }
    }

    public void puzzle2()
    {
        int[] maxElfsCalories = new int[3];
        using var fs = new FileStream("input-1.txt", FileMode.Open, FileAccess.Read);
        using var file = new StreamReader(fs);
        {
            var currentMaxCalories = 0;
            var line = file.ReadLine();
            while (line != null)
            {
                if (line == "")
                {
                    CheckScores();
                    currentMaxCalories = 0;
                }
                else
                {
                    var calories = int.Parse(line);
                    currentMaxCalories += calories;
                }
                line = file.ReadLine();
            }
            CheckScores();

            void CheckScores()
            {
                if (currentMaxCalories > maxElfsCalories[0])
                {
                    maxElfsCalories[2] = maxElfsCalories[1];
                    maxElfsCalories[1] = maxElfsCalories[0];
                    maxElfsCalories[0] = currentMaxCalories;
                }
                else if (currentMaxCalories > maxElfsCalories[1])
                {
                    maxElfsCalories[2] = maxElfsCalories[1];
                    maxElfsCalories[1] = currentMaxCalories;
                }
                else if (currentMaxCalories > maxElfsCalories[2])
                {
                    maxElfsCalories[2] = currentMaxCalories;
                }
            }

            Console.WriteLine($"result day1-2: total calories {maxElfsCalories[0] + maxElfsCalories[1] + maxElfsCalories[2]}");
        }
    }
}