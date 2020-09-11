using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    /// <summary>
    /// 
    /// This class represents a tank on the battlefield, as distinct from Tank which represents a particular model of tank.
    /// 
    /// Author Jai Hunt n10006869 October 2017
    /// 
    /// Note: All private fields have been marked with an underscore at the beginning of the name
    /// for code clarity for the user.
    /// 
    /// </summary>

    public class ControlledTank
    {
        private TankController _player;         // player private field
        private int _tankX;                     // tank x value
        private int _tankY;                     // tank y value
        private float _angle;                   // tank angle
        private int _tankPower;                 // tank projectile power
        private int _weapon;                    // weapon
        private int _durability;                    // durability
        private int _deltadurability;               // current durability
        private Tank _type;                     // type of tank 
        private Bitmap _image;                  // bitmap
        private Game _game;                     // game


        public ControlledTank(TankController player, int tankX, int tankY, Game game)
        {
            /// <summary>
            ///
            /// This constructor stores player, tankX, tankY and game as private fields of ControlledTank. It then gets the Tank by using the TankController's GetTank() method,
            /// then calls GetArmour() on it and stores this as the ControlledTank's current durability. This will go down as the tank takes damage.
            /// 
            /// The constructor also initialises the angle, power and current weapon private variables, which start at 0, 25 and 0 respectively. 
            /// Angle should be stored as a private float, while power and current weapon should be stored as private ints.
            /// 
            /// Finally, it should also call Tank's CreateBitmap method, passing in the colour (retrieved from TankController's TankColour()) and current angle.
            /// The return value should then be stored as yet another private field.
            /// 
            /// </summary>

            _tankX = tankX;
            _tankY = tankY;
            _player = player;
            _type = GetTank();
            _durability = _type.GetArmour();
            _deltadurability = _durability;
            _angle = 0;
            _tankPower = 25;
            _weapon = 0;
            _image = _type.CreateBitmap(_player.TankColour(), _angle);
            _game = game;

        }

        public TankController GetPlayerById()
        {
            ///<summary>
            ///
            /// This returns the TankController associated with this ControlledTank.
            /// 
            /// </summary>

            return _player;
        }
        public Tank GetTank()
        {
            /// <summary>
            ///
            /// This returns the Tank associated with this ControlledTank.
            /// 
            /// </summary>

            return _player.GetTank();
        }

        public float GetTankAngle()
        {
            /// <summary>
            /// 
            /// This returns the ControlledTank's current aiming angle. -90 means the turret is pointing to the left, 
            /// 90 means the turret is pointing to the right, while 0 means the turret is pointing straight up.
            /// 
            /// </summary>

            return _angle;
        }

        public void SetAimingAngle(float angle)
        {
            /// <summary>
            /// 
            /// This method sets the ControlledTank's current aiming angle. As changing the angle changes how the tank is displayed, 
            /// this code calls CreateBitmap again, overwriting the existing bitmap.
            /// 
            /// </summary>

            _angle = angle;
        }

        public int GetTankPower()
        {
            /// <summary>
            /// 
            /// This returns the ControlledTank's current turret velocity. 
            /// 5 is minimum power. 100 is maximum power.
            /// 
            /// </summary>

            if (_tankPower > 100)
            {
                _tankPower = 100;
            }
            if (_tankPower < 25)
            {
                _tankPower = 25;
            }
            return _tankPower;
        }

        public void SetPower(int power)
        {
            /// <summary>
            /// 
            /// This method sets the ControlledTank's current turret velocity.
            /// 
            /// </summary>

            _tankPower = power;
        }

        public int GetPlayerWeapon()
        {
            /// <summary>
            /// 
            /// This returns the index of the current weapon equipped by the ControlledTank.
            /// 
            /// </summary>

            return _weapon;
        }
        public void SelectWeapon(int newWeapon)
        {
            /// <summary>
            /// 
            /// This method sets the ControlledTank's current weapon.
            /// 
            /// </summary>

            _weapon = newWeapon;
        }

        public void Render(Graphics graphics, Size displaySize)
        {
            /// <summary>
            /// 
            /// This method draws the ControlledTank to graphics, scaled to the provided displaySize.
            /// The ControlledTank's durability will also be shown as a percentage.
            /// 
            /// Code Referenced: CAB201 ASSIGNMENT AMS
            /// </summary>

            int drawX1 = displaySize.Width * _tankX / Battlefield.WIDTH;
            int drawY1 = displaySize.Height * _tankY / Battlefield.HEIGHT;
            int drawX2 = displaySize.Width * (_tankX + Tank.WIDTH) / Battlefield.WIDTH;
            int drawY2 = displaySize.Height * (_tankY + Tank.HEIGHT) / Battlefield.HEIGHT;
            graphics.DrawImage(_image, new Rectangle(drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1));

            int drawY3 = displaySize.Height * (_tankY - Tank.HEIGHT) / Battlefield.HEIGHT;
            Font font = new Font("Arial", 8);
            Brush brush = new SolidBrush(Color.White);

            int pct = _deltadurability * 100 / _durability;
            if (pct < 100)
            {
                graphics.DrawString(pct + "%", font, brush, new Point(drawX1, drawY3));
            }
        }

        public int GetX()
        {
            /// <summary>
            ///
            /// Returns the current horizontal position of the ControlledTank.
            /// 
            /// </summary>

            return _tankX;
        }
        public int Y()
        {
            /// <summary>
            ///
            /// Returns the current vertical position of the ControlledTank.
            /// 
            /// </summary>

            return _tankY;
        }

        public void Launch()
        {
            _type = _player.GetTank();
            _type.WeaponLaunch(_weapon, this, _game);
        }

        public void DamageArmour(int damageAmount)
        {
            _deltadurability -= damageAmount;
        }

        public bool IsAlive()
        {
            /// <summary>
            ///
            /// This returns true if this ControlledTank's durability is greater than 0; otherwise it returns false. 
            /// If the ControlledTank is less than or equal to 0, it is considered destroyed and will not receive turns or be drawn to the screen.
            /// 
            /// </summary>

            if (_deltadurability > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ProcessGravity()
        {
            /// <summary>
            /// 
            /// Calling this method calls the ControlledTank to fall down one tile, if possible. If the ControlledTank moves as 
            /// a result of this method, the method will return true. Otherwise it will return false.
            /// 
            /// </summary>

            if (IsAlive())
            {
                Battlefield _battlefield = _game.GetArena();
                if (_battlefield.TankCollisionAt(_tankX, _tankY + 1))
                {
                    return false;
                }
                else
                {
                    _tankY++;
                    _deltadurability--;
                    if (_tankY == (Battlefield.HEIGHT - Tank.HEIGHT))
                    {
                        _deltadurability = 0;
                    }
                    return true;
                }
            } 
            else
            {
                return false;
            }
        }
    }
}
