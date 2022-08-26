using TMPro;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;
using UnityEngine.UI;

namespace Prototype.Scripts
{
    public class UIController : MonoBehaviour
    {
        public delegate void CommandSubmittedAction(string input);
        public static event CommandSubmittedAction OnCommandSubmittedAction ;

        [SerializeField] private RawImage menu;
        private bool showingMenu;
        
        [SerializeField] private TMP_InputField commandInput;
        // [SerializeField] private TMP_Text infoText;
        [SerializeField] private TMP_Text slotsAvailableText;
        [SerializeField] private TMP_Text slot1;
        [SerializeField] private TMP_Text slot2;
        [SerializeField] private TMP_Text slot3;
        [SerializeField] private TMP_Text slot4;
        [SerializeField] private TMP_Text slot5;
        
        private void Awake()
        {
            menu.gameObject.SetActive(false);
            
            slot1.text = null;
            slot2.text = null;
            slot3.text = null;
            slot4.text = null;
            slot5.text = null;
            commandInput.onEndEdit.AddListener(Submit);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!showingMenu)
                {
                    showingMenu = true;
                    menu.gameObject.SetActive(true);
                    return;
                }

                showingMenu = false;
                menu.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            Interactable.OnInteractionSuccess += OnInteractionSuccess;
            // ItemInteractable.OnItemAdded += OnItemAdded;
            Inventory.OnItemAdded += OnItemAdded;
            Inventory.OnRemoveItemAction += OnRemoveItemAction;
        }
    
        private void OnDisable()
        {
            Interactable.OnInteractionSuccess -= OnInteractionSuccess;
            // ItemInteractable.OnItemAdded -= OnItemAdded;
            Inventory.OnItemAdded -= OnItemAdded;
            Inventory.OnRemoveItemAction -= OnRemoveItemAction;
        }

        // private void OnObjectivesChangedAction(string text)
        // {
        //     infoText.text = $"Objectives: \n \n{text}";
        // }

        private void OnRemoveItemAction(string value)
        {
            if (value == slot1.text)
            {
                slot1.text = null;
            }
            else if (value == slot2.text)
            {
                slot2.text = null;
            }
            else if (value == slot3.text)
            {
                slot3.text = null;
            }
            else if (value == slot4.text)
            {
                slot4.text = null;
            }
            else if (value == slot5.text)
            {
                slot5.text = null;
            }
            
            slotsAvailableText.text = $"Slots Available: {5 - Inventory._instance.GetSize()}";
        }
        
        private void OnItemAdded(string value)
        {
            if (slot1.text == null)
            {
                slot1.text = value;
            }
            else if(slot2.text == null)
            {
                slot2.text = value;
            }
            else if(slot3.text == null)
            {
                slot3.text = value;
            }
            else if(slot4.text == null)
            {
                slot4.text = value;
            }
            else if(slot5.text == null)
            {
                slot5.text = value;
            }

            slotsAvailableText.text = $"Slots Available: {5 - Inventory._instance.GetSize()}";
        }
        
        private void OnInteractionSuccess(string text)
        {
            // infoText.text = text;
            commandInput.text = "";
        }

        private void Submit(string text)
        {
            Debug.Log(text);
            OnCommandSubmittedAction?.Invoke(text);
        }
    }
}
