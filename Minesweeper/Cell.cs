namespace Minesweeper
{
    internal class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int NeighborMineCount { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal class GameBoard
    {
        public Cell[,] Grid { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int TotalMines { get; private set; }
        public int FlagsPlaced { get; private set; }
        public bool GameActive { get; set; }
        public bool FirstClick { get; private set; } = true;

        public GameBoard(int width, int height, int mines)
        {
            Width = width;
            Height = height;
            TotalMines = mines;
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            Grid = new Cell[Width, Height];
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    Grid[x, y] = new Cell(x, y);
            FlagsPlaced = 0;
            GameActive = true;
        }

        public void PlaceMines(int firstClickX, int firstClickY)
        {
            var random = new Random();
            int minesPlaced = 0;

            while (minesPlaced < TotalMines)
            {
                int x = random.Next(Width);
                int y = random.Next(Height);

                // Гарантируем, что первая ячейка и её соседи без мин
                if (!Grid[x, y].IsMine &&
                    Math.Abs(x - firstClickX) > 1 &&
                    Math.Abs(y - firstClickY) > 1)
                {
                    Grid[x, y].IsMine = true;
                    minesPlaced++;
                }
            }
            CalculateNeighborCounts();
            FirstClick = false;
        }

        public void CalculateNeighborCounts()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (!Grid[x, y].IsMine)
                    {
                        Grid[x, y].NeighborMineCount = CountMinesAround(x, y);
                    }
                }
            }
        }

        private int CountMinesAround(int x, int y)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nx = x + i, ny = y + j;
                    if (nx >= 0 && nx < Width && ny >= 0 && ny < Height)
                        if (Grid[nx, ny].IsMine) count++;
                }
            }
            return count;
        }

        public void RevealCell(int x, int y)
        {
            if (!GameActive || Grid[x, y].IsRevealed || Grid[x, y].IsFlagged)
                return;

            if (FirstClick)
            {
                PlaceMines(x, y);
                FirstClick = false;
            }

            Grid[x, y].IsRevealed = true;

            if (Grid[x, y].IsMine)
            {
                GameActive = false;
                return;
            }

            // Flood Fill для пустых ячеек
            if (Grid[x, y].NeighborMineCount == 0)
            {
                var stack = new Stack<Cell>();
                stack.Push(Grid[x, y]);

                while (stack.Count > 0)
                {
                    var cell = stack.Pop();
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int nx = cell.X + i, ny = cell.Y + j;
                            if (nx >= 0 && nx < Width && ny >= 0 && ny < Height)
                            {
                                var neighbor = Grid[nx, ny];
                                if (!neighbor.IsRevealed && !neighbor.IsFlagged)
                                {
                                    neighbor.IsRevealed = true;
                                    if (neighbor.NeighborMineCount == 0)
                                        stack.Push(neighbor);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ToggleFlag(int x, int y)
        {
            if (!GameActive || Grid[x, y].IsRevealed) return;

            Grid[x, y].IsFlagged = !Grid[x, y].IsFlagged;
            FlagsPlaced += Grid[x, y].IsFlagged ? 1 : -1;
        }
    }
}
