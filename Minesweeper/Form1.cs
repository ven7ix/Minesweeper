namespace Minesweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeGame("Новичок");
            SetupTimer();
        }

        private GameBoard board;
        private System.Windows.Forms.Timer gameTimer;
        private int elapsedSeconds;
        private readonly Dictionary<string, (int w, int h, int m)> difficulties = new()
        {
            ["Новичок"] = (9, 9, 10),
            ["Любитель"] = (16, 16, 40),
            ["Профессионал"] = (30, 16, 99)
        };

        private void InitializeGame(string difficulty)
        {
            var (w, h, m) = difficulties[difficulty];
            board = new GameBoard(w, h, m);
            elapsedSeconds = 0;

            if (difficulty == "Новичок")
            {
                MinimumSize = new Size(15 * 20, 15 * 20);
                Size = MinimumSize;
                MaximumSize = MinimumSize;
            }
            else if (difficulty == "Любитель")
            {
                MinimumSize = new Size(20 * 20, 22 * 20);
                Size = MinimumSize;
                MaximumSize = MinimumSize;
            }
            else if (difficulty == "Профессионал")
            {
                MinimumSize = new Size(34 * 20, 22 * 20);
                Size = MinimumSize;
                MaximumSize = MinimumSize;
            }


            UpdateDisplay();
            DrawField();
        }

        private void SetupTimer()
        {
            gameTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            gameTimer.Tick += (s, e) =>
            {
                elapsedSeconds++;
                lblTimer.Text = $"Время: {elapsedSeconds}s";
            };
        }

        private void UpdateDisplay()
        {
            lblMines.Text = $"Мины: {board.TotalMines - board.FlagsPlaced}";
            lblTimer.Text = $"Время: {elapsedSeconds}s";
        }

        private void DrawField()
        {
            var bmp = new Bitmap(picField.Width, picField.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                for (int x = 0; x < board.Width; x++)
                {
                    for (int y = 0; y < board.Height; y++)
                    {
                        DrawCell(g, x, y);
                    }
                }
            }
            picField.Image = bmp;
        }

        private void DrawCell(Graphics g, int x, int y)
        {
            var cell = board.Grid[x, y];
            var rect = new Rectangle(x * 20, y * 20, 20, 20);

            if (cell.IsRevealed)
            {
                g.FillRectangle(Brushes.LightGray, rect);
                if (cell.IsMine)
                {
                    g.FillEllipse(Brushes.Black, rect);
                }
                else if (cell.NeighborMineCount > 0)
                {
                    var color = cell.NeighborMineCount switch
                    {
                        1 => Color.Blue,
                        2 => Color.Green,
                        3 => Color.Red,
                        4 => Color.DarkBlue,
                        5 => Color.Brown,
                        6 => Color.Teal,
                        7 => Color.Black,
                        8 => Color.Gray,
                        _ => Color.Black
                    };
                    using var brush = new SolidBrush(color);
                    g.DrawString(cell.NeighborMineCount.ToString(),
                        new Font("Arial", 10), brush, rect.Location);
                }
            }
            else
            {
                g.FillRectangle(Brushes.Gray, rect);
                if (cell.IsFlagged)
                {
                    g.DrawString("⚑", new Font("Arial", 12), Brushes.Red, rect.Location);
                }
            }
            g.DrawRectangle(Pens.DarkGray, rect);
        }

        private void PicField_MouseClick(object sender, MouseEventArgs e)
        {
            if (!board.GameActive) return;

            int x = e.X / 20;
            int y = e.Y / 20;

            if (x >= 0 && x < board.Width && y >= 0 && y < board.Height)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (board.FirstClick) gameTimer.Start();
                    board.RevealCell(x, y);
                    UpdateDisplay();
                    DrawField();
                    CheckGameEnd();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    board.ToggleFlag(x, y);
                    UpdateDisplay();
                    DrawField();
                }
            }
        }

        private void CheckGameEnd()
        {
            if (!board.GameActive)
            {
                gameTimer.Stop();
                if (MessageBox.Show("Игра окончена! Вы подорвались на мине.") == DialogResult.OK)
                {
                    InitializeGame(comboDifficulty?.SelectedItem?.ToString() ?? "Новичок");
                }
            }
            else if (IsVictory())
            {
                gameTimer.Stop();
                if (MessageBox.Show($"Поздравляем! Вы выиграли за {elapsedSeconds} секунд.") == DialogResult.OK)
                {
                    InitializeGame(comboDifficulty?.SelectedItem?.ToString() ?? "Новичок");
                }
            }
        }

        private bool IsVictory()
        {
            for (int x = 0; x < board.Width; x++)
                for (int y = 0; y < board.Height; y++)
                    if (!board.Grid[x, y].IsRevealed && !board.Grid[x, y].IsMine)
                        return false;
            return true;
        }

        private void ComboDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            gameTimer.Stop();
            InitializeGame(comboDifficulty?.SelectedItem?.ToString() ?? "Новичок");
        }

        private void MenuStrip_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            InitializeGame(comboDifficulty.SelectedItem?.ToString() ?? "Новичок");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboDifficulty.DataSource = difficulties.Keys.ToList();
        }
    }
}
