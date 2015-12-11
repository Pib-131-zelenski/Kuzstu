using System.Drawing;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;

namespace Galaxy.Environments.Actors
{
    public class BallLightning : BaseActor
    {
        #region Constant

        private const int MaxSpeed = 2;
        private const long StartFlyMs = 2000;

        #endregion

        #region Constructors

        public BallLightning(ILevelInfo info) : base(info)
        {
            Width = 30;
            Height = 30;
            ActorType = ActorType.BallLightning;
        }

        #endregion

        #region Overrides

        #endregion

        public override void Update()
        {
            h_changePosition();
            base.Update();
        }

        #region Overrides

        public override void Load()
        {
            Load(@"Assets\BallLightning.png");
        }

        #endregion

        #region Private methods

        protected virtual
            void h_changePosition
            ()
        {
            Position = new Point(Position.X - 2, Position.Y - 2);
        }

        #endregion
    }
}