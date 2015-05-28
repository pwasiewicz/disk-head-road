namespace DiskHeadRoad.DiskAccessors
{
    using System;
    using System.Linq;

    public class CLookAccessStrategy : DiskAccessStrategyBase
    {
        public CLookAccessStrategy(int maxCylindersNo) : base(maxCylindersNo) { }

        public override string Name
        {
            get { return "clook"; }
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
            for (var i = closestPos + 1; i < requestsOrdered.Length; i++) this.WriteRequest(requestsOrdered[i]);


            var length = 0;
            var minRequest = startCylinder;

            if (closestPos > -1)
            {
                length += startCylinder - Math.Min(0, requestsOrdered[0]);
                minRequest = requestsOrdered[0];
            }

            if (closestPos < requests.Length - 1)
            {
                var maxRequest = requestsOrdered[requestsOrdered.Length-1];
                length += maxRequest  - minRequest;
                length += minRequest - (requestsOrdered[closestPos + 1]);

            }

            return length;
        }
    }
}
