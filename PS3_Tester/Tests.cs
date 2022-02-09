using System;
using PS3;

namespace PS3_Tester
{
    public class Route
    {
        public int one;
        public int two;
        public Route(int o, int t)
        {
            one = o;
            two = t;
        }
    }

    class Tests: PS3Class
    {

        private int numDest;
        private Route[] routes;
        bool readDest;
        bool readRouteNum;
        int currRoute;

        public Tests()
        {
            readDest = false;
            readRouteNum = false;
            currRoute = 0;
        }

        public void generateRoutes(int numRoutes, int numDestinations)
        {
            Route[] rs = new Route[numRoutes];
            Random r = new Random();
            for (int i = 0; i < numRoutes; i++)
            {
                //rs.SetValue(new Route(r.Next(1, numDestinations), r.Next(1, numDestinations)), i);
                int from = r.Next(1, numDestinations);
                int to = r.Next(1, numDestinations);
                while(to == from)
                {
                    to = r.Next(1, numDestinations);
                }

                rs[i] = new Route(from, to);
                //rs[i].one = r.Next(1, numDestinations);
                //rs[i].two = r.Next(1, numDestinations);
            }

            this.routes = rs;
        }

        static void Main(string[] args)
        {
            
            Tests t = new Tests();
            t.numDest = 10;
            t.generateRoutes(11, t.numDest);
            t.run();

        }

        public override string ReadLine()
        {
            if (!readDest)
            {
                readDest = true;
                return numDest.ToString();
            }
            else if (!readRouteNum)
            {
                readRouteNum = true;
                return routes.Length.ToString();
            }
            else
            {
                string route = "";
                route += routes[currRoute].one.ToString() + " " + routes[currRoute].two.ToString();
                currRoute++;
                return route;
            }
        }

        public override void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}
