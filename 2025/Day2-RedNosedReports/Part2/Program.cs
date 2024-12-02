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

    if (IsSafe(levels) || CanBeMadeSafe(levels))
    {
        safeReports++;
    }
}

Console.WriteLine(safeReports);

static bool IsSafe(List<int> levels)
{
    return IsIncreasing(levels) || IsDecreasing(levels);
}

static bool CanBeMadeSafe(List<int> levels)
{
    for (int i = 0; i < levels.Count; i++)
    {
        List<int> modified = new(levels);
        modified.RemoveAt(i);

        if (IsSafe(modified))
        {
            return true;
        }
    }

    return false;
}

static bool IsIncreasing(List<int> levels)
{
    for (int i = 0; i < levels.Count - 1; i++)
    {
        int difference = levels[i + 1] - levels[i];
        if (difference < 1 || difference > 3)
        {
            return false;
        }
    }

    return true;
}

static bool IsDecreasing(List<int> levels)
{
    for (int i = 0; i < levels.Count - 1; i++)
    {
        int difference = levels[i] - levels[i + 1];
        if (difference < 1 || difference > 3)
        {
            return false;
        }
    }

    return true;
}
