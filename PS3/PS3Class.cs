﻿using System;

namespace PS3
{
    public class PS3Class
    {
        static void Main(string[] args)
        {
            PS3Class p = new PS3Class();
            p.run();
        }

        public void run()
        {
            int numDestinations = Int32.Parse(ReadLine());
            int numRoutes = Int32.Parse(ReadLine());

            for(int i = 0; i < numRoutes; i++)
            {
                string pair = ReadLine();
                string[] elements = pair.Split(' ');
                int dest1 = Int32.Parse(elements[0]);
                int dest2 = Int32.Parse(elements[1]);
            }

        }

        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }

        public virtual void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}
