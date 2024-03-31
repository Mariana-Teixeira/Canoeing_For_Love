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

    /// <summary>
    /// Used when the dialogue system needs a conditional branch.
    /// </summary>
    public struct DialoguePath
    {
        public Guid PrimaryNodeGUID { get; private set; }

        public Guid SecondaryNodeGUID { get; private set; }
        public Guid BackupNodeGUID { get; private set; }
        public string Character { get; private set; }
        public int MinimumScore { get; private set; }

        public DialoguePath(Guid primaryNode, Guid secondaryNode, Guid backupNode, string character, int score)
        {
            this.PrimaryNodeGUID = primaryNode;
            this.SecondaryNodeGUID = secondaryNode;
            this.BackupNodeGUID = backupNode;
            this.Character = character;
            this.MinimumScore = score;
        }
    }

    public struct DialogueChoices
    {
        public Guid NextNodeGUID { get; private set; }
        public string ChoiceDialogue { get; private set; }
        public string AddItem { get; private set; }

        public DialogueChoices(Guid nextNode, string dialogue, string item)
        {
            this.NextNodeGUID = nextNode;
            this.ChoiceDialogue = dialogue;
            this.AddItem = item;
        }
    }

    /// <summary>
    /// Collection of possible events each <c>DialogueRuntimeNode</c> stores. <c>DialogueTree</c> sends these events to other systems.
    /// </summary>
    public enum DialogueEvents
    {
        SHOW_DIALOGUE,
        SHOW_NAMEPLATE,
        SHOW_CHOICESPANEL,
        DISPLAY_CHARACTER,
        DISPLAY_BACKGROUND,
        ADD_SCORE,
        REMOVE_SCORE,
        GOTO_NEXTNODE,
        GOTO_PATHNODE,
        ANIMATE_CAMERA,
        PLAY_SOUND,
        SHOW_CREDITS,
    }
}
