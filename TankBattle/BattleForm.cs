using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class BattleForm : Form
    {
        private Color landscapeColour;
        private Random rng = new Random();
        private Image backgroundImage = null;
        private int levelWidth = 160;
        private int levelHeight = 120;
        private Game currentGame;

        private BufferedGraphics backgroundGraphics;
        private BufferedGraphics gameplayGraphics;

        private string[] imageFilenames = { "Images\\background1.jpg",
                            "Images\\background2.jpg",
                            "Images\\background3.jpg",
                            "Images\\background4.jpg"};                 //Image Array

        private Color[] landscapeColours = { Color.FromArgb(255, 0, 0, 0),
                             Color.FromArgb(255, 73, 58, 47),
                             Color.FromArgb(255, 148, 116, 93),
                             Color.FromArgb(255, 133, 119, 109) };      //LandscapeColours


        public BattleForm(Game game)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            currentGame = game;
            int Rand = rng.Next(0, 3);
            landscapeColour = landscapeColours[Rand];
            backgroundImage = Image.FromFile(imageFilenames[Rand]);

            InitializeComponent();

            backgroundGraphics = InitDisplayBuffer();
            gameplayGraphics = InitDisplayBuffer();

            DrawBackground();
            DrawGameplay();
            NewTurn();
        }

        // From https://stackoverflow.com/questions/13999781/tearing-in-my-animation-on-winforms-c-sharp
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        public void EnableControlPanel()
        {
            /// <summary>
            /// 
            /// This method is used to enable the control panel so the (human) controller 
            /// can control their tank. Called from Human.NewTurn().
            /// 
            /// </summary>

            controlPanel.Enabled = true;
        }

        public void SetAimingAngle(float angle)
        {
            /// <summary>
            /// 
            /// This method alters the value of the UpDownNumeric used to control the angle, 
            /// setting it to the provided value.
            /// 
            /// </summary>
            
            AngleNo.Value = (decimal)angle;
        }

        public void SetPower(int power)
        {
            /// <summary>
            /// 
            /// This method alters the value of the TrackBar used to control the power level, setting it to the provided value.
            /// 
            /// </summary>

            PowerBar.Value = power;
        }
        public void SelectWeapon(int weapon)
        {
            /// <summary>
            /// 
            /// This method changes the selected item in the ComboBox, setting it to the provided value.
            /// 
            /// </summary>

            WeaponSelect.SelectedItem = weapon;
        }

        public void Launch()
        {
            /// <summary>
            /// 
            /// This method is called both externally (by the computer player when it wants to fire) 
            /// and by the 'Fire!' button when it is clicked. It does the following:
            /// 
            /// </summary>

            ControlledTank _tank = currentGame.GetPlayerTank();
            _tank.Launch();
            controlPanel.Enabled = false;
            timer1.Enabled = true;
        }

        private void DrawBackground()
        {
            Graphics graphics = backgroundGraphics.Graphics;
            Image background = backgroundImage;
            graphics.DrawImage(backgroundImage, new Rectangle(0, 0, displayPanel.Width, displayPanel.Height));

            Battlefield battlefield = currentGame.GetArena();
            Brush brush = new SolidBrush(landscapeColour);

            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    if (battlefield.TerrainAt(x, y))
                    {
                        int drawX1 = displayPanel.Width * x / levelWidth;
                        int drawY1 = displayPanel.Height * y / levelHeight;
                        int drawX2 = displayPanel.Width * (x + 1) / levelWidth;
                        int drawY2 = displayPanel.Height * (y + 1) / levelHeight;
                        graphics.FillRectangle(brush, drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1);
                    }
                }
            }
        }

        public BufferedGraphics InitDisplayBuffer()
        {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            Graphics graphics = displayPanel.CreateGraphics();
            Rectangle dimensions = new Rectangle(0, 0, displayPanel.Width, displayPanel.Height);
            BufferedGraphics bufferedGraphics = context.Allocate(graphics, dimensions);
            return bufferedGraphics;
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = displayPanel.CreateGraphics();
            gameplayGraphics.Render(graphics);
        }

        private void DrawGameplay()
        {
            /// <summary>
            /// 
            /// This newly-created method is used to draw the gameplay elements of the screen (that is, everything but the terrain). 
            /// 
            /// </summary>

            backgroundGraphics.Render(gameplayGraphics.Graphics);
            currentGame.DrawTanks(gameplayGraphics.Graphics, displayPanel.Size);
            currentGame.RenderEffects(gameplayGraphics.Graphics, displayPanel.Size);
        }

        private void NewTurn()
        {
            /// <summary>
            /// 
            /// This newly-created method is used to update form elements to reflect who the current player is. 
            /// 
            /// </summary>

            ControlledTank _tank = currentGame.GetPlayerTank();
            Tank _type = _tank.GetTank();
            TankController _player = _tank.GetPlayerById();

            float angle = _tank.GetTankAngle();
            int power = _tank.GetTankPower();

            Text = "Round" + currentGame.GetCurrentRound() + "of" + currentGame.GetCurrentRound();
            BackColor = _player.TankColour();
            Player.Text = _player.Name();
            SetAimingAngle(angle);
            SetPower(power);

            if (currentGame.Wind() >= 0)
            {
                WindSpeed.Text = currentGame.Wind() + "E";
            }
            else
            {
                WindSpeed.Text = currentGame.Wind() + "W";
            }
            WeaponSelect.Items.Clear();

            string[] avaliableWeapons = _type.ListWeapons();

            for (int i = 0; i < avaliableWeapons.Length; i++)
            {
                WeaponSelect.Items.Add(avaliableWeapons[i]);
            }
            SelectWeapon(WeaponSelect.SelectedIndex);
            _player.NewTurn(this, currentGame);
        }

        //GUI FORMS
        private void Player_Click(object sender, EventArgs e)
        {
            
        }

        private void Wind_Click(object sender, EventArgs e)
        {
            //--
        }

        private void WindSpeed_Click(object sender, EventArgs e)
        {

        }

        private void Weapon_Click(object sender, EventArgs e)
        {
            //--
        }

        private void WeaponSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

            ControlledTank tank = currentGame.GetPlayerTank();
            int weapon = WeaponSelect.SelectedIndex;
            tank.SelectWeapon(weapon);

        }

        private void Angle_Click(object sender, EventArgs e)
        {
            //--
        }

        private void AngleNo_ValueChanged(object sender, EventArgs e)
        {

            ControlledTank tank = currentGame.GetPlayerTank();
            float angle = (float)AngleNo.Value;

            tank.SetAimingAngle(angle);
            DrawGameplay();
            displayPanel.Invalidate();
        }

        private void PowerBar_Scroll(object sender, EventArgs e)
        {

            ControlledTank tank = currentGame.GetPlayerTank();
            int power = PowerBar.Value;
            tank.SetPower(power);
        }

        private void PowerNo_Click(object sender, EventArgs e)
        {

        }

        private void Fire_Click(object sender, EventArgs e)
        {
            Launch();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool ticking = currentGame.WeaponEffectStep();
            if (!ticking)
            {
                currentGame.ProcessGravity();
                DrawBackground();
                DrawGameplay();
                displayPanel.Invalidate();
                if (currentGame.ProcessGravity() == false)
                {
                    timer1.Enabled = false;
                    currentGame.EndTurn();
                    if (currentGame.EndTurn() == true)
                    {
                        NewTurn();
                    }
                    else
                    {
                        Dispose();
                        currentGame.NextRound();
                    }
                }
            }
            else
            {
                DrawGameplay();
                displayPanel.Invalidate();
            }
        }

        private void controlPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
