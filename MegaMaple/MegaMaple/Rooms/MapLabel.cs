using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MegaMaple.Rooms
{
    public class MapLabel
    {
        String mapName;
        public Texture2D maplabel;
        public SpriteFont Font2;



        public MapLabel(String mapName)
        {
            this.mapName = mapName;
            this.maplabel = Statics.CONTENT.Load<Texture2D>("Textures/maplabel");
            this.Font2 = Statics.CONTENT.Load<SpriteFont>("Fonts/NpcChat");
        }

        public void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.maplabel, Vector2.Zero, Color.White);
            Statics.SPRITEBATCH.DrawString(this.Font2, mapName, new Vector2(5, 0), Color.White);
        }

    }
}
