using System;
using System.Drawing;

namespace The_Quest
{
    abstract class Weapon : Mover
    {
        public abstract string Name { get; }
        public bool PickedUp { get; private set; }

        public Weapon(Game game, Point location) : base(game, location)
        {
            PickedUp = false;
        }

        public void PickUpWeapon()
        {
            PickedUp = true;
        }
        public abstract void Attack(Direction direction, Random random);
        protected bool DamageEnemy(Direction direction,  int radius, int damage, Random random)
        {
            Point target = game.PlayerLocation;
            for (int distance = 0; distance < radius / 2; distance++)
            {
                foreach(Enemy enemy in game.Enemies)
                {
                    if(NearBy(enemy.Location, target, distance))
                    {
                        enemy.Hit(damage, random);
                        return true;
                    }
                }
                target = Move(direction, target, game.Boundaries);
            }
            return false;
        }
        private bool NearBy(Point enemyTarget, Point target, int distance)
        {
            if (NearBy(enemyTarget, distance) && NearBy(target, distance))
                return true;
            else
                return false;
        }
        private Point Move(Direction direction, Point target, Rectangle boundaries)
        {
            target = Move(direction, boundaries);
            return target;
        }

        protected Direction ClockwiseDirection(Direction direction)
        {
            Direction clockWiseDirection = direction;

            switch (direction)
            {
                case Direction.Up:
                    clockWiseDirection = Direction.Right;
                    break;
                case Direction.Right:
                    clockWiseDirection = Direction.Down;
                    break;
                case Direction.Down:
                    clockWiseDirection = Direction.Left;
                    break;
                case Direction.Left:
                    clockWiseDirection = Direction.Up;
                    break;
            }

            return clockWiseDirection;
        }

        protected Direction CounterClockwiseDirection(Direction direction)
        {
            Direction counterClockWiseDirection = direction;

            switch (direction)
            {
                case Direction.Up:
                    counterClockWiseDirection = Direction.Left;
                    break;
                case Direction.Right:
                    counterClockWiseDirection = Direction.Up;
                    break;
                case Direction.Down:
                    counterClockWiseDirection = Direction.Right;
                    break;
                case Direction.Left:
                    counterClockWiseDirection = Direction.Down;
                    break;
            }

            return counterClockWiseDirection;
        }

    }
}