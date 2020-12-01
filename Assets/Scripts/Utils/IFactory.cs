namespace CoolGoal.Util.Factory
{
    public interface IFactory<T>
    {
        T GetObject();
    }
}
