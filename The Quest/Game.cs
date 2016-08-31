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
        public List<Enemy> Enemies;
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
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Ghost(this, GetRandomLocation(random)) };
                    WeaponInRoom = new BluePotion(this, GetRandomLocation(random));
                    break;
                case 3:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Ghoul(this, GetRandomLocation(random)) };
                    WeaponInRoom = new Bow(this, GetRandomLocation(random));
                    break;
                case 4:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Bat(this, GetRandomLocation(random)), new Ghost(this, GetRandomLocation(random)) };
                    if (CheckPlayerInventory("Bow"))
                    {
                        if (!CheckPlayerInventory("Blue Potion")|| (CheckPlayerInventory("Blue Potion") && _player.CheckPotionUsed("Blue Potion")))
                        {
                            WeaponInRoom = new BluePotion(this, GetRandomLocation(random));
                        }
                    }
                    else
                    {
                        WeaponInRoom = new Bow(this, GetRandomLocation(random));
                    }
                    break;
                case 5:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Bat(this, GetRandomLocation(random)), new Ghoul(this, GetRandomLocation(random)) };
                    WeaponInRoom = new RedPotion(this, GetRandomLocation(random));
                    break;
                case 6:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Ghost(this, GetRandomLocation(random)), new Ghoul(this, GetRandomLocation(random)) };
                    WeaponInRoom = new Mace(this, GetRandomLocation(random));
                    break;
                case 7:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Bat(this, GetRandomLocation(random)), new Ghost(this, GetRandomLocation(random)), new Ghoul(this, GetRandomLocation(random)) };
                    WeaponInRoom = null;
                    if (CheckPlayerInventory("Mace"))
                    {
                        if (!CheckPlayerInventory("Red Potion")
                                || (CheckPlayerInventory("Red Potion")
                                    && _player.CheckPotionUsed("Red Potion")))
                        {
                            WeaponInRoom = new RedPotion(this, GetRandomLocation(random));
                        }
                    }
                    else
                    {
                        WeaponInRoom = new Mace(this, GetRandomLocation(random));
                    }
                    break;
                case 8:
                    Application.Exit();
                    break;
            }
        }
    }
}
