using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueSet
{
    public string convoId;
    public DoublyLinkedList<DialogueItem> dialogueItemsList;
    public Graph<DialogueItem> dialogueItemGraph;

}

// [Serializable]
// public class DialogueItem
// {
//     public string id;
//     public string speaker;
//     public string name;
//     public Sprite image;
//     [TextArea] public string dialogue;
//
//     //added type
//     public string type;
// }
