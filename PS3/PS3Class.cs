using System;

namespace PS3
{
    public class PS3Class
    {
        private long[] connections; // holds numDestinations bitwise set inclusion representations (each is numDestinations long) (connection doesnt include itself)
        private long[] connectionsInclusive; // used to mirror connections, and identify correct connection using bitwise and
        private long idealSol;
        private long everyIsland;
        private int idealIslandCount;
        private int numDestinations;

        static void Main(string[] args)
        {
            PS3Class p = new PS3Class();
            p.run();
        }

        public void run()
        {
            string dumbestStringEverPleaseBeMoreClearInTheInstructionsNextTime = ReadLine();
            string[] elems = dumbestStringEverPleaseBeMoreClearInTheInstructionsNextTime.Split(' ');
            numDestinations = Int32.Parse(elems[0]);
            int numRoutes = Int32.Parse(elems[1]);

            connections = new long[numDestinations + 1];  // ignore index 0
            connectionsInclusive = new long[numDestinations + 1];

            everyIsland = 0;
            idealIslandCount = numDestinations;

            // Every island is "connected" to itself
            for(int i = 1; i < numDestinations + 1; i++)
            {
                connectionsInclusive[i] = connectionsInclusive[i] | ((long)1 << (i - 1));

                // Compose bitwise representation of every island included in set
                everyIsland = (long)1 | everyIsland << 1;  // 11111111
            }

            for (int i = 0; i < numRoutes; i++)
            {
                string pair = ReadLine();
                string[] elements = pair.Split(' ');
                int dest1 = Int32.Parse(elements[0]);
                int dest2 = Int32.Parse(elements[1]);

                // update the connection at connections[dest1]
                connections[dest1] = connections[dest1] | ((long)1 << (dest2 - 1));
                //connectionsInclusive[dest1] = connectionsInclusive[dest1] | ((long)1 << (dest2 - 1));

                // update the connection at connections[dest2]
                connections[dest2] = connections[dest2] | ((long)1 << (dest1 - 1));
                //connectionsInclusive[dest2] = connectionsInclusive[dest2] | ((long)1 << (dest1 - 1));
            }

            long partialSolution = 0; // 000000
            int currIsland = 0;


            buildShop(partialSolution, currIsland, 0);

            WriteLine(idealIslandCount.ToString() + " ");

            string IdealSolution = Convert.ToString(idealSol, 2);
            string islands = "";
            for (int i = 1; i < IdealSolution.Length + 1; i++)
            {
                int currChar = IdealSolution.Length - i;
                if (IdealSolution[currChar] == '1')
                {
                    islands += i.ToString() + " ";
                }
            }
            islands = islands.Substring(0, islands.Length - 1);

            WriteLine(islands);
        }

        public void buildShop(long partialSol, int currIsland, int numShops)
        {
            currIsland++;
            if (currIsland > numDestinations)
            {
                //if(numShops <= idealIslandCount)
                //{
                //    idealIslandCount = numShops;
                //    idealSol = partialSol;
                //}
                isSolution(partialSol);
                return;
            }
            if (numShops > idealIslandCount)
            {
                return;
            }

            //if (isSolutionV2(partialSol))
            //{
            //    if(numShops < idealIslandCount)
            //    {
            //        idealIslandCount = numShops;
            //        idealSol = partialSol;
            //    }
            //    return;
            //}

            long partialSolYes = partialSol | ((long)1 << (currIsland - 1));
            numShops++;
            buildShop(partialSolYes, currIsland, numShops);  // Yes, build a shop on currIsland

            if(isPossibleConnectionAfterNo(partialSol, currIsland))
                buildShop(partialSol, currIsland, --numShops); // No, don't build a shop on currIsland
        }

        public bool isPossibleConnectionAfterNo(long partial, int currIsland)
        {
            if (connections[currIsland] == 0)
            {
                return false;
            }
            else
            {
                long connectedIslandsWithShops = connections[currIsland] & partial;
                long currIslandLong = Convert.ToInt64(Math.Pow(2, currIsland - 1));

                // There are no routes connecting islands with shops on them and all of the connected islands have already been decided
                if (connectedIslandsWithShops == 0 && currIslandLong > connections[currIsland])
                {
                    return false;
                }
            }
            return true;
        }

        //public void checkIfIdealSol(long sol)
        //{

        //    if(isSolution(sol) && islandCount <= idealIslandCount)
        //    {
        //        idealIslandCount = islandCount;
        //        idealSol = sol;
        //    }
        //}

        public bool isSolution(long sol)
        {
            int islandCount = 0;
            string solution = Convert.ToString(sol, 2);
            long currentCoveredIslands = sol;

            for (int i = 1; i < solution.Length + 1; i++)
            {
                int currChar = solution.Length - i;
                if (solution[currChar] == '1')
                {
                    islandCount++;
                    currentCoveredIslands = currentCoveredIslands | (connections[i] << 0);   // Partial solution and the connection of every island in partial solution should touch every island
                }
            }

            if (currentCoveredIslands == everyIsland)
            {
                if(islandCount <= idealIslandCount)
                {
                    idealIslandCount = islandCount;
                    idealSol = sol;
                }
                return true;
            }
            else
                return false;

        }

        public bool isSolutionV2(long partialSol)
        {
            long currentCoveredIslands = partialSol;

            for(int i = 1; i < connectionsInclusive.Length; i++)
            {
                long conn = connectionsInclusive[i];
                // Does the partial soltuion contain island i
                if ((conn & partialSol) != 0)
                {
                    currentCoveredIslands = currentCoveredIslands | connections[i];
                }
            }

            if (currentCoveredIslands == everyIsland)
            {
                return true;
            }
            else
                return false;
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
