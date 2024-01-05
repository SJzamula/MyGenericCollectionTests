using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class MyGenericCollection<T> : IEnumerable<T>
{
    private class Node
    {
        public T Data { get; set; }
        public Node Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    private Node head;
    private Node tail;
    private int count;

    public event EventHandler ItemAdded;
    public event EventHandler ItemRemoved;
    public event EventHandler CollectionCleared;

    public MyGenericCollection()
    {
        head = tail = null;
        count = 0;
    }

    public void Add(T item)
    {
        Node newNode = new Node(item);
        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            tail = newNode;
        }
        count++;
        ItemAdded?.Invoke(this, EventArgs.Empty);
    }

    public bool Remove(T item)
    {
        Node current = head;
        Node previous = null;
        bool removed = false;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, item))
            {
                if (previous == null)
                {
                    head = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
                if (current == tail)
                {
                    tail = previous;
                }
                count--;
                removed = true;
                break;
            }

            previous = current;
            current = current.Next;
        }

        if (removed)
        {
            ItemRemoved?.Invoke(this, EventArgs.Empty);
        }

        return removed;
    }

    public void Clear()
    {
        head = tail = null;
        count = 0;
        CollectionCleared?.Invoke(this, EventArgs.Empty);
    }

    public void PrintList()
    {
        Node current = head;
        while (current != null) {
            Console.Write(current.Data + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Data;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (Node current = head; current != null; current = current.Next)
        {
            yield return current.Data;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => count;
}

class MyCollection
{
    public void Run()
    {
        var collection = new MyGenericCollection<int>();
        collection.ItemAdded += (sender, e) => Console.WriteLine("Item added");
        collection.ItemRemoved += (sender, e) => Console.WriteLine("Item removed");
        collection.CollectionCleared += (sender, e) => Console.WriteLine("Collection cleared");

        collection.Add(1);
        collection.Add(2);
        collection.Add(4);
        collection.Remove(2);
        collection.PrintList();
        collection.Clear();
        collection.PrintList();

        foreach (var item in collection)
        {
            Console.WriteLine(item);
        }
    }
}
