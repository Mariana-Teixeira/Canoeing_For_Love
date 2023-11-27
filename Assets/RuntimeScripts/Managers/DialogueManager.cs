using System;
using DialogueTree;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Drawing;
using UnityEngine.Profiling;

public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;
    ChoicesPanel choicePanel;

    [SerializeField] Camera cam;


    public int headNode;

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
        try{
            string json = File.ReadAllText("Assets/RuntimeScripts/SaveLoadSystem/data.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            int active = jsonObj["active"];
            headNode = jsonObj["loaders"][active]["node"];
            print(headNode);
            if (headNode==0){
                headNode = 1;
            }
        }
        catch(Exception e){
            Debug.LogError(e);
        }
        
    }

     public void SaveGame(){
        // maybe refactor in the future, maybe not
        DataToSave d = new DataToSave();
        d.setNode(tree.data.guids.FirstOrDefault(x => x.Value == tree.CurrentNode.Guid).Key);
        string json = File.ReadAllText("Assets/RuntimeScripts/SaveLoadSystem/data.json");
        dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        int active = jsonObj["active"];
        // render and save screenshot
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        cam.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;
        byte[] byteArray = renderedTexture.EncodeToPNG();
        File.WriteAllBytes("Assets/Resources/screens/image" + active + ".png", byteArray);
        // save game data: node and image
        jsonObj["loaders"][active]["node"] = d.getNode();
        jsonObj["loaders"][active]["image"] = "image" + active;
        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText("Assets/RuntimeScripts/SaveLoadSystem/data.json", output);
    }
}