using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Galaxy.Core.Environment;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Galaxy.Environments.Actors
{
    internal class Ship0 : Ship
    {
        public Ship0(ILevelInfo info) : base(info)
        {
            Width = 30;
            Height = 30;
        }

        public override void Load()
        {
            Load(@"Assets\ship.png");
            base.Load();
        }

        protected override void h_changePosition()
        {
            Point playerPosition = Info.GetPlayerPosition();
            
            Position = new Point((int)(Position.X ), (int)(Position.Y+ Position.X/70));
        }
    
    }
}