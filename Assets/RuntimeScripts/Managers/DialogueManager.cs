using System;
using System.Diagnostics;
using DialogueTree;

public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;

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

    public void DisplayChoices()
    {

    }

    public void PublishChosenNode()
    {
        PlayerNode player = (PlayerNode)tree.CurrentNode;
        Guid nextNode = player.Choices[0].NextNodeGUID;
        tree.GoToNextNode(nextNode);
        NotifyObserver(tree.CurrentNode);
    }
}