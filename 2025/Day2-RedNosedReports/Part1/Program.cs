List<string> reports = [];
int safeReports = 0;

while (Console.ReadLine() is string line)
{
    reports.Add(line);
}

foreach (string report in reports)
{
    List<int> levels = report
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();

    if (!IsDecreasing(levels) && !IsIncreasing(levels))
    {
        continue;
    }

    if (!IsStable(levels))
    {
        continue;
    }

    safeReports++;
}

Console.WriteLine(safeReports);

bool IsIncreasing(List<int> levels)
{
    for (int index = 0; index < levels.Count - 1; index++)
    {
        if (levels[index + 1] < levels[index])
        {
            return false;
        }
    }

    return true;
}

bool IsDecreasing(List<int> levels)
{
    for (int index = 0; index < levels.Count - 1; index++)
    {
        if (levels[index + 1] > levels[index])
        {
            return false;
        }
    }

    return true;
}

bool IsStable(List<int> levels)
{
    for (int index = 0; index < levels.Count - 1; index++)
    {
        int difference = Math.Abs(levels[index] - levels[index + 1]);

        if (difference > 3 || difference < 1)
        {
            return false;
        }
    }

    return true;
}
