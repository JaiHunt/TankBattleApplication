using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    /// <summary>
    /// 
    /// Author Jai Hunt n10006869 October 2017
    /// 
    /// Note: All private fields have been marked with an underscore at the beginning of the name
    /// for code clarity for the user.
    /// 
    /// </summary>

    public abstract class AttackEffect
    {
        protected Game _game;

        public void RecordCurrentGame(Game game)
        {
            /// <summary>
            ///
            /// This is called in Game's AddEffect.
            /// 
            /// </summary>
            _game = game;
        }

        public abstract void ProcessTimeEvent();
        public abstract void Render(Graphics graphics, Size displaySize);
    }
}

