using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    /// <summary>
    /// 
    /// This is a simple title screen / main menu form, and the first thing you will see upon loading the game. 
    /// 
    /// </summary>
    
    public partial class IntroForm : Form
    {
        public IntroForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //--
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            Game game = new Game(2, 1);
            TankController player1 = new Human("Player 1", Tank.GetTank(1), Game.PlayerColour(1));
            TankController player2 = new Human("Player 2", Tank.GetTank(1), Game.PlayerColour(2));
            game.SetPlayer(1, player1);
            game.SetPlayer(2, player2);
            game.NewGame();
        }
    }
}
