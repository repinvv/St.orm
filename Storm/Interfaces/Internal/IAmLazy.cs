namespace St.Orm.Interfaces.Internal
{
    using System.Collections.Generic;

    public interface IAmLazy<T>
    {
        // used for structures only
        void StoreSource(List<T> sourceList, int sourceIndex);

        T Clone();

        T ClonedFrom();
    }
}
