using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<T> where T: IComparable
{
    public List<T> Vertices
    {
        get { return new List<T>(vertices.Keys); }
    }
    
    //List<Vertex<T>> vertices = new List<Vertex<T>>();
    Dictionary<T, List<T>> vertices = new Dictionary<T, List<T>>();

    public void AddVertex(T data)
    {
        vertices.Add(data, new List<T>());
    }

    public bool AddEdge(T from, T to)
    {
        if (!vertices.ContainsKey(from) || !vertices.ContainsKey(to))
        {
            return false;
        }

        // check if from is already connected to to 
        if (vertices[from].Contains(to))
        {
            return false;
        }
    
        // add edge from from to to 
        vertices[from].Add(to);
        return true;
    }

    public List<T> GetConnectedVertices(T data)
    {
        if (vertices.ContainsKey(data))
        {
            // Debug.Log($"Number of vertices: {vertices[data].Count}");
            return vertices[data];
        }

        Debug.Log("Returning nothing");
        return new List<T>();
    }

    public bool RemoveVertex(T data)
    {
        if (!vertices.ContainsKey(data))
        {
            return false;
        }

        vertices.Remove(data);

        foreach(var pair in vertices)
        {
            if (pair.Value.Contains(data))
            {
                pair.Value.Remove(data);
                return true;
            }
        }

        //foreach (KeyValuePair<T, List<T>> pair in vertices)
        //{
        //    if (pair.Value.Contains(data))
        //    {
        //        pair.Value.Remove(data);
        //        return true;
        //    }
        //}

        return false;
    }

    public void GetVertexAt(int index)
    {
        
    }

    public bool ContainsVertex(T data)
    {
        return vertices.ContainsKey(data);
    }

}
