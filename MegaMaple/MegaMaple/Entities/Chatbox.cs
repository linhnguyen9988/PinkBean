using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MegaMaple.Entities
{
    public class Chatbox
    {
        String[] text;
        public Texture2D chatboxSprite;
        public Texture2D npcSprite;
        public Vector2 Position;
        public String npcName;
        public SpriteFont Font;

        public Chatbox(Texture2D npcSprite, String[] text, String name)
        {
            this.chatboxSprite = Statics.CONTENT.Load<Texture2D>("Textures/chatbox");
            this.npcSprite = npcSprite;
            this.Position = new Vector2(140, 180);
            this.npcName = name;
            this.text = text;
            Font = Statics.CONTENT.Load<SpriteFont>("Fonts/NpcChat");
        }

        public void setText(String[] newText)
        {
            this.text = newText;
        }

        public void Draw()
        {
            Vector2 npcSpritePosition = new Vector2(this.Position.X+42, this.Position.Y+10);
            

            Statics.SPRITEBATCH.Draw(this.chatboxSprite, this.Position, Color.White);
            Statics.SPRITEBATCH.Draw(this.npcSprite, new Vector2(this.Position.X + 40, Util.CenterChatboxPortraitSprite(this.npcSprite.Height)), Color.White);
            //Statics.SPRITEBATCH.DrawString(this.Font, this.npcName, new Vector2(this.Position.X + 65, this.Position.Y + 117)
             //   , Color.White);

            Statics.SPRITEBATCH.DrawString(this.Font, this.npcName, new Vector2(this.Position.X + 72 - 12*text.Length, this.Position.Y + 117)
                , Color.White);

            int offset = 0;

            foreach (String x in text)
            {
                Statics.SPRITEBATCH.DrawString(this.Font, x, new Vector2(this.Position.X + 160, this.Position.Y + 30 + offset)
                , Color.Black);
                offset += 15;
            }
        }
    }
}
