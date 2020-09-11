using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class ComputerOpponent : TankController
    {
        public ComputerOpponent(string name, Tank tank, Color colour) : base(name, tank, colour)
        {
            throw new NotImplementedException();
        }

        public override void BeginRound()
        {
            throw new NotImplementedException();
        }

        public override void NewTurn(BattleForm gameplayForm, Game currentGame)
        {
            throw new NotImplementedException();
        }

        public override void HitPos(float x, float y)
        {
            throw new NotImplementedException();
        }
    }
}
