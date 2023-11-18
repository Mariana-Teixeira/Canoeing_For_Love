using DialogueTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class DialogueData
{
    public Dictionary<Guid, DialogueRuntimeNode> graph { get; private set; }

    public Dictionary<int, Guid> guids { get; private set; }
    public Guid headNode { get; private set; }
    //public Dictionary<string,Character> characterDict { get; private set; }

    public Dictionary<string,CameraAnimation> cameraDict { get; private set; }

    public StreamReader r { get; private set; }


    
    public DialogueData()
    {
        StreamReader r = new StreamReader("Assets/RuntimeScripts/DialogueGraph/dialogue.json");
        var json = r.ReadToEnd();
        List<DialogueNode> dialogueNodes = JsonConvert.DeserializeObject<List<DialogueNode>>(json);

        // Character Morse = new Character("Morse", "Morse");
        // Character Maria = new Character("Maria", "Maria");
        // characterDict = new Dictionary<string,Character>{
        //     { "Maria", Maria },
        //     { "Morse", Morse }
        // };

        cameraDict = new Dictionary<string,CameraAnimation>{
            { "normal", CameraAnimation.NORMAL },
            { "shake", CameraAnimation.SHAKE_UP }
        };

        guids = new Dictionary<int, Guid>{};

         graph = new Dictionary<Guid, DialogueRuntimeNode>
        {
            // { npc_c.Guid, npc_c },
            // { npc_b.Guid, npc_b },
            // { npc_a.Guid, npc_a },
            // { player_z.Guid, player_z },
            // { npc_d.Guid, npc_d },
            // { npc_e.Guid, npc_e }
        };

         var endNode = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "End of Playable" },

            { DialogueEvents.SHOW_NAMEPLATE, "Developers" },

            { DialogueEvents.DISPLAY_BACKGROUND, string.Empty },
        });    
        guids.Add(dialogueNodes.Count+1, endNode.Guid);
        graph.Add(guids[key: dialogueNodes.Count+1], endNode);

        foreach (var n in dialogueNodes){
            Hashtable hasher = new Hashtable();
            guids.Add(n.Id, Guid.NewGuid());
            if (n.Dialogue!=null){
                hasher.Add(DialogueEvents.SHOW_DIALOGUE, n.Dialogue);
            }
            if (n.Background!=null){
                hasher.Add(DialogueEvents.DISPLAY_BACKGROUND, n.Background);
            }
            if (n.Character!=null){
                hasher.Add(DialogueEvents.DISPLAY_CHARACTER, n.Character);
                hasher.Add(DialogueEvents.SHOW_NAMEPLATE, string.Concat(n.Character[0].ToString().ToUpper(), n.Character.Substring(1)));
            }
            if(n.NextNode!=0){
                hasher.Add(DialogueEvents.GOTO_NEXTNODE, guids[n.NextNode]);
            }
            else if(n.NextNode==0){
                hasher.Add(DialogueEvents.GOTO_NEXTNODE, guids[dialogueNodes.Count+1]);
            }
            if(n.Animation!=null){
                hasher.Add(DialogueEvents.ANIMATE_CAMERA, cameraDict[n.Animation]);
            }
            if(n.Choices!=null){
                List<DialogueChoices> choices2 = new ();
                foreach(var choice in n.Choices){
                    var ch = new DialogueChoices(guids[choice.NextNode], choice.Dialogue);
                    choices2.Add(ch);
                    
                }
                DialogueChoices[] choices = choices2.ToArray();
                hasher.Add(DialogueEvents.GOTO_CHOICESPANEL, choices);
            }
            var npc = new DialogueRuntimeNode(guids[n.Id], hasher);
            if (n.Id==1){
                headNode = npc.Guid;
            }
            graph.Add(guids[n.Id], npc);
        } 


       
    }

    /// <summary>
    /// Create a DialogueData file in project directory.
    /// </summary>
    void InstanciateFile()
    { }
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
    public List<Choice> Choices { get; set; }
}

public class Choice
{
    public int NextNode { get; set; }
    public string Dialogue { get; set; }
}