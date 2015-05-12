namespace St.Orm.Interfaces.Internal
{
    public interface IAmClonable<T>
    {
        T Clone();

        T ClonedFrom();
    }
}
