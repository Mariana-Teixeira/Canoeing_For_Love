using System;
using DialogueTree;
using System.Collections;
using UnityEngine;

public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;
    ChoicesPanel choicePanel;

    private void Awake() => tree = new DialogueRuntimeTree();

    private void Start() => choicePanel = ChoicesPanel.instance;

    public void StartDialogueTree()
    {
        tree.GoToHeadNode();
        NotifyObserver(tree.CurrentNode);
    }

    public void ExecuteNodeTypeAction()
    {
        Guid nextNodeGUID;
        if (tree.CurrentNode.GetType() == typeof(NPCNode))
        {
            NPCNode node = (NPCNode)tree.CurrentNode;
            nextNodeGUID = node.NextNodeGUID;
        }
        else
        {
            PlayerNode node = (PlayerNode)tree.CurrentNode;
            DisplayChoicesPanel(node, out nextNodeGUID);
        }
        GoToNextNode(nextNodeGUID);
    }
    public void DisplayChoicesPanel(PlayerNode node, out Guid nextNodeGuid)
    {
        StartCoroutine(CheckHasAnswer());
        nextNodeGuid = node.Choices[choicePanel.GetAnswer()].NextNodeGUID;
    }

    public void GoToNextNode(Guid nextNodeGuid)
    {
        tree.GoToNextNode(nextNodeGuid);
        NotifyObserver(tree.CurrentNode);
    }


    public IEnumerator CheckHasAnswer(){
        yield return new WaitUntil(()=>choicePanel.GetAnswer()!=-1);
    }
}