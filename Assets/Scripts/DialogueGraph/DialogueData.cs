using DialogueTree;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueData
{
    public Guid headNode { get; private set; }

    string nametodisplay;
    ///<value> Dictionary containing all nodes of the dialogue graph. </value>
    public Dictionary<Guid, DialogueRuntimeNode> graph { get; private set; }
    public Dictionary<int, Guid> guids { get; private set; }
    ///<value> Dictionary containing all camera animations. </value>
    public Dictionary<string,CameraAnimation> cameraDict { get; private set; }
    public StreamReader reader { get; private set; }
    
    public DialogueData(string json)
    {
        Debug.Log("Dialogue Data Constructor.");
        //StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "dialogue.json"));
        //var json = reader.ReadToEnd();

        
        List<DialogueNode> dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);
        cameraDict = new Dictionary<string,CameraAnimation>
        {
            { "normal", CameraAnimation.NORMAL },
            { "shake", CameraAnimation.SHAKE },
        };

        guids = new Dictionary<int, Guid>{};
        graph = new Dictionary<Guid, DialogueRuntimeNode> {};

        // last node
        var endNode = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_CREDITS, "Developers" },
        }); 

        guids.Add(dialogueNodes.Count+1, endNode.Guid);
        graph.Add(guids[key: dialogueNodes.Count+1], endNode);


        // logic to interpret data gotten from the json file
        // This loop checks for the existence of dialogue, character, animations, backgrounds, next node, audio, and love score changes.
        // The ones that are detected call an event from DialogueEvents and pass the needed value. 
        foreach (var node in dialogueNodes){
            Hashtable hasher = new Hashtable();
            guids.Add(node.Id, Guid.NewGuid());
            if (node.ShowDialogue!=null){
                hasher.Add(DialogueEvents.SHOW_DIALOGUE, node.ShowDialogue);
            }
            if (node.DisplayCharacter!=null && node.DisplayCharacter != ""){
                hasher.Add(DialogueEvents.DISPLAY_CHARACTER, node.DisplayCharacter);
                nametodisplay = node.DisplayCharacter;
                if (nametodisplay.Contains("_")){
                    nametodisplay = node.DisplayCharacter.Split("_")[0];
                }
                nametodisplay = string.Concat(nametodisplay[0].ToString().ToUpper(), nametodisplay.Substring(1));
                hasher.Add(DialogueEvents.SHOW_NAMEPLATE, nametodisplay);
            }
            if (node.DisplayBackground!=null){
                hasher.Add(DialogueEvents.DISPLAY_BACKGROUND, node.DisplayBackground);
            }
            if(node.GoToNextNode!=0){
                hasher.Add(DialogueEvents.GOTO_NEXTNODE, guids[node.GoToNextNode]);
            }
            else if(node.GoToNextNode==0){
                hasher.Add(DialogueEvents.GOTO_NEXTNODE, guids[dialogueNodes.Count+1]);
            }
            if(node.PlayAnimation!=null){
                hasher.Add(DialogueEvents.ANIMATE_CAMERA, cameraDict[node.PlayAnimation]);
            }
            if(node.PlayAudio!=null){
                hasher.Add(DialogueEvents.PLAY_SOUND,node.PlayAudio);
            }
            if(node.ShowChoicePanel!=null){
                List<DialogueChoices> choices2 = new ();
                foreach(var choice in node.ShowChoicePanel)
                {
                    if(choice.GoToNextNode==0){
                        var _choice = new DialogueChoices(guids[dialogueNodes.Count+1], choice.ShowDialogue, choice.AddItem);
                        choices2.Add(_choice);
                    }
                    else{
                        var _choice = new DialogueChoices(guids[choice.GoToNextNode], choice.ShowDialogue, choice.AddItem);
                        choices2.Add(_choice);
                    }
                    
                }
                DialogueChoices[] choices = choices2.ToArray();
                hasher.Add(DialogueEvents.SHOW_CHOICESPANEL, choices);
            }
            if (node.GoToPathNode != null)
            {
                DialoguePath pathChoice;
                if(node.GoToPathNode.GoToSecondaryNode>0){
                    pathChoice = new DialoguePath(guids[node.GoToPathNode.GoToPrimaryNode], guids[node.GoToPathNode.GoToSecondaryNode], guids[node.GoToPathNode.GoToBackupNode],node.GoToPathNode.Character, node.GoToPathNode.MinimumScore);
                }
                else{
                    pathChoice = new DialoguePath(guids[node.GoToPathNode.GoToPrimaryNode], guids[dialogueNodes.Count+1], guids[node.GoToPathNode.GoToBackupNode],node.GoToPathNode.Character, node.GoToPathNode.MinimumScore);
                }
                
                hasher.Add(DialogueEvents.GOTO_PATHNODE, pathChoice);
            }
            if (node.IncreaseCharacterScore != null)
            {
                hasher.Add(DialogueEvents.ADD_SCORE, node.IncreaseCharacterScore);
            }
            if (node.DecreaseCharacterScore != null)
            {
                hasher.Add(DialogueEvents.REMOVE_SCORE, node.DecreaseCharacterScore);
            }
            var npc = new DialogueRuntimeNode(guids[node.Id], hasher);
            if (node.Id==1){
                headNode = npc.Guid;
            }
            graph.Add(guids[node.Id], npc);
        }
    }
}

// goto_node[node, character, score]

// Objects that are gonna be fetched from json files
public class DialogueNode
{
    public int Id { get; set; }
    public int Type { get; set; }
    public string ShowDialogue { get; set; }
    public List<Choice> ShowChoicePanel { get; set; }
    public string ShowNameplate { get; set; }
    public string DisplayCharacter { get; set; }
    public string DisplayBackground { get; set; }
    public int GoToNextNode { get; set; }
    public PathChoice GoToPathNode { get; set; }
    public string PlayAnimation { get; set; }
    public string PlayAudio {get; set;}
    public string IncreaseCharacterScore { get; set; }
    public string DecreaseCharacterScore { get; set; }
    public string AcquireItem { get; set; }
}

public class Choice
{
    public int GoToNextNode { get; set; }
    public string ShowDialogue { get; set; }
    public string AddItem { get; set; }
}

public class PathChoice
{
    public int GoToPrimaryNode { get; set; }
    public int GoToSecondaryNode{get; set; }
    public int GoToBackupNode { get; set; }
    public string Character { get; set; }
    public int MinimumScore { get; set; }
}