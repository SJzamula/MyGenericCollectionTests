using NUnit.Framework;
using System;

[TestFixture]
public class MyGenericCollectionTests
{
    [Test]
    public void Add_ShouldIncreaseCount()
    {
        var collection = new MyGenericCollection<int>();
        collection.Add(1);

        Assert.AreEqual(1, collection.Count);
    }

    [Test]
    public void Remove_ValidItem_ShouldDecreaseCount()
    {
        var collection = new MyGenericCollection<int>();
        collection.Add(1);
        collection.Remove(1);

        Assert.AreEqual(0, collection.Count);
    }

    [Test]
    public void Remove_InvalidItem_ShouldNotAlterCount()
    {
        var collection = new MyGenericCollection<int>();
        collection.Add(1);
        collection.Remove(2); // 2 is not in the collection

        Assert.AreEqual(1, collection.Count);
    }

    [Test]
    public void Indexer_ValidIndex_ShouldReturnItem()
    {
        var collection = new MyGenericCollection<int>();
        collection.Add(1);
        collection.Add(2);

        Assert.AreEqual(2, collection[1]);
    }

    [Test]
    public void Indexer_InvalidIndex_ShouldThrow()
    {
        var collection = new MyGenericCollection<int>();
        collection.Add(1);

        Assert.Throws<ArgumentOutOfRangeException>(() => { var item = collection[1]; });
    }

    [Test]
    public void Clear_ShouldResetCount()
    {
        var collection = new MyGenericCollection<int>();
        collection.Add(1);
        collection.Clear();

        Assert.AreEqual(0, collection.Count);
    }

    [Test]
    public void Remove_MiddleItem_ShouldLinkPreviousWithNext()
    {
        var collection = new MyGenericCollection<int>();
        collection.Add(1);
        collection.Add(2);
        collection.Add(3);
        collection.Remove(2);

        Assert.AreEqual(2, collection.Count);
        Assert.AreEqual(1, collection[0]);
        Assert.AreEqual(3, collection[1]);
    }


    [Test]
    public void GetEnumerator_ShouldIterateOverAllItems()
    {
        var collection = new MyGenericCollection<int>();
        collection.Add(1);
        collection.Add(2);
        collection.Add(3);

        int itemCount = 0;
        foreach (var item in collection)
        {
            itemCount++;
        }

        Assert.AreEqual(3, itemCount);
    }
}
