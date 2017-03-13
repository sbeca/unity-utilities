using System;
using System.Collections;
using System.Collections.Generic;

public static class SortedList
{
    // Helper function for when T implements IComparable and CompareTo suits your sorting requirements.
    // E.g. if you need ints in ascending order then use: SortedList<int> list = SortedList.CreateListOfComparable<int>();
    // This saves you having to create a new Comparer class for base types.
    public static SortedList<T> CreateListOfComparable<T>() where T : IComparable
    {
        return new SortedList<T>(new ComparableComparer<T>());
    }

    private class ComparableComparer<T> : IComparer<T> where T : IComparable
    {
        public int Compare(T x, T y)
        {
            return x.CompareTo(y);
        }
    }
}

public class SortedList<T> : ICollection<T>, IList<T>
{
    private List<T> data;
    private IComparer<T> comparer;

    public SortedList(IComparer<T> comparer)
    {
        data = new List<T>();
        this.comparer = comparer;
    }

#region IList[T] implementation

    public int IndexOf(T item)
    {
        return data.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        data.Insert(index, item);
        Sort();
    }

    public void RemoveAt(int index)
    {
        data.RemoveAt(index);
    }

    public T this[int index]
    {
        get
        {
            return data[index];
        }
        set
        {
            data[index] = value;
            Sort();
        }
    }

#endregion

#region ICollection[T] implementation

    public void Add(T item)
    {
        data.Add(item);
        Sort();
    }

    public int Count
    {
        get
        {
            return data.Count;
        }
    }

    public void Clear()
    {
        data.Clear();
    }

    public bool Contains(T item)
    {
        return data.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        for (int i = 0; i < data.Count; ++i)
        {
            array.SetValue(data[i], arrayIndex);
            arrayIndex = arrayIndex + 1;
        }
    }

    public bool Remove(T item)
    {
        return data.Remove(item);
    }

    public bool IsReadOnly
    {
        get
        {
            return false;
        }
    }

#endregion

#region IEnumerable implementation

    IEnumerator IEnumerable.GetEnumerator()
    {
        return data.GetEnumerator();
    }

#endregion

#region IEnumerable[T] implementation

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return data.GetEnumerator();
    }

#endregion

    private void Sort()
    {
        data.Sort(comparer);
    }
}
