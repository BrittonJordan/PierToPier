using System;

namespace PS3
{
    public class PS3Class
    {
        private long[] connections; // holds numDestinations bitwise set inclusion representations (each is numDestinations long)
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
            //WriteLine("test123");

            numDestinations = Int32.Parse(ReadLine());
            int numRoutes = Int32.Parse(ReadLine());

            connections = new long[numDestinations + 1];  // ignore index 0
            everyIsland = 0;
            idealIslandCount = numDestinations;

            // Every island is "connected" to itself
            for(int i = 1; i < numDestinations + 1; i++)
            {
                //connections[i] = connections[i] | ((long)1 << (i - 1));  // Took this out to implement optimization

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

                // update the connection at connections[dest2]
                connections[dest2] = connections[dest2] | ((long)1 << (dest1 - 1));
            }

            long partialSolution = 0; // 000000
            int currIsland = 0;


            buildShop(partialSolution, currIsland);

            WriteLine(idealIslandCount.ToString());

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

        public void buildShop(long partialSol, int currIsland)
        {
            currIsland++;
            if (currIsland > numDestinations)
            {
                checkIfIdealSol(partialSol);
                return;
            }

            long partialSolYes = partialSol | ((long)1 << (currIsland - 1));
            buildShop(partialSolYes, currIsland);  // Yes, build a shop on currIsland

            if(connections[currIsland] != 0)
                buildShop(partialSol, currIsland); // No, don't build a shop on currIsland

            //WriteLine("Done with level " + currIsland);
        }

        public void checkIfIdealSol(long sol)
        {
            int islandCount = 0;
            string solution = Convert.ToString(sol, 2);
            long currentCoveredIslands = sol;

            for(int i = 1; i < solution.Length + 1; i++)
            {
                int currChar = solution.Length - i;
                if(solution[currChar] == '1')
                {
                    islandCount++;
                    currentCoveredIslands = currentCoveredIslands | (connections[i] << 0);   // Partial solution and the connection of every island in partial solution should touch every island
                }
            }

            if(currentCoveredIslands == everyIsland && islandCount <= idealIslandCount)
            {
                idealIslandCount = islandCount;
                idealSol = sol;
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
