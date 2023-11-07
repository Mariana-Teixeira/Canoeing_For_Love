using System;
using System.Collections;

namespace DialogueTree
{
    public class DialogueRuntimeNode
    {
        public Guid Guid;
        public Character Character { get; private set; }
        public Hashtable DialogueEvents { get; private set; }

        public DialogueRuntimeNode(Guid myGuid, Character character, Hashtable dialogueEvents = null)
        {
            this.Guid = myGuid;
            this.Character = character;
            this.DialogueEvents = dialogueEvents;
        }
    }

    public class PlayerNode : DialogueRuntimeNode
    {
        public DialogueChoices[] Choices { get; private set; }

        public PlayerNode(Guid myGuid, Character player, DialogueChoices[] choices, Hashtable dialogueEvents = null) : base(myGuid, player, dialogueEvents)
        {
            this.Choices = choices;
        }
    }

    public class NPCNode : DialogueRuntimeNode
    {
        public Guid NextNodeGUID { get; private set; }
        public string Dialogue { get; private set; }

        public NPCNode(Guid myGuid, Character character, Guid nextNodeGuid, string dialogue, Hashtable dialogueEvents = null) : base(myGuid, character, dialogueEvents)
        {
            this.NextNodeGUID = nextNodeGuid;
            this.Dialogue = dialogue;
        }
    }

    public struct DialogueChoices
    {
        public Guid NextNodeGUID { get; private set; }
        public string ChoiceDialogue { get; private set; }

        public DialogueChoices(Guid nextNode, string dialogue)
        {
            this.NextNodeGUID = nextNode;
            this.ChoiceDialogue = dialogue;
        }
    }
}