using System;
using System.Linq;
using System.Windows.Forms;

namespace krestikiinoliki
{
    public partial class Form1 : Form
    {
        // Переменные
        private bool playerTurn = true; // true - игрок, false - AI
        private int turnCount = 0;      // Счетчик ходов
        private Button[] buttons;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            foreach (Button btn in buttons)
            {
                btn.Click += new EventHandler(PlayerMove);
                btn.Text = "";
                btn.Enabled = true;
            }

            lblTurn.Text = "Ход: Игрок";
            turnCount = 0;
            playerTurn = true;
        }

        private void PlayerMove(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Enabled)
            {
                btn.Text = "X";
                btn.Enabled = false;
                turnCount++;

                if (CheckWin())
                {
                    EndGame("Игрок победил!");
                    return;
                }

                if (turnCount >= 9)
                {
                    EndGame("Ничья!");
                    return;
                }

                playerTurn = false;
                lblTurn.Text = "Ход: Бот";
                BotMove();
            }
        }

        private void BotMove()
        {
            // Простая логика для ИИ: выбирает первую доступную кнопку
            Button bestMove = buttons.FirstOrDefault(b => b.Enabled);
            if (bestMove != null)
            {
                bestMove.Text = "O";
                bestMove.Enabled = false;
                turnCount++;

                if (CheckWin())
                {
                    EndGame("Бот победил!");
                    return;
                }

                if (turnCount >= 9)
                {
                    EndGame("Ничья!");
                    return;
                }

                playerTurn = true;
                lblTurn.Text = "Ход: Игрок";
            }
        }

        private bool CheckWin()
        {
            // Победные комбинации
            int[,] winPatterns = new int[,]
            {
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, // Горизонтали
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, // Вертикали
                { 0, 4, 8 }, { 2, 4, 6 }              // Диагонали
            };

            for (int i = 0; i < winPatterns.GetLength(0); i++)
            {
                int a = winPatterns[i, 0], b = winPatterns[i, 1], c = winPatterns[i, 2];

                if (buttons[a].Text != "" &&
                    buttons[a].Text == buttons[b].Text &&
                    buttons[b].Text == buttons[c].Text)
                {
                    return true;
                }
            }

            return false;
        }

        private void EndGame(string message)
        {
            MessageBox.Show(message, "Игра окончена");
            foreach (Button btn in buttons)
            {
                btn.Enabled = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }
    }
}
