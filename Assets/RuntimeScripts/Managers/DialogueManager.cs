using System;
using System.Diagnostics;
using DialogueTree;
using System.Collections;
using UnityEngine;

public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;

    ChoicesPanel choicePanel;

    private void Awake() {
        tree = new DialogueRuntimeTree();
        
    } 

    private void Start(){
        choicePanel = ChoicesPanel.instance;
    }


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
        StartCoroutine(CheckHasAnswer());
        Guid nextNode = player.Choices[choicePanel.GetAnswer()].NextNodeGUID;
        tree.GoToNextNode(nextNode);
        NotifyObserver(tree.CurrentNode);
    }

    public IEnumerator CheckHasAnswer(){
        yield return new WaitUntil(()=>choicePanel.GetAnswer()!=-1);
    }

}