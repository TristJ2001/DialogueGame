using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public delegate void NextClickedAction();
    public static event NextClickedAction OnNextClicked;
    
    public delegate void PreviousClickedAction();
    public static event PreviousClickedAction OnPreviousClicked;
    
    public delegate void ExitClickedAction();
    public static event ExitClickedAction OnExitClickedAction;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI button2Text;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject dialogueBox;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        nextButton.onClick.AddListener(() => { OnNextClicked?.Invoke(); });
        previousButton.onClick.AddListener(() => { OnPreviousClicked?.Invoke();});
        exitButton.onClick.AddListener(() => { OnExitClickedAction?.Invoke(); });
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void HideOption2Button()
    {
        previousButton.gameObject.SetActive(false);
    }
    
    public void ShowOption2Button()
    {
        previousButton.gameObject.SetActive(true);
    }
    
    public void ShowDialogueBox()
    {
        dialogueBox.SetActive(true);
    }

    public void HideDialogueBox()
    {
        dialogueBox.SetActive(false);
    }
    
    public Sprite Image
    {
        set { image.sprite = value; }
    }

    public string Name
    {
        set { name.text = value; }
    }

    public string Dialogue
    {
        set { dialogue.text = value; }
        get { return dialogue.text; }
    }

    public string ButtonText
    {
        set { buttonText.text = value; }
    }
    
    public string Button2Text
    {
        set { button2Text.text = value; }
    }
}