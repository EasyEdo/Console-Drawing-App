using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawer
{
    class DrawingFileIO
    {
        private static int[,] tempExample = { { 0, 1, 2 }, { 3, 4, 5 } };

        public static Canvas GetCanvas(string Name)
        {
            int[,] draft = new int[25,100];
            
            try
            {
                using (StreamReader reader = new StreamReader("res/drawings.txt"))
                {
                    String line = "";
                    while (!reader.EndOfStream && !line.Equals(Name))
                    {
                        line = reader.ReadLine();
                    }

                    if (line.Equals(Name))
                    {
                        for (int i = 0; i < draft.GetLength(0); i++)
                        {
                            line = reader.ReadLine();
                            int counter = 0;
                            for (int j = 0; j < 2 * (draft.Length / draft.GetLength(0)); j += 2)
                            {
                                draft[i, counter] = int.Parse(line[j] + "");
                                counter++;
                            }
                        }
                    } 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new Canvas(Name, draft);
        }

        public static void SaveCanvas(Canvas canvas, bool overwrite)
        {
            try
            {
                // copying from original file (drawings.txt) to a second one (drawings2.txt), and adding the new drawing to the second file.
                if (!GetExistingNames().Contains(canvas.Name) || overwrite)
                {
                    using (StreamReader reader2 = new StreamReader("res/drawings.txt"))
                    {
                        using (StreamWriter writer = new StreamWriter("res/drawings2.txt"))
                        {
                            if (!overwrite)
                            {
                                string drawingsFile = reader2.ReadToEnd();
                                writer.Write(drawingsFile);
                                writer.WriteLine($"{canvas.Name}");
                                for (int i = 0; i < canvas.Draft.GetLength(0); i++)
                                {
                                    for (int j = 0; j < canvas.Draft.Length / canvas.Draft.GetLength(0); j++)
                                    {
                                        writer.Write(canvas.Draft[i, j] + " ");
                                    }
                                    writer.WriteLine();
                                }
                            }
                            else // in case we're overwriting an old drawing we read all the way to it, overwrite it, and keep copying the rest
                            {
                                String line = "";
                                while (!reader2.EndOfStream && !line.Equals(canvas.Name))
                                {
                                    line = reader2.ReadLine();
                                    writer.WriteLine(line);
                                }
                                if (line.Equals(canvas.Name))
                                {
                                    for (int i = 0; i < canvas.Draft.GetLength(0); i++)
                                    {
                                        for (int j = 0; j < canvas.Draft.Length / canvas.Draft.GetLength(0); j++)
                                        {
                                            writer.Write(canvas.Draft[i, j] + " ");
                                            reader2.Read();
                                        }
                                        writer.WriteLine();
                                        reader2.ReadLine();
                                    }
                                }
                                string restOfText = reader2.ReadToEnd();
                                writer.Write(restOfText);
                            }
                        }
                    }
                    // copying from second file back to original, with the update.
                    using (StreamReader reader3 = new StreamReader("res/drawings2.txt"))
                    {
                        using (StreamWriter writer2 = new StreamWriter("res/drawings.txt"))
                        {
                            string drawingsFile = reader3.ReadToEnd();
                            writer2.Write(drawingsFile);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("A drawing named " + canvas.Name + " already exists.\nPlease either press 'n' to try a different name." +
                        "or 'r' to replace the existing one.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static IList<string> GetExistingNames()
        {
            IList<string> drawings = new List<string>();
            drawings.Clear();
            try
            {
                // adding all usernames to the Drawings list and seeing if name exists.
                using (StreamReader reader = new StreamReader("res/drawings.txt"))
                {
                    while(!reader.EndOfStream)
                    {
                        string name = reader.ReadLine();
                        if (!name.Contains(" "))
                            drawings.Add(name);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return drawings;
        }
    }
}
