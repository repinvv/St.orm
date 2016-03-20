﻿namespace Storm.Interfaces
{
    public interface IDalEntity<TDal>
    {
        TDal Clone();

        TDal ClonedFrom();

        bool[] GetPopulated();
    }
}
