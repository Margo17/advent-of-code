using System.Text.RegularExpressions;

string corruptedMemory = "";
int overallMultiplicationResult = 0;

Regex patternToMatchInstructionNumbers = new(@"^mul\((\d+),(\d+)\)$");
Regex multiplicationInstructionPattern = new(@"mul\(\d+,\d+\)");

while (Console.ReadLine() is string line)
{
    corruptedMemory += line;
}

MatchCollection potentialInstructions = multiplicationInstructionPattern.Matches(corruptedMemory);

foreach (Match instruction in potentialInstructions)
{
    string potentialInstruction = instruction.Value;

    if (Regex.IsMatch(potentialInstruction, multiplicationInstructionPattern.ToString()))
    {
        Match match = Regex.Match(potentialInstruction, patternToMatchInstructionNumbers.ToString());
        int number1 = int.Parse(match.Groups[1].Value);
        int number2 = int.Parse(match.Groups[2].Value);

        overallMultiplicationResult += number1 * number2;
    }
}

Console.WriteLine(overallMultiplicationResult);
