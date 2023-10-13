using System;

namespace DialogueTree
{
    // TODO: Refactor to Abstract Class or Interface.
    public class DialogueRuntimeNode
    {}

    public class PlayerNode : DialogueRuntimeNode
    {
        public Guid Guid { get; private set; }
        public Choices[] Choices { get; private set; }

        public PlayerNode(Guid guid, Choices[] choices)
        {
            this.Guid = guid;
            this.Choices = choices;
        }
    }

    public class NPCNode : DialogueRuntimeNode
    {
        public Guid Guid { get; private set; }
        public Guid NextNodeGUID { get; private set; }
        string NPCDialogue;

        public NPCNode(Guid myGuid, Guid nextNodeGuid, string npcDialogue)
        {
            this.Guid = myGuid;
            this.NextNodeGUID = nextNodeGuid;
            this.NPCDialogue = npcDialogue;
        }

        public override string ToString()
        {
            return NPCDialogue;
        }
    }

    public struct Choices
    {
        public Guid NextNodeGUID { get; private set; }
        string ChoiceDialogue;

        public Choices(Guid nextNode, string dialogue)
        {
            this.NextNodeGUID = nextNode;
            this.ChoiceDialogue = dialogue;
        }

        public override string ToString()
        {
            return ChoiceDialogue;
        }
    }
}