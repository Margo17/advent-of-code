var leftList = new List<int>();
var rightList = new List<int>();
var similarityScore = 0;

while (Console.ReadLine() is string line)
{
    string[] listNumbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    leftList.Add(int.Parse(listNumbers[0]));
    rightList.Add(int.Parse(listNumbers[1]));
}

var leftFirst = leftList.First();

foreach (var number in leftList)
{
    var numberAppearancesInRightList = rightList
        .Where(n => n.Equals(number))
        .Count();
    similarityScore += number * numberAppearancesInRightList;
}

leftList.RemoveAt(0);

Console.WriteLine(similarityScore);
