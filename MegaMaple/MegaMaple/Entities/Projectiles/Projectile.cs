using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MegaMaple.Entities.Projectiles
{
    
    public abstract class Projectile : Entity
    {
        public bool right = true;
        public bool isActive = false;
        public int moveSpeed = 10;


        public virtual void Collides(Entities.Entity entity) { }
    }
}
