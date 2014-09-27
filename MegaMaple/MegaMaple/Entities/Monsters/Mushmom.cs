using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MegaMaple.Entities.Monsters
{
    class Mushmom : Monster
    {
        Random r = new Random();
        double timeElapsed = 0;
        int timeToMoveRandomly = 2000;

        double frameUpdateTime = 0;
        int updateFrameDelay = 200;


        bool moveRight = false;

        List<Texture2D> movementFramesLeft = new List<Texture2D>();
        List<Texture2D> movementFramesRight = new List<Texture2D>();

        int currentFrame = 0;

        public Mushmom()
        {
            loadContent();

            this.Position = new Vector2(600, 366);
            this.healthbar = new Screens.HealthBar(this.health);
            this.OriginalPosition = Position;
        }

        public override void loadContent()
        {
            this.Sprite = Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Mushmom");
            this.hitbox = this.Bound;
            movementFramesLeft.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_000"));
            movementFramesLeft.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_001"));
            movementFramesLeft.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_002"));
            movementFramesLeft.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_003"));
            movementFramesLeft.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_004"));
            movementFramesLeft.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_005"));

            movementFramesRight.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_000f"));
            movementFramesRight.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_001f"));
            movementFramesRight.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_002f"));
            movementFramesRight.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_003f"));
            movementFramesRight.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_004f"));
            movementFramesRight.Add(Statics.CONTENT.Load<Texture2D>("Textures/Mushmom/Move/frame_005f"));
        }

        public override void MoveRandomly()
        {
            if (!isAggroed())
            {
                if (Statics.GAMETIME.TotalGameTime.TotalMilliseconds > timeElapsed + timeToMoveRandomly)
                {
                    moveRight = (r.Next(2) == 1);
                    timeElapsed = Statics.GAMETIME.TotalGameTime.TotalMilliseconds;
                }

                if (moveRight && this.Position.X < Statics.GAME_WIDTH - this.Sprite.Width)
                {
                    this.Position.X += this.movementSpeed;
                }

                else if (this.Position.X > 0)
                {
                    this.Position.X -= this.movementSpeed;
                }
            }
        }

        public void moveTo(Vector2 positionToMoveTowards)
        {
            if (this.Position.X > positionToMoveTowards.X)
            {
                this.Position.X -= this.movementSpeed;
                moveRight = false;
            }

            else if (this.Position.X < positionToMoveTowards.X - this.Sprite.Width)
            {
                this.Position.X += this.movementSpeed;
                moveRight = true;
            }
        }

        /*public override void Reset()
        {
            this.isAggroed = false;
            this.health = 28;
            this.Position = this.OriginalPosition;
        }*/

        public override void Update()
        {
            MoveRandomly();
            updateDisplayedFrame();
            this.hitbox = this.Bound;

            /*if (this.health < 28)
            {
                isAggroed = true;
            }*/
            
            healthbar.health = this.health;
        }

        public void updateDisplayedFrame()
        {
            if (Statics.GAMETIME.TotalGameTime.TotalMilliseconds > frameUpdateTime + updateFrameDelay)
            {
                frameUpdateTime = Statics.GAMETIME.TotalGameTime.TotalMilliseconds;
                if (currentFrame++ + 2 > movementFramesLeft.Count)
                {
                    currentFrame = 0;
                }

                if (moveRight)
                {
                    this.Sprite = movementFramesRight[currentFrame];
                }
                else
                {
                    this.Sprite = movementFramesLeft[currentFrame];
                }
                this.Position = new Vector2(this.Position.X, Util.CenterSpriteYOffset(this.Sprite.Height));
            }
        }

        /*public override void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.Sprite, this.Position, Color.White);
            healthbar.Draw();
        }*/
    }
}
