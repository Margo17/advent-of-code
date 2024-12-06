// Directions: (dx, dy)
int[,] directions =
{
    { 0, 1 },   // Right
    { 0, -1 },  // Left
    { 1, 0 },   // Down
    { -1, 0 },  // Up
    { 1, 1 },   // Down-right
    { 1, -1 },  // Down-left
    { -1, 1 },  // Up-right
    { -1, -1 }  // Up-left
};

int height = 0;
int width = 0;
List<string> lines = [];

while (Console.ReadLine() is string line)
{
    width = line.Length;
    lines.Add(line);
    height++;
}

string targetWord = "XMAS";
int wordCount = 0;

char[,] grid = new char[height, width];
for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        grid[i, j] = lines[i][j];
    }
}

for (int r = 0; r < height; r++)
{
    for (int c = 0; c < width; c++)
    {
        for (int d = 0; d < directions.GetLength(0); d++)
        {
            int dx = directions[d, 0];
            int dy = directions[d, 1];

            if (IsWordInDirection(grid, r, c, dx, dy, targetWord))
            {
                wordCount++;
            }
        }
    }
}

Console.WriteLine($"The word '{targetWord}' appears {wordCount} times.");

static bool IsWordInDirection(
    char[,] grid,
    int startX,
    int startY,
    int dx,
    int dy,
    string word)
{
    int rows = grid.GetLength(0);
    int cols = grid.GetLength(1);
    int wordLength = word.Length;

    for (int i = 0; i < wordLength; i++)
    {
        int x = startX + i * dx;
        int y = startY + i * dy;

        if (x < 0 || x >= rows || y < 0 || y >= cols || grid[x, y] != word[i])
        {
            return false;
        }
    }

    return true;
}

