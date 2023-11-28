using DialogueTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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

            { DialogueEvents.DISPLAY_BACKGROUND, "black" },
        });    
        guids.Add(dialogueNodes.Count+1, endNode.Guid);
        graph.Add(guids[key: dialogueNodes.Count+1], endNode);

        foreach (var node in dialogueNodes){
            Hashtable hasher = new Hashtable();
            guids.Add(node.Id, Guid.NewGuid());
            if (node.Dialogue!=null){
                hasher.Add(DialogueEvents.SHOW_DIALOGUE, node.Dialogue);
            }
            if (node.Background!=null){
                hasher.Add(DialogueEvents.DISPLAY_BACKGROUND, node.Background);
            }
            if (node.Character!=null){
                hasher.Add(DialogueEvents.DISPLAY_CHARACTER, node.Character);
                hasher.Add(DialogueEvents.SHOW_NAMEPLATE, string.Concat(node.Character[0].ToString().ToUpper(), node.Character.Substring(1)));
            }
            if(node.NextNode!=0){
                hasher.Add(DialogueEvents.GOTO_NEXTNODE, guids[node.NextNode]);
            }
            else if(node.NextNode==0){
                hasher.Add(DialogueEvents.GOTO_NEXTNODE, guids[dialogueNodes.Count+1]);
            }
            if(node.Animation!=null){
                hasher.Add(DialogueEvents.ANIMATE_CAMERA, cameraDict[node.Animation]);
            }
            if(node.Audio!=null){
                hasher.Add(DialogueEvents.PLAY_SOUND,node.Audio);
            }
            if(node.Choices!=null){
                List<DialogueChoices> choices2 = new ();
                foreach(var choice in node.Choices){
                    var ch = new DialogueChoices(guids[choice.NextNode], choice.Dialogue);
                    choices2.Add(ch);
                    
                }
                DialogueChoices[] choices = choices2.ToArray();
                hasher.Add(DialogueEvents.GOTO_CHOICESPANEL, choices);
            }
            var npc = new DialogueRuntimeNode(guids[node.Id], hasher);
            if (node.Id==1){
                headNode = npc.Guid;
            }
            graph.Add(guids[node.Id], npc);
        }  
    }
}


public class DialogueNode
{
    public int Id { get; set; }
    public int Type { get; set; }
    public string Dialogue { get; set; }

    public string Background { get; set; }
    public string Character { get; set; }
    public int NextNode { get; set; }
    public string Animation { get; set; }

    public string Audio {get; set;}
    public List<Choice> Choices { get; set; }
}

public class Choice
{
    public int NextNode { get; set; }
    public string Dialogue { get; set; }
}