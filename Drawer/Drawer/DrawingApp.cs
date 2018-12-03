using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drawer {
    class DrawingApp {
        private static int[,] canvas;
        private static int left = 10;
        private static int top = 4;

        public static ConsoleColor[] Colors = {ConsoleColor.White, ConsoleColor.Black,
            ConsoleColor.Green,ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Blue,
            ConsoleColor.Magenta, ConsoleColor.DarkCyan, ConsoleColor.DarkYellow,
            ConsoleColor.DarkGray};

        private static int currentColor = 0;

        public static void Draw() {
            PrintWelcomeScreen();
        }

        public static void DrawLoop(int currentColor) {
            DrawingApp.currentColor = currentColor;
            ConsoleKeyInfo cursor = Console.ReadKey();
            if (cursor.Key == ConsoleKey.UpArrow) {
                canvas[top - 4, left - 10] = currentColor;
                Console.SetCursorPosition(left, !(top == 4) ? --top : top = 28);
                DrawLoop(currentColor);
            }
            else if (cursor.Key == ConsoleKey.DownArrow) {
                canvas[top - 4, left - 10] = currentColor;
                Console.SetCursorPosition(left, !(top == 28) ? ++top : top = 4);
                DrawLoop(currentColor);
            }
            else if (cursor.Key == ConsoleKey.LeftArrow) {
                canvas[top - 4, left - 10] = currentColor;
                Console.SetCursorPosition(!(left == 10) ? --left : left = 109, top);
                DrawLoop(currentColor);
            }
            else if (cursor.Key == ConsoleKey.RightArrow) {
                canvas[top - 4, left - 10] = currentColor;
                Console.SetCursorPosition(!(left == 109) ? ++left : left = 10, top);
                DrawLoop(currentColor);
            }

            switch (cursor.Key) {
                case ConsoleKey.D0:
                    KeyParameters(0);
                    break;
                case ConsoleKey.D1:
                    KeyParameters(1);
                    break;
                case ConsoleKey.D2:
                    KeyParameters(2);
                    break;
                case ConsoleKey.D3:
                    KeyParameters(3);
                    break;
                case ConsoleKey.D4:
                    KeyParameters(4);
                    break;
                case ConsoleKey.D5:
                    KeyParameters(5);
                    break;
                case ConsoleKey.D6:
                    KeyParameters(6);
                    break;
                case ConsoleKey.D7:
                    KeyParameters(7);
                    break;
                case ConsoleKey.D8:
                    KeyParameters(8);
                    break;
                case ConsoleKey.D9:
                    KeyParameters(9);
                    break;
                case ConsoleKey.S:
                    SaveUnder();
                    break;
                case ConsoleKey.M:
                    PrintWelcomeScreen();
                    break;
                case ConsoleKey.F:
                    FlipColors();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
        }

        private static void FlipColors() {
            bool colorExists = false;
            Console.SetCursorPosition(10,31);
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = Colors[0];

            Console.Write("Please enter the number of the color you wish to flip: ");
            string cursor = Console.ReadLine();
            if (int.TryParse(cursor, out int num) && num > -1 && num < 10) {
                Console.SetCursorPosition(10,31);
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = Colors[0];

                Console.Write("Please enter the number of the color you would like to flip to: ");
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int flip) && num > -1 && num < 10) {
                    for (int i = 0; i < 25; i++) {
                        for (int j = 0; j < 100; j++) {
                            if (canvas[i, j] == num) {
                                canvas[i, j] = flip;
                                colorExists = true;
                            }
                        }
                    }

                    if (!colorExists) {
                        Console.SetCursorPosition(10, 32);
                        Console.WriteLine("Please choose a color that is currently on your canvas: ");
                        Thread.Sleep(1000);
                        Console.SetCursorPosition(10, 32);
                        Console.WriteLine("                                                                  ");
                        FlipColors();
                    }
                    else {
                        Console.SetCursorPosition(10, 31);
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("                                                                      ");
                        DrawCanvas();
                    }
                }
                else {
                    Console.SetCursorPosition(10, 32);
                    Console.WriteLine("Please enter a number between 0 and 9: ");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(10, 32);
                    Console.WriteLine("                                                                      ");
                    FlipColors();
                }
            }
            else {
                Console.SetCursorPosition(10, 32);
                Console.WriteLine("Please enter a number between 0 and 9: ");
                Thread.Sleep(1000);
                Console.SetCursorPosition(10, 32);
                Console.WriteLine("                                                                        ");
                FlipColors();
            }
        }

        private static void KeyParameters(int n) {
            currentColor = n;
            Console.BackgroundColor = Colors[n];
            Console.ForegroundColor = Colors[n];
            canvas[top - 4, left - 10] = n;
            Console.SetCursorPosition(left, top);
            Console.Write(" ");
            Console.SetCursorPosition(left, top);
            DrawLoop(currentColor);
        }

        private static void SaveUnder()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("What name should this drawing be saved under?: ");
            string name = Console.ReadLine();
            Canvas myCanvas = new Canvas(name, canvas);
            if (!DrawingFileIO.GetExistingNames().Contains(myCanvas.Name))
                DrawingFileIO.SaveCanvas(myCanvas, false);
            else
            {
                Console.WriteLine($"There is already a drawing named {myCanvas.Name}.\nType 'n' to try a different name " +
                    "or 'r' to replace the existing drawing.");
                ConsoleKeyInfo choice = Console.ReadKey();
                if (choice.Key == ConsoleKey.N)
                {
                    SaveUnder();
                }
                else if (choice.Key == ConsoleKey.R)
                {
                    DrawingFileIO.SaveCanvas(myCanvas, true);
                }
            }
            Console.WriteLine("\nDrawing Saved.");
            Thread.Sleep(900);
            PrintWelcomeScreen();
        }

        private static void NewDrawing() {
            // Draw purple background
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < 34; i++) {
                for (int j = 0; j < 120; j++) {
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            // Draw canvas
            canvas = new int[25, 100];
            for (int i = 0; i < 25; i++) {
                for (int j = 0; j < 100; j++) {
                    canvas[i, j] = 0;
                }
            }
            DrawCanvas();
        }

        private static void DrawCanvas() {
            Console.SetCursorPosition(10, 2);
            Console.Write("Options: S-Save, M-Menu, F-Flip Color, Esc-Close Program");
            
            Console.SetCursorPosition(10, 4);
            Console.CursorVisible = false;
            for (int i = 0; i < 25; i++) {
                Console.CursorLeft = 10;
                for (int j = 0; j < 100; j++) {
                    switch (canvas[i, j]) {
                        case 0:
                            Console.BackgroundColor = Colors[0];
                            Console.ForegroundColor = Colors[0];
                            break;
                        case 1:
                            Console.BackgroundColor = Colors[1];
                            Console.ForegroundColor = Colors[1];
                            break;
                        case 2:
                            Console.BackgroundColor = Colors[2];
                            Console.ForegroundColor = Colors[2];
                            break;
                        case 3:
                            Console.BackgroundColor = Colors[3];
                            Console.ForegroundColor = Colors[3];
                            break;
                        case 4:
                            Console.BackgroundColor = Colors[4];
                            Console.ForegroundColor = Colors[4];
                            break;
                        case 5:
                            Console.BackgroundColor = Colors[5];
                            Console.ForegroundColor = Colors[5];
                            break;
                        case 6:
                            Console.BackgroundColor = Colors[6];
                            Console.ForegroundColor = Colors[6];
                            break;
                        case 7:
                            Console.BackgroundColor = Colors[7];
                            Console.ForegroundColor = Colors[7];
                            break;
                        case 8:
                            Console.BackgroundColor = Colors[8];
                            Console.ForegroundColor = Colors[8];
                            break;
                        case 9:
                            Console.BackgroundColor = Colors[9];
                            Console.ForegroundColor = Colors[9];
                            break;
                            
                    }
                    Console.Write(canvas[i, j]);
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            ListColors();
            
            Console.CursorVisible = true;
            Console.SetCursorPosition(left, top);
            
            KeyParameters(0);
        }
        
        /// <summary>
        /// Prints a list of colors in the following format:
        /// "Colors: 0-White 1-Black, 2-Green 3-Yellow 4-Red 5-Blue 6-Magenta 7-Cyan 8-Orange"
        /// </summary>
        private static void ListColors() {
            Console.SetCursorPosition(10, 30);
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = Colors[0];
            Console.Write("Colors: ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 0-");
            Console.BackgroundColor = Colors[0];
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 1-");
            Console.BackgroundColor = Colors[1];
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 2-");
            Console.BackgroundColor = Colors[2];
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 3-");
            Console.BackgroundColor = Colors[3];
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 4-");
            Console.BackgroundColor = Colors[4];
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 5-");
            Console.BackgroundColor = Colors[5];
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 6-");
            Console.BackgroundColor = Colors[6];
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 7-");
            Console.BackgroundColor = Colors[7];
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 8-");
            Console.BackgroundColor = Colors[8];
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 9-");
            Console.BackgroundColor = Colors[9];
            Console.Write(" ");
        }

        private static void UploadDrawing() {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            if (DrawingFileIO.GetExistingNames().Count == 0) { 
                Console.WriteLine("There are currently no drawings saved, please create a new one.");
                Thread.Sleep(2000);
                PrintWelcomeScreen();
            }
            else
            {
                Console.WriteLine("Please choose the number corresponding to the drawing you wish to upload:");
                int counter = 1;
                var drawingNames =
                    from n in DrawingFileIO.GetExistingNames()
                    select n;
                foreach (string v in drawingNames) {
                    Console.WriteLine($"{counter++:00}: {v}");
                }

                string choice = Console.ReadLine();
                if(int.TryParse(choice, out int num) && num > 0 && num <= DrawingFileIO.GetExistingNames().Count)
                {
                    IList<string> names = DrawingFileIO.GetExistingNames();
                    Canvas drawing = DrawingFileIO.GetCanvas(names[num-1]);
                    PrintChosenCanvas(drawing);
                }
                else
                {
                    Console.WriteLine($"Please enter a number between 1-{DrawingFileIO.GetExistingNames().Count}.");
                    Thread.Sleep(1200);
                    UploadDrawing();
                }
            }
            
        }

        private static void PrintChosenCanvas(Canvas drawing)
        {
            Console.Clear();
            canvas = drawing.Draft;
            // Draw purple background
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < 34; i++) {
                for (int j = 0; j < 120; j++) {
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            DrawCanvas();
        }

        private static void PrintWelcomeScreen() {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.SetCursorPosition(15, 4);
            Console.WriteLine("Welcome to the Drawing App !");
            Console.CursorLeft = 10;
            Console.WriteLine("Options: ");
            Console.CursorLeft = 10;
            Console.WriteLine("1. Read instructions. ");
            Console.CursorLeft = 10;
            Console.WriteLine("2. Resume / edit existing drawing. ");
            Console.CursorLeft = 10;
            Console.WriteLine("3. Create a new drawing. ");
            Console.CursorLeft = 10;
            Console.Write("Please enter your choice here: ");

            string choice = Console.ReadLine();
            if (int.TryParse(choice, out int result) && result > 0 && result < 4) {
                Console.Clear();
                
                if (result == 1)
                    PrintInstructions();
                if (result == 2)
                    UploadDrawing();
                if (result == 3)
                    NewDrawing();
            }
            else {
                Console.SetCursorPosition(10, 11);
                Console.Write("Please enter a number between 1-3.");
                Thread.Sleep(1500); // allow the user to read message before it disappears
                Console.SetCursorPosition(10, 11);
                Console.Write("                                          ");
                PrintWelcomeScreen();
            }
        }

        private static void PrintInstructions() {
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetCursorPosition(10, 4);
            Console.WriteLine("Instruction:");
            Console.CursorLeft = 10;
            Console.WriteLine("Once you either resume an existing drawing or start your own new one, ");
            Console.CursorLeft = 10;
            Console.WriteLine("the cursor will be place within the canvas area (where you can draw). ");
            Console.CursorLeft = 10;
            Console.Write($"Use the {(char)8593} to move the cursor upwards, {(char)8595} downwards ");
            Console.Write($"{(char)8592} or {(char)8594} to move it");
            Console.CursorLeft = 10;
            Console.WriteLine("left or right respectively.");
            Console.SetCursorPosition(10, 9);
            Console.WriteLine("Once the cursor is positioned in a certain place, make sure to select");
            Console.CursorLeft = 10;
            Console.WriteLine("the number corresponding to the color you wish to use from the number-");
            Console.CursorLeft = 10;
            Console.WriteLine("color index found on the drawing screen, below the drawing.");
            Console.SetCursorPosition(10, 13);
            Console.WriteLine("Once you press a number, that same square the cursor is positioned on will");
            Console.CursorLeft = 10;
            Console.WriteLine("be colored in the desired color.");
            Console.CursorLeft = 10;
            Console.WriteLine("A menu above the drawing will allow you to either save the drawing,");
            Console.CursorLeft = 10;
            Console.WriteLine("return to the main menu, or exit the program.");
            Console.SetCursorPosition(10, 19);
            Console.WriteLine("Please press any key to return to the main menu.");

            while (!Console.KeyAvailable)  // wait for user
            {
                Thread.Sleep(100);
            }

            // Clears the console before going back to the main menu.
            Console.Clear();
            
            PrintWelcomeScreen();
        }
    }
}


