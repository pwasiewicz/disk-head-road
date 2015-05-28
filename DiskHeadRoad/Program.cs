namespace DiskHeadRoad
{
    using System;
    using System.Linq;
    using DiskAccessors;
    using Nakov.IO;

    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Cyllinders amount? ");
            var cyllinders = Cin.NextInt();

            Console.Write("Init cyllinder no? ");
            var initCyllinder = Cin.NextInt();

            Console.Write("Requests amount? ");
            var requestsNo = Cin.NextInt();

            Console.WriteLine("Put requests (space-separated):");
            var requests = new int[requestsNo];
            for (var i = 0; i < requestsNo; i++)
            {
                requests[i] = Cin.NextInt();
            }

            var strategies = new DiskAccessStrategyBase[6];
            strategies[0] = new CLookAccessStrategy(cyllinders);
            strategies[1] = new CScanAccessStrategy(cyllinders);
            strategies[2] = new FcfsAccessStrategy(cyllinders);
            strategies[3] = new LookAccessStrategy(cyllinders);
            strategies[4] = new ScanAccessStrategy(cyllinders);
            strategies[5] = new SstfAccessStrategy(cyllinders);

            Console.Write("strategy ({0})? ", string.Join(",", strategies.Select(s => s.Name).Concat(new[] { "all" })));
            var strategy = Cin.NextToken();


            if (!strategy.Equals("all"))
            {

                var headAccess = strategies.FirstOrDefault(s => s.Name.Equals(strategy));
                if (headAccess == null)
                {
                    Console.WriteLine("Invalid strategy.");
                    return;
                }

                var distance = headAccess.AccessCylinderLength(initCyllinder, requests);
                Console.WriteLine("Total distance travelled by head: {0}", distance);
            }
            else
            {
                foreach (var strategyImpl in strategies)
                {
                    Console.WriteLine("Strategy: {0}", strategyImpl.Name);

                    var distance = strategyImpl.AccessCylinderLength(initCyllinder, requests);

                    Console.WriteLine("Total distance travelled by head: {0}", distance);
                }
            }

            Console.Out.Flush();
        }
    }
}
