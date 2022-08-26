using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TestDialogueLoader))]
public class TestDialogueManager : MonoBehaviour
{
    private TestDialogueLoader loader;

    // private Dictionary<string, Graphs<DialogueItem>> dialogueSets;
    private Dictionary<string, DialogueSet> dialogueSets;
    
    private DialogueUI dialogueUI;
    private DialogueSet currentDialogueSet;
    private Graph<DialogueItem> graph;
    private DialogueItem currentDialogueItem; 
    
    [SerializeField] private float typeDelay = 0.002f;
    
    void Awake()
    {
        loader = GetComponent<TestDialogueLoader>();
        dialogueUI = GetComponent<DialogueUI>();
    }

    void Start()
    {
        dialogueSets = loader.DialogueSets;
        // Test();
        // DisplayDialogueSet("HumphreyConvo1");
        DisplayDialogueSet("TestScene");
        // Test();
    }

    private void OnEnable()
    {
        DialogueUI.OnPreviousClicked += OnOption2Clicked;
        DialogueUI.OnNextClicked += OnOption1Clicked;
    }
    
    private void OnDisable()
    {
        DialogueUI.OnPreviousClicked -= OnOption2Clicked;
        DialogueUI.OnNextClicked -= OnOption1Clicked;
    }

    private void OnOption1Clicked()
    {
        // List<DialogueItem> connectedVertices = graph.GetConnectedVertices(currentDialogueItem);

        // Debug.Log(graph.GetConnectedVertices(currentDialogueItem).Count);

        // Debug.Log(currentDialogueItem.ID);
        // List<DialogueItem> connectedVertices = graph.GetConnectedVertices(currentDialogueItem);

        // if (connectedVertices == null)
        // {
        //     Debug.Log("List is empty");
        // }
        // else
        // {
        //     Debug.Log("List is not empty");
        //     Debug.Log(connectedVertices.Count);
        // }
        
        // foreach(DialogueItem item in connectedVertices)
        // {
        //     Debug.Log(item.Dialogue);
        // }

        
        try
        {
            currentDialogueItem = graph.GetConnectedVertices(currentDialogueItem).ElementAt(0);
        }
        catch (Exception e)
        {
            Debug.Log("...Index was outside the bounds of the array");
        }
        
        // Debug.Log(currentDialogueItem.Dialogue);
        
        DisplayItems();
    }
    
    private void OnOption2Clicked()
    {
        // List<DialogueItem> connectedVertices = graph.GetConnectedVertices(currentDialogueItem);
        //
        // currentDialogueItem = connectedVertices[1];
        
        currentDialogueItem = graph.GetConnectedVertices(currentDialogueItem).ElementAt(1);
        
        // Debug.Log(currentDialogueItem.Dialogue);
        
        DisplayItems();
    }
    
    private void DisplayDialogueSet(string dialogueSetID)
    {
        foreach (DialogueSet set in dialogueSets.Values)
        {
            if (set.convoId == dialogueSetID)
            {
                currentDialogueSet = set;
                graph = currentDialogueSet.dialogueItemGraph;
            }
        }
        
        // currentDialogueItemIndex = 0;
        DisplayItems();
        // dialogueUI.ShowDialogueBox();
    }

    public void DisplayItems()
    {
        // Graphs<DialogueItem> graph = currentDialogueSet.dialogueItemGraph;

        // DialogueItem current = FindFirstVertex(graph);

        if (currentDialogueItem == null)
        {
            currentDialogueItem = FindFirstVertex(graph);
            
            // dialogueUI.Name = currentDialogueItem.Name;
            // dialogueUI.Image = currentDialogueItem.Image;
            // dialogueUI.Dialogue = currentDialogueItem.Dialogue;
            //
            // StopAllCoroutines();
            // StartCoroutine(TypeTextCoroutine(currentDialogueItem.Dialogue));
        }
        // Debug.Log(current.Name +": " + current.Dialogue);

        // List<DialogueItem> connectedVertices = graph.GetConnectedVertices(currentDialogueItem);

        if (currentDialogueItem.ID == "END")
        {
            dialogueUI.ButtonText = "END";
        }
        
        dialogueUI.Name = currentDialogueItem.Name;
        dialogueUI.Image = currentDialogueItem.Image;
        dialogueUI.Dialogue = currentDialogueItem.Dialogue;
        
        Debug.Log($"Number of options: {currentDialogueItem.Options.Count}" );
        
        // List<DialogueItem> connectedVertices = graph.GetConnectedVertices(currentDialogueItem);
        //
        // Debug.Log(connectedVertices[1].Dialogue);

        if (currentDialogueItem.Options.Count == 1)
        {
            DialogueOption option = currentDialogueItem.Options.ElementAt(0);
            dialogueUI.ButtonText = option.Text;
            
            dialogueUI.HideOption2Button();
        }
        else
        {
            dialogueUI.ShowOption2Button();
            
            DialogueOption option1 = currentDialogueItem.Options.ElementAt(0);
            DialogueOption option2 = currentDialogueItem.Options.ElementAt(1);

            dialogueUI.ButtonText = option1.Text;
            dialogueUI.Button2Text = option2.Text;
        }
        
        // foreach(DialogueOption option in currentDialogueItem.Options)
        // {
        //     
        // }
        
        StopAllCoroutines();
        StartCoroutine(TypeTextCoroutine(currentDialogueItem.Dialogue));
        
        
        // List<DialogueItem> connectedVertices = graph.GetConnectedVertices(current);
        // int index = Random.Range(0, connectedVertices.Count);

        // Debug.Log(index);
        // Debug.Log(connectedVertices.Count);

        // current = connectedVertices[index];
        // Debug.Log(current.Name + ": " + current.Dialogue);


    }
    
    IEnumerator TypeTextCoroutine(string text)
    {
        dialogueUI.Dialogue = "";
        foreach (char c in text)
        {
            dialogueUI.Dialogue += c + "";
            yield return new WaitForSeconds(typeDelay);
        }
    }

    void Test()
    {
        foreach(DialogueSet set in dialogueSets.Values )
        {
           
            if (set.convoId == "PASSWORD_SCENE")
            {
                Graph<DialogueItem> graph = set.dialogueItemGraph;
                
                DialogueItem current = FindFirstVertex(graph);
                Debug.Log(current.Name +": " + current.Dialogue);
                
                do
                {
                    List<DialogueItem> connectedVertices = graph.GetConnectedVertices(current);
                    int index = Random.Range(0, connectedVertices.Count);
                
                    // Debug.Log(index);
                    Debug.Log("Count: " + connectedVertices.Count);
                    
                    current = connectedVertices[index];
                    Debug.Log(current.Name + ": " + current.Dialogue);
                } 
                while (current.ID != "END");
            }
        }
        // Graphs<DialogueItem> graph = dialogueSets["HumphreyConvo1"];
        // DialogueItem current = FindFirstVertex(graph);
        // Debug.Log(current.Name +": " + current.Dialogue);
        //
        // do
        // {
        //     List<DialogueItem> connectedVertices = graph.GetConnectedVertices(current);
        //     int index = Random.Range(0, connectedVertices.Count);
        //
        //     Debug.Log(index);
        //     Debug.Log(connectedVertices.Count);
        //     
        //     current = connectedVertices[index];
        //     Debug.Log(current.Name + ": " + current.Dialogue);
        // } 
        // while (current.ID != "END");
    }

    DialogueItem FindFirstVertex(Graph<DialogueItem> graph)
    {
        foreach (DialogueItem item in graph.Vertices)
        {
            if (item.ID == "START")
            {
                return item;
            }
        }

        return null;
    }
}
