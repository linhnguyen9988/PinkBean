using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaMaple.Screens
{
    public abstract class Screen
    {


        public virtual void LoadContent() { }

        public virtual void Update() { }

        public virtual void Draw() { }
    }
}
