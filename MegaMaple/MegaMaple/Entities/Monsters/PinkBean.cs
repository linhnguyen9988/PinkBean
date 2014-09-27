using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MegaMaple.Entities.Monsters
{
    class PinkBean : Monster
    {
        /*~PinkBean()
        {
            bigbang.Dispose();
            damagereflect.Dispose();
            death.Dispose();
            genesis.Dispose();
            musicnote.Dispose();
            quake.Dispose();


            bigbang= null;
            damagereflect=null;
            death=null;
            genesis = null;
            musicnote = null;
            quake = null;
        }*/

        //Pink Bean stuff
        public enum State
        {
            IDLE,
            DAMAGE_REFLECT,
            BIG_BANG,
            MUSIC_NOTE,
            GENESIS,
            BEAN_QUAKE,
            DEAD
        }

        public State currentState = State.IDLE;
        int amountOfAttacks = 5; //amount of attacks I've coded for pink bean
        bool currentlyAttacking = false;

        Random r = new Random();
        Rectangle originalHitbox = new Rectangle(600, 390, 97, 87);
        //amount of pixels to move the hitbox and damage reflect icon during the damage reflect animation
        int[] damageReflectHitboxOffsets = { -1, -2, -3, -4, -3, -2, -6, -7, -8, -9, -11, -8, -8, -7, -6, -4, 0, 0, 0, 0, 0 };

        //Animations
        int currentFrame = 0;
        double frameUpdateTime = 0;
        int updateFrameInterval = 120;

        Vector2 buffIconLocation = new Vector2(620, 350); //point to draw buff icons at
        List<Texture2D> idleFrames = new List<Texture2D>(); //pink bean idle animation

        //Damage Reflect
        List<Texture2D> damageReflectFrames = new List<Texture2D>();
        Texture2D damageReflectIcon;
        int damageReflectXOffset = -75; //amount of pixels to move the pink bean sprite so it's centered on the original position

        //Big Bang
        List<Texture2D> bigBangFrames = new List<Texture2D>();
        int bigBangXOffset = -210;
        //amount of pixels to move the sprite downward during the big bang animation
        int[] bigBangYOffsets = { 0, 0, 0, 0, 0, 0, 0, 0, 57, 59, 57, 51, 44, 32, 13, 0, 0, 50, 50, 50, 45, 45, 30, 0, 0 };
        int bigBangRange = 200;
        int bigBangDamage = 10;

        //Falling Music Note Attack
        List<Texture2D> megaphoneFrames = new List<Texture2D>();
        //amount of pixels to move the sprite downward during the music note casting animation
        int[] megaphoneYOffsets = { 7, 9, 10, 11, 7, 8, 9, 46, 47, 41, 35, 25, 11, 
                                      29, 36, 49, 52, 16, 12, 13,
                                      0 };
        int megaphoneXOffset = -210;

        List<Texture2D> musicNoteFrames = new List<Texture2D>();
        Vector2 musicNoteLocation1 = new Vector2(620, 350); //point to draw the first falling music note at
        int musicNoteRange = 250;
        int musicNoteDamage = 10;

        //Genesis
        List<Texture2D> pbCastGenesisFrames = new List<Texture2D>();

        //amount of pixels to move the frames so pink bean is in the same place as the hitbox
        const int randomAssMagicNumber1 = 37;
        const int randomAssMagicNumber2 = 88;
        //Offsets ripped from the .wz files again, thank god I learned how to do that instead of hardcoding everything
        int[] genesisCastingXOffsets = {112-randomAssMagicNumber1, 114-randomAssMagicNumber1, 116-randomAssMagicNumber1, 135-randomAssMagicNumber1, 133-randomAssMagicNumber1, 126-randomAssMagicNumber1, 119-randomAssMagicNumber1, 119-randomAssMagicNumber1, 119-randomAssMagicNumber1, 119-randomAssMagicNumber1, 168-randomAssMagicNumber1, 178-randomAssMagicNumber1, 181-randomAssMagicNumber1, 183-randomAssMagicNumber1, 123-randomAssMagicNumber1,
                                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] genesisCastingYOffsets = {128, 130, 130, 130, 130, 130, 131, 138, 145, 155, 202, 191, 165, 169, 120,
                                randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2, randomAssMagicNumber2 };

        List<Texture2D> genesisEffectFrames = new List<Texture2D>();
        //offsets ripped directly from MapleStory's .wz files
        int[] genesisEffectYOffsets = { 0,18,19,20,24,29,31,35,37,80,785,785,785,785,785,785,785,785,785,785,785,785,785,771,464,17,12,12};
        int[] genesisEffectXOffsets = { 0,331,337,338,348,351,354,359,358,357,358,389,387,391,397,397,396,398,400,402,382,356,355,130,155,110,107,107};
        Vector2 genesisEffectOrigin = new Vector2(620, 350); //point to draw genesis at
        int genesisRange = 400;
        int genesisDamage = 14;

        //Bean Quake (attack where the mini pink beans shoot out of the ground)
        List<Texture2D> castBeanQuakeFrames = new List<Texture2D>();
        //amount of pixels to move pink bean to the right so it is in the same place as the hitbox
        const int a = 37;
        int[] castBeanQuakeXOffsets = { 42 -a, 44-a, 45-a, 45-a, 46-a, 46-a, 47-a, 71-a, 72-a, 74-a, 76-a, 77-a, 80-a, 82-a, 42-a, 38-a, 38-a, 38-a, 38-a, 38-a };
        int[] castBeanQuakeYOffsets = { 92, 93, 93, 92, 92, 91, 91, 194, 310, 315, 318, 316, 317, 90, 90, 88, 88, 88, 88, 88 };

        List<Texture2D> beanQuakeEffectFrames = new List<Texture2D>();
        Vector2 beanQuakeEffectOrigin = new Vector2(0, 350); //point to draw the first mini bean at
        int beanQuakeEffectDistance = 100; //distance between each mini bean shooting out of the ground

        int[] beanQuakeXOffsets = { 53, 53, 53, 53, 53, 53, 53, 53, 45, 46, 47, 48, 50, 51, 53, 53, 53, 53, 53, 53};
        int[] beanQuakeYOffsets = { 12, 12, 12, 12, 12, 12, 12, 12, 68, 145, 149, 152, 153, 152, 12, 12, 12, 12, 12, 12 };

        //Death animation
        List<Texture2D> deathFrames = new List<Texture2D>();
        int[] deathAnimationXOffsets = {3, -4, -4, -8, -10, -10, -10, -10, -10, 10, 27, 10, 27, 10, 27, 
                             10, 27, 10, 27, 10, 27, 10, 27, 10, 27, 10, 27, 8, 8, 9, -14, -11, -32,
                             -71, -72, -70, -71, -74, -74, -74, -73, -72, -70, -71, -73, -71, -72, -73, 
                             -72, -71, -72, -76, -78, -79, -80 };
        int[] deathAnimationYOffsets = {93, 89, 88, 88, 88, 87, 85, 85, 85, 91, 89, 91, 89, 91, 89, 91,
                             89, 91, 89, 91, 89, 91, 89, 91, 89, 91, 89, 85, 85, 89, 92, 93, 101, 100,
                             103, 100, 100, 100, 102, 99, 100, 101, 99, 99, 99, 97, 99, 100, 100, 97, 96,
                             94, 93, 87, 85 };   
        //End of animations

        // Audio objects
        SoundEffect bigbang;
        SoundEffect damagereflect;
        SoundEffect death;
        SoundEffect genesis;
        SoundEffect musicnote;
        SoundEffect quake;

        Audio audio;

        public PinkBean()
        {
            this.maxHealth = 84;

            loadContent();
            audio = new Audio(Statics.CONTENT);
            this.Position = new Vector2(600, Util.CenterSpriteYOffset(this.Sprite.Height));
            this.hitbox = originalHitbox;
            this.healthbar = new Screens.HealthBar(this.health);
            this.OriginalPosition = Position;
        }

        public override void loadContent()
        {
            //load sprites
            this.Sprite = Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Idle/frame_000");
            this.damageReflectIcon = Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Attacks/DamageReflect/Deflect_Missiles");

            int amountOfFramesIdle = 5;
            int amountOfFramesDR = 19;
            int amountOfFramesBB = 23;
            int amountOfFramesMegaphone = 19;
            int amountOfFramesGenesis = 14;
            int amountOfFramesGenesisEffect = 27;
            int amountOfFramesBeanQuake = 19;
            int amountOfFramesBeanQuakeEffect = 19;
            int amountOfFramesDeath = 54;

            for (int i = 0; i <= amountOfFramesIdle; i++)
            {
                idleFrames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Idle/frame_00" + i));
            }

            for (int i = 0; i <= amountOfFramesDR; i++)
            {
                damageReflectFrames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Attacks/DamageReflect/frame_" + i));
            }

            for (int i = 0; i <= amountOfFramesBB; i++)
            {
                bigBangFrames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Attacks/BigBang/frame_" + i));
            }

            for (int i = 0; i <= amountOfFramesGenesis; i++)
            {
                pbCastGenesisFrames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Attacks/Genesis/CastingAnimation/" + i));
            }

            for (int i = 0; i <= amountOfFramesGenesisEffect; i++)
            {
                genesisEffectFrames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Attacks/Genesis/GenesisEffect/" + i));
            }

            addFillerFrames(amountOfFramesGenesisEffect, pbCastGenesisFrames);

            for (int i = 0; i <= amountOfFramesMegaphone; i++)
            {
                megaphoneFrames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Attacks/MusicNote/Megaphone/frame_" + i));
                musicNoteFrames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Attacks/MusicNote/MusicNote/frame_" + i));
            }

            for (int i = 0; i <= amountOfFramesBeanQuake; i++)
            {
                castBeanQuakeFrames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Attacks/BeanQuake/CastingAnimation/" + i));
            }

            for (int i = 0; i <= amountOfFramesBeanQuakeEffect; i++)
            {
                beanQuakeEffectFrames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Attacks/BeanQuake/Effect/" + i));
            }

            for (int i = 0; i <= amountOfFramesDeath; i++)
            {
                deathFrames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/DeathAnimation/" + i));
            }

            //load sounds
            /*bigbang = Statics.CONTENT.Load<SoundEffect>("Sounds/PinkBean/bigbang");
            damagereflect = Statics.CONTENT.Load<SoundEffect>("Sounds/PinkBean/damagereflect");
            death = Statics.CONTENT.Load<SoundEffect>("Sounds/PinkBean/death");
            genesis = Statics.CONTENT.Load<SoundEffect>("Sounds/PinkBean/genesis");
            musicnote = Statics.CONTENT.Load<SoundEffect>("Sounds/PinkBean/musicnote");
            quake = Statics.CONTENT.Load<SoundEffect>("Sounds/PinkBean/quake");*/
        }

        //Adds idle animation frames to a list of animation frames, just like the .wz files
        public void addFillerFrames(int maxFrames, List<Texture2D> frames)
        {
            for (int i = frames.Count; i <= maxFrames; i++)
            {
                frames.Add(Statics.CONTENT.Load<Texture2D>("Textures/PinkBean/Idle/frame_00" + i%5));
            }
        }

        public override void Update()
        {
            updateDisplayedFrame();

            healthbar.health = this.health;

            if (health < 0 && currentState != State.DEAD)
            {
                currentState = State.DEAD;
                try
                {
                    audio.die.Play();
                }
                catch (Exception e) { }
                    //death.CreateInstance().Play();
            }

            else if (isAggroed() && !currentlyAttacking)
            {
                chooseRandomAttack();
            }
        }

        public override void Reset()
        {
            currentFrame = 0;
            currentState = State.IDLE;

            base.Reset();
            //health = 1;
        }

        public override void chooseRandomAttack()
        {
            currentFrame = 0;
            currentlyAttacking = true;

            switch (r.Next(amountOfAttacks))
            {
                case 0:
                    currentState = State.DAMAGE_REFLECT;
                    //damagereflect.CreateInstance().Play();
                    break;

                case 1:
                    currentState = State.BIG_BANG;
                    //bigbang.CreateInstance().Play();
                    break;

                case 2:
                    musicNoteLocation1.X = 50 + r.Next(Statics.GAME_WIDTH - 200);
                    currentState = State.MUSIC_NOTE;
                    //musicnote.CreateInstance().Play();
                    break;

                case 3:
                    currentState = State.GENESIS;
                    //genesis.CreateInstance().Play();
                    break;

                case 4:
                    currentState = State.BEAN_QUAKE;
                    //quake.CreateInstance().Play();
                    break;
            }
        }

        public bool withinMagicAttackRange(Player player)
        {
            return withinBigBangRange(player) || withinMusicNoteRange(player) || 
                withinGenesisRange(player) || withinBeanQuakeRange(player);
        }

        public bool withinBigBangRange(Player player)
        {
            //frame 17 is when pink bean actually casts BB
            if (currentState == State.BIG_BANG && player.Position.X > this.hitbox.Left - bigBangRange && currentFrame == 17)
            {
                return Damage(player, bigBangDamage);
            }
            return false;
        }

        public bool withinGenesisRange(Player player)
        {
            //frame 10 is when the genesis beam leaves the ground
            if (currentState == State.GENESIS && player.Position.X > this.hitbox.Center.X - genesisRange && currentFrame == 10)
            {
                return Damage(player, genesisDamage);
            }
            return false;
        }

        public bool withinMusicNoteRange(Player player)
        {
            //frame 15 is when the music note comes in contact with the floor
            if (currentState == State.MUSIC_NOTE && player.Position.X > musicNoteLocation1.X &&
                player.Position.X < musicNoteLocation1.X + musicNoteRange && currentFrame == 15)
            {
                return Damage(player, musicNoteDamage);
            }
            return false;
        }

        public bool withinBeanQuakeRange(Player player)
        {
            //frame 8 is when the mini bean leaves the ground, frame 14 is when it disappears
            if (currentState == State.BEAN_QUAKE && player.Position.X < originalHitbox.Center.X - 250 && currentFrame == 8)
            {
                return Damage(player, player.health - 1); // 1/1 attack
            }
            return false;
        }

        private List<Texture2D> getCurrentAnimationFrames()
        {
            switch (currentState)
            {
                case State.DEAD:
                    return this.deathFrames;

                case State.DAMAGE_REFLECT:
                    //move the deflect prayer icon up and down
                    this.Position.X = this.OriginalPosition.X + damageReflectXOffset;
                    this.hitbox = new Rectangle(originalHitbox.X, originalHitbox.Y + damageReflectHitboxOffsets[currentFrame]
                        , originalHitbox.Width, originalHitbox.Height);
                    return this.damageReflectFrames;

                case State.BIG_BANG:
                    this.Position.X = this.OriginalPosition.X + bigBangXOffset;
                    return this.bigBangFrames;

                case State.MUSIC_NOTE:
                    this.Position.X = this.OriginalPosition.X + megaphoneXOffset;
                    return this.megaphoneFrames;

                case State.GENESIS:
                    return this.pbCastGenesisFrames;

                case State.BEAN_QUAKE:
                    return this.castBeanQuakeFrames;

                default:
                    this.hitbox = originalHitbox;
                    this.Position.X = this.OriginalPosition.X;
                    return this.idleFrames;
            }
        }

        public void updateDisplayedFrame()
        {
            if (Statics.GAMETIME.TotalGameTime.TotalMilliseconds > frameUpdateTime + updateFrameInterval)
            {
                frameUpdateTime = Statics.GAMETIME.TotalGameTime.TotalMilliseconds;

                this.Sprite = getCurrentAnimationFrames()[currentFrame];

                switch (currentState)
                {
                    case State.DAMAGE_REFLECT:
                        this.Position.Y = Util.CenterSpriteYOffset(this.Sprite.Height);
                        break;

                    case State.DEAD:
                        this.Position.Y = Util.CenterSpriteYOffset(deathAnimationYOffsets[currentFrame]);
                        this.Position.X = originalHitbox.Left - deathAnimationXOffsets[currentFrame];
                        break;

                    case State.BIG_BANG:
                        this.Position.Y = Util.CenterSpriteYOffset(this.Sprite.Height - bigBangYOffsets[currentFrame]);
                        break;

                    case State.MUSIC_NOTE:
                        this.Position.Y = Util.CenterSpriteYOffset(this.Sprite.Height - megaphoneYOffsets[currentFrame]);
                        break;

                    case State.GENESIS:
                        this.Position.Y = Util.CenterSpriteYOffset(genesisCastingYOffsets[currentFrame]); 
                        this.Position.X = originalHitbox.Left - genesisCastingXOffsets[currentFrame];
                        break;

                    case State.BEAN_QUAKE:
                        this.Position.Y = Util.CenterSpriteYOffset(castBeanQuakeYOffsets[currentFrame]);
                        this.Position.X = originalHitbox.Left - castBeanQuakeXOffsets[currentFrame];
                        break;
                }

                if (currentFrame++ + 2 > getCurrentAnimationFrames().Count)
                {
                    if (currentState == State.DEAD)
                    {
                        Statics.amountOfPinkBeansKilled++;
                        Reset();
                    }

                    currentlyAttacking = false;
                    currentFrame = 0;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();

            if (currentState == State.DAMAGE_REFLECT)
            {
                buffIconLocation.Y = originalHitbox.Y - damageReflectIcon.Height + damageReflectHitboxOffsets[currentFrame];
                Statics.SPRITEBATCH.Draw(this.damageReflectIcon, this.buffIconLocation, Color.White);
            }

            if (currentState == State.MUSIC_NOTE)
            {
                musicNoteLocation1.Y = Util.CenterSpriteYOffset(musicNoteFrames[currentFrame].Height);
                Statics.SPRITEBATCH.Draw(musicNoteFrames[currentFrame], musicNoteLocation1, Color.White);                
            }

            if (currentState == State.GENESIS)
            {
                genesisEffectOrigin.X = originalHitbox.Center.X - genesisEffectXOffsets[currentFrame];
                genesisEffectOrigin.Y = Util.CenterSpriteYOffset(genesisEffectFrames[currentFrame].Height); 

                Statics.SPRITEBATCH.Draw(genesisEffectFrames[currentFrame], genesisEffectOrigin, Color.White);
            }

            if (currentState == State.BEAN_QUAKE)
            {
                beanQuakeEffectOrigin.X = -beanQuakeXOffsets[currentFrame];
                beanQuakeEffectOrigin.Y = Util.CenterSpriteYOffset(beanQuakeEffectFrames[currentFrame].Height);

                for (int i = 0; i < 5; i++)
                {
                    Statics.SPRITEBATCH.Draw(beanQuakeEffectFrames[currentFrame], new Vector2(beanQuakeEffectOrigin.X + beanQuakeEffectDistance * i, beanQuakeEffectOrigin.Y) , Color.White);
                }
            }
        }
    }
}
