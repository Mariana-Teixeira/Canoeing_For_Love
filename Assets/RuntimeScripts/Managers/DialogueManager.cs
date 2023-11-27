using System;
using DialogueTree;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.IO;

public class DialogueManager : NodePublisher
{
    DialogueRuntimeTree tree;
    ChoicesPanel choicePanel;


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
        DataToSave dts = null;
        try{
            string dataToLoad = "";
            using(FileStream stream = new FileStream("Assets/RuntimeScripts/SaveLoadSystem/data.json", FileMode.Open)){
                using (StreamReader reader = new StreamReader(stream)){
                    dataToLoad = reader.ReadToEnd();
                }
            }
            dts = JsonUtility.FromJson<DataToSave>(dataToLoad);
            if(dts!=null){
                headNode = dts.node;
            }
            else{
                headNode = 1;
            }
        }
        catch(Exception e){
            Debug.LogError(e);
        }
        
    }

     public void SaveGame(){
        DataToSave d = new DataToSave();
        d.setNode(tree.data.guids.FirstOrDefault(x => x.Value == tree.CurrentNode.Guid).Key);
        string dataToStore = JsonUtility.ToJson(d, true);
        using (FileStream stream = new FileStream("Assets/RuntimeScripts/SaveLoadSystem/data.json", FileMode.Open)){
            using (StreamWriter writer = new StreamWriter(stream)){
                writer.Write(dataToStore);
            }
        }
    }
}