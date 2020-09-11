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

    public class Bullet : AttackEffect
    {
        private Explosion _explosion;
        private TankController _player;
        private float _xVelocity;
        private float _yVelocity;
        private float _x;
        private float _y;
        private float _gravity;

        public Bullet(float x, float y, float angle, float power, float gravity, Explosion explosion, TankController player)
        {
            /// <summary>
            /// 
            /// This method constructs a new Bullet. The x, y, gravity, explosion 
            /// and player fields should all be stored in private fields of Bullet.
            /// 
            /// </summary>

            _x = x;
            _y = y;
            _explosion = explosion;
            _player = player;
            _gravity = gravity;

            float angleRadians = (90 - angle) * (float)Math.PI / 180;
            float magnitude = power / 50;
            _xVelocity = (float)Math.Cos(angleRadians) * magnitude;
            _yVelocity = (float)Math.Sin(angleRadians) * -magnitude;
        }

        public override void ProcessTimeEvent()
        {
            /// <summary>
            /// 
            /// This method moves the given projectile according to its angle, power, gravity and the wind
            /// 
            /// </summary>

            for (int i = 0; i <= 10; i++)
            {
                _x += _xVelocity + (_game.Wind() / 1000.0f);
                _y += _yVelocity;

                if (_x < 0 || _x > Battlefield.WIDTH || _y > Battlefield.HEIGHT)
                {
                    _game.RemoveWeaponEffect(this);
                    return;
                }
                else if (_game.CheckHitTank(_x, _y))
                {
                    _player.HitPos(_x, _y);
                    _explosion.Explode(_x, _y);
                    _game.AddEffect(_explosion);
                    _game.RemoveWeaponEffect(this);
                    return;
                }
                _yVelocity += _gravity;
            }

        }

        public override void Render(Graphics graphics, Size size)
        {
            /// <summary>
            /// 
            /// This method draws the Bullet as a small white circle.
            /// 
            /// </summary>

            float x = (float)this._x * size.Width / Battlefield.WIDTH;
            float y = (float)this._y * size.Height / Battlefield.HEIGHT;
            float s = size.Width / Battlefield.WIDTH;

            RectangleF r = new RectangleF(x - s / 2.0f, y - s / 2.0f, s, s);
            Brush b = new SolidBrush(Color.WhiteSmoke);

            graphics.FillEllipse(b, r);
        }
    }
}
