using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublyLinkedList<T>
{
    private int listSize;
    private Node<T> firstNode;
    private Node<T> lastNode;

    public int ListSize
    {
        get { return listSize; }
    }
    
    public void ClearList()
    {
        listSize = 0;
        firstNode = new Node<T>(null, default(T), null, null);
        
        firstNode.next = lastNode;
        lastNode.previous = firstNode;
    }
    
    public void AddNodeAtFront(string id, T data)
    {
        Node<T> newNode = new Node<T>(id, data, firstNode, null);
        // Debug.Log($"{id} was added to the list");
        newNode.next = firstNode;
        firstNode = newNode;

        listSize++;
    }

    public void AddNodeAtBack(string id, T data)
    {
        Node<T> newNode = new Node<T>(id, data, null, lastNode);
       
        newNode.previous = lastNode;
        lastNode.next = newNode;
        lastNode = newNode;
        
        listSize++;
    }
    
    
    private void AddNodeBefore(string id, Node<T> node, T data)
    {
        Node<T> newNode = new Node<T>(id, data, node, node.previous);
        node.previous = newNode;
        newNode.previous.next = newNode;

        listSize++;
    }

    public T FindWithID(string id)
    {
        Node<T> node = firstNode;
        for (int i = 0; i < listSize; i++)
        {
            if (node.id == id)
            {
                return node.data;
            }
            
            node = node.next;
        }

        return node.data;
    }
    
    public T FindAt(int index)
    {
        if(index >= listSize || index < 0)
        {
            throw new IndexOutOfRangeException();
        }

        return FindNode(index).data;
    }
    
    public void AddNode(string id, T value)
    {
        AddNodeBefore(id, lastNode, value);
    }
    
    

    public void AddNodeAt(string id, T value, int index)
    {
        Node<T> insertBeforeNode = FindNode(index);
        AddNodeBefore(id, insertBeforeNode, value);
    }
    
    
    public T Remove(T value)
    {
        Node<T> node = firstNode;
        for(int i = 0; i < listSize; i++)
        {
            node = node.next;
            if(node.data.Equals(value))
            {
                RemoveNode(node);
                return node.data;
            }
        }
        return default;
    }
    
    private T RemoveNode(Node<T> node)
    {
        node.next.previous = node.previous;
        node.previous.next = node.next;
        listSize--;

        return node.data;
    }
    
    private Node<T> FindNode(int index)
    {
        if(index < 0 || index >= listSize)
        {
            throw (new IndexOutOfRangeException());
        }

        Node<T> node = firstNode;

        for (int i = 0; i < index; i++)
        {
            node = node.next;
        }
        
        return node;
    }
    
    public override string ToString()
    {
        string value = "";
        Node<T> node = firstNode;

        for(int i = 0; i < listSize; i++)
        {
            node = node.next;
            value += node.data + " ";
        }

        return value;
    }

    private void ResizeIfNeeded()
    {
        
    }
    
    public class Node<X>
    {
        public string id;
        public T data;
        public Node<X> next;
        public Node<X> previous;

        public Node(string id, T data, Node<X> next, Node<X> previous)
        {
            this.id = id;
            this.data = data;
            this.next = next;
            this.previous = previous;
        }
    }
}
