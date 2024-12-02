var leftList = new List<int>();
var rightList = new List<int>();
var listLength = 0;
var totalDistance = 0;

while (Console.ReadLine() is string line)
{
    string[] listNumbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    leftList.Add(int.Parse(listNumbers[0]));
    rightList.Add(int.Parse(listNumbers[1]));

    listLength++;
}

for (var index = 0; index < listLength; index++)
{
    var leftLowest = leftList.Min();
    var rightLowest = rightList.Min();

    totalDistance += Math.Abs(rightLowest - leftLowest);
	
    leftList.Remove(leftLowest);
    rightList.Remove(rightLowest);
}

Console.WriteLine(totalDistance);
