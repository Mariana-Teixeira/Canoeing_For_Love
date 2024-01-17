using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DialogueTree;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, INodeSubscriber
{

    // Inventory manager manages the love score for each character and the items chosen for the race.
    // Also saves/loads those informations from the save file.
    public Slider kenSlider;
    public Slider allenSlider;

    public List<string> itemsChosen = new();


    private readonly DataFileHandler dfh = new();

    int kenScore = 0;
    int allenScore = 0;
    int maxScore = 15;

    public List<string> ItemsChosen
    {
        get
        {
            return itemsChosen;
        }
    }

    public int KenScore
    {
        get
        {
            return kenScore;
        }
    }
    public int AllenScore
    {
        get
        {
            return allenScore;
        }
    }

    #region Node Publisher
    NodePublisher publisher;
    private void Awake()
    {
        publisher = GetComponent<NodePublisher>();
    }
    private void OnEnable() => publisher.AddObserver(this);
    private void OnDisable() => publisher.RemoveObserver(this);
    #endregion

    private void Start()
    {
        kenSlider.maxValue = maxScore;
        allenSlider.maxValue = maxScore;
    }

    public void OnNotifyNode(DialogueRuntimeNode node)
    {
        var hash = node.DialogueEvents;

        if (hash.ContainsKey(DialogueEvents.ADD_SCORE))
        {
            IncreaseRelationshipScore((string)hash[DialogueEvents.ADD_SCORE]);
        }
        if (hash.ContainsKey(DialogueEvents.REMOVE_SCORE))
        {
            DecreaseRelationshipScore((string)hash[DialogueEvents.REMOVE_SCORE]);
        }
    }

    public void IncreaseRelationshipScore(string character)
    {
        if (character == "ken")
        {
            kenScore++;
            kenSlider.value++;
        }
        else if (character == "allen")
        {
            allenScore++;
            allenSlider.value++;
        }
    }

    public void DecreaseRelationshipScore(string character)
    {
        if (character == "ken")
        {
            if(kenScore > 0){
                kenScore--;
                kenSlider.value--;
            }
        }
        else if (character == "allen")
        {
            if(allenScore > 0){
                allenScore--;
                allenSlider.value--;
            }
        }
    }

    public void LoadInventory(){
        try{
            string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "data.json"));
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            int active = jsonObj["active"];
            allenScore = jsonObj["loaders"][active]["allenScore"];
            kenScore = jsonObj["loaders"][active]["kenScore"];
            string A = jsonObj["loaders"][active]["items"];
            string[] a = A.Split("_");
            foreach (var item in a){
                itemsChosen.Add(item);
            }            
            print(itemsChosen.Count);
            allenSlider.value = allenScore;
            kenSlider.value = kenScore;

        }
        catch(Exception e){
            Debug.LogError(e);
        }
    }

    public void NewInventory(){
        kenScore = 0;
        allenScore = 0;
    }

    public void SaveInventory(DialogueRuntimeTree tree, Camera cam){
        dfh.SaveGame(tree, cam, kenScore, allenScore, itemsChosen, true);
    }

    public void SaveInventoryWithoutExit(DialogueRuntimeTree tree, Camera cam){
        dfh.SaveGame(tree, cam, kenScore, allenScore, itemsChosen, false);
    }


}