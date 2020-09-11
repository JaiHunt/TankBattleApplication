using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Explosion : AttackEffect
    {
        private int _explosionRadius;
        private int _explosionDamage;
        private int _earthDestructionRadius;
        private float _x;
        private float _y;
        private float _lifespan;
        private Battlefield _effect;

        public Explosion(int explosionDamage, int explosionRadius, int earthDestructionRadius)
        {
            /// <summary>
            /// 
            /// The Explosion takes the explosion damage, explosion radius and earth destruction
            /// radius values it is passed and stores them as private fields.
            /// 
            /// </summary>

            _explosionRadius = explosionRadius;
            _explosionDamage = explosionDamage;
            _earthDestructionRadius = earthDestructionRadius;

        }

        public void Explode(float x, float y)
        {
            /// <summary>
            /// 
            /// This method detonates the Explosion at the specified location.
            /// 
            /// </summary>

            _x = x;
            _y = y;
            _lifespan = 1.0f;
        }

        public override void ProcessTimeEvent()
        {
            /// <summary>
            /// 
            /// This method reduces the Explosion's lifespan by 0.05, and if it reaches 0 (or lower),
            /// calls DamageArmour(), GetArena(), RemoveWeaponEffect();
            /// 
            /// </summary>

            _lifespan -= 0.05f;
            if (_lifespan <= 0)
            {
                _effect = _game.GetArena();
                _effect.DestroyTerrain(_x, _y, _earthDestructionRadius);
                _game.RemoveWeaponEffect(this);
            }
        }

        public override void Render(Graphics graphics, Size displaySize)
        {
            /// <summary>
            /// 
            /// This method draws one frame of the Explosion. The idea is to draw a circle that
            /// expands, cycling from yellow to red and then fading out.
            /// 
            /// </summary>

            float x = (float)this._x * displaySize.Width / Battlefield.WIDTH;
            float y = (float)this._y * displaySize.Height / Battlefield.HEIGHT;
            float radius = displaySize.Width * (float)((1.0 - _lifespan) * _earthDestructionRadius * 3.0 / 2.0) / Battlefield.WIDTH;

            int alpha = 0, red = 0, green = 0, blue = 0;

            if (_lifespan < 1.0 / 3.0)
            {
                red = 255;
                alpha = (int)(_lifespan * 3.0 * 255);
            }
            else if (_lifespan < 2.0 / 3.0)
            {
                red = 255;
                alpha = 255;
                green = (int)((_lifespan * 3.0 - 1.0) * 255);
            }
            else
            {
                red = 255;
                alpha = 255;
                green = 255;
                blue = (int)((_lifespan * 3.0 - 2.0) * 255);
            }

            RectangleF rect = new RectangleF(x - radius, y - radius, radius * 2, radius * 2);
            Brush b = new SolidBrush(Color.FromArgb(alpha, red, green, blue));

            graphics.FillEllipse(b, rect);
        }
    }
}
