using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MegaMaple.Entities.Monsters;
using MegaMaple.Entities;
using Microsoft.Xna.Framework.Media;

namespace MegaMaple.Rooms
{
    class MushmomRoom : Room
    {
        Mushmom mushmom;

        public MushmomRoom()
        {
            mushmom = new Mushmom();
            this.maplabel = new MapLabel("Someone Else's House");
            this.proj = new Entities.Projectiles.PlayerProjectileFactory();

            this.background = Statics.CONTENT.Load<Texture2D>("Textures/background");
            this.roomNPCs = new Entity[1] { mushmom};
            this.player = new Player();
        }

        public override void UpdateMonsters()
        {
            if (this.proj.Collides(mushmom))
            {
                player.DealDamageToMonster(mushmom, 5);
                if (mushmom.health <= 0)
                {
                    returnToMainRoom();
                }
            }

            if (mushmom.TouchDamage(player))
            {
                if (player.health <= 0)
                {
                    deathHandler();
                }
            }

            if (mushmom.isAggroed())
            {
                mushmom.moveTo(player.Position);
            }
        }

        public override bool Validate()
        {
            return Statics.currentBoss == 1;
        }

        public override void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.background, Vector2.Zero, Color.White);
            maplabel.Draw();
            proj.Draw();

            foreach (Entity e in roomNPCs)
            {
                e.Draw();
            }
            player.Draw();
        }
    }
}
