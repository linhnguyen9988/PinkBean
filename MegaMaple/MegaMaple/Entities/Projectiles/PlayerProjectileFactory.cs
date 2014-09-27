using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MegaMaple.Entities.Projectiles
{
    public class PlayerProjectileFactory
    {
        List<Projectile> projectiles = new List<Projectile>();
        public bool canFire = true;


        public PlayerProjectileFactory()
        {

        }

        public void FireProjectile(Vector2 origin, bool right)
        {
            bool aCreateNew = true;
            foreach (Projectile projectile in projectiles)
            {
                if (projectile.isActive == false)
                {
                    projectile.right = right;
                    aCreateNew = false;
                    projectile.isActive = true;
                    projectile.Position = origin;
                    break;
                }
            }

            if (aCreateNew == true)
            {
                Projectile proj = new PlayerProjectile();
                proj.right = right;
                proj.isActive = true;
                proj.Position = origin;
                projectiles.Add(proj);
            }
        }

        public void setPosition(int index, Vector2 origin)
        {
            projectiles[0].Position = origin;
        }

        public void Reset()
        {
            foreach (Projectile p in projectiles)
            {
                p.Position = p.OriginalPosition;
                p.isActive = false;
            }
        }

        public void Update()
        {
            foreach (Projectile p in projectiles)
            {
                if (p.Position.X > Statics.GAME_WIDTH || p.Position.X < 0)
                {
                    if (p.isActive)
                    {
                        this.canFire = true;
                    }
                    p.isActive = false;
                }
                else
                {
                    p.Update();
                }
            }
        }

        public bool Collides(Monsters.Monster monster)
        {
            foreach (Projectile p in projectiles)
            {
                if (p.Bound.Intersects(monster.hitbox))
                {
                    p.isActive = false;
                    p.Position = p.OriginalPosition;
                    return true;
                }
            }
            return false;
        }

        public void Draw()
        {
            foreach (Projectile p in projectiles)
            {
                if (p.isActive)
                {
                    p.Draw();
                }
            }
        }
    }
}
