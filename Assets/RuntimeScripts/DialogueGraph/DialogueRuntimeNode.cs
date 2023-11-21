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
        SHOW_DIALOGUE,
        SHOW_NAMEPLATE,
        DISPLAY_CHARACTER,
        DISPLAY_BACKGROUND,
        GOTO_NEXTNODE,
        GOTO_CHOICESPANEL,
        ANIMATE_CAMERA,
        PLAY_SOUND,
    }
}
