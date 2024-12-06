// Directions: Diagonals (dx, dy)
int[,] directions =
{
    { 1, 1 },   // Down-right
    { -1, -1 }, // Up-left
    { 1, -1 },  // Down-left
    { -1, 1 }   // Up-right
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

// Create grid
char[,] grid = new char[height, width];
for (int r = 0; r < height; r++)
{
    for (int c = 0; c < width; c++)
    {
        grid[r, c] = lines[r][c];
    }
}

string targetWord = "MAS";
int xMasCount = 0;

// Traverse grid to find X-MAS patterns
for (int r = 0; r < height; r++)
{
    for (int c = 0; c < width; c++)
    {
        // Check all diagonal pairs that could form an "X"
        for (int d = 0; d < directions.GetLength(0); d += 2)
        {
            int dx1 = directions[d, 0];
            int dy1 = directions[d, 1];
            int dx2 = directions[d + 1, 0];
            int dy2 = directions[d + 1, 1];

            // Check if both diagonals form "MAS"
            if (IsWordInDirection(grid, r, c, dx1, dy1, targetWord) &&
                IsWordInDirection(grid, r, c, dx2, dy2, targetWord))
            {
                xMasCount++;
            }
        }
    }
}

Console.WriteLine($"The X-MAS pattern appears {xMasCount} times.");

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

        // Check boundaries and character match
        if (x < 0 || x >= rows || y < 0 || y >= cols || grid[x, y] != word[i])
        {
            return false;
        }
    }

    return true;
}
