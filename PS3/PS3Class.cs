using System;

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
