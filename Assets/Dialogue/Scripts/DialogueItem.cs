
using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueItem : IComparable
{
    public string ID { get; set; }
    public string Speaker { get; set; }
    public string Name { get; set; } 
    public string ImageName { get; set; }
    public Sprite Image { get; set; }
    public string Dialogue { get; set; }

    public List<DialogueOption> Options { get; set; } = new List<DialogueOption>();
    
    
    public int CompareTo(object obj)
    {
        DialogueItem other = (DialogueItem) obj;

        // if (other.ID == this.ID)
        // {
        //     return 0;
        // }

        return this.ID.CompareTo(other.ID);
    }
}
