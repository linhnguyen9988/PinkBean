using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MegaMaple.Entities.Projectiles
{
    //An ilbi throwing star
    class PlayerProjectile : Projectile
    {
        
        public PlayerProjectile()
        {
            this.Sprite = Statics.CONTENT.Load<Texture2D>("Textures/ilbi");
            this.Position = new Vector2(0, 360);
        }

        public void setPosition(Player player)
        {
            this.Position = player.projectileSpawnPoint;
        }

        public override void Update()
        {
            this.Position = new Vector2(this.Position.X + this.moveSpeed * (right? 1: -1), this.Position.Y);
        }

        public override void Draw()
        {
            if (this.isActive)
            {
                Statics.SPRITEBATCH.Draw(this.Sprite, this.Position, Color.White);
            }
        }
    }
}
