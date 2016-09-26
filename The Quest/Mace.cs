using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Quest
{
    class Mace : Weapon
    {
        private const int ATTACK_RADIUS = 40;
        private const int DAMAGE = 6;

        public Mace(Game game, Point location) : base(game, location)
        {
        }

        public override string Name
        {
            get
            {
                return "Mace";
            }
        }

        public override void Attack(Direction direction, Random random)
        {
            if (!DamageEnemy(direction, ATTACK_RADIUS, DAMAGE, random))
            {
                if (!DamageEnemy(ClockwiseDirection(direction), ATTACK_RADIUS, DAMAGE, random))
                {
                    DamageEnemy(CounterClockwiseDirection(direction), ATTACK_RADIUS, DAMAGE, random);
                }
            }
        }
    }
}
