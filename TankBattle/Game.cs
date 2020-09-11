using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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
 
    public class Game
    {
        private TankController[] _numPlayers;     // Array of the number of players
        private ControlledTank[] _numTanks;       // Array of the number of tanks
        private List<AttackEffect> _effect;      // List of attack effects
        private Battlefield _currentBattle;      // The current Battlefield that is being played

        private int[] _playerLoc;                // Int array of the locations of players
        private int _numRounds;                   // The number of rounds of the game
        private int _currentRound;               // The current round being played
        private int _startPlayer;                // The starting player who controls the tanks
        private int _currentPlayer;              // The current player playing the turn
        private int _wind;                       // The windspeed

        private IntroForm begin = new IntroForm(); // Initiates IntroForm

        public Game(int numPlayers, int numRounds)
        {
            /// <summary>
            /// 
            /// Game's constructor.
            /// This is called with the number of players in the game
            /// and the number of rounds that will be played. 
            /// 
            /// </summary>

            _numPlayers = new TankController[numPlayers];
            _numRounds = numRounds;
            _effect = new List<AttackEffect>();
        }

        public int GetNumPlayers()
        {
            /// <summary>
            /// 
            /// This method returns the total number of players in
            /// the game.
            /// 
            /// </summary>

            return _numPlayers.Length;
        }

        public int GetCurrentRound()
        {
            /// <summary>
            /// 
            /// This method returns the current round of gameplay,
            /// which is a private field of Game.
            /// 
            /// </summary>

            return _currentRound;
        }

        public int GetNumRounds()
        {
            /// <summary>
            /// 
            /// This method returns the total number of rounds the
            /// game will last for.
            /// 
            /// </summary>

            return _numRounds;
        }

        public void SetPlayer(int playerNum, TankController player)
        {
            /// <summary>
            /// 
            /// This method takes a player number(guaranteed to be between 1 and the number of players)
            /// and sets the appropriate field in Game's TankController array to player.
            /// 
            /// </summary> 

           _numPlayers[playerNum - 1] = player;        //-1 accounts for array index  
        }

        public TankController GetPlayerById(int playerNum)
        {
            /// <summary>
            /// 
            /// This method takes a player number (between 1 and the number of players)
            /// and returns the appropriate TankController from the TankController array.
            /// 
            /// </summary>

            return _numPlayers[playerNum - 1];
        }

        public ControlledTank GetBattleTank(int playerNum)
        {
            /// <summary>
            /// 
            /// This method takes a player number (between 1 and the number of players)
            /// and returns the ControlledTank associated with the TankController of that number.
            /// 
            /// </summary>

            return _numTanks[playerNum - 1];
        }

        public static Color PlayerColour(int playerNum)
        {
            /// <summary> 
            ///
            /// This static method takes a player number (between 1 and the number of players)
            /// and returns an appropriate colour to be used to represent that player.
            /// 
            /// </summary>

            Color c = new Color();      //Value that returns a for loop for each color
            Color[] Colour = { Color.Red, Color.LightBlue, Color.Green, Color.Yellow, Color.Pink, Color.Purple, Color.Brown, Color.White, Color.DarkGray };     //Array of colours
            for (int i = 0; i < playerNum; i++)
            {
                c = Colour[i];
            }
            return c;
        }

        public static int[] GetPlayerLocations(int numPlayers)
        {
            // <summary> 
            ///
            /// Given a number of players, this static method returns an array giving the horizontal
            /// positions of those players on the map. 
            /// 
            /// Player positions in the array are from left to right.
            /// 
            /// Each player is placed equally close to its neighbours - for four players, if the first player is 40 tiles away from the second player,
            /// the second player should be (approximately) 40 tiles away from the third player and the third player should be (approximately) 40 tiles away from the last player.
            /// The leftmost and rightmost players are placed a distance away from their respective horizontal border that is half the size of the distance between players.
            /// 
            /// </summary>

            int[] position = new int[numPlayers];
            int x_pos = 0;
            for (int i = 0; i < numPlayers; i++)
            {
                x_pos = x_pos + (Battlefield.WIDTH / numPlayers) / 2;   //Loops the positions of the players depending on the number of players
                position[i] = x_pos;
            }
            return position;
        }

        public static void Rearrange(int[] array)
        {
            /// <summary>
            /// 
            /// This method, given an array of at least 1,
            /// randomises the other of the numbers in it.
            /// 
            /// </summary>

            Random shuffle = new Random();      //Random value used for shuffle
            for (int i = array.Length; i > 0; i--)
            {
                int x = shuffle.Next(i);
                int y = array[x];
                array[x] = array[i - 1];
                array[i - 1] = y;
            }
        }

        public void NewGame()
        {
            /// <summary>
            /// 
            /// This method begins a new game.
            /// 
            /// The current round is initialised to 1, and the 
            /// the starting TankController is initialised to 0.
            /// 
            /// </summary>

            _currentRound = 1;
            _startPlayer = 0;
            StartRound();       // StartRound() is called  
        }

        public void StartRound()
        {
            /// <summary>
            /// 
            /// This method begins a new round of gameplay. A game consists of multiple rounds,
            /// and a round consists of multiple turns.
            /// 
            /// </summary>

            Random rand = new Random();
            _playerLoc = GetPlayerLocations(_numPlayers.Length);
            _currentPlayer = _startPlayer;
            _currentBattle = new Battlefield();
            _numTanks = new ControlledTank[_numPlayers.Length];
            _wind = rand.Next(-100, 100);

            foreach (TankController value in _numPlayers)
            {
                value.BeginRound();
            }
            Rearrange(_playerLoc);
            for (int i = 0; i < _numPlayers.Length; i++)
            {
                int xPos = _playerLoc[i];
                int yPos = _currentBattle.TankVerticalPosition(xPos);
                _numTanks[i] = new ControlledTank(_numPlayers[i], xPos, yPos, this);
            }
            BattleForm battle = new BattleForm(this);
            battle.Show();
            SetupForm setup = new SetupForm();
            setup.Show();

        }

        public Battlefield GetArena()
        {
            /// <summary>
            /// 
            /// This method returns the current Battlefield used by the game. 
            /// This is stored in a private field and is initialised by StartRound().
            /// 
            /// </summary> 

            return _currentBattle;
        }

        public void DrawTanks(Graphics graphics, Size displaySize)
        {
            /// <summary>
            /// 
            /// This method tells all the ControlledTanks to draw themselves. The graphics and displaySize arguments
            /// are not important here; they are simply passed along to the Render methods.
            /// 
            /// </summary>

            int comprise = 0;
            foreach (ControlledTank value in _numTanks)
            {
                value.IsAlive();
                comprise++;

                if (value.IsAlive() == true)
                {
                    value.Render(graphics, displaySize);
                }
            }
        }

        public ControlledTank GetPlayerTank()
        {
            /// <summary>
            /// 
            /// This method returns the ControlledTank associated with the current player. Both the current player and
            /// an array of ControlledTank are private fields of Game and are also initialised in StartRound().
            /// 
            /// </summary>

            return _numTanks[_currentPlayer];
        }

        public void AddEffect(AttackEffect weaponEffect)
        {
            /// <summary>
            /// 
            /// This method adds the given AttackEffect to its list/array of AttackEffects. 
            /// 
            /// </summary>

            _effect.Add(weaponEffect);
            
        }

        public bool WeaponEffectStep()
        {
            /// <summary>
            ///
            /// This method loops through all AttackEffects in the array, calling ProcessTimeEvent() on each.
            /// 
            /// </summary>

            int comprise = 0;
            foreach (AttackEffect value in _effect)
            {
                value.ProcessTimeEvent();
                comprise++;
            }
            if (comprise > 0)
            {
                return true;
            }
            return false;
        }

        public void RenderEffects(Graphics graphics, Size displaySize)
        {
            /// <summary>
            /// 
            /// This method loops through all AttackEffects in the array, calling Render() on each. graphics and
            /// displaySize are not used by this method itself, but are passed to AttackEffect's 
            /// Render() method.
            /// 
            /// </summary>

            foreach (AttackEffect value in _effect)
            {
                value.Render(graphics, displaySize);
            }
        }

        public void RemoveWeaponEffect(AttackEffect weaponEffect)
        {
            /// <summary>
            /// 
            /// This method removes the AttackEffect referenced by weaponEffect from the array or list used by
            /// Game to store active AttackEffects.
            /// 
            /// </summary>

            _effect.Remove(weaponEffect);
        }

        public bool CheckHitTank(float projectileX, float projectileY)
        {
            /// <summary>
            /// 
            /// This method returns true if a Bullet at projectileX, projectileY will hit something.
            /// 
            /// </summary>

            bool ifHit = false;     //bool that returns value

            if (projectileX > Battlefield.WIDTH || projectileX < 0 || projectileY > Battlefield.HEIGHT )
            {
                ifHit = false;
            }
            if (_currentBattle.TerrainAt((int)projectileX, (int)projectileY))
            {
                ifHit = true;
            }
            for (int i = 0; i < _numTanks.Length; i++)
            {
                float tankX = _numTanks[i].GetX();
                float tankY = _numTanks[i].Y();
                if (projectileX > tankX && projectileY > tankY && projectileX < (tankX + Tank.WIDTH) && projectileY < (tankY + Tank.HEIGHT) && _numTanks[i].IsAlive() == true)
                {
                    ifHit = true;
                }
                else
                {
                    ifHit = false;
                }
            }
            return ifHit;
        }

        public void DamageArmour(float damageX, float damageY, float explosionDamage, float radius)
        {
            /// <summary>
            /// 
            /// This method inflicts up to explosionDamage damage on any ControlledTanks
            /// within the circle described by damageX, damageY and radius.
            /// 
            /// </summary>
            float tankX;
            float tankY;
            float tankCentre;
            float distance;

            _numTanks = new ControlledTank[_numPlayers.Length];

            for (int i = 0; i < _numTanks.Length; i++)
            {
                tankX = _numTanks[i].GetX();
                tankY = _numTanks[i].Y();
                tankCentre = tankX + tankY + (Tank.WIDTH + Tank.HEIGHT / 2);
                if (_numTanks[i].IsAlive() == true)
                {
                    distance = (float)Math.Sqrt(Math.Pow((damageX - tankX), 2) + Math.Pow((damageY - tankY), 2));
                    if (distance > radius)
                    {
                        explosionDamage = 0;
                    }
                    else if (distance > (radius / 2) && distance < radius)
                    {
                        explosionDamage = explosionDamage * ((distance - radius) / radius);
                    }
                    else if (distance < radius / 2)
                    {
                        explosionDamage = radius;
                    }
                    _numTanks[i].DamageArmour((int)explosionDamage);
                }
            }
        }

        public bool ProcessGravity()
        {
            /// <summary>
            /// 
            /// This method is called after all AttackEffect animations have finished and moves any terrain and/or ControlledTanks
            /// that are floating in the air down. Similarly to WeaponEffectStep, this method returns false once there is nothing left
            /// to move, and true until then. It will be called in a loop by the BattleForm.
            /// 
            /// </summary>

            bool moved = false;
            _numTanks = new ControlledTank[_numPlayers.Length];

            for (int i = 0; i < _numTanks.Length; i++)
            {
                if (_currentBattle.ProcessGravity() == true)
                {
                    moved = true;
                }
                else
                {
                    moved = false;
                }
            }
            return moved;
        }

        public bool EndTurn()
        {
            /// <summary>
            /// 
            /// This method is called once the current turn is over. It checks how many ControlledTanks are still in battle, makes a determination as to whether
            /// the round is over or not, and if not changes the current player to the next player that's still in the running. This method returns true
            /// if the round is still going, and false if it's over.
            /// 
            /// </summary>

            _numTanks = new ControlledTank[_numPlayers.Length];
            Random rand = new Random();

            for (int i = 0; i < _numTanks.Length; i++)
            {
                if (_numTanks[i].IsAlive() == true)
                {
                    _currentPlayer++;
                }
            }

            if (_currentPlayer > 1)
            {
                return true;
            }
            else if (_currentPlayer == 1)
            {
                RewardWinner();
            }

            _wind = rand.Next(-10, 10);
            if (_wind < -100)
            {
                _wind = -100;
            }
            else if (_wind > 100)
            {
                _wind = 100;
            }
            return false;
        }

        public void RewardWinner()
        {
            /// <summary>
            ///
            /// This method is called by EndTurn() after it decides that the current round is over. 
            /// It finds out which player won the round and rewards that player with a point.
            /// 
            /// </summary>

            TankController victor;
            foreach (ControlledTank value in _numTanks)
            {
                if (value.IsAlive())
                {
                    victor = value.GetPlayerById();
                    victor.Winner();
                }
            }
        }

        public void NextRound()
        {
            /// <summary>
            /// 
            /// This method is called by BattleForm after the round is over 
            /// (BattleForm decides whether the round is over based on whether EndTurn() returned true or false).
            /// 
            /// </summary>

            _currentRound++;
            if (_currentRound <= _numRounds)
            {
                _currentPlayer++;

                if (_currentPlayer > _numPlayers.Length)
                {
                    _currentPlayer = 0;
                    StartRound();
                }
            }
            else
            {
                begin.Show();
            }
        }
        
        public int Wind()
        {
            ///<summary>
            ///
            /// This method returns the current wind speed, which is a private
            /// field of Game and ranges between -100 and 100.
            /// 
            /// </summary>
            return _wind;
        }
    }
}
