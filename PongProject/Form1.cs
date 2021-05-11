using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NonInvasiveKeyboardHookLibrary;

namespace PongProject
{
    public partial class PongForm : Form
    {
        FieldController field;
        Player player;
        Ball ball;
        ControllerPhysics physics;
        Computer computer;
        public Label scoreLabel;

        KeyboardHookManager keyboardHookManager = new KeyboardHookManager();

        delegate void keyboardhook();



        public PongForm()
        {
            InitializeComponent();

            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;

            scoreLabel = new Label();
            scoreLabel.Location = new Point(46 * 10, 4 * 10);
            this.scoreLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            this.scoreLabel.BackColor = Color.Transparent;
            this.Controls.Add(scoreLabel);

            updateFieldTimer.Tick += new EventHandler(update);
            this.KeyDown += new KeyEventHandler(getKey);
            keyboardhook keyHook = new keyboardhook(keyHookEvent);
            keyboardHookManager.RegisterHotkey(NonInvasiveKeyboardHookLibrary.ModifierKeys.Control, 0x31, () =>
            {
                Invoke(keyHook);
            });

            Init();
        }
        private void keyHookEvent()
        {
            this.WindowState = FormWindowState.Normal;
            //keyboardHookManager.Stop();
        }
        private void getKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.WindowState = FormWindowState.Minimized;
                //keyboardHookManager.Start();
            }

            field.field[player.playerPadY, player.playerPadX] = 0;
            for (int i = 1; i < Player.playerPadWidth; i++)
            {
                field.field[player.playerPadY + i, player.playerPadX] = 0;
            }
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (player.playerPadY > 0)
                    {
                        player.playerPadY -= 2;
                    }
                    break;
                case Keys.Down:
                    if (player.playerPadY + Player.playerPadWidth - 1 < FieldController.fieldHeight - 1)
                    {
                        player.playerPadY += 2;
                    }
                    break;
            }
            field.field[player.playerPadY, player.playerPadX] = Player.playerPadLocation;
            for (int i = 1; i < Player.playerPadWidth; i++)
            {
                field.field[player.playerPadY + i, player.playerPadX] = Player.playerPadBox;
            }
        }

        private void update(object sender, EventArgs e)
        {
            if (ball.ballLocationX + ball.ballDirectionX > FieldController.fieldWidth - 2 || ball.ballLocationX + ball.ballDirectionX < 1)
            {
                Init();
            }

            field.field[ball.ballLocationY, ball.ballLocationX] = 0;
            if (!physics.isCollide(field, ball, player))
            {
                ball.ballLocationX += ball.ballDirectionX;
            }
            if (!physics.isCollide(field, ball, player))
            {
                ball.ballLocationY += ball.ballDirectionY;
            }
            scoreLabel.Text = "Score: " + player.score;
            field.field[ball.ballLocationY, ball.ballLocationX] = Ball.ballLocation;
            computer.computerAI(field, ball);
            Invalidate();
        }

        public void Init()
        {
            field = new FieldController();
            player = new Player();
            ball = new Ball();
            physics = new ControllerPhysics();
            computer = new Computer();

            player.score = 0;

            scoreLabel.Text = "Score: " + player.score;

            this.Width = (FieldController.fieldWidth + 2) * 10;
            this.Height = (FieldController.fieldHeight + 5) * 10;

            updateFieldTimer.Interval = 30;


            player.playerPadX = 0;
            player.playerPadY = (FieldController.fieldHeight - 12) / 2;

            ball.ballLocationX = player.playerPadX + 1;
            ball.ballLocationY = player.playerPadY + (Player.playerPadWidth / 2) - 2;

            computer.computerPadX = FieldController.fieldWidth - 1;
            computer.computerPadY = (FieldController.fieldHeight - 12) / 2;


            field.field[player.playerPadY, player.playerPadX] = Player.playerPadLocation;
            for (int i = 1; i < Player.playerPadWidth; i++)
            {
                field.field[player.playerPadY + i, player.playerPadX] = Player.playerPadBox;
            }
            field.field[computer.computerPadY, computer.computerPadX] = Computer.computerPadLocation;
            for (int i = 1; i < Computer.computerPadWidth; i++)
            {
                field.field[computer.computerPadY + i, computer.computerPadX] = Computer.computerPadBox;
            }

            field.field[ball.ballLocationY, ball.ballLocationX] = Ball.ballLocation;
            ball.ballDirectionX = 1;
            ball.ballDirectionY = -1;

            updateFieldTimer.Start();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            field.DrawElements(e.Graphics);
            field.DrawBorders(e.Graphics);
        }
       

        private void PongForm_Load(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "Pong";
            notifyIcon1.BalloonTipText = "Application minimized\nPress 'Ctrl + 1' to maximize";
            notifyIcon1.Text = "Pong";
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //this.Show();
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
            keyboardHookManager.Stop();
        }

        private void PongForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.Hide();
                updateFieldTimer.Stop();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
                keyboardHookManager.Start();
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                updateFieldTimer.Start();
                keyboardHookManager.Stop();
                notifyIcon1.Visible = false;
            }
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
