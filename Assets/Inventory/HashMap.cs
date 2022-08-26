using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HashMap<TKey, TValue> where TKey : IComparable
{
    private const int DEFAULT_SIZE = 30;
    private const float RESIZE_CAPACITY = 0.8f;

    private int size = 0;
        
    private List<KeyValuePair<TKey, TValue>>[] items;

    public List<KeyValuePair<TKey, TValue>>[] Items
    {
        get { return items; }
    }

    public int Size
    {
        get { return size; }
    }
    public HashMap()
    {
        items = new List<KeyValuePair<TKey, TValue>>[DEFAULT_SIZE];
    }
        
    public HashMap(int setCapacity)
    {
        items = new List<KeyValuePair<TKey, TValue>>[setCapacity];
    }

    public List<string> GetKeys(List<KeyValuePair<TKey, TValue>>[] items)
    {
        List<string> keys = new List<string>();
        
        foreach (List<KeyValuePair<TKey, TValue>> chain in items)
        {
            if (chain == null)
            {
                continue;
            }
            
            foreach (KeyValuePair<TKey, TValue> pair in chain)
            {
                keys.Add(pair.Key.ToString());
            }
        }

        return keys;
    }

    public void Add(TKey key, TValue value)
    {
        int index = GetIndex(key);

        if (items[index] == null)
        {
            items[index] = new List<KeyValuePair<TKey, TValue>>();
        }
            
        items[index].Add(new KeyValuePair<TKey, TValue>(key, value));
        // Debug.Log($"{key} was added to the hashmap at index: {index}, chainIndex: {FindChainIndex(index, key)}");
        size++;

        ResizeIfNeeded();
    }

    public bool Remove(TKey key)
    {
        int index = GetIndex(key);
        if (items[index] == null)
        {
            return false;
        }

        int chainIndex = FindChainIndex(index, key);
        if (chainIndex > 0)
        {
            items[index].RemoveAt(chainIndex);
        }

        size--;
        return chainIndex >= 0;
    }
        
    public TValue GetValue(TKey key)
    {
        int index = GetIndex(key);
        if (items[index] == null)
        {
            throw new ArgumentException("Value does not exist at the key");
        }
    
        int chainIndex = FindChainIndex(index, key);
        if (chainIndex < 0)
        {
            throw new ArgumentException("The chain is empty");
        }

        return items[index][chainIndex].Value;
    }
        
    private int GetIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode() % items.Length);
    }

    private int FindChainIndex(int index, TKey key)
    {
        int chainIndex = 0;

        while (chainIndex < items[index].Count)
        {
            if (items[index][chainIndex].Key.Equals(key))
            {
                return chainIndex;
            }
            chainIndex++;
        }

        return -1;
    }

    public bool HasKey(TKey key)
    {
        foreach (List<KeyValuePair<TKey, TValue>> chain in items)
        {
            if (chain == null)
            {
                continue;
            }
            
            foreach (KeyValuePair<TKey, TValue> pair in chain)
            {
                if (pair.Equals(null))
                {
                    continue;
                }
                
                if (pair.Key.CompareTo(key) == 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void ResizeIfNeeded()
    {
        if (size / (float) items.Length < RESIZE_CAPACITY)
        {
            return;
        }

        List<KeyValuePair<TKey, TValue>>[] oldItems = items;
        items = new List<KeyValuePair<TKey, TValue>>[oldItems.Length * 2];

        foreach(List<KeyValuePair<TKey, TValue>> chain in oldItems)
        {
            if (chain == null)
            {
                continue;
            }
            
            foreach(KeyValuePair<TKey, TValue> pair in chain)
            {
                int index = GetIndex(pair.Key);

                if (items[index] == null)
                {
                    items[index] = new List<KeyValuePair<TKey, TValue>>();
                }
                
                items[index].Add(new KeyValuePair<TKey, TValue>(pair.Key, pair.Value));
            }
        }
    }
    
}
