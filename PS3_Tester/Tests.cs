using System;
using PS3;

namespace PS3_Tester
{
    class Tests: PS3Class
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public override string ReadLine()
        {
            //if (firstRead)
            //{
            //    firstRead = false;
            //    return solarData.Length.ToString();
            //}
            //return solarData[currentIndex].ToString();
            return "";
        }

        public override void WriteLine(string s)
        {
            //string[] elements = s.Split(' ');
            //if (elements[0] == "minimum")
            //{
            //    Console.WriteLine(elements[1] + " " + elements[2]);
            //}
            //else
            //{
            //    currentIndex = Int32.Parse(elements[1]);
            //}
        }
    }
}
