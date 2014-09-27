using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MegaMaple
{
    public class Statics
    {
        public static int GAME_WIDTH = 800;
        public static int GAME_HEIGHT = 600;

        public static String GAME_TITLE = "MegaMaple";

        public static Random RANDOM = new Random();

        public static GameTime GAMETIME;
        public static SpriteBatch SPRITEBATCH;
        public static ContentManager CONTENT;
        public static GraphicsDevice GRAPHICSDEVICE;

        public static Texture2D PIXEL;

        public static Managers.InputManager INPUT;

        //public static bool DEBUG = false;

        public static bool chatting = false;
        public static int currentBoss = 1;
        public static bool inMainRoom = true;
        public static int deaths = 0;
        public static bool playerJustDied = false;
        //public static int GroundLevel = 404; //how many pixels 

        public static int amountOfPinkBeansKilled = 0;
    }
}
