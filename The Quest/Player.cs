using System;
using System.Collections.Generic;
using System.Drawing;

namespace The_Quest
{
    class Player : Mover
    {
        private const int RADIUS = 10;

        private Weapon _equippedWeapon;
        private List<Weapon> _inventory = new List<Weapon>();

        public int Hitpoints { get; private set; }
        public IEnumerable<string> Weapons
        { get
            {
                List<string> names = new List<string>();
                foreach (Weapon weapon in _inventory)
                    names.Add(weapon.Name);
                return names;
            }
        }

        public Player(Game game, Point location) : base(game, location)
        {
            Hitpoints = 10;
        }

        public bool IsWeaponEquipped(string weaponName)
        {
            if(_equippedWeapon != null)
                if (weaponName.Equals(_equippedWeapon.Name))
                    return true;
            return false;
        }

        public void Hit(int maxDamage, Random random)
        {
            Hitpoints -= random.Next(1, maxDamage);
        }

        public void IncreaseHealth(int health, Random random)
        {
            Hitpoints += random.Next(1, health);
        }

        public void Equip(string weaponName)
        {
            foreach (Weapon weapon in _inventory)
                if (weaponName == weapon.Name)
                    _equippedWeapon = weapon;
        }
        
        public void Move(Direction direction)
        {
            base.location = Move(direction, game.Boundaries);
            if (!game.WeaponInRoom.PickedUp)
            {
               if (NearBy(game.WeaponInRoom.Location, RADIUS))
                {
                    game.WeaponInRoom.PickUpWeapon();
                    _inventory.Add(game.WeaponInRoom);
                    Equip(game.WeaponInRoom.Name);
                }
            }
        }

        public void Attack(Direction direction, Random random)
        {
            if(_equippedWeapon != null)
                _equippedWeapon.Attack(direction, random);
        }
        public bool CheckPotionUsed(string potionName)
        {
            IPotion potion;
            bool potionUsed = true;

            foreach (Weapon weapon in _inventory)
            {
                if (weapon.Name == potionName && weapon is IPotion)
                {
                    potion = weapon as IPotion;
                    potionUsed = potion.Used;
                }
            }

            return potionUsed;
        }
    }
}