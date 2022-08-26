using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Scripts
{
    public class Interactable : MonoBehaviour
    {
        // public static Interactable _instance { get; private set; }
        
        public delegate void InteractionSuccessAction(string message);
        public static event InteractionSuccessAction OnInteractionSuccess;

        public delegate void WalkToAction(string target);
        public static event WalkToAction OnWalkToAction;
        
        public delegate void TalkToAction(string target);
        public static event TalkToAction OnTalkToAction;
    
        [SerializeField] private List<KeywordTypePair> keywords = new List<KeywordTypePair>();
        
        [SerializeField] private string entityName;
        [SerializeField] private string descriptionText;

        private GameObject citizen;

        protected EntityType entityType = EntityType.OBJECT;

        private bool isWalking;

        private string target;

        protected const string LOOK_AT = "look";
        protected const string INSPECT = "inspect";
        protected const string AND = "and";
        protected const string WALK_TO = "walk to";
        protected const string TALK_TO = "talk to";

        protected const string HUMPHREY = "humphrey";
        protected const string GUARD = "guard";
        protected const string CAPTAIN = "captain";
        protected const string MAYOR = "mayor";
        protected const string GILBERT = "gilbert";
        protected const string CHESTER = "chester";
        protected const string UNLIKEABLE = "unlikeable";
        protected const string CITIZEN = "citizen";
        
        protected const string BOX = "box";

        public List<KeywordTypePair> Keywords
        {
            get => keywords;
        }

        public string EntityName
        {
            get => entityName.ToLower();
        }

        // public bool IsWalking
        // {
        //     get => isWalking;
        // }

        public string Target
        {
            get => target;
        }

        private void Awake()
        {
            // if (_instance != null)
            // {
            //     Destroy(this);
            //     return;
            // }
            //
            // _instance = this;
            
            InitializeKeywords();
        }

        // private void OnEnable()
        // {
        //     throw new NotImplementedException();
        // }

        protected virtual void InitializeKeywords()
        {
            keywords.Add(new KeywordTypePair(INSPECT, EntityType.ACTION));
            keywords.Add(new KeywordTypePair(LOOK_AT, EntityType.ACTION));
            keywords.Add(new KeywordTypePair(AND, EntityType.CONJUNCTION));
            keywords.Add(new KeywordTypePair(WALK_TO, EntityType.ACTION));
            keywords.Add(new KeywordTypePair(TALK_TO, EntityType.ACTION));
            keywords.Add(new KeywordTypePair(HUMPHREY, EntityType.OBJECT));
            keywords.Add(new KeywordTypePair(GUARD, EntityType.OBJECT));
            keywords.Add(new KeywordTypePair(CAPTAIN, EntityType.OBJECT));
            keywords.Add(new KeywordTypePair(MAYOR, EntityType.OBJECT));
            keywords.Add(new KeywordTypePair(GILBERT, EntityType.OBJECT));
            keywords.Add(new KeywordTypePair(CHESTER, EntityType.OBJECT));
            Keywords.Add(new KeywordTypePair(UNLIKEABLE, EntityType.OBJECT));
            Keywords.Add(new KeywordTypePair(CITIZEN, EntityType.OBJECT));
            
            keywords.Add(new KeywordTypePair(BOX, EntityType.OBJECT));
            keywords.Add(new KeywordTypePair(EntityName, entityType));
        }

        public bool ExecuteCommand(Command command)
        {
            if (command.Object == "guard" && EntityName == "humphrey")
            {
                return HandleCommand(command);
            }

            if (command.Object == "citizen" && command.Action == "walk to")
            {
                if (HumphreyManager._instance.IsConvoCompleted() == false)
                {
                    Debug.Log("You cannot access that character yet");
                    return false;
                }
                // citizen = GameObject.FindGameObjectWithTag("citizen");
                OnWalkToAction?.Invoke(command.Object);
            }
            
            if (command.Object == "citizen" && command.Action == "talk to")
            {
                if (HumphreyManager._instance.IsConvoCompleted() == false)
                {
                    Debug.Log("You cannot access that character yet");
                    return false;
                }
                // citizen = GameObject.FindGameObjectWithTag("citizen");
                OnTalkToAction?.Invoke(command.Object);
            }
            
            if (command.Object == "unlikeable" && command.Action == "walk to")
            {
                if (HumphreyManager._instance.IsConvoCompleted() == false)
                {
                    Debug.Log("You cannot access that character yet");
                    return false;
                }
                // citizen = GameObject.FindGameObjectWithTag("citizen");
                OnWalkToAction?.Invoke(command.Object);
            }
            
            if (command.Object == "unlikeable" && command.Action == "talk to")
            {
                if (HumphreyManager._instance.IsConvoCompleted() == false)
                {
                    Debug.Log("You cannot access that character yet");
                    return false;
                }
                // citizen = GameObject.FindGameObjectWithTag("citizen");
                OnTalkToAction?.Invoke(command.Object);
            }
            
            if (command.Object != EntityName && command.Item != EntityName)
            {
                return false;
            }
            
            return HandleCommand(command);

        }

        protected virtual bool HandleCommand(Command command)
        {
            if (command.Action == LOOK_AT || command.Action == INSPECT)
            {
                BroadcastInteraction(descriptionText);
                return true;
            }

            if (command.Action == WALK_TO)
            {
                if (command.Object == null)
                {
                    BroadcastInteraction("Please enter your destination");
                    return false;
                }
                
                

                if ((command.Object == "gilbert" || command.Object == "captain" || command.Object == "mayor" || command.Object == "chester"
                    || command.Object == "unlikeable" || command.Object == "citizen") &&
                    HumphreyManager._instance.IsConvoCompleted() == false)
                {
                    Debug.Log("You cannot access that character yet");
                    return false;
                }

                // isWalking = true;
                // target = command.Object;
                
                // BroadcastInteraction($"{command.Action} : {command.Object}");
                
                if (command.Object == "guard")
                {
                    OnWalkToAction?.Invoke("humphrey");
                    return true;
                }

                OnWalkToAction?.Invoke(command.Object);
                return true;
            }

            if (command.Action == TALK_TO)
            {
                if (command.Object == null)
                {
                    BroadcastInteraction("Please enter who you would like to talk to");
                    return false;
                }
                
                Debug.Log(command.Object);
                
                if ((command.Object == "gilbert" || command.Object == "captain" || command.Object == "mayor" || command.Object == "chester") &&
                    HumphreyManager._instance.IsConvoCompleted() == false)
                {
                    Debug.Log("You cannot access that character yet");
                    return false;
                }
                
                if (command.Object == "guard")
                {
                    OnTalkToAction?.Invoke("humphrey");
                    return true;
                }

                OnTalkToAction?.Invoke(command.Object);
                return true;
            }

            return false;
        }


        public bool IsWalking()
        {
            if (isWalking)
            {
                return true;
            }

            return false;
        }
        
        protected void BroadcastInteraction(string message)
        {
            OnInteractionSuccess?.Invoke(message);
        }
    }
}
