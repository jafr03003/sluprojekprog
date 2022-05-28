using System;
using kys;

namespace kys
{
    public static class Program
    {
        [STAThread]
        static void Main() 
        {
            using (var game = new Game2())   
                game.Run();  
        }
    }
}
