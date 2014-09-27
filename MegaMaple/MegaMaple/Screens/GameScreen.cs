using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace MegaMaple.Screens
{
    public class GameScreen : Screen
    {   
        public Rooms.Rooms rooms;
        //public Entities.Player player;

        public GameScreen()
        {

        }

        public override void LoadContent()
        {
            Reset();
            base.LoadContent();
        }

        public void Reset()
        {
            //player = new Entities.Player();
            rooms = new Rooms.Rooms();
        }

        public override void Update()
        {
            rooms.getCurrentRoom().Update();
            //player.Update();
            

            base.Update();
        }

        public override void Draw()
        {
            Statics.SPRITEBATCH.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null);

            rooms.getCurrentRoom().Draw();
            //player.Draw();

            Statics.SPRITEBATCH.End();
            base.Draw();
        }
    }
}
