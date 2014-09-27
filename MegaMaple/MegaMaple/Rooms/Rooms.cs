using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MegaMaple.Rooms;

namespace MegaMaple.Rooms
{
    public class Rooms
    {
        Room[] rooms;
        Room currentRoom;

        public Rooms()
        {
            rooms = new Room[2]{new PinkBeanRoom(), new MushmomRoom()};
        }

        public Room getCurrentRoom()
        {
            foreach(Room room in rooms)
            {
                if (room.Validate())
                {
                    if (room != currentRoom)
                    {
                        room.Reset();
                        currentRoom = room;
                        currentRoom.PlayRoomMusic();
                    }
                    return room;
                }
            }
            return rooms[0];
        }
    }
}
