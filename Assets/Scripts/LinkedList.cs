using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList<T>
{
    private int listSize;
    private Node<T> firstNode;
    
    public void ClearList()
    {
        listSize = 0;
        firstNode = new Node<T>(null, default(T), null);
    }
    
    public void AddNodeAtFront(string id, T data)
    {
        Node<T> newNode = new Node<T>(id, data, firstNode);
        firstNode = newNode.next;
        firstNode = newNode;

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
        AddNodeAt(id, value, listSize);
    }

    public void AddNodeAt(string id, T value, int index)
    {
        Node<T> insertBeforeNode = FindNode(index);
        AddNodeBefore(id, insertBeforeNode, value);
    }
    
    private void AddNodeBefore(string id, Node<T> node, T data)
    {
        Node<T> newNode = new Node<T>(id, data, node);
        
        if (node == firstNode)
        {
            newNode = firstNode;
        }
        node = newNode.next;
        listSize++;
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
    
    public class Node<X>
    {
        public string id;
        public T data;
        public Node<X> next;

        public Node(string id, T data, Node<X> next)
        {
            this.id = id;
            this.data = data;
            this.next = next;
        }
    }
}
