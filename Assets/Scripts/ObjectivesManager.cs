using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesManager : MonoBehaviour
{
    public static ObjectivesManager _instance { get; private set; }

    [SerializeField] private TMP_Text input;
    [SerializeField] private TMP_Text completedQuests;

    public bool humphreyFirstConvoCompleted;

    public bool CaptainQuestAccepted;
    public bool CaptainQuestCompleted;
    public bool CaptainRewardRecieved;

    public bool ChesterQuestAccepted;
    public bool ChesterQuestCompleted;
    public bool ChesterQuestFailed;
    public bool GaveChangeToChester;
    public bool KeptChangeChester;

    public bool GaveChangeToGilbert;

    public bool GaveChangeToUnlikeable;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        SetText("Talk to the guard");
        completedQuests.text = "Completed Quests: \n";
    }

    private void OnEnable()
    {
        DialogueManager.OnHumphrey1DialogueEnded += OnHumphrey1DialogueEnded;
        DialogueManager.OnCaptainQuestAccepted += OnCaptainQuestAccepted;
        DialogueManager.OnCaptainQuestCompleted += OnCaptainQuestCompleted;
        CaptainManager.OnCaptainRewardRecieved += OnCaptainRewardRecieved;
        DialogueManager.OnChesterQuestAccepted += OnChesterQuestAccepted;
        DialogueManager.OnChesterQuestCompleted += OnChesterQuestCompleted;
        DialogueManager.OnChesterQuestFailed += OnChesterQuestFailed;
        DialogueManager.OnGaveChangeToChester += OnGaveChangeToChester;
        DialogueManager.OnKeptChangeChester += OnKeptChangeChester;
        DialogueManager.OnGaveChangeToGilbert += OnGaveChangeToGilbert;
        DialogueManager.OnGaveChangeToUnlikeable += OnGaveChangeToUnlikeable;
    }

    private void OnDisable()
    {
        DialogueManager.OnHumphrey1DialogueEnded -= OnHumphrey1DialogueEnded;
        DialogueManager.OnCaptainQuestAccepted -= OnCaptainQuestAccepted;
        DialogueManager.OnCaptainQuestCompleted -= OnCaptainQuestCompleted;
        CaptainManager.OnCaptainRewardRecieved -= OnCaptainRewardRecieved;
        DialogueManager.OnChesterQuestAccepted -= OnChesterQuestAccepted;
        DialogueManager.OnChesterQuestCompleted -= OnChesterQuestCompleted;
        DialogueManager.OnChesterQuestFailed -= OnChesterQuestFailed;
        DialogueManager.OnGaveChangeToChester -= OnGaveChangeToChester;
        DialogueManager.OnKeptChangeChester -= OnKeptChangeChester;
        DialogueManager.OnGaveChangeToGilbert -= OnGaveChangeToGilbert;
        DialogueManager.OnGaveChangeToUnlikeable -= OnGaveChangeToUnlikeable;
    }

    private void OnGaveChangeToUnlikeable()
    {
        GaveChangeToUnlikeable = true;
        SetQuestCompletedText("Gave change to Unlikeable");

        if (GaveChangeToChester && GaveChangeToGilbert)
        {
            SetQuestCompletedText("You gave change to all of the Unlikeables! Good for you!");
        }
    }

    private void OnGaveChangeToGilbert()
    {
        GaveChangeToGilbert = true;
        SetQuestCompletedText("Gave change to Gilbert");
        
        if (GaveChangeToChester && GaveChangeToUnlikeable)
        {
            SetQuestCompletedText("You gave change to all of the Unlikeables! Good for you!");
        }
    }

    private void OnKeptChangeChester()
    {
        KeptChangeChester = true;
        SetText("No new tasks");
        SetQuestCompletedText("Did not give change to Chester");
    }

    private void OnGaveChangeToChester()
    {
        GaveChangeToChester = true;
        SetText("No new tasks");
        SetQuestCompletedText("Gave change to Chester");
        
        if (GaveChangeToUnlikeable && GaveChangeToGilbert)
        {
            SetQuestCompletedText("You gave change to all of the Unlikeables! Good for you!");
        }
    }

    private void OnChesterQuestFailed()
    {
        ChesterQuestFailed = true;
        SetText("Talk to Chester");
        SetQuestCompletedText("Failed to get change from Mayor");
    }

    private void OnChesterQuestCompleted()
    {
        ChesterQuestCompleted = true;
        SetText("Talk to Chester");
        SetQuestCompletedText("Pretended to be a delivery boy to get change from the mayor");
    }

    private void OnChesterQuestAccepted()
    {
        ChesterQuestAccepted = true;
        SetText("Talk to the Mayor");
    }

    private void OnCaptainRewardRecieved()
    {
        CaptainRewardRecieved = true;
        SetText("No new tasks");
    }

    private void OnCaptainQuestCompleted()
    {
        CaptainQuestCompleted = true;
        SetText("Speak to Captain");
        SetQuestCompletedText("Spoke to Chester about keeping quiet");
    }

    private void OnCaptainQuestAccepted()
    {
        CaptainQuestAccepted = true;
        SetText("Talk to Chester");
    }

    private void OnHumphrey1DialogueEnded()
    {
        humphreyFirstConvoCompleted = true;
        SetText("Talk to the captain");
        SetQuestCompletedText("Spoke to Humphrey");
    }

    private void SetQuestCompletedText(string text)
    {
        completedQuests.text += ("\n" + text);
    }

    private void SetText(string text)
    {
        input.text = $"OBJECTIVES: \n\n{text}";
    }
}
