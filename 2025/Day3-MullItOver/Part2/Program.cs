using System.Text.RegularExpressions;

string corruptedMemory = "";
int overallMultiplicationResult = 0;
bool isMultiplicationEnabled = true;

Regex patternToMatchInstructionNumbers = new(@"^mul\((\d+),(\d+)\)$");
Regex multiplicationsAndConditionalsPattern = new(@"(mul\(\d+,\d+\)|do\(\)|don't\(\))", RegexOptions.IgnoreCase);
Regex doConditionalPattern = new(@"do\(\)", RegexOptions.IgnoreCase);
Regex dontConditionalPattern = new(@"don't\(\)", RegexOptions.IgnoreCase);

while (Console.ReadLine() is string line)
{
    corruptedMemory += line;
}

MatchCollection allInstructions = multiplicationsAndConditionalsPattern.Matches(corruptedMemory);

foreach (Match instruction in allInstructions)
{
    string potentialInstruction = instruction.Value;

    if (Regex.IsMatch(potentialInstruction, doConditionalPattern.ToString()))
    {
        isMultiplicationEnabled = true;
    }
    else if (Regex.IsMatch(potentialInstruction, dontConditionalPattern.ToString()))
    {
        isMultiplicationEnabled = false;
    }

    if (Regex.IsMatch(potentialInstruction, patternToMatchInstructionNumbers.ToString()) &&
        isMultiplicationEnabled)
    {
        Match match = Regex.Match(potentialInstruction, patternToMatchInstructionNumbers.ToString());
        int number1 = int.Parse(match.Groups[1].Value);
        int number2 = int.Parse(match.Groups[2].Value);

        overallMultiplicationResult += number1 * number2;
    }
}

Console.WriteLine(overallMultiplicationResult);
