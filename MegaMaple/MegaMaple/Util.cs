using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaMaple
{
    public class Util
    {
        
        static int GroundOffset = 123; //height of the ground in the background
        static int ChatboxPortraitOffset = 305; //height of the chatbox portrait displayed

        //Centers a sprite on the ground
        public static int CenterSpriteYOffset(int spriteHeight)
        {
            return Statics.GAME_HEIGHT - spriteHeight - GroundOffset;
        }

        //Centers a sprite on the chatbox portrait window
        public static int CenterChatboxPortraitSprite(int spriteHeight)
        {
            return Statics.GAME_HEIGHT - spriteHeight - ChatboxPortraitOffset;
        }
    }
}
