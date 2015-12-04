#region using

using System;
using System.Diagnostics;
using System.Windows;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

#endregion

namespace Galaxy.Environments.Actors
{
    public class Ship2 : Ship
    {

        #region Private fields

        private bool m_flying;
        private Stopwatch m_flyTimer;

        #endregion

        #region Constructors

        public Ship2(ILevelInfo info)
            : base(info)
        {
            Width = 30;
            Height = 30;
            ActorType = ActorType.Enemy;
        }

        #endregion


        #region Overrides

        public override void Load()
        {
            base.Load();
            Load(@"Assets\\shipppper.png");
        }

        public EnemyBullet CreatEnemyBullet(Ship2 ship)
        {
            var enbullet = new EnemyBullet(Info);
            int positionY = ship.Position.Y + 30;
            int positionX = ship.Position.X + 15;
            enbullet.Position = new Point(positionX, positionY);
            enbullet.Load();
            return enbullet;
        }
        #endregion


        public override void h_changePosition()
        {
            Position = new Point((int)(Position.X), (int)(Position.Y ));
        }
    }
}
