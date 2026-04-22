public class GameBoard
{
    public const int Empty   = 0;
    public const int Player1 = 1;
    public const int Player2 = 2;

    private readonly int[] _cells = new int[9];

    private static readonly int[][] WinLines =
    {
        new[] { 0, 1, 2 },
        new[] { 3, 4, 5 },
        new[] { 6, 7, 8 },
        new[] { 0, 3, 6 },
        new[] { 1, 4, 7 },
        new[] { 2, 5, 8 },
        new[] { 0, 4, 8 },
        new[] { 2, 4, 6 },
    };

    public int GetCell(int index) => _cells[index];

    public bool PlaceMark(int index, int player)
    {
        if (index < 0 || index >= 9 || _cells[index] != Empty)
            return false;

        _cells[index] = player;
        return true;
    }

    public (int winner, int[] winLine) CheckResult()
    {
        foreach (int[] line in WinLines)
        {
            int a = _cells[line[0]];
            int b = _cells[line[1]];
            int c = _cells[line[2]];

            if (a != Empty && a == b && b == c)
                return (a, line);
        }

        foreach (int cell in _cells)
        {
            if (cell == Empty)
                return (0, null);
        }

        return (-1, null); // draw
    }

    public void Reset()
    {
        for (int i = 0; i < 9; i++)
            _cells[i] = Empty;
    }
}
