using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawer
{
    struct Canvas
    {
        public string Name { get; }
        public int[,] Draft { get; }

        public Canvas(string name, int[,] draft)
        {
            Name = name;
            Draft = new int[25, 100];
            for (int i = 0; i < draft.GetLength(0); i++)
            {
                for (int j = 0; j < draft.Length / draft.GetLength(0); j++)
                {
                    Draft[i, j] = draft[i, j];
                }
            }
            
        }

        public void PrintDrawing()
        {
            for (int i = 0; i < Draft.GetLength(0); i++)
            {
                Console.SetCursorPosition(6, 5 + i);
                for (int j = 0; j < Draft.Length / Draft.GetLength(0); j++)
                {
                    Console.BackgroundColor = DrawingApp.Colors[Draft[i, j]];
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

}
