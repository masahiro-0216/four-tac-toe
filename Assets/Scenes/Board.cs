using UnityEngine; // ← 必要！

public class Board
{
    private int[,,] grid = new int[4, 4, 4]; // [x, y, z]

    public int GetAvailableY(int x, int z)
    {
        for (int y = 0; y < 4; y++)
        {
            if (grid[x, y, z] == 0)
                return y;
        }
        return -1;
    }

    public void SetCell(int x, int y, int z, int player)
    {
        grid[x, y, z] = player;
    }

    public bool CheckWin(int x, int y, int z, int player)
    {
        Vector3Int[] directions = new Vector3Int[]
        {
            new(1,0,0), new(0,1,0), new(0,0,1),
            new(1,1,0), new(1,0,1), new(0,1,1),
            new(1,1,1), new(-1,1,1), new(1,-1,1), new(1,1,-1)
        };

        foreach (var dir in directions)
        {
            int count = 1;
            count += CountInDirection(x, y, z, dir, player);
            count += CountInDirection(x, y, z, -dir, player);
            if (count >= 4) return true;
        }

        return false;
    }

    private int CountInDirection(int x, int y, int z, Vector3Int dir, int player)
    {
        int count = 0;
        for (int i = 1; i < 4; i++)
        {
            int nx = x + dir.x * i;
            int ny = y + dir.y * i;
            int nz = z + dir.z * i;
            if (IsInBounds(nx, ny, nz) && grid[nx, ny, nz] == player)
                count++;
            else break;
        }
        return count;
    }

    private bool IsInBounds(int x, int y, int z)
    {
        return x >= 0 && x < 4 && y >= 0 && y < 4 && z >= 0 && z < 4;
    }
}
