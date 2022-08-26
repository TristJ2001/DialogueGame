using System;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

namespace Prototype.Scripts
{
    public class ItemInteractable : Interactable
    {
        // public delegate void ItemAddedAction(string input);
        // public static event ItemAddedAction OnItemAdded;
        
        protected const string TAKE = "take";
        protected const string PICK_UP = "pick up";
        protected const string GRAB = "grab";
        protected const string GOLD_COIN = "goldcoin";
        protected const string WOODEN_COIN = "woodencoin";

        private bool canPickUp = false;

        private string[] splitSentences;
        private void OnTriggerEnter(Collider other)
        {
            canPickUp = true;
            BroadcastInteraction($"{EntityName} can be picked up");
        }

        private void OnTriggerExit(Collider other)
        {
            canPickUp = false;
            BroadcastInteraction("");
        }

        protected override void InitializeKeywords()
        {
            entityType = EntityType.ITEM;
        
            base.InitializeKeywords();
        
            // Keywords.Add(new KeywordTypePair(TAKE, EntityType.ACTION));
            // Keywords.Add(new KeywordTypePair(PICK_UP, EntityType.ACTION));
            // Keywords.Add(new KeywordTypePair(GRAB, EntityType.ACTION));
            // Keywords.Add(new KeywordTypePair(GOLD_COIN, EntityType.ITEM));
            // Keywords.Add(new KeywordTypePair(WOODEN_COIN, EntityType.ITEM));
        }

        protected override bool HandleCommand(Command command)
        {
            bool baseResult = base.HandleCommand(command);

            if (baseResult)
            {
                return true;
            }

            
            if (command.Action == TAKE || command.Action == PICK_UP || command.Action == GRAB)
            {
                if (command.Item == GOLD_COIN || command.Item == WOODEN_COIN)
                {
                    canPickUp = true;
                }
                
                if (canPickUp)
                {
                    if (command.Item2 != "")
                    {
                        GameObject item1 = GameObject.FindGameObjectWithTag(command.Item);
                        GameObject item2 = GameObject.FindGameObjectWithTag(command.Item2);
                        
                        Debug.Log($"{command.Item} : {command.Item2}");
                        Inventory._instance.Add(command.Item, item1);
                        // OnItemAdded?.Invoke(command.Item);
                        Inventory._instance.Add(command.Item2, item2);
                        // OnItemAdded?.Invoke(command.Item2);
                        BroadcastInteraction($"The {command.Item} and {command.Item2} were placed in your inventory.");
                        return true;
                    }

                    if (Inventory._instance.GetSize() >= 5)
                    {
                        BroadcastInteraction($"Your inventory is full, could not add item.");
                        return false;
                    }
                    
                    Inventory._instance.Add(command.Item, gameObject);
                    // OnItemAdded?.Invoke(command.Item);
                    BroadcastInteraction($"The {command.Item} was placed in your inventory.");
                    canPickUp = false;
                    return true;
                }
            }

            return false;
        }
    }
}