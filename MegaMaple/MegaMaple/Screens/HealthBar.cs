using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MegaMaple.Screens
{
    public class HealthBar
    {
        public Texture2D healthbarBackground;
        public Texture2D healthbarComponent;
        public Texture2D healthbarComponent2;
        public Texture2D healthbarComponent3;
        public int health = 28;
        private int healthPerBar = 28;
        public Vector2 Position;
        

        public HealthBar(int health)
        {
            this.health = health;
            this.healthbarBackground = Statics.CONTENT.Load<Texture2D>("Textures/lifebar");
            this.healthbarComponent = Statics.CONTENT.Load<Texture2D>("Textures/lifebarpart");
            this.healthbarComponent2 = Statics.CONTENT.Load<Texture2D>("Textures/lifebarpart2");
            this.healthbarComponent3 = Statics.CONTENT.Load<Texture2D>("Textures/lifebarpart3");
            this.Position = new Vector2(740, 40);            
        }

        public void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.healthbarBackground, this.Position, Color.White);

            //draw first lifebar, color scheme same as megaman zero series
            for (int i = 0; i < health; i++)
            {
                if (i < 28)
                {
                    Statics.SPRITEBATCH.Draw(this.healthbarComponent,
                        new Vector2(Position.X + 12, Position.Y + 152 + (-i * this.healthbarComponent.Height))
                        , Color.White);
                }
            }

            //draw second health bar
            for (int i = 0; i < health - healthPerBar; i++)
            {
                if (i < 28)
                {
                    Statics.SPRITEBATCH.Draw(this.healthbarComponent2,
                        new Vector2(Position.X + 12, Position.Y + 152 + (-i * this.healthbarComponent2.Height))
                        , Color.White);
                }
            }

            //draw third lifebar
            for (int i = 0; i < health - 2 * healthPerBar; i++)
            {
                Statics.SPRITEBATCH.Draw(this.healthbarComponent3,
                    new Vector2(Position.X + 12, Position.Y + 152 + (-i * this.healthbarComponent2.Height))
                    , Color.White);
            }
        }
    }
}
