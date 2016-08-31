using System;
using System.Drawing;

namespace The_Quest
{
    abstract class Enemy : Mover
    {
        private const int NEAR_PLAYER_DISTANCE = 25;

        public int HitPoints { get; private set; }

        public Enemy(Game game, Point location, int hitPoints) : base(game, location)
        {
            HitPoints = hitPoints;
        }

        public abstract void Move(Random random);

        public void Hit(int maxDamage, Random random)
        {
            HitPoints -= random.Next(1, maxDamage);
        }
        protected bool NearPlayer()
        {
            return (NearBy(game.PlayerLocation, NEAR_PLAYER_DISTANCE));
        }
        protected Direction FindPlayerDirection(Point playerLocation)
        {
            Direction directionToMove;

            if (playerLocation.X > location.X + 10)
                directionToMove = Direction.Right;
            else if (playerLocation.X < location.X - 10)
                directionToMove = Direction.Left;
            else if (playerLocation.Y < location.Y - 10)
                directionToMove = Direction.Up;
            else
                directionToMove = Direction.Down;             
            return directionToMove;
        }
    }
}