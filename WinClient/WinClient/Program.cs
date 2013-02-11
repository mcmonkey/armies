using System;

namespace WinClient
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Armies game = new Armies())
            {
                game.Run();
            }
        }
    }
#endif
}

