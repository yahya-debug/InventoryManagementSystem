using System.Collections.Concurrent;

interface IDB<T, Y> where Y : notnull {
    void Edit(ConcurrentDictionary<Y, T> data);
    ConcurrentDictionary<Y, T> Read();
}