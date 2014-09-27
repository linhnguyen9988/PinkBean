using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MegaMaple
{
    public class Audio
    {
        private ContentManager content;
        public SoundEffect die;
        public SoundEffectInstance dieInstance;
        public Audio(ContentManager content)
        {
            this.content = content;
            die = content.Load<SoundEffect>("Sounds/PinkBean/death");
            dieInstance = die.CreateInstance();
        }

        public void playDie()
        {
            dieInstance.Play();
        }

        ~Audio()
        {
            System.Threading.Thread.Sleep(3000);
            MediaPlayer.Stop();
            dieInstance.Dispose();
                
            
            die.Dispose();
            
        }
    }
}
