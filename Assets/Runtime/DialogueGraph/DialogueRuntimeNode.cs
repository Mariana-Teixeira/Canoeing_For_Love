using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;

namespace DialogueTree
{
    // TODO: Refactor to Abstract Class or Interface.
    public class DialogueRuntimeNode
    {}

    public class PlayerNode : DialogueRuntimeNode
    {
        public Guid Guid { get; private set; }
        public Choices[] choices { get; private set; }

        public PlayerNode(Guid guid, Choices[] choices)
        {
            this.Guid = guid;
            this.choices = choices;
        }

        public override string ToString()
        {
            string s = string.Empty;
            foreach (var choice in choices)
            {
                s += choice.ToString() + " | ";
            }
            return s;
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