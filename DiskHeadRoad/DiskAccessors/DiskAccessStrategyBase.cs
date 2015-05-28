namespace DiskHeadRoad.DiskAccessors
{
    using System;
    using System.Globalization;

    public abstract class DiskAccessStrategyBase : IDiskAccessStrategy
    {
        private int currentTime = 0;
        protected int MaxCylindersNo;

        protected DiskAccessStrategyBase(int maxCylindersNo)
        {
            if (maxCylindersNo > 9999) throw new NotSupportedException("Only 9999 cyllinders are supported.");
            this.MaxCylindersNo = maxCylindersNo;
        }

        public abstract string Name { get; }
        public abstract int AccessCylinderLength(int startCylinder, int[] requests);


        protected void WriteRequest(int cylinder)
        {
            if (cylinder > this.MaxCylindersNo || cylinder < 1)
            {
                throw new ArgumentOutOfRangeException("cylinder");
            }

            if (this.currentTime == 0)
            {
                for (var i = 0; i < 73; i++) Console.Write("=");
                Console.WriteLine();
            }

            var currTimeLength = this.currentTime.ToString(CultureInfo.InvariantCulture).Length;
            if (currTimeLength >= 100) throw new NotSupportedException("Max 99 request supported only.");

            Console.Write(this.currentTime + "|");
            for (var i = currTimeLength; i < 3; i++) Console.Write(" ");

            var requestPositionRatio = (float)cylinder/(this.MaxCylindersNo);
            var requestPosition = (int) Math.Ceiling(73*requestPositionRatio);

            for (var i = 0; i < requestPosition; i++)
            {
                Console.Write(" ");
            }

            Console.WriteLine("* ({0})", cylinder);

            this.currentTime += 1;

        }
    }
}
