public class PickupPoints : PickupBase
{
    public int PointAmount = 5;
    protected override object OnPickedUp()
    {
        return PointAmount;
    }
}
