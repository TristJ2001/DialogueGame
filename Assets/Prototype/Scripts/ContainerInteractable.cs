using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Prototype.Scripts
{
    public class ContainerInteractable : Interactable
    {
        // public delegate void RemoveItemAction(string value);
        // public static event RemoveItemAction OnRemoveItemAction;

        public delegate void GaveChangeToUnlikeableAction();
        public static event GaveChangeToUnlikeableAction OnGaveChangeToUnlikeableAction;
        
        public delegate void GaveChangeToGilbertAction();
        public static event GaveChangeToGilbertAction OnGaveChangeToGilbertAction;
        
        // public delegate void ItemAddedAction(string input);
        // public static event ItemAddedAction OnItemAdded;
        
        private Dictionary<string, GameObject> boxInventory = new Dictionary<string, GameObject>();
    
        private const string PLACE = "place";
        private const string PUT = "put";
        private const string USE = "use";
        private const string ADD = "add";
        private const string TAKE = "take";
        private const string PICK_UP = "pick up";
        private const string GRAB = "grab";
        private const string GIVE = "give";
        private const string BOX = "box";
        private const string WOODEN_COIN = "woodencoin";
        private const string CHANGE = "change";
        private const string SILVER = "silver";
        // private const string GILBERT = "gilbert";
        private const string BANK = "bank";
        private const string PLATINUM = "platinum";

        protected override void InitializeKeywords()
        {
            base.InitializeKeywords();
        
            Keywords.Add(new KeywordTypePair(TAKE, EntityType.ACTION));
            Keywords.Add(new KeywordTypePair(PICK_UP, EntityType.ACTION));
            Keywords.Add(new KeywordTypePair(GRAB, EntityType.ACTION));
            Keywords.Add(new KeywordTypePair(PLACE, EntityType.ACTION));
            Keywords.Add(new KeywordTypePair(PUT, EntityType.ACTION));
            Keywords.Add(new KeywordTypePair(USE, EntityType.ACTION));
            Keywords.Add(new KeywordTypePair(ADD, EntityType.ACTION));
            Keywords.Add(new KeywordTypePair(GIVE, EntityType.ACTION));
            Keywords.Add(new KeywordTypePair(BOX, EntityType.OBJECT));
            Keywords.Add(new KeywordTypePair(WOODEN_COIN, EntityType.ITEM));
            Keywords.Add(new KeywordTypePair(CHANGE, EntityType.ITEM));
            Keywords.Add(new KeywordTypePair(SILVER, EntityType.ITEM));
            // Keywords.Add(new KeywordTypePair(GILBERT, EntityType.OBJECT));
            Keywords.Add(new KeywordTypePair(BANK, EntityType.OBJECT));
            Keywords.Add(new KeywordTypePair(PLATINUM, EntityType.ITEM));
        }

        public void Start()
        {
            if (EntityName == BANK)
            {
                GameObject PlatinumCoin = GameObject.FindGameObjectWithTag("platinum");
                boxInventory.Add("platinum", PlatinumCoin);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (EntityName.Equals("unlikeable"))
            {
                if (Inventory._instance.HasItem("change"))
                {
                    BroadcastInteraction("You can give change to the unlikeable");
                }
            }
            else if (EntityName.Equals("gilbert"))
            {
                if (Inventory._instance.HasItem("change"))
                {
                    BroadcastInteraction("You can give change to Gilbert");
                }
            }
        }

        protected override bool HandleCommand(Command command)
        {
            bool baseResult = base.HandleCommand(command);

            if (baseResult)
            {
                return true;
            }

            if (command.Item == "")
            {
                return false;
            }

            //taking something out of box
            if (command.Action == TAKE || command.Action == GRAB || command.Action == PICK_UP)
            {
                if (!boxInventory.ContainsKey(command.Item))
                {
                    return false;
                }

                GameObject removeObject = boxInventory[command.Item];
                Inventory._instance.Add(command.Item, removeObject);
                BroadcastInteraction($"{command.Item} was taken out of the {EntityName} and placed in your inventory");
            }

            //putting something into box
            if (command.Action == PLACE || command.Action == PUT || command.Action == USE || command.Action == ADD || command.Action == GIVE)
            {
                GameObject objectToAdd = Inventory._instance.Remove(command.Item);

                if (objectToAdd == null)
                {
                    throw new ArgumentException("You do not have that item in your inventory");
                }

                if (command.Item2 != "" && command.Object == BOX)
                {
                    BroadcastInteraction($"You cannot use both of those items together on the box");
                    return false;
                }

                if ((command.Item == CHANGE && command.Item2 == SILVER && command.Object == BANK) ||
                    (command.Item == SILVER && command.Item2 == CHANGE && command.Object == BANK))
                {
                    BroadcastInteraction($"You gave {command.Item} and {command.Item2} to the bank and received a platinum coin");
                    GameObject item1 = GameObject.FindGameObjectWithTag(command.Item);
                    GameObject item2 = GameObject.FindGameObjectWithTag(command.Item2);
                    
                    boxInventory.Add(command.Item, item1);
                    
                    Inventory._instance.Remove(command.Item);
                    // OnRemoveItemAction?.Invoke(command.Item);
                    
                    boxInventory.Add(command.Item2, item2);

                    Inventory._instance.Remove(command.Item2);
                    // OnRemoveItemAction?.Invoke(command.Item2);
                    
                    GameObject PlatinumCoin = GameObject.FindGameObjectWithTag("platinum");
                    Inventory._instance.Add("platinum", PlatinumCoin);

                    return true;
                }

                // if (command.Object == UNLIKEABLE && command.Item == CHANGE)
                // {
                //     OnGaveChangeToUnlikeableAction?.Invoke();
                // }
                
                // if (command.Object == GILBERT && command.Item == CHANGE)
                // {
                //     OnGaveChangeToGilbertAction?.Invoke();
                // }

                if (command.Item == WOODEN_COIN)
                {
                    BroadcastInteraction($"You cannot place that item in there");
                    return false;
                }
                
                boxInventory.Add(command.Item, objectToAdd);
                Inventory._instance.Remove(command.Item);
                // OnRemoveItemAction?.Invoke(command.Item);
                
                if (command.Action == GIVE)
                {
                    BroadcastInteraction($"{command.Item} was given to {EntityName}");
                }
                else
                {
                    BroadcastInteraction($"{command.Item} was placed into the {EntityName}");
                }
                return true;
            }

            return false;
        }
    }
}
