namespace DiskHeadRoad.DiskAccessors
{
    using System;
    using System.Linq;

    public class ScanAccessStrategy : DiskAccessStrategyBase
    {
        public ScanAccessStrategy(int maxCylindersNo) : base(maxCylindersNo) {}

        public override string Name
        {
            get { return "scan"; }
        }

        public override int AccessCylinderLength(int startCylinder, int[] requests)
        {
            this.WriteRequest(startCylinder);

            var requestsOrdered = requests.OrderBy(r => r).ToArray();
            var closestPos = Array.BinarySearch(requestsOrdered, startCylinder);
            if (closestPos < 0)
            {
                closestPos = ~closestPos;
                closestPos -= 1;
            }

            for (var i = closestPos; i >= 0; i--) this.WriteRequest(requestsOrdered[i]);
            for (var i = closestPos + 1; i < requests.Length; i++) this.WriteRequest(requestsOrdered[i]);

            return startCylinder + this.MaxCylindersNo;
        }
    }
}
