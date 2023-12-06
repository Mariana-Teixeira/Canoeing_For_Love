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

    private void Awake() {
        inventory = gameObject.GetComponent<InventoryManager>();
        tree = new DialogueRuntimeTree();
    } 

    private void Start()
    {
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
        int score;
        Guid node = Guid.Empty;
        if(dialogueBool.Character != null){
            if(dialogueBool.Character == "both"){
                // Inside this if, primaryNode is for Ken, secondaryNode is for Allen, backupNode is for backup
                // Outside of this if, primaryNode can be for both, depends on the character name
                // if there's no character name, then go down to random
                if(inventory.KenScore>inventory.AllenScore){
                    node = inventory.KenScore >= dialogueBool.MinimumScore ? dialogueBool.PrimaryNodeGUID : dialogueBool.BackupNodeGUID;
                }
                else if(inventory.KenScore<inventory.AllenScore){
                    node = inventory.AllenScore >= dialogueBool.MinimumScore ? dialogueBool.SecondaryNodeGUID : dialogueBool.BackupNodeGUID;
                }
                else if(inventory.KenScore==inventory.AllenScore){
                    Random random = new Random();
                    double test = random.NextDouble();
                    node = inventory.AllenScore >= dialogueBool.MinimumScore ? test > 0.5 ? dialogueBool.PrimaryNodeGUID : dialogueBool.SecondaryNodeGUID : dialogueBool.BackupNodeGUID;
                }
                GoToNextNode(node);
            }
            else{
                score = dialogueBool.Character == "ken" ? inventory.KenScore : inventory.AllenScore;
                node = score >= dialogueBool.MinimumScore ? dialogueBool.PrimaryNodeGUID : dialogueBool.BackupNodeGUID;
            }
        }
        else{
            // here we have the ability to have a random, picks a random node, out of the two given
            Random random = new Random();
            double test = random.NextDouble();
            node = test > 0.5 ? dialogueBool.PrimaryNodeGUID : dialogueBool.BackupNodeGUID;
        }
        GoToNextNode(node);
        
    }

    
    public IEnumerator CheckHasAnswer(DialogueChoices[] choices){
        yield return new WaitUntil(()=>choicePanel.GetAnswer()!=-1);
        nextNode = choices[choicePanel.GetAnswer()].NextNodeGUID;
        GoToNextNode(nextNode);
    }


    public void LoadGame(){
        headNode = dfh.LoadGame();
        inventory.LoadInventory();
    }

    public void NewGame(){
        dfh.NewGame();
        headNode = 1;
        inventory.NewInventory();
    }

     public void SaveGame(){
        // dfh.SaveGame(tree, cam);
        inventory.SaveInventory(tree, cam);
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