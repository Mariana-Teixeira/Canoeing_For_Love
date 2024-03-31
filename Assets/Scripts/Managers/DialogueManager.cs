using DialogueTree;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

/// <summary>
/// Dialogue Manager is concerned with the saving, loading and iterating of a dialogue graph.
/// </summary>
public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;
    InventoryManager inventory;
    ChoicesPanel choicePanel;

    [SerializeField] Camera cam;


    public int headNode;

    //private readonly DataFileHandler dfh = new();

    public Guid nextNode = Guid.Empty;

    
    private void Awake() {
        Debug.Log("DialogueManager Awake Method");
        inventory = gameObject.GetComponent<InventoryManager>();
        StartCoroutine(CreateDialogueTree());
    } 

    private void Start()
    {
        Debug.Log("DialogueManager Start Method");
        choicePanel = ChoicesPanel.instance;
    }

    public IEnumerator CreateDialogueTree()
    {
        Debug.Log("Reading JSON File through WebRequest...");
        var path = Path.Combine(Application.streamingAssetsPath, "dialogue.json");

        using (var request = UnityWebRequest.Get(path))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                yield break;
            }

            Debug.Log("Creating Dialogue Tree...");
            var json = request.downloadHandler.text;
            tree = new DialogueRuntimeTree(json);
            yield return new WaitForSeconds(3);
            StartDialogueTree();
        }

    }

    public void StartDialogueTree()
    {
        Debug.Log("Notifying Observers of Head Node...");
        tree.GoToHeadNode(tree.data.guids[headNode]);
        NotifyObserver(tree.CurrentNode);
    }

    /// <summary>
    /// Called when the current node of the dialogue graph is updated.
    /// Executes an action for each <c>DialogueEvent</c> stored in the current node.
    /// </summary>
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
        if (hash.ContainsKey(DialogueEvents.SHOW_CREDITS))
        {
            tree.GoToCredits();
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

    /// <summary>
    /// Choses a dialogue branch based on a conditionant.
    /// </summary>
    /// <param name="dialogueBool"> Stores a character, respective minimum score, item or random and the paths available for that branch. </param>
    // When there are conditionants to the players path, either based on score, items in the inventory or random
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
            else if(dialogueBool.Character.Contains("item_")){
                string item = dialogueBool.Character.Split("_")[1];
                node = inventory.itemsChosen.Contains(item) ? dialogueBool.PrimaryNodeGUID : dialogueBool.BackupNodeGUID;
                GoToNextNode(node);
            }
            else if(dialogueBool.Character.Contains("check_")){
                string character = dialogueBool.Character.Split("_")[1];
                int race = 0;
                if(inventory.ItemsChosen.Contains("red shell")){
                    race++;
                }
                if(inventory.ItemsChosen.Contains("termit spray")){
                    race++;
                }
                if (character == "allen"){
                    if(inventory.ItemsChosen.Contains("cake pops")){
                        race++;
                    }
                }
                else if(character == "ken"){
                    if(inventory.ItemsChosen.Contains("nut bar")){
                        race++;
                    }
                }
                print("items: " + inventory.itemsChosen.Count);
                print("points from race: " + race);
                node = race < 2 ? dialogueBool.PrimaryNodeGUID : dialogueBool.BackupNodeGUID;
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

    // Check if the player has chosen an option
    public IEnumerator CheckHasAnswer(DialogueChoices[] choices){
        yield return new WaitUntil(()=>choicePanel.GetAnswer()!=-1);
        nextNode = choices[choicePanel.GetAnswer()].NextNodeGUID;
        inventory.itemsChosen.Add(choices[choicePanel.GetAnswer()].AddItem);
        GoToNextNode(nextNode);
    }

    //public void LoadGame(){
    //    inventory.LoadInventory();
    //    headNode = dfh.LoadGame();
    //}

    public void NewGame(){
        //dfh.NewGame();
        headNode = 1;
        inventory.NewInventory();
    }

    //public void SaveGame()
    //{
    //    inventory.SaveInventory(tree, cam);
    //}

    //public void SaveGameWithoutExit()
    //{
    //    inventory.SaveInventoryWithoutExit(tree, cam);
    //}
}