namespace DiskHeadRoad
{
    public interface IDiskAccessStrategy
    {
        string Name { get; }
        int AccessCylinderLength(int startCylinder, int[] requests);
    }
}
