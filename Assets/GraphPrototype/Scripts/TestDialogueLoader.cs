using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.AI;
using File = System.IO.File;
using Object = UnityEngine.Object;

public class TestDialogueLoader : MonoBehaviour
{
    [SerializeField] private Object[] dialogueJSONFiles;
    
    [SerializeField] private string jsonFilePath = "Dialogue2.json";

    // private Dictionary<string, Graphs<DialogueItem>> dialogueSets
    //     = new Dictionary<string, Graphs<DialogueItem>>();
    
    private Dictionary<string, DialogueSet> dialogueSets
        = new Dictionary<string, DialogueSet>();

    [SerializeField] private List<Sprite> images;

    // public Dictionary<string, Graphs<DialogueItem>> DialogueSets
    // {
    //     get { return dialogueSets; }
    // }
    
    public Dictionary<string, DialogueSet> DialogueSets
    {
        get { return dialogueSets; }
    }

    void Awake()
    {
        // if (dialogueJSONFiles == null)
        // {
        //     Debug.Log("No files");
        // }
        // else
        // {
        //     Debug.Log("There are files");
        // }
        // foreach (Object jsonFile in dialogueJSONFiles)
        // {
        //     ParseDialogueJSON(jsonFile.ToString());
        // }
        
        LoadJsonFromFile(Application.dataPath + "/StreamingAssets/" + jsonFilePath);
    }
    
    private void LoadJsonFromFile(string pathToJson)
    {
        try
        {
            string json = File.ReadAllText(pathToJson);
            ParseDialogueJSON(json);
            // OnDialogueLoaded2?.Invoke(_dialogueSets);
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private Graph<DialogueItem> ParseDialogueItems(JSONNode dialogueItemsNode)
    {
        Graph<DialogueItem> dialogueGraph = new Graph<DialogueItem>();

        // if (dialogueItemsNode == null)
        // {
        //     Debug.Log("Node is empty");
        // }
        // else
        // {
        //     Debug.Log("Node is not empty");
        // }
        
        foreach (JSONNode item in dialogueItemsNode)
        {
            DialogueItem newDialogueItem = new DialogueItem();

            newDialogueItem.ID = item["id"];

            // Debug.Log(newDialogueItem.ID);
            newDialogueItem.Name = item["name"];

            // Debug.Log(newDialogueItem.Name);
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

            foreach (JSONNode optionNode in item["options"])
            {
                DialogueOption option = new DialogueOption();
                option.Text = optionNode["text"];
                option.LinkID = optionNode["linkID"];
                option.RequiredItem = optionNode["requiredItem"];

                newDialogueItem.Options.Add(option);
                // Debug.Log(option.LinkID + " was added");
            }

            dialogueGraph.AddVertex(newDialogueItem);
            
        }

        foreach (DialogueItem item in dialogueGraph.Vertices)
        {
            // Debug.Log(item.ID);
            foreach (DialogueOption option in item.Options)
            {
                
                foreach (DialogueItem otherItem in dialogueGraph.Vertices)
                {
                    if (item == otherItem)
                    {
                        continue;
                    }

                    
                    if (option.LinkID == otherItem.ID)
                    {
                        dialogueGraph.AddEdge(item, otherItem);
                        // Debug.Log($"{item.ID} is linked to {otherItem.ID}");
                    }
                }
            }
        }

        return dialogueGraph;
    }

    void ParseDialogueJSON(string jsonText)
    {
        // Debug.Log(jsonText);
        // // JSONNode root = JSON.Parse(jsonText);
        //
        // Graphs<DialogueItem> graph = new Graphs<DialogueItem>();
        //
        // JSONNode rootNode = JSON.Parse(jsonText);
        // string id = rootNode["convoId"];
        // Debug.Log(id);
        //
        // graph = ParseDialogueItems(rootNode["dialogueItems"]);
        //
        // // dialogueSets.Add(id, graph);
        // dialogueSets[id] = graph;
        
        
        JSONNode rootNode = JSON.Parse(jsonText);
    
        foreach (JSONNode dialogueSetNode in rootNode["dialogueSets"])
        {
            DialogueSet newDialogueSet = new DialogueSet();
            newDialogueSet.convoId = dialogueSetNode["convoId"];
            newDialogueSet.dialogueItemGraph = ParseDialogueItems(dialogueSetNode["dialogueItems"]);
            
            dialogueSets.Add(newDialogueSet.convoId, newDialogueSet);
        }
        

        // foreach (JSONNode dialogueSetNode in rootNode["dialogueSets"])
        // {
        //     DialogueSet newDialogueSet = new DialogueSet();
        //     newDialogueSet.convoId = dialogueSetNode["convoId"];
        //     newDialogueSet.dialogueItemGraph = ParseDialogueItems(dialogueSetNode["dialogueItems"]);
        //     
        //     dialogueSets.Add(newDialogueSet.convoId, newDialogueSet);
        // }

        //     Debug.Log("Before loop");
        //     //creating all the vertices of the graph
        //     foreach (JSONNode dialogueItemNode in root["dialogueItems"])
        //     {
        //         DialogueItem item = new DialogueItem();
        //         Debug.Log("Item created");
        //         item.ID = dialogueItemNode["id"];
        //         Debug.Log("ID added");
        //         item.Name = dialogueItemNode["name"];
        //         Debug.Log("Name added");
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
        //     dialogueSets[id] = graph;
        // }
    }
}
