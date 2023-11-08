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
        Character Morse = new Character("Morse", "Morse");
        Character Maria = new Character("Maria", "Maria");

        // Instantiate a Dictionary for testing purposes.
        var npc_e = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.DISPLAY_DIALOGUE, "Hey to you too!" },
            { DialogueEvents.DISPLAY_CHARACTER, Maria },
            { DialogueEvents.GO_TO_NEXT_NODE, Guid.Empty },
        });

        var npc_d = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.DISPLAY_DIALOGUE, "I guess..." },
            { DialogueEvents.DISPLAY_CHARACTER, Morse },
            { DialogueEvents.GO_TO_NEXT_NODE, Guid.Empty },
        });

        var choice_e = new DialogueChoices(npc_e.Guid, "Hi guys!");
        var choice_d = new DialogueChoices(npc_d.Guid, "The mood seems low.");
        DialogueChoices[] choices = { choice_e, choice_d };

        var player_z = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.OPEN_CHOICES_PANEL, choices },
        });

        var npc_c = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.DISPLAY_DIALOGUE, "How are you?" },
            { DialogueEvents.DISPLAY_CHARACTER, Maria },
            { DialogueEvents.GO_TO_NEXT_NODE, player_z.Guid },
            { DialogueEvents.ANIMATE_CAMERA, CameraAnimation.NORMAL },
        });

        var npc_b = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable{
            { DialogueEvents.DISPLAY_DIALOGUE, "Hey..." },
            { DialogueEvents.DISPLAY_CHARACTER, Morse },
            { DialogueEvents.GO_TO_NEXT_NODE, npc_c.Guid },
            { DialogueEvents.ANIMATE_CAMERA, CameraAnimation.SHAKE_UP },
        });

        var npc_a = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.DISPLAY_DIALOGUE, "Hello!" },
            { DialogueEvents.DISPLAY_CHARACTER, Maria },
            { DialogueEvents.GO_TO_NEXT_NODE, npc_b.Guid },
        });
        
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