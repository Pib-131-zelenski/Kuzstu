﻿#region using

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Galaxy.Core.Actors;
using Galaxy.Core.Collision;
using Galaxy.Core.Environment;
using Galaxy.Environments.Actors;

#endregion

namespace Galaxy.Environments
{
    /// <summary>
    ///   The level class for Open Mario.  This will be the first level that the player interacts with.
    /// </summary>
    public class LevelOne : BaseLevel
    {
        private int m_frameCount;

        #region Constructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="LevelOne" /> class.
        /// </summary>
        public LevelOne()
        {
            // Backgrounds
            FileName = @"Assets\LevelOne.png";

            // Enemies
            for (int i = 0; i < 1; i++)
            {
                var ship = new Ship(this);
                int positionY = ship.Height + 10;
                int positionX = 150 + i*(ship.Width + 50);

                ship.Position = new Point(positionX, positionY);

                Actors.Add(ship);
            }
            for (int i = 0; i < 3; i++)
            {
                var ship2 = new Ship2(this);
                int positionY = ship2.Height + 30;
                int positionX = 150 + i*(ship2.Width + 70);

                ship2.Position = new Point(positionX, positionY);

                Actors.Add(ship2);
            }
            // Player
            Player = new PlayerShip(this);
            int playerPositionX = Size.Width/2 - Player.Width/2;
            int playerPositionY = Size.Height - Player.Height - 50;
            Player.Position = new Point(playerPositionX, playerPositionY);
            Actors.Add(Player);
        }

        #endregion

        #region Overrides
        
        private void EnemyShip()
        {
            //пули создаются  
            Ship2[] enshipShip2s = Actors.Where(actor => actor is Ship2).Cast<Ship2>().ToArray();
            if (!IsPressed(VirtualKeyStates.Space)) return;
            {
                foreach (var ship2 in enshipShip2s)
                {
                    Actors.Add(ship2.CreatEnemyBullet(ship2));
                }
            }


            //пули, долетевшие до низа, уничтожаются
            EnemyBullet[] bullets = Actors.Where(actor => actor is EnemyBullet).Cast<EnemyBullet>().ToArray();
            foreach (var bul in bullets)
            {
                if (bul.Position.Y >= BaseLevel.DefaultHeight)
                {
                    Actors.Remove(bul);
                }
            }
        }


        private void h_dispatchKey()
        {
            if (!IsPressed(VirtualKeyStates.Space)) return;

            if (m_frameCount%10 != 0) return;


            Bullet bullet = new Bullet(this)
            {
                Position = Player.Position
            };

            bullet.Load();
            Actors.Add(bullet);
        }

        public override BaseLevel NextLevel()
        {
            return new StartScreen();
        }


        public override void Update()
        {
            m_frameCount++;
            h_dispatchKey();
            EnemyShip();
            base.Update();

            IEnumerable<BaseActor> killedActors = CollisionChecher.GetAllCollisions(Actors);

            foreach (BaseActor killedActor in killedActors)
            {
                if (killedActor.IsAlive)
                    killedActor.IsAlive = false;
            }

            List<BaseActor> toRemove = Actors.Where(actor => actor.CanDrop).ToList();
            BaseActor[] actors = new BaseActor[toRemove.Count()];
            toRemove.CopyTo(actors);

            foreach (BaseActor actor in actors.Where(actor => actor.CanDrop))
            {
                Actors.Remove(actor);
            }

            if (Player.CanDrop)
                Failed = true;

            //has no enemy
            if (Actors.All(actor => actor.ActorType != ActorType.Enemy))
                Success = true;
        }

        #endregion
    }
}