using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TestDialogueLoader))]

public class DialogueManager : MonoBehaviour
{
    public delegate void DialogueFinished();
    public static event DialogueFinished OnDialogueFinished;
    
    public delegate void Humphrey1DialogueEnded();
    public static event Humphrey1DialogueEnded OnHumphrey1DialogueEnded;
    
    public delegate void HumphreySpeaking();
    public static event HumphreySpeaking OnHumphreySpeaking;
    
    public delegate void GilbertDialogueEnded();
    public static event GilbertDialogueEnded OnGilbertDialogueEnded;
    
    public delegate void CaptainDialogueEnded();
    public static event CaptainDialogueEnded OnCaptainDialogueEnded;
    
    public delegate void MayorDialogueEnded();
    public static event MayorDialogueEnded OnMayorDialogueEnded;
    
    public delegate void ChesterDialogueEnded();
    public static event ChesterDialogueEnded OnChesterDialogueEnded;
    
    public delegate void CaptainQuestAccepted();
    public static event CaptainQuestAccepted OnCaptainQuestAccepted;
    
    public delegate void CaptainQuestCompleted();
    public static event CaptainQuestCompleted OnCaptainQuestCompleted;
    
    public delegate void ChesterQuestAccepted();
    public static event ChesterQuestAccepted OnChesterQuestAccepted;
    
    public delegate void ChesterQuestCompleted();
    public static event ChesterQuestCompleted OnChesterQuestCompleted;
    
    public delegate void ChesterQuestFailed();
    public static event ChesterQuestFailed OnChesterQuestFailed;
    
    public delegate void GaveChangeToChester();
    public static event GaveChangeToChester OnGaveChangeToChester;
    
    public delegate void KeptChangeChester();
    public static event KeptChangeChester OnKeptChangeChester;
    
    public delegate void GaveChangeToGilbert();
    public static event GaveChangeToGilbert OnGaveChangeToGilbert;
    
    public delegate void GaveChangeToUnlikeable();
    public static event GaveChangeToUnlikeable OnGaveChangeToUnlikeable;
    
    public delegate void FinishedSpeakingWithUnlikeable();
    public static event FinishedSpeakingWithUnlikeable OnFinishedSpeakingWithUnlikeable;
    
    public delegate void FinishedSpeakingWithCitizen();
    public static event FinishedSpeakingWithCitizen OnFinishedSpeakingWithCitizen;
    
    
    [SerializeField] private float typeDelay = 0.002f;
    private DoublyLinkedList<DialogueSet> _dialogueSets;
    
    private int currentDialogueItemIndex;
    private string DialogueItemID;
    
    private DialogueUI dialogueUI;
    
    private TestDialogueLoader loader;
    private Dictionary<string, DialogueSet> dialogueSets;
    
    private DialogueSet currentDialogueSet;
    private Graph<DialogueItem> graph;
    private DialogueItem currentDialogueItem;



    // private DialogueLoader loader;
    // private Dictionary<string, Graphs<DialogueItem>> dialogueSets;

    void Awake()
    {
        // loader = GetComponent<DialogueLoader>();
        dialogueUI = GetComponent<DialogueUI>();
        loader = GetComponent<TestDialogueLoader>();
    }
    void Start()
    {
        dialogueUI.HideDialogueBox();
        dialogueSets = loader.DialogueSets;
        // dialogueSets = loader.DialogueSets;
    }

    private void OnEnable()
    {
        DialogueLoader.OnDialogueLoaded2 += OnDialogueLoaded2;
        // DialogueUI.OnNextClicked += OnNextClicked;
        // DialogueUI.OnPreviousClicked += OnPreviousClicked;
        DialogueTrigger.OnDialogueTriggered += OnDialogueTriggered;
        DialogueTrigger.OnDialogueEnded += OnDialogueEnded;
        DialogueUI.OnExitClickedAction += OnDialogueEnded;
        
        DialogueUI.OnPreviousClicked += OnOption2Clicked;
        DialogueUI.OnNextClicked += OnOption1Clicked;
    }

    private void OnDisable()
    {
        DialogueLoader.OnDialogueLoaded2 -= OnDialogueLoaded2;
        // DialogueUI.OnNextClicked -= OnNextClicked;
        // DialogueUI.OnPreviousClicked -= OnPreviousClicked;
        DialogueTrigger.OnDialogueTriggered -= OnDialogueTriggered;
        DialogueTrigger.OnDialogueEnded -= OnDialogueEnded;
        DialogueUI.OnExitClickedAction -= OnDialogueEnded;
        
        DialogueUI.OnPreviousClicked -= OnOption2Clicked;
        DialogueUI.OnNextClicked -= OnOption1Clicked;
    }

    private void OnDialogueEnded()
    {
        currentDialogueItem = null;
        
        OnDialogueFinished?.Invoke();
        dialogueUI.HideDialogueBox();
    }
    
    private void OnDialogueLoaded2(DoublyLinkedList<DialogueSet> _dialogueSets)
    {
        this._dialogueSets = _dialogueSets;
    }

    private void OnNextClicked()
    {
        currentDialogueItemIndex++;
        DisplayDialogueItem();
    }

    private void OnPreviousClicked()
    {
        if (currentDialogueItemIndex == 0)
        {
            return;
        }
        currentDialogueItemIndex--;
        DisplayDialogueItem();
    }
    
    private void OnOption1Clicked()
    {
        SFXManager._instance.PlaySound("Click");
        
        if (currentDialogueItem.Options.ElementAt(0).LinkID == "CLOSE")
        {
            DialogueEnd();
            currentDialogueItem = null;
            return;
        }

        if (currentDialogueItem.ID == "CaptainConvo_GiveQuest" || currentDialogueItem.ID == "CaptainGiveQuestConvo_Line2")
        {
            OnCaptainQuestAccepted?.Invoke();
        }

        if (currentDialogueItem.ID == "ChesterConvo_Line17" || currentDialogueItem.ID == "ChesterConvo_Line11" || 
            currentDialogueItem.ID == "ChesterGiveQuestConvo_QuestOffer_Line2")
        {
            OnChesterQuestAccepted?.Invoke();
        }

        if (currentDialogueItem.ID == "MayorConvo1_Password")
        {
            OnChesterQuestCompleted?.Invoke();
        }

        if (currentDialogueItem.ID == "ChesterQuestedCompleted_Line2")
        {
            OnKeptChangeChester?.Invoke();
        }
        
        string reqItem = currentDialogueItem.Options.ElementAt(0).RequiredItem;
        if (reqItem != null)
        {
            if (Inventory._instance.HasItem(reqItem))
            {
                currentDialogueItem = graph.GetConnectedVertices(currentDialogueItem).ElementAt(0);
                Inventory._instance.Remove(reqItem);
                DisplayItems2();
                return;
            }
            else
            {
                Debug.Log("You do not have that item");
                return;
            }
        }
        
        try
        {
            currentDialogueItem = graph.GetConnectedVertices(currentDialogueItem).ElementAt(0);
        }
        catch (Exception e)
        {
            Debug.Log("...Index was outside the bounds of the array");
        }

        DisplayItems2();
    }
    
    private void OnOption2Clicked()
    {
        SFXManager._instance.PlaySound("Click");
        
        string reqItem = currentDialogueItem.Options.ElementAt(1).RequiredItem;
        if (reqItem != null)
        {
            if (Inventory._instance.HasItem(reqItem))
            {
                if (currentDialogueItem.ID == "ChesterQuestedCompleted_Line2")
                {
                    OnGaveChangeToChester?.Invoke();
                }

                if (currentDialogueItem.ID == "GilbertIdleConvo_Line2" ||
                    currentDialogueItem.ID == "GilbertConvo1_line13")
                {
                    OnGaveChangeToGilbert?.Invoke();
                }

                if (currentDialogueItem.ID == "UnlikeableConvo_Line2")
                {
                    OnGaveChangeToUnlikeable?.Invoke();
                }
                
                currentDialogueItem = graph.GetConnectedVertices(currentDialogueItem).ElementAt(1);
                Inventory._instance.Remove(reqItem);
                DisplayItems2();
                return;
            }
            else
            {
                Debug.Log("You do not have that item");
                return;
            }
        }

        if (currentDialogueItem.ID == "ChesterConvo_QuestDecline" || currentDialogueItem.ID == "ChesterGiveQuestConvo_Line2")
        {
            OnCaptainQuestCompleted?.Invoke();
        }

        if (currentDialogueItem.ID == "MayorConvo1_Password")
        {
            OnChesterQuestFailed?.Invoke();
        }
        
        
        currentDialogueItem = graph.GetConnectedVertices(currentDialogueItem).ElementAt(1);
        
        DisplayItems2();
    }

    private void OnDialogueTriggered(string id)
    {
        // DisplayDialogueSet(id);
        Debug.Log(id);
        DisplayDialogueSet2(id);
    }
    
    private void DisplayDialogueSet2(string dialogueSetID)
    {
        foreach (DialogueSet set in dialogueSets.Values)
        {
            if (set.convoId == dialogueSetID)
            {
                currentDialogueSet = set;
                graph = currentDialogueSet.dialogueItemGraph;
            }
        }
        
        DisplayItems2();
        dialogueUI.ShowDialogueBox();
    }
    
    public void DisplayItems2()
    {
        if (currentDialogueItem == null)
        {
            currentDialogueItem = FindFirstVertex(graph);
        }

        // if (currentDialogueItem.ID == "END")
        // {
        //     dialogueUI.ButtonText = "END";
        // }
        
        dialogueUI.Name = currentDialogueItem.Name;
        dialogueUI.Image = currentDialogueItem.Image;
        dialogueUI.Dialogue = currentDialogueItem.Dialogue;
        
        Debug.Log($"Number of options: {currentDialogueItem.Options.Count}" );
        
        
        if (currentDialogueItem.Options.Count == 1)
        {
            DialogueOption option = currentDialogueItem.Options.ElementAt(0);
            
            // if (option.RequiredItem != null)
            // {
            //     dialogueUI.ButtonText =$"R: {option.Text}";
            //     dialogueUI.HideOption2Button();
            //     return;
            // }
            
            dialogueUI.ButtonText = option.Text;
            
            dialogueUI.HideOption2Button();
        }
        else
        {
            dialogueUI.ShowOption2Button();
            
            DialogueOption option1 = currentDialogueItem.Options.ElementAt(0);
            DialogueOption option2 = currentDialogueItem.Options.ElementAt(1);

            // if (option1.RequiredItem != null)
            // {
            //     string reqItem = currentDialogueItem.Options.ElementAt(1).RequiredItem;
            //     if (Inventory._instance.HasItem(reqItem))
            //     {
            //         dialogueUI.ButtonText = $"R: {option1.Text}";
            //     }
            //
            //     return;
            // }
            
            if (option2.RequiredItem != null)
            {
                string reqItem = currentDialogueItem.Options.ElementAt(1).RequiredItem;

                if (Inventory._instance.HasItem(reqItem))
                {
                    dialogueUI.Button2Text = $"R: {option2.Text}";
                    return;
                }
                
                dialogueUI.HideOption2Button();
            }
            
            dialogueUI.ButtonText = option1.Text;
            dialogueUI.Button2Text = option2.Text;
        }
        
        
        StopAllCoroutines();
        StartCoroutine(TypeTextCoroutine(currentDialogueItem.Dialogue));
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

    private void DisplayDialogueSet(string dialogueSetID)
    {
        Debug.Log("Before searching");
        currentDialogueSet = _dialogueSets.FindWithID(dialogueSetID);
        Debug.Log("After searching");
        
        currentDialogueItemIndex = 0;
        DisplayDialogueItem();
        dialogueUI.ShowDialogueBox();
    }

    private void DisplayDialogueItem()
    {
        //if dialogue is finished
        if (currentDialogueItemIndex >= currentDialogueSet.dialogueItemsList.ListSize)
        {
            DialogueEnd();
            currentDialogueSet = null;
            currentDialogueItemIndex = 0;
            return;
        }

        // button says continue if last dialogue item
        if (currentDialogueItemIndex == currentDialogueSet.dialogueItemsList.ListSize - 1)
        {
            dialogueUI.ButtonText = "Continue";
        }
        else
        {
            dialogueUI.ButtonText = "Next";
        }
        
        DialogueItemID = $"{currentDialogueSet.convoId}_line{currentDialogueItemIndex}";
        DialogueItem item = currentDialogueSet.dialogueItemsList.FindWithID(DialogueItemID);
        
        dialogueUI.Name = item.Name;
        IndicateSpeaker(item.Speaker);
        dialogueUI.Image = item.Image;
        
        StopAllCoroutines();
        StartCoroutine(TypeTextCoroutine(item.Dialogue));
    }

    private void IndicateSpeaker(string speaker)
    {
        if (speaker == "Humphrey")
        {
            OnHumphreySpeaking?.Invoke();
        }
    }
    
    private void DialogueEnd()
    {
        if (currentDialogueSet.convoId == "HumphreyConvo1")
        {
            OnHumphrey1DialogueEnded?.Invoke();
        }
        else if (currentDialogueSet.convoId == "CaptainConvo")
        {
            OnCaptainDialogueEnded?.Invoke();
        }
        else if (currentDialogueSet.convoId == "MayorConvo1")
        {
            OnMayorDialogueEnded?.Invoke();
        } 
        else if (currentDialogueSet.convoId == "GilbertConvo1")
        {
            OnGilbertDialogueEnded?.Invoke();
        }
        else if (currentDialogueSet.convoId == "ChesterConvo")
        {
            OnChesterDialogueEnded?.Invoke();
        }
        else if (currentDialogueSet.convoId == "UnlikeableConvo" ||
                 currentDialogueSet.convoId == "UnlikeableThankfulLine")
        {
            OnFinishedSpeakingWithUnlikeable?.Invoke();
        }
        else if (currentDialogueSet.convoId == "CitizenLine")
        {
            OnFinishedSpeakingWithCitizen?.Invoke();
        }

        OnDialogueEnded();
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
}
