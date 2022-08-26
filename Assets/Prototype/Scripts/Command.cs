namespace Prototype.Scripts
{
    public class Command
    {
        public string Action { get; private set; }
        public string Item { get; private set; }
        public string Object { get; private set; }
        public string Conjunction { get; private set; }
        public string Item2 { get; private set; }
        
        public string NPC { get; private set; }

        public Command(string action, string item, string _object, string conjunction, string item2, string npc)
        {
            Action = action;
            Item = item;
            Object = _object;
            Conjunction = conjunction;
            Item2 = item2;
            NPC = npc;
        }

        public bool IsValidCommand()
        {
            if (Action == "" && Item == "" && Object == "")
            {
                return false;
            }
      
            if (Action == "")
            {
                return false;
            }
      
            return true;
        }
    }
}
