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
        Guid nextNodeGUID = Guid.Empty;
        var hash = tree.CurrentNode.DialogueEvents;
        if (hash.ContainsKey(DialogueEvents.OPEN_CHOICES_PANEL))
        {
            DisplayChoicesPanel((DialogueChoices[])hash[DialogueEvents.OPEN_CHOICES_PANEL], out nextNodeGUID);
        }
        else if (hash.ContainsKey(DialogueEvents.GO_TO_NEXT_NODE))
        {
            nextNodeGUID = (Guid)hash[DialogueEvents.GO_TO_NEXT_NODE];
        }
        GoToNextNode(nextNodeGUID);
    }
    public void DisplayChoicesPanel(DialogueChoices[] choices, out Guid nextNodeGuid)
    {
        StartCoroutine(CheckHasAnswer());
        nextNodeGuid = choices[choicePanel.GetAnswer()].NextNodeGUID;
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