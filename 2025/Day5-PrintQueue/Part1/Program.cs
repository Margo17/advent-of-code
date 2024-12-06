// Input: Rules and Updates
List<string> rulesInput = [];
List<string> updatesInput = [];

bool isRuleSection = true;
int sumOfMiddlePages = 0;

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
    var pages = update.Split(',').Select(int.Parse).ToList();

    if (IsValidOrder(pages, rules))
    {
        int middlePage = pages[pages.Count / 2];
        sumOfMiddlePages += middlePage;
    }
}

Console.WriteLine($"Sum of middle pages: {sumOfMiddlePages}");

static Dictionary<int, HashSet<int>> ParseRules(List<string> rulesInput)
{
    var rules = new Dictionary<int, HashSet<int>>();

    foreach (var rule in rulesInput)
    {
        var parts = rule.Split('|');
        int before = int.Parse(parts[0]);
        int after = int.Parse(parts[1]);

        if (!rules.ContainsKey(before))
        {
            rules[before] = new HashSet<int>();
        }

        rules[before].Add(after);
    }

    return rules;
}

static bool IsValidOrder(List<int> pages, Dictionary<int, HashSet<int>> rules)
{
    var position = pages.Select((page, index) => new { page, index })
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

