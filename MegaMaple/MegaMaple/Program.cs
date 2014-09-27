using System;
using System.Windows.Forms;

namespace MegaMaple
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            try
            {
                using (Game game = new Game())
                    game.Run();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
#endif
}

