using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MegaMaple.Entities.Monsters
{
    public abstract class Monster : Entity
    {
        public int health = 28;
        public int maxHealth = 28;
        public int touchDamage = 5;
        public Screens.HealthBar healthbar;
        //public Boolean isAggroed = false;
        public int movementSpeed = 2;
        public int damageCooldown = 2000;
        public double c = 0;
        public Rectangle hitbox;

        public virtual void MoveRandomly() { }

        public virtual void chooseRandomAttack() { }

        public virtual bool TouchDamage(Player player)
        {
            if (this.hitbox.Intersects(player.Bound))
            {
                return Damage(player, this.touchDamage);
            }
            return false;
        }

        public virtual bool isAggroed()
        {
            return health < maxHealth;
        }

        public bool Damage(Player player, int damage)
        {
            if (Statics.GAMETIME.TotalGameTime.TotalMilliseconds > c + damageCooldown)
            {
                player.health -= damage;
                c = Statics.GAMETIME.TotalGameTime.TotalMilliseconds;
                return true;
            }
            return false;
        }

        public override void Reset()
        {
            //this.isAggroed = false;
            this.health = maxHealth;
            this.Position = this.OriginalPosition;
        }

        public override void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.Sprite, this.Position, Color.White);
            healthbar.Draw();
        }

        public virtual bool withinMagicAttackRange()
        {
            return false;
        }
    }
}
