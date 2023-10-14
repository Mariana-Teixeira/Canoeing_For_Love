using System;
using DialogueTree;

public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;
     public int GoToChoice;

    private void Awake() => tree = new DialogueRuntimeTree();


    public void StartDialogueTree()
    {
        tree.GoToHeadNode();
        NotifyObserver(tree.CurrentNode);
    }

    public void PublishNextNode()
    {
        NPCNode npc = (NPCNode)tree.CurrentNode;
        Guid nextNode = npc.NextNodeGUID;
        tree.GoToNextNode(nextNode);
        NotifyObserver(tree.CurrentNode);
    }

    public void PublishChosenNode()
    {
        PlayerNode player = (PlayerNode)tree.CurrentNode;
        Guid nextNode = player.Choices[GoToChoice].NextNodeGUID;
        tree.GoToNextNode(nextNode);
        NotifyObserver(tree.CurrentNode);
    }
}