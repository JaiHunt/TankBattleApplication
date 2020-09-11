using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class Human : TankController
    {
        public Human(string name, Tank tank, Color colour) : base(name, tank, colour)
        {
            /// <summary>
            /// 
            /// Constructor for the Human class. Handled elsewhere, returns nothing
            /// 
            /// </summary>

            // --
        }

        public override void BeginRound()
        {
            /// <summary>
            /// 
            /// This method is called each round, but it doesn't need to do anything here
            /// 
            /// </summary>

            // --
        }

        public override void NewTurn(BattleForm gameplayForm, Game currentGame)
        {
            /// <summary>
            /// 
            /// This method is called when it's this player's turn. Because this player is human-controlled, this method calls EnableControlPanel() on the BattleForm passed to this method.
            /// 
            /// </summary>

            gameplayForm.EnableControlPanel();
        }

        public override void HitPos(float x, float y)
        {
            /// <summary>
            /// 
            /// This method is called each time a shot fired by this player lands, but it doesn't need to do anything here.
            /// 
            /// </summary>

            // --
        }
    }
}
