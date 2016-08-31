using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Quest
{
    class Game
    {
        private Player _player;
        private int _level = 0;
        private Rectangle _boundaries;

        public Rectangle Boundaries { get { return _boundaries; } }
        public IEnumerable<Enemy> Enemies { get; private set; }
        public Weapon WeaponInRoom { get; private set; }
        public Point PlayerLocation { get { return _player.Location; } }
        public int PlayerHitPoints { get { return _player.Hitpoints; } }
        public IEnumerable<string> PlayerWeapons { get { return _player.Weapons; } }
        public int Level { get { return _level; } }

        public Game(Rectangle boundaries)
        {
            _boundaries = boundaries;
            _player = new Player(this, new Point(boundaries.Left + 10, boundaries.Top + 70));
        }

        public void Move(Direction direction, Random random)
        {
            _player.Move(direction);
            foreach (Enemy enemy in Enemies)
                enemy.Move(random);
        }

        public void Equip(string weaponName)
        {
            _player.Equip(weaponName);
        }
        public bool CheckPlayerInventory(string weaponName)
        {
            return _player.Weapons.Contains(weaponName);
        }
        public void HitPlayer(int maxDamage, Random random)
        {
            _player.Hit(maxDamage, random);
        }
        public void IncreasePlayerHealth(int health, Random random)
        {
            _player.IncreaseHealth(health, random);
        }
        public void Attack(Direction direction, Random random)
        {
            _player.Attack(direction, random);
            foreach (Enemy enemy in Enemies)
                enemy.Move(random);
        }
        private Point GetRandomLocation(Random random)
        {
            int x = _boundaries.Left + random.Next(_boundaries.Right / 10 - _boundaries.Left / 10) * 10;
            int y = _boundaries.Top + random.Next(_boundaries.Bottom / 10 - _boundaries.Top / 10) * 10;
            return new Point(x, y);
        }
        public void NewLevel(Random random)
        {
            //unfinished
            _level++;
            switch (_level)
            {
                case 1:
                    Enemies = new List<Enemy>() { new Bat(this, GetRandomLocation(random)) };
                    WeaponInRoom = new Sword(this, GetRandomLocation(random));
                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;
                case 7:

                    break;
                case 8:
                    Application.Exit();
                    break;
            }
        }
    }
}
