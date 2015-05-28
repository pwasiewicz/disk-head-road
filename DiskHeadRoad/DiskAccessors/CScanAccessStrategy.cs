namespace DiskHeadRoad.DiskAccessors
{
    using System;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;

    public class CScanAccessStrategy : DiskAccessStrategyBase
    {
        public CScanAccessStrategy(int maxCylindersNo) : base(maxCylindersNo) { }

        public override string Name
        {
            get { return "cscan"; }
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
            for (var i = requests.Length - 1; i > closestPos; i--) this.WriteRequest(requestsOrdered[i]);

            var length = 0;
            if (closestPos > -1)
            {
                length += startCylinder;
            }

            if (closestPos < requestsOrdered.Length - 1)
            {
                length += this.MaxCylindersNo;
                length += requestsOrdered[requestsOrdered.Length - 1] - requestsOrdered[closestPos + 1];
            }


            return length;
        }
    }
}
