using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MegaMaple.Entities;
using Microsoft.Xna.Framework.Media;

namespace MegaMaple.Rooms
{
    public abstract class Room
    {
        public Entities.Entity[] roomNPCs;
        public Texture2D background;
        public Player player;
        public Entities.Projectiles.PlayerProjectileFactory proj;
        public MapLabel maplabel;
        

        public virtual void Update() {
            if (proj != null)
            {
                UpdateProjectiles();
            }
            UpdateMonsters();
            UpdateNPCs();
            UpdatePlayers();
        }

        public virtual void PlayRoomMusic() { } 

        public virtual void UpdateMonsters() { }

        public void UpdateTransition()
        {

        }

        public void UpdateProjectiles()
        {
            if (Statics.INPUT.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.RightControl))
            {
                proj.FireProjectile(this.player.projectileSpawnPoint, Player.currentState == Player.State.RIGHT);
                proj.canFire = false;
            }
            proj.Update();
        }

        public void UpdatePlayers()
        {
            player.Update();
        }

        public void UpdateNPCs()
        {
            foreach (Entity e in roomNPCs)
            {
                e.Update();
            }
        }

        public virtual void deathHandler()
        {
            returnToMainRoom();
        }

        public void returnToMainRoom()
        {
            Statics.inMainRoom = true;
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
        }

        public virtual void Draw() { }

        public virtual bool Validate() { return false; } //return true if conditions for being in room

        public virtual void Reset() {
            foreach (Entity e in roomNPCs)
            {
                e.Reset();
            }
            if (proj != null)
            {
                proj.Reset();
            }
            player.Reset();
        }
    }
}