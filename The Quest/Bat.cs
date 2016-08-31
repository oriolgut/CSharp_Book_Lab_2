using System;
using System.Drawing;

namespace The_Quest
{
    class Bat : Enemy
    {
        public Bat(Game game, Point location) : base(game, location, 6) { }

        public override void Move(Random random)
        {
            if(random.Next(2) == 1)
            {
                
            }
                
        }
    }
}