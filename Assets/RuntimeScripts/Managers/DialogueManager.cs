using System;
using DialogueTree;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Drawing;
using UnityEngine.Profiling;
using Unity.VisualScripting;

public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;
    ChoicesPanel choicePanel;

    [SerializeField] Camera cam;


    public int headNode;

    private readonly DataFileHandler dfh = new();

    public Guid nextNode = Guid.Empty;

    private void Awake() => tree = new DialogueRuntimeTree();

    private void Start() {
        choicePanel = ChoicesPanel.instance;
    } 

    public void StartDialogueTree()
    {
        tree.GoToHeadNode(tree.data.guids[headNode]);
        NotifyObserver(tree.CurrentNode);
    }

    public void ExecuteNodeTypeAction()
    {
        var hash = tree.CurrentNode.DialogueEvents;
        if (hash.ContainsKey(DialogueEvents.GOTO_CHOICESPANEL))
        {
            DisplayChoicesPanel((DialogueChoices[])hash[DialogueEvents.GOTO_CHOICESPANEL]);
        }
        else if (hash.ContainsKey(DialogueEvents.GOTO_NEXTNODE))
        {
            nextNode = (Guid)hash[DialogueEvents.GOTO_NEXTNODE];
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
        nextNode = choices[choicePanel.GetAnswer()].NextNodeGUID;
        GoToNextNode(nextNode);
    }


    public void LoadGame(){
        headNode = dfh.LoadGame();
    }

     public void SaveGame(){
        dfh.SaveGame(tree, cam);
    }
}