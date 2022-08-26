using UnityEngine;

namespace Prototype.Scripts
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager _instance { get; private set; }

        private Interactable[] interactables;
        private TextParser parser;
    
        void Awake()
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
            InitializeTextParser();
        }

        private void OnEnable()
        {
            UIController.OnCommandSubmittedAction += OnCommandSubmitted;
        }
    
        private void OnDisable()
        {
            UIController.OnCommandSubmittedAction -= OnCommandSubmitted;
        }
    

        private void OnCommandSubmitted(string rawCommand)
        {
            Command command = parser.Parse(rawCommand);

            foreach (Interactable interactable in interactables)
            {
                if (interactable.ExecuteCommand(command))
                {
                    break;
                }
            }
        }

        private void InitializeTextParser()
        {
            interactables = FindObjectsOfType<Interactable>();
            parser = new TextParser();

            foreach (Interactable interactable in interactables)
            {
                foreach (KeywordTypePair pair in interactable.Keywords)
                {
                    parser.AddKeyword(pair.Keyword, pair.Type);
                }
            }
        }
    
    }
}

