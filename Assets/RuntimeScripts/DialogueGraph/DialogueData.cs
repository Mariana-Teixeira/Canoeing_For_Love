using DialogueTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

public class DialogueData
{
    public Guid headNode { get; private set; }
    public Dictionary<Guid, DialogueRuntimeNode> graph { get; private set; }
    public Dictionary<int, Guid> guids { get; private set; }
    public Dictionary<string,CameraAnimation> cameraDict { get; private set; }
    public StreamReader reader { get; private set; }
    
    public DialogueData()
    {
        StreamReader reader = new StreamReader("Assets/GameData/test_dialogue.json");
        var json = reader.ReadToEnd();
        List<DialogueNode> dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);

        cameraDict = new Dictionary<string,CameraAnimation>
        {
            { "normal", CameraAnimation.NORMAL },
            { "shake", CameraAnimation.SHAKE_UP },
        };

        guids = new Dictionary<int, Guid>{};
        graph = new Dictionary<Guid, DialogueRuntimeNode> {};

        var endNode = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "End of Playable" },

            { DialogueEvents.SHOW_NAMEPLATE, "Developers" },

            { DialogueEvents.DISPLAY_BACKGROUND, string.Empty },
        }); 

        guids.Add(dialogueNodes.Count+1, endNode.Guid);
        graph.Add(guids[key: dialogueNodes.Count+1], endNode);

        foreach (var node in dialogueNodes){
            Hashtable hasher = new Hashtable();
            guids.Add(node.Id, Guid.NewGuid());
            if (node.ShowDialogue!=null){
                hasher.Add(DialogueEvents.SHOW_DIALOGUE, node.ShowDialogue);
            }
            if (node.DisplayCharacter!=null){
                hasher.Add(DialogueEvents.DISPLAY_CHARACTER, node.DisplayCharacter);
                hasher.Add(DialogueEvents.SHOW_NAMEPLATE, string.Concat(node.DisplayCharacter[0].ToString().ToUpper(), node.DisplayCharacter.Substring(1)));
            }
            if (node.DisplayBackground!=null){
                hasher.Add(DialogueEvents.DISPLAY_BACKGROUND, node.DisplayBackground);
            }
            if(node.GoToNextNode>100000){
                Random random = new Random();
                double test = random.NextDouble();
                if(test<0.5){
                    string help = node.GoToNextNode.ToString();
                    string nextNode = help.Substring(0,3);
                    int next = int.Parse(nextNode);
                    hasher.Add(DialogueEvents.GOTO_NEXTNODE, guids[next]);
                }
                else{
                    string help = node.GoToNextNode.ToString();
                    string nextNode = help.Substring(3,3);
                    int next = int.Parse(nextNode);
                    hasher.Add(DialogueEvents.GOTO_NEXTNODE, guids[next]);
                }

            }
            else if(node.GoToNextNode!=0){
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
                        var _choice = new DialogueChoices(guids[dialogueNodes.Count+1], choice.ShowDialogue);
                        choices2.Add(_choice);
                    }
                    else{
                        var _choice = new DialogueChoices(guids[choice.GoToNextNode], choice.ShowDialogue);
                        choices2.Add(_choice);
                    }
                    
                }
                DialogueChoices[] choices = choices2.ToArray();
                hasher.Add(DialogueEvents.SHOW_CHOICESPANEL, choices);
            }
            if (node.GoToPathNode != null)
            {
                var pathChoice = new DialoguePath(guids[node.GoToPathNode.GoToPrimaryNode], guids[node.GoToPathNode.GoToBackupNode],
                    node.GoToPathNode.Character, node.GoToPathNode.MinimumScore);
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
}

public class PathChoice
{
    public int GoToPrimaryNode { get; set; }
    public int GoToBackupNode { get; set; }
    public string Character { get; set; }
    public int MinimumScore { get; set; }
}