using System;
using System.Collections;

namespace DialogueTree
{
    public class DialogueRuntimeNode
    {
        public Guid Guid;
        public Hashtable DialogueEvents { get; private set; }

        public DialogueRuntimeNode(Guid myGuid, Hashtable dialogueEvents = null)
        {
            this.Guid = myGuid;
            this.DialogueEvents = dialogueEvents;
        }
    }

    public struct Character
    {
        public string Name { get; private set; }
        public string PortraitPath { get; private set; }
        public Character(string name, string portraitPath)
        {
            this.Name = name;
            this.PortraitPath = portraitPath;
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
    public enum DialogueEvents
    {
        DISPLAY_DIALOGUE,
        DISPLAY_CHARACTER,
        GO_TO_NEXT_NODE,
        OPEN_CHOICES_PANEL,
        ANIMATE_CAMERA,
    }
}
