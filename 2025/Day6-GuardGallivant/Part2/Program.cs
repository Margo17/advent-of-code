List<string> inputMap = [];
while (Console.ReadLine() is string line)
{
    inputMap.Add(line);
}

int rows = inputMap.Count;
int cols = inputMap[0].Length;

char[,] grid = new char[rows, cols];
int guardRow = 0, guardCol = 0;
char direction = '^';

// Parse the input and locate the guard
for (int r = 0; r < rows; r++)
{
    for (int c = 0; c < cols; c++)
    {
        grid[r, c] = inputMap[r][c];
        if ("^>v<".Contains(grid[r, c]))
        {
            guardRow = r;
            guardCol = c;
            direction = grid[r, c];
            grid[r, c] = '.'; // Clear guard's initial position
        }
    }
}

// Define movements for each direction
Dictionary<char, (int dr, int dc)> moves = new()
{
    { '^', (-1, 0) },
    { '>', (0, 1) },
    { 'v', (1, 0) },
    { '<', (0, -1) }
};

// Turn right mapping
Dictionary<char, char> turnRight = new()
{
    { '^', '>' },
    { '>', 'v' },
    { 'v', '<' },
    { '<', '^' }
};

// Simulate the guard's path
HashSet<(int, int)> visited = SimulatePath(
    grid, guardRow,guardCol, direction, moves, turnRight, rows, cols);

// Find valid obstruction positions
int obstructionCount = 0;

for (int r = 0; r < rows; r++)
{
    for (int c = 0; c < cols; c++)
    {
        if (grid[r, c] == '.' && (r != guardRow || c != guardCol))
        {
            // Place an obstruction temporarily
            grid[r, c] = '#';

            // Check if the guard gets stuck in a loop
            if (IsGuardStuck(grid, guardRow, guardCol, direction, moves, turnRight, rows, cols))
            {
                obstructionCount++;
            }

            // Remove the obstruction
            grid[r, c] = '.';
        }
    }
}

Console.WriteLine($"Number of valid obstruction positions: {obstructionCount}");

static HashSet<(int, int)> SimulatePath(
    char[,] grid,
    int guardRow,
    int guardCol,
    char direction,
    Dictionary<char, (int dr, int dc)> moves,
    Dictionary<char, char> turnRight, int rows, int cols)
{
    HashSet<(int, int)> visited = [];
    visited.Add((guardRow, guardCol));

    while (true)
    {
        int nextRow = guardRow + moves[direction].dr;
        int nextCol = guardCol + moves[direction].dc;

        // Check if the guard is about to leave the grid
        if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols)
        {
            break;
        }

        // Check if there is an obstacle
        if (grid[nextRow, nextCol] == '#')
        {
            // Turn right if there's an obstacle
            direction = turnRight[direction];
        }
        else
        {
            // Move forward
            guardRow = nextRow;
            guardCol = nextCol;
            if (visited.Contains((guardRow, guardCol)))
            {
                // Detected a loop
                break;
            }
            visited.Add((guardRow, guardCol));
        }
    }

    return visited;
}

static bool IsGuardStuck(char[,] grid,
    int guardRow,
    int guardCol,
    char direction,
    Dictionary<char, (int dr, int dc)> moves,
    Dictionary<char, char> turnRight, int rows, int cols)
{
    HashSet<(int, int, char)> seenStates = [];
    while (true)
    {
        int nextRow = guardRow + moves[direction].dr;
        int nextCol = guardCol + moves[direction].dc;

        // Check if the guard is about to leave the grid
        if (nextRow < 0 ||
            nextRow >= rows ||
            nextCol < 0 ||
            nextCol >= cols)
        {
            return false; // Not stuck, leaves the grid
        }

        // Check if there is an obstacle
        if (grid[nextRow, nextCol] == '#')
        {
            // Turn right if there's an obstacle
            direction = turnRight[direction];
        }
        else
        {
            // Move forward
            guardRow = nextRow;
            guardCol = nextCol;
        }

        // Check if we've seen this state before
        var currentState = (guardRow, guardCol, direction);
        if (seenStates.Contains(currentState))
        {
            return true; // Detected a loop
        }
        seenStates.Add(currentState);
    }
}
