using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MegaMaple.Entities
{
    public class Player : Entity
    {
        public enum State
        {
            RIGHT,
            LEFT, 
            JUMPING_RIGHT,
            JUMPING_LEFT
        }

        public static State currentState;

        const int gravity = 15;
        int GroundLevel = 0;

        int ySpeed = 0;
        public int health = 28;

        bool jumping = false;  
        public bool damageMonster = false;
        public bool onPlatform = false;
        public Vector2 projectileSpawnPoint; //where a projectile appears when the player fires it

        public int projectileSpawnYOffset = -40;
        public int projectileSpawnXOffset = -35;

        public Screens.HealthBar healthbar;

        public Projectiles.PlayerProjectileFactory proj;

        Texture2D movingLeft;
        Texture2D movingRight;
        Texture2D jumpingLeft;
        Texture2D jumpingRight;
        bool right = false;
        

        public Player()
        {
            movingRight = Statics.CONTENT.Load<Texture2D>("Textures/dexless");
            movingLeft = Statics.CONTENT.Load<Texture2D>("Textures/dexlessleft");
            jumpingRight = Statics.CONTENT.Load<Texture2D>("Textures/jumpright");
            jumpingLeft = Statics.CONTENT.Load<Texture2D>("Textures/jumpleft");
            Sprite = movingRight;
            
            this.Position = new Vector2(100, Util.CenterSpriteYOffset(Sprite.Height));
            GroundLevel = Util.CenterSpriteYOffset(Sprite.Height);
            this.OriginalPosition = this.Position;
            this.projectileSpawnPoint = new Vector2(this.Position.X - projectileSpawnXOffset, this.Position.Y - projectileSpawnYOffset);
            this.proj = new Projectiles.PlayerProjectileFactory();
            this.healthbar = new Screens.HealthBar(this.health);
            this.healthbar.Position = new Vector2(50, 40);
        }

        public override void Reset()
        {
            this.Position = this.OriginalPosition;
            this.jumping = false;
            this.onPlatform = false;
            this.health = 28;
        }

        public override void Update()
        {
            this.projectileSpawnPoint = new Vector2(this.Position.X - projectileSpawnXOffset, this.Position.Y - projectileSpawnYOffset);

            if (jumping)
            {
                this.Position.Y -= ySpeed;
                ySpeed--;
                jumping = !canJump();
            }

            if (canJump()) //Set idle frame if no key is pressed
            {
                if (currentState == State.RIGHT)
                {
                    Sprite = movingRight;
                }

                if (currentState == State.LEFT)
                {
                    Sprite = movingLeft;
                }
            }

            foreach (Keys key in Statics.INPUT.currentPressedKeys)
            {
                if (key == Keys.Left && this.Position.X > 0)
                {
                    this.Position.X -= 5;
                    currentState = State.LEFT;
                    
                    if (this.Position.Y < GroundLevel)
                    {
                        Sprite = jumpingLeft;
                    }
                    else
                    {
                        Sprite = movingLeft;
                    }
                }

                if (key == Keys.Right && this.Position.X < Statics.GAME_WIDTH - this.Sprite.Width)
                {
                    this.Position.X += 5;
                    currentState = State.RIGHT;
                    right = true;

                    if (this.Position.Y < GroundLevel)
                    {
                        Sprite = jumpingRight;
                    }
                    else
                    {
                        Sprite = movingRight;
                    }
                }
            }

            if (Statics.INPUT.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                Jump();
            }

            if (Statics.INPUT.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.RightControl))
            {
                proj.FireProjectile(this.projectileSpawnPoint, right);
                proj.canFire = false;
                damageMonster = true;
            }

            healthbar.health = this.health;

            if (this.health <= 0)
            {
                Statics.playerJustDied = true;
                Statics.deaths++;
            }

            proj.Update();
        }

        public void DealDamageToMonster(Monsters.Monster monster, int damage)
        {
            monster.health -= damage;
            damageMonster = false;
        }

        private bool notOutOfBounds(int offset)
        {
            return this.Position.X < Statics.GAME_WIDTH && this.Position.X > 0;
        }

        public void Jump()
        {
            if (canJump())
            {
                ySpeed = gravity;
                jumping = true;
            }
        }

        public bool canJump()
        {
            return onPlatform || this.Position.Y == GroundLevel;
        }

        public override void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.Sprite, this.Position, Color.White);
            healthbar.Draw();
        }
    }
}
