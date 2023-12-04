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

    public struct DialoguePath
    {
        public Guid PrimaryNodeGUID { get; private set; }
        public Guid BackupNodeGUID { get; private set; }
        public string Character { get; private set; }
        public int MinimumScore { get; private set; }

        public DialoguePath(Guid primaryNode, Guid backupNode, string character, int score)
        {
            this.PrimaryNodeGUID = primaryNode;
            this.BackupNodeGUID = backupNode;
            this.Character = character;
            this.MinimumScore = score;
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
        SHOW_CHOICESPANEL,
        DISPLAY_CHARACTER,
        DISPLAY_BACKGROUND,
        ADD_SCORE,
        GOTO_NEXTNODE,
        GOTO_PATHNODE,
        ANIMATE_CAMERA,
        PLAY_SOUND,
    }
}
