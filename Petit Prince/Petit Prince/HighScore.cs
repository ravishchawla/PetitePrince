using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//Copyright (C) Ravish Chawla 2011  
namespace Petit_Prince
{
    class HighScore
    {

        private int[] highScores = new int[6]{0,0,0,0,0,0};
     
        //makes an array of 6 elements, giving each value 0. 

        public void updateHighScore(int score)
        {
            
            
            highScores[5] = score;
            Array.Sort(highScores);
            Array.Reverse(highScores);

            //takes a score value, and puts it in the 6th index. 
            //sorts and reverses the array. 
                           
                


                StreamWriter writer = new StreamWriter("scores.txt");
                {
                    for (int j = 0; j < 4; j++)
                    {
                        writer.WriteLine(highScores[j]);
                    }
                }

                writer.Close();
            //writes the highscores to a text file. 
            }
        

        public int[] readHighScore()
        {

            if (File.Exists("scores.txt") == true)
            {
                StreamReader reader = new StreamReader("scores.txt");
                {
                    for (int i = 0; i < 4; i++)
                    {
                        string s = reader.ReadLine();
                        if (s != null)
                            highScores[i] = int.Parse(s);
                    }
                }

                reader.Close();
            }

            else
            {
                StreamWriter writer = new StreamWriter("scores.txt");
                {
                    for (int j = 0; j < 4; j++)
                    {
                        writer.WriteLine(highScores[j]);
                    }
                }

                writer.Close();



                StreamReader reader = new StreamReader("scores.txt");
                {
                    for (int i = 0; i < 4; i++)
                    {
                        highScores[i] = int.Parse(reader.ReadLine());
                    }
                }

                reader.Close();
            }

            
            return (highScores);
        //reads the highscores from the textfile and returns them. 
        }
    }

}
