using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{

    /// <summary>
    /// 
    /// This class represents the landscape, the arena on which the tanks battle. 
    /// The terrain is randomly generated and can be destroyed during the round. 
    /// A new Battlefield with a newly-generated terrain is created for each round.
    /// 
    /// Author Jai Hunt n10006869 October 2017
    /// 
    /// Note: All private fields have been marked with an underscore at the beginning of the name
    /// for code clarity for the user.
    /// 
    /// </summary>

    public class Battlefield
    {
        public const int WIDTH = 160;       // Width of the map
        public const int HEIGHT = 120;      // Height of the map

        private bool[,] _fieldArray;        //Battlefield terrain 2-dimensional array

        public Battlefield()
        {
            ///<summary>
            ///
            /// This constructor randomly generates the terrain on which the tanks will battle. It creates a two-dimensional array of bools to be used 
            /// for representing the terrain (where 'true' means there is terrain at that location). 
            /// The size of the array is in the constants Battlefield.WIDTH and Battlefield.HEIGHT (160 x 120).
            /// 
            /// </summary>

            Random rand = new Random();                     // Random instance  
            int mapSky = 4;                                 // Area above terrain to be left clear - acts as the sky and ensures room for tank 
            int mapGround = 10;                             // Area below ground to be left full - acts as the ground and ensures no 'floating' blocks
            _fieldArray = new bool[WIDTH, HEIGHT];              // Array set to WIDTH and HEIGHT constants for map dimensions   
            int groundLevel = rand.Next(mapGround, HEIGHT - mapSky);    // Generates new layer after layer is drawn in required dimensions    

            for (int i = 0; i < WIDTH; i++)
            {
                int tileGen = rand.Next(0, 3);
                if (tileGen == 0)
                {
                    groundLevel -= 1;
                }
                else if (tileGen == 2)
                {
                    groundLevel += 1;
                }
                else
                {
                    groundLevel += 0;
                }
                for (int j = groundLevel; j < HEIGHT; j++)
                {
                    _fieldArray[i, j] = true;
                }
            }
        }

        public bool TerrainAt(int x, int y)
        {
            ///<summary>
            ///
            /// This returns whether there is any terrain at the given coordinates. 
            /// If there is, it returns true. Otherwise, it returns false.
            /// 
            /// </summary>

            return _fieldArray[x, y];
        }

        public bool TankCollisionAt(int x, int y)
        {
            /// <summary>
            /// 
            /// This is similar to TerrainAt, but it returns whether there is room for a tank-sized object (a tank is Tank.WIDTH wide and Tank.HEIGHT tall) at the given coordinates.
            /// The coordinates refer to the top left of the tank position, which means that if our map looks like this:
            /// 
            /// </summary>
            
            int counter = 0;

            for (int i = x; i < (x + Tank.WIDTH); i++)
            {
                for (int j = y; j < (y + Tank.HEIGHT); j++)
                {
                    if (TerrainAt(i, j) == true)
                    {
                        counter++;
                    }
                }
            }
            if (counter > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int TankVerticalPosition(int x)
        {
            /// <summary>
            /// 
            /// This method takes the x coordinate of a tank and, using that, finds the largest (lowest) y coordinate where a tank can go.
            /// 
            /// </summary>
            
            int _y;
            for (_y = 0; _y < (Battlefield.HEIGHT - Tank.HEIGHT - 1); _y++)
            {
                if (TankCollisionAt(x, _y))
                {
                    break;
                }
            }
            return _y - 1;
        }

        public void DestroyTerrain(float destroyX, float destroyY, float radius)
        {
            /// <summary>
            /// 
            /// This method destroys all terrain within a circle centred around destroyX, destroyY. 
            /// This method is called after a Bullet explodes, destroying the terain
            /// 
            /// </summary>

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    double distance = Math.Sqrt(Math.Pow(i - destroyY, 2) + Math.Pow(j - destroyX, 2));
                    if (distance < radius)
                    {
                        _fieldArray[j, i] = false;
                    }

                }
            }
        }

        public bool ProcessGravity()
        {
            /// <summary>
            /// 
            /// This method is called after all AttackEffect animations have finished and moves any terrain and/or ControlledTanks that are floating in the air down.
            /// Similarly to WeaponEffectStep, this method returns false once there is nothing left to move, and true until then. 
            /// It will be called in a loop by the BattleForm.
            /// 
            /// </summary>

            bool moved = false;

            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = (HEIGHT - 1); j > 0; j--)
                {
                    if (TerrainAt(i, j) == false && TerrainAt(i, j - 1) == true)
                    {
                        _fieldArray[i, j] = true;
                        _fieldArray[i, j - 1] = false;
                        moved = true;
                    }
                    else
                    {
                        moved = false;
                    }
                }
            }
            return moved;
        }
    }
}
