namespace DiskHeadRoad.DiskAccessors
{
    using System;

    public class FcfsAccessStrategy : DiskAccessStrategyBase
    {
        public FcfsAccessStrategy(int maxCylindersNo) : base(maxCylindersNo) {}

        public override string Name
        {
            get { return "fcfs"; }
        }

        public override int AccessCylinderLength(int startCylinder, int[] requests)
        {
            this.WriteRequest(startCylinder);

            var length = 0;

            foreach (var request in requests)
            {
                length += Math.Abs(request - startCylinder);
                startCylinder = request;

                this.WriteRequest(request);
            }

            return length;
        }
    }
}
