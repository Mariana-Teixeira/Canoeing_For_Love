using DialogueTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class DialogueData
{
    public Dictionary<Guid, DialogueRuntimeNode> graph { get; private set; }
    public Guid headNode { get; private set; }

    public DialogueData()
    {
        var charMorse = "Morse";
        var charMaria = "Maria";

        // Instantiate a Dictionary for testing purposes.
        var npc_e = new NPCNode(Guid.NewGuid(), Guid.Empty, "NPC", "AYYYYYYY", charMaria);
        var npc_d = new NPCNode(Guid.NewGuid(), Guid.Empty, "OtherNPC", "OOOOOOO", charMorse);
        var choice_e = new DialogueChoices(npc_e.Guid, "choice_e");
        var choice_d = new DialogueChoices(npc_d.Guid, "choice_d");

        DialogueChoices[] choices = { choice_e, choice_d };
        var player_z = new PlayerNode(Guid.NewGuid(), "Player", choices);
        var npc_c = new NPCNode(Guid.NewGuid(), player_z.Guid, "NPC", "npc_c", charMaria);
        var npc_b = new NPCNode(Guid.NewGuid(), npc_c.Guid, "OtherNPC", "npc_b", charMorse);
        var npc_a = new NPCNode(Guid.NewGuid(), npc_b.Guid, "NPC", "npc_a", charMaria);
        
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