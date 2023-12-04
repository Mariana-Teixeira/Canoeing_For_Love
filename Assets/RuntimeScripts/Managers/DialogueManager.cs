using System;
using DialogueTree;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Net.Sockets;
using System.Linq;
using System.IO;
using System.Drawing;
using UnityEngine.Profiling;
using Unity.VisualScripting;


public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;
    InventoryManager inventory;
    ChoicesPanel choicePanel;

    [SerializeField] Camera cam;


    public int headNode;

    private readonly DataFileHandler dfh = new();

    public Guid nextNode = Guid.Empty;

    private void Awake() => tree = new DialogueRuntimeTree();

    private void Start()
    {
        choicePanel = ChoicesPanel.instance;
        inventory = GetComponent<InventoryManager>();
    }

    public void StartDialogueTree()
    {
        tree.GoToHeadNode(tree.data.guids[headNode]);
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
        if (hash.ContainsKey(DialogueEvents.GOTO_PATHNODE))
        {
            DialoguePath choices = (DialoguePath)hash[DialogueEvents.GOTO_PATHNODE];
            GoToPathNode(choices);
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
        tree.GoToNextNode(nextNodeGuid);
        NotifyObserver(tree.CurrentNode);
    }

    public void GoToPathNode(DialoguePath dialogueBool)
    {
        var score = dialogueBool.Character == "ken" ? inventory.KenScore : inventory.AllenScore;
        var node = score >= dialogueBool.MinimumScore ? dialogueBool.PrimaryNodeGUID : dialogueBool.BackupNodeGUID;
        tree.GoToNextNode(node);
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