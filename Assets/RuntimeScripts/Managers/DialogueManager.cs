using System;
using DialogueTree;
using System.Collections;
using UnityEngine;

public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;
    ChoicesPanel choicePanel;

    public Guid nextNode = Guid.Empty;

    private void Awake() => tree = new DialogueRuntimeTree();

    private void Start() => choicePanel = ChoicesPanel.instance;

    public void StartDialogueTree()
    {
        tree.GoToHeadNode();
        NotifyObserver(tree.CurrentNode);
    }

    public void ExecuteNodeTypeAction()
    {
        var hash = tree.CurrentNode.DialogueEvents;
        if (hash.ContainsKey(DialogueEvents.OPEN_CHOICES_PANEL))
        {
            DisplayChoicesPanel((DialogueChoices[])hash[DialogueEvents.OPEN_CHOICES_PANEL]);
        }
        else if (hash.ContainsKey(DialogueEvents.GO_TO_NEXT_NODE))
        {
            nextNode = (Guid)hash[DialogueEvents.GO_TO_NEXT_NODE];
            GoToNextNode(nextNode);
        }
       
    }
    public void DisplayChoicesPanel(DialogueChoices[] choices)
    {
        StartCoroutine(CheckHasAnswer(choices: choices));
        
        
    }

    public void GoToNextNode(Guid nextNodeGuid)
    {
        tree.GoToNextNode(nextNodeGuid);
        NotifyObserver(tree.CurrentNode);
    }


    public IEnumerator CheckHasAnswer(DialogueChoices[] choices){
        yield return new WaitUntil(()=>choicePanel.GetAnswer()!=-1);
        print("fattypumpum");
        print(choicePanel.GetAnswer());
        nextNode = choices[choicePanel.GetAnswer()].NextNodeGUID;
        GoToNextNode(nextNode);
    }
}