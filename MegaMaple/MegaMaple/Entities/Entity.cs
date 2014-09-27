using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MegaMaple.Entities
{
    public abstract class Entity
    {
        public Texture2D Sprite;
        public Vector2 Position;
        public Vector2 OriginalPosition;

        public virtual void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.Sprite, this.Position, Color.White);
        }

        public virtual Rectangle Bound { 
            get { 
                return new Rectangle((int)this.Position.X, (int)this.Position.Y, Sprite.Bounds.Width, Sprite.Bounds.Height); 
            } 
        }

        public virtual void Update() { }
        public virtual void Reset() { }
        public virtual void loadContent() { }
    }
}
