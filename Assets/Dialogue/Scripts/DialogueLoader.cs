
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.AI;
using UnityEngine.Windows;
using File = System.IO.File;
using Object = System.Object;

public class DialogueLoader : MonoBehaviour
{
    public delegate void DialogueLoadedAction2(DoublyLinkedList<DialogueSet> dialogueSets);
    public static event DialogueLoadedAction2 OnDialogueLoaded2;

    // [SerializeField] private Object[] dialogueJSONFiles;
    // private Dictionary<string, Graphs<DialogueItem>> dialogueSets 
    //     = new Dictionary<string, Graphs<DialogueItem>>();
    
    // public Dictionary<string, Graphs<DialogueItem>> DialogueSets
    // {
    //     get { return dialogueSets; }
    // }
    
    [SerializeField] private string jsonFilePath = "Dialogue.json";
    [SerializeField] private List<Sprite> images;

    private DoublyLinkedList<DialogueSet> _dialogueSets = new DoublyLinkedList<DialogueSet>();
    
    void Start()
    {
        LoadJsonFromFile(Application.dataPath + "/StreamingAssets/" + jsonFilePath);
        
    }
    
    // void Awake()
    // {
    //     foreach (Object jsonFile in dialogueJSONFiles)
    //     {
    //         ParseDialogueJSON(jsonFile.ToString());
    //     }
    // }
    

    private void LoadJsonFromFile(string pathToJson)
    {
        try
        {
            string json = File.ReadAllText(pathToJson);
            ParseDialogue(json);
            OnDialogueLoaded2?.Invoke(_dialogueSets);
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void ParseDialogue(string json)
    {
        JSONNode rootNode = JSON.Parse(json);
    
        foreach (JSONNode dialogueSetNode in rootNode["dialogueSets"])
        {
            DialogueSet newDialogueSet = new DialogueSet();
            newDialogueSet.convoId = dialogueSetNode["convoId"];
            newDialogueSet.dialogueItemsList = ParseDialogueItems(dialogueSetNode["dialogueItems"]);
            
            _dialogueSets.AddNodeAtFront(newDialogueSet.convoId, newDialogueSet);
        }
    }
    
    private DoublyLinkedList<DialogueItem> ParseDialogueItems(JSONNode dialogueItemsNode)
    {
        DoublyLinkedList<DialogueItem> dialogueList = new DoublyLinkedList<DialogueItem>();
    
        foreach (JSONNode item in dialogueItemsNode)
        {
            DialogueItem newDialogueItem = new DialogueItem();
            
            newDialogueItem.ID = item["id"];
            
            newDialogueItem.Name = item["name"];
    
            newDialogueItem.Speaker = item["speaker"];
            
            // newDialogueItem.Type = item["type"];
            
            foreach (Sprite image in images)
            {
                if (image.name == item["imageName"])
                {
                    newDialogueItem.Image = image;
                    break;
                }
            }
    
            newDialogueItem.Dialogue = item["dialogue"];
            
            dialogueList.AddNodeAtFront(newDialogueItem.ID, newDialogueItem);
        }
        return dialogueList;
    }
    
    // void ParseDialogueJSON(string jsonText)
    // {
    //     Debug.Log(jsonText);
    //     JSONNode root = JSON.Parse(jsonText);
    //
    //     Graphs<DialogueItem> graph = new Graphs<DialogueItem>();
    //     string convoId = root["convoId"];
    //
    //     //creating all the vertices of the graph
    //     foreach (JSONNode dialogueItemNode in root["dialogueItems"])
    //     {
    //         DialogueItem item = new DialogueItem();
    //         item.ID = dialogueItemNode["id"];
    //         item.Speaker = dialogueItemNode["speaker"];
    //         item.Name = dialogueItemNode["name"];
    //         item.ImageName = dialogueItemNode["imageName"];
    //         item.Dialogue = dialogueItemNode["dialogue"];
    //
    //         foreach (JSONNode optionNode in dialogueItemNode["options"])
    //         {
    //             DialogueOption option = new DialogueOption();
    //             option.Text = optionNode["text"];
    //             option.LinkID = optionNode["linkID"];
    //             option.RequiredItem = optionNode["requiredItem"];
    //             
    //             item.Options.Add(option);
    //         }
    //         
    //         graph.AddVertex(item);
    //     }
    //
    //     //creating edges between vertices
    //     foreach (DialogueItem item in graph.Vertices)
    //     {
    //         foreach (DialogueOption option in item.Options)
    //         {
    //             foreach (DialogueItem otherItem in graph.Vertices)
    //             {
    //                 if (item == otherItem)
    //                 {
    //                     continue;
    //                 }
    //
    //                 if (option.LinkID == otherItem.ID)
    //                 {
    //                     graph.AddEdge(item, otherItem);
    //                 }
    //             }
    //         }
    //     }
    //
    //     dialogueSets[convoId] = graph;
    // }
}
