namespace DiskHeadRoad.DiskAccessors
{
    using System;
    using System.Linq;

    public class SstfAccessStrategy : DiskAccessStrategyBase
    {
        public SstfAccessStrategy(int maxCylindersNo) : base(maxCylindersNo) {}

        public override string Name
        {
            get
            {
                return "sstf";
                
            }
        }

        public override int AccessCylinderLength(int startCylinder, int[] requests)
        {
            this.WriteRequest(startCylinder);

            var length = 0;
            var elements = requests.ToList();
            while (elements.Count > 0)
            {
                var ordered = elements.OrderBy(e => Math.Abs(e - startCylinder));
                var winner = ordered.First();
                elements = ordered.Skip(1).ToList();

                length += Math.Abs(winner - startCylinder);
                startCylinder = winner;

                this.WriteRequest(startCylinder);
            }

            return length;
        }
    }
}