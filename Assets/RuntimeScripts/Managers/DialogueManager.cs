using System;
using DialogueTree;
using System.Collections;
using UnityEngine;

public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;
    InventoryManager inventory;
    ChoicesPanel choicePanel;

    public Guid nextNode = Guid.Empty;

    private void Awake() => tree = new DialogueRuntimeTree();

    private void Start()
    {
        choicePanel = ChoicesPanel.instance;
        inventory = GetComponent<InventoryManager>();
    }

    public void StartDialogueTree()
    {
        tree.GoToHeadNode();
        NotifyObserver(tree.CurrentNode);
    }

    public void ExecuteNodeTypeAction()
    {
        var hash = tree.CurrentNode.DialogueEvents;
        Debug.Log(tree.CurrentNode.Guid);
        if (hash.ContainsKey(DialogueEvents.SHOW_CHOICESPANEL))
        {
            DisplayChoicesPanel((DialogueChoices[])hash[DialogueEvents.SHOW_CHOICESPANEL]);
            return;
        }
        if (hash.ContainsKey(DialogueEvents.GOTO_BOOLNODE))
        {
            DialogueBool choices = (DialogueBool)hash[DialogueEvents.GOTO_BOOLNODE];
            GoToBoolNode(choices);
            return;
        }
        if (hash.ContainsKey(DialogueEvents.GOTO_NEXTNODE))
        {
            nextNode = (Guid)hash[DialogueEvents.GOTO_NEXTNODE];
            GoToNextNode(nextNode);
            return;
        }       
    }
    public void DisplayChoicesPanel(DialogueChoices[] choices)
    {
        StartCoroutine(CheckHasAnswer(choices: choices));
    }

    public void GoToNextNode(Guid nextNodeGuid)
    {
        Debug.Log("GOTONEXTNODE");
        tree.GoToNextNode(nextNodeGuid);
        NotifyObserver(tree.CurrentNode);
    }

    public void GoToBoolNode(DialogueBool dialogueBool)
    {
        Debug.Log("GOTOBOOLNODE");
        tree.GoToNextNode(dialogueBool.PrimaryNodeGUID);
        NotifyObserver(tree.CurrentNode);
    }

    public IEnumerator CheckHasAnswer(DialogueChoices[] choices){
        yield return new WaitUntil(()=>choicePanel.GetAnswer()!=-1);
        nextNode = choices[choicePanel.GetAnswer()].NextNodeGUID;
        GoToNextNode(nextNode);
    }
}