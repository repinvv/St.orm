namespace St.Orm.Interfaces
{
    public interface ICloneable<out TDal>
    {
        bool[] GetPopulated();

        TDal Clone();

        TDal ClonedFrom();
    }
}
