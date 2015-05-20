using System;
using System.Collections.Generic;

namespace CAIN
{
    interface IDBTable<T>
    {
        bool Exists(long ID);

        bool Exists(T obj);

        long GetID(T obj);

        T Get(long ID);

        List<T> List(int count = 0);
        
        bool Insert(ref T obj);

        bool Edit(T obj);

        bool Delete(long ID);
    }
}
