// Input: Rules and Updates
List<string> rulesInput = [];
List<string> updatesInput = [];

bool isRuleSection = true;
int sumOfMiddlePagesValid = 0;
int sumOfMiddlePagesCorrected = 0;

while (Console.ReadLine() is string line)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        isRuleSection = false;
        continue;
    }

    if (isRuleSection)
    {
        rulesInput.Add(line);
    }
    else
    {
        updatesInput.Add(line);
    }
}

Dictionary<int, HashSet<int>> rules = ParseRules(rulesInput);

foreach (var update in updatesInput)
{
    List<int> pages = update.Split(',').Select(int.Parse).ToList();

    if (IsValidOrder(pages, rules))
    {
        // Sum middle pages for valid updates
        int middlePage = pages[pages.Count / 2];
        sumOfMiddlePagesValid += middlePage;
    }
    else
    {
        // Correct the order for invalid updates
        List<int> correctedPages = CorrectOrder(pages, rules);
        int middlePageCorrected = correctedPages[correctedPages.Count / 2];
        sumOfMiddlePagesCorrected += middlePageCorrected;
    }
}

Console.WriteLine($"Sum of middle pages (valid updates): {sumOfMiddlePagesValid}");
Console.WriteLine($"Sum of middle pages (corrected updates): {sumOfMiddlePagesCorrected}");

static Dictionary<int, HashSet<int>> ParseRules(List<string> rulesInput)
{
    Dictionary<int, HashSet<int>> rules = [];

    foreach (var rule in rulesInput)
    {
        var parts = rule.Split('|');
        int before = int.Parse(parts[0]);
        int after = int.Parse(parts[1]);

        if (!rules.ContainsKey(before))
        {
            rules[before] = [];
        }

        rules[before].Add(after);
    }

    return rules;
}

static bool IsValidOrder(List<int> pages, Dictionary<int, HashSet<int>> rules)
{
    Dictionary<int, int> position = pages.Select((page, index) => new { page, index })
        .ToDictionary(x => x.page, x => x.index);

    foreach (var rule in rules)
    {
        int before = rule.Key;
        foreach (var after in rule.Value)
        {
            if (position.ContainsKey(before) && position.ContainsKey(after))
            {
                if (position[before] >= position[after])
                {
                    return false;
                }
            }
        }
    }

    return true;
}

static List<int> CorrectOrder(List<int> pages, Dictionary<int, HashSet<int>> rules)
{
    // Topological sorting using Kahn's algorithm
    Dictionary<int, int> inDegree = [];
    Dictionary<int, List<int>> adjacencyList = [];

    foreach (var page in pages)
    {
        inDegree[page] = 0;
        adjacencyList[page] = [];
    }

    foreach (var rule in rules)
    {
        int before = rule.Key;
        foreach (var after in rule.Value)
        {
            if (pages.Contains(before) && pages.Contains(after))
            {
                adjacencyList[before].Add(after);
                inDegree[after]++;
            }
        }
    }

    Queue<int> queue = new(inDegree.Where(kvp => kvp.Value == 0).Select(kvp => kvp.Key));
    List<int> sortedPages = [];

    while (queue.Count > 0)
    {
        int current = queue.Dequeue();
        sortedPages.Add(current);

        foreach (var neighbor in adjacencyList[current])
        {
            inDegree[neighbor]--;
            if (inDegree[neighbor] == 0)
            {
                queue.Enqueue(neighbor);
            }
        }
    }

    return sortedPages;
}
