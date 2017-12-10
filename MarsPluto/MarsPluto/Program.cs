using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;


namespace MarsPluto
{
    class Program
    {
        public static void Main(string[] args)
        {
            try {
                MainGame game = new MainGame(700, 400);
                game.Run();
   
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
            }
        }
    }
}
