public class PickupAmmo : PickupBase
{
    public int AmmoAmount = 3;

    protected override object OnPickedUp()
    {
        return AmmoAmount;
    }
}
