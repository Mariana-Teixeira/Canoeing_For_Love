using DialogueTree;
using System;
using System.Collections;
using System.Collections.Generic;

public class DialogueData
{
    public Dictionary<Guid, DialogueRuntimeNode> graph { get; private set; }
    public Guid headNode { get; private set; }

    public DialogueData()
    {
        Character Player = new Character("Player", string.Empty);
        Character Morse = new Character("Morse", "Morse");
        Character Maria = new Character("Maria", "Maria");

        // Instantiate a Dictionary for testing purposes.
        var npc_e = new NPCNode(Guid.NewGuid(), Maria, Guid.Empty, "npc_e");
        var npc_d = new NPCNode(Guid.NewGuid(), Morse, Guid.Empty, "npc_d");

        var choice_e = new DialogueChoices(npc_e.Guid, "choice_e");
        var choice_d = new DialogueChoices(npc_d.Guid, "choice_d");
        DialogueChoices[] choices = { choice_e, choice_d };
        var player_z = new PlayerNode(Guid.NewGuid(), Player, choices);

        var npc_c = new NPCNode(Guid.NewGuid(), Maria, player_z.Guid, "NPC");
        var npc_b = new NPCNode(Guid.NewGuid(), Morse, npc_c.Guid, "OtherNPC", new Hashtable{ { DialogueEvents.ANIMATE_CAMERA, CameraAnimation.SHAKE_UP } });
        var npc_a = new NPCNode(Guid.NewGuid(), Maria, npc_b.Guid, "NPC");
        
        headNode = npc_a.Guid;

        graph = new Dictionary<Guid, DialogueRuntimeNode>
        {
            { npc_c.Guid, npc_c },
            { npc_b.Guid, npc_b },
            { npc_a.Guid, npc_a },
            { player_z.Guid, player_z },
            { npc_d.Guid, npc_d },
            { npc_e.Guid, npc_e }
        };
        
    }

    /// <summary>
    /// Create a DialogueData file in project directory.
    /// </summary>
    void InstanciateFile()
    { }
}

public struct Character
{
    public string Name { get; private set; }
    public string PortraitPath { get; private set; }
    public Character(string name, string portraitPath)
    {
        this.Name = name;
        this.PortraitPath = portraitPath;
    }
}

public enum DialogueEvents { ANIMATE_CAMERA }