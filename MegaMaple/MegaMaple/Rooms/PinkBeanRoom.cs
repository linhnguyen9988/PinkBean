using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MegaMaple.Entities.Monsters;
using MegaMaple.Entities;
using Microsoft.Xna.Framework.Media;

namespace MegaMaple.Rooms
{
    class PinkBeanRoom : Room
    {
        ~PinkBeanRoom()
        {
            FightingPinkBeen.Dispose();
        }

        Song FightingPinkBeen = Statics.CONTENT.Load<Song>("Sounds/FightingPinkBeen");

        PinkBean pb;
        public SpriteFont Font;
        public Chatbox playerDeath;
        public String[] DeathText = { "You have died.", "Press Y to continue." };
        bool ShowDeathChatbox = false;


        public PinkBeanRoom()
        {
            Font = Statics.CONTENT.Load<SpriteFont>("Fonts/NpcChat");
            pb = new PinkBean();
            playerDeath = new Chatbox(pb.Sprite, DeathText, "Pink Bean");

            this.maplabel = new MapLabel("Twilight of Gods");
            this.proj = new Entities.Projectiles.PlayerProjectileFactory();

            this.background = Statics.CONTENT.Load<Texture2D>("Textures/pinkbeanbackground");
            this.roomNPCs = new Entity[1] { pb };
            this.player = new Player();
        }

        public override void PlayRoomMusic()
        {
            MediaPlayer.Play(FightingPinkBeen);
        }

        public override void Update()
        {
            if (ShowDeathChatbox)
            {
                if (Statics.INPUT.isKeyDown(Microsoft.Xna.Framework.Input.Keys.Y))
                {
                    Statics.amountOfPinkBeansKilled = 0;
                    ShowDeathChatbox = false;
                    
                    Reset();
                }
            }
            else
            {
                base.Update();
            }
        }

        public override void UpdateMonsters()
        {
            if (this.proj.Collides(pb))
            {
                if (pb.currentState == PinkBean.State.DAMAGE_REFLECT)
                {
                    player.health--;
                    
                    if (player.health <= 0)
                    {
                        ShowDeathChatbox = true;
                        //returnToMainRoom();
                    }
                }

                else
                {
                    player.DealDamageToMonster(pb, 1);
                    if (pb.health <= 0)
                    {
                        //returnToMainRoom();
                    }
                }
            }

            if (pb.TouchDamage(player) || pb.withinMagicAttackRange(player))
            {
                if (player.health <= 0)
                {
                    ShowDeathChatbox = true;
                    //returnToMainRoom();
                }
            }
        }

        public override bool Validate()
        {
            return true;
        }

        public override void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.background, Vector2.Zero, Color.White);
            
            maplabel.Draw();
            proj.Draw();

            

            foreach (Entity e in roomNPCs)
            {
                e.Draw();
            }

            if (ShowDeathChatbox)
            {
                playerDeath.Draw(); 
            }
            player.Draw();



            Statics.SPRITEBATCH.DrawString(this.Font, "Pink Beans killed: " + Statics.amountOfPinkBeansKilled, new Vector2(5, 540)
                , Color.Black);
        }
    }
}
