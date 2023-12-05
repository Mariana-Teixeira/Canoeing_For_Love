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
using Random = System.Random;
using UnityEditor;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine.EventSystems;

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
        if (hash.ContainsKey(DialogueEvents.SHOW_CHOICESPANEL))
        {
            DisplayChoicesPanel((DialogueChoices[])hash[DialogueEvents.SHOW_CHOICESPANEL]);
            return;
        }
        if (hash.ContainsKey(DialogueEvents.GOTO_PATHNODE))
        {
            Debug.Log("insede gotopathnode");
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
        int score;
        Guid node = Guid.Empty;
        Debug.Log("yaaaaaaaaaaa");
        if(dialogueBool.Character != null){
            score = dialogueBool.Character == "ken" ? inventory.KenScore : inventory.AllenScore;
            node = score >= dialogueBool.MinimumScore ? dialogueBool.PrimaryNodeGUID : dialogueBool.BackupNodeGUID;
        }
        else{
            Random random = new Random();
            double test = random.NextDouble();
            Debug.Log(test);
            node = test > 0.5 ? dialogueBool.PrimaryNodeGUID : dialogueBool.BackupNodeGUID;
        }
        Debug.Log(node);
        GoToNextNode(node);
        
    }

    
    public IEnumerator CheckHasAnswer(DialogueChoices[] choices){
        yield return new WaitUntil(()=>choicePanel.GetAnswer()!=-1);
        nextNode = choices[choicePanel.GetAnswer()].NextNodeGUID;
        GoToNextNode(nextNode);
    }


    public void LoadGame(){
        headNode = dfh.LoadGame();
    }

    public void NewGame(){
        dfh.NewGame();
        headNode = 1;
    }

     public void SaveGame(){
        dfh.SaveGame(tree, cam);
    }
}

// public class Click : StandaloneInputModule
// {
//     public void ClickAt(float x, float y)
//     {
//         Input.simulateMouseWithTouches = true;
//         var pointerData = GetTouchPointerEventData(new Touch() 
//         {
//             position = new Vector2(x, y),
//         }, out bool b, out bool bb);

//         ProcessTouchPress(pointerData, true, true);
//     }

// }