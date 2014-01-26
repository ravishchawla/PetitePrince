using System;
//Copyright (C) Ravish Chawla 2011
namespace Petit_Prince
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PetitPrince game = new PetitPrince())
            {
                game.Run();
            }
        }
    }
#endif
}

