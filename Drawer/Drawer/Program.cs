using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(122, 35);
            Console.ForegroundColor = ConsoleColor.White;
            DrawingApp.Draw();
            int[,] draft = new int[25,100];
            for(int i = 0; i < draft.GetLength(0); i++)
            {
                for(int j = 0; j < draft.Length / draft.GetLength(0); j++)
                {
                    draft[i, j] = 3;
                }
            }
            Canvas canvas = new Canvas("Daniel", draft);
            //DrawingFileIO.SaveCanvas(canvas);

            int[,] draft2 = new int[25, 100];
            for (int i = 0; i < draft.GetLength(0); i++)
            {
                for (int j = 0; j < draft.Length / draft.GetLength(0); j++)
                {
                    draft2[i, j] = 2;
                }
            }
            Canvas canvas2 = new Canvas("Ethan", draft2);
            //DrawingFileIO.SaveCanvas(canvas2);
            //DrawingFileIO.SaveCanvas(canvas2);
            //Console.WriteLine("done");

            //Canvas? myCanvas = DrawingFileIO.GetCanvas("Ethan");
            //canvas.PrintDrawing();
        }
    }
}
