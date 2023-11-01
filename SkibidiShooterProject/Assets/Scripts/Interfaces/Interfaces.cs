public interface IHitable
{
    void TakeHit(float damage);
    public float Health { get; }
}