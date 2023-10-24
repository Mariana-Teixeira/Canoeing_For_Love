using System;

namespace DialogueTree
{
    public class DialogueRuntimeNode
    {
        public Guid Guid;
        public string displayName;
        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public DialogueRuntimeNode(Guid myGuid, string characterName)
        {
            this.Guid = myGuid;
            this.displayName = characterName;
        }
    }

    public class PlayerNode : DialogueRuntimeNode
    {
        public DialogueChoices[] Choices { get; private set; }

        public PlayerNode(Guid myGuid, string playerName, DialogueChoices[] choices) : base(myGuid, playerName)
        {
            this.Choices = choices;
        }
    }

    public class NPCNode : DialogueRuntimeNode
    {
        public Guid NextNodeGUID { get; private set; }
        string characterDialogue;
        string imagePath;

        public string CharacterDialogue
        {
            get
            {
                return characterDialogue;
            }
        }
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
        }

        public NPCNode(Guid myGuid, Guid nextNodeGuid, string npcName, string npcDialogue, string imagePath) : base(myGuid, npcName)
        {
            this.NextNodeGUID = nextNodeGuid;
            this.characterDialogue = npcDialogue;
            this.imagePath = imagePath;
        }
    }

    public struct DialogueChoices
    {
        public Guid NextNodeGUID { get; private set; }
        string choiceDialogue;

        public DialogueChoices(Guid nextNode, string dialogue)
        {
            this.NextNodeGUID = nextNode;
            this.choiceDialogue = dialogue;
        }
    }
}