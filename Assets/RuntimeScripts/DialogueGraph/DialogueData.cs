using DialogueTree;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;

public class DialogueData
{
    public Dictionary<Guid, DialogueRuntimeNode> graph { get; private set; }
    public Guid headNode { get; private set; }

    public DialogueData()
    {
        #region Day 1 Introduction

        var node18 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Allen was saying something about going to the mountains for a self-imposed \"manly marathon\". " },

            { DialogueEvents.SHOW_NAMEPLATE, "Ken" },
        });

        var node17 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "There's never transit on the island, there aren't many motor vehicles around. Nevertheless, he gets ready to hop on his canoe and row away." },

            { DialogueEvents.GOTO_NEXTNODE, node18.Guid },
        });

        var node16 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Even when he needs to go downtown, he always chooses to canoe: there's never transit through the river." },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.GOTO_NEXTNODE, node17.Guid },
        });

        var node15 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Morning! I forgot to buy ingredients to make pancakes for breakfast, so I'm packing my canoe to go to the market." },

            { DialogueEvents.DISPLAY_BACKGROUND, "backgrounds/kens_house" },

            { DialogueEvents.SHOW_NAMEPLATE, "Ken" },

            { DialogueEvents.GOTO_NEXTNODE, node16.Guid },
        });

        var node14 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You hear someone packing a canoe outside the hut and and decide to check who's outside." },

            { DialogueEvents.GOTO_NEXTNODE, node15.Guid },
        });

        var node13 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You are unneccessful, the kitchen is empty. How are you suppose to seduce a handsome fellow with an empty stomach?" },

            { DialogueEvents.GOTO_NEXTNODE, node14.Guid },
        });

        var node12 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You wake up and make your way into Ken's kitchen." +
            "\nThe brothers are nowhere to be seen, so you grab one of the canoe shaped bowls and look for something to eat." },

            { DialogueEvents.GOTO_NEXTNODE, node13.Guid },
        });

        var node11 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "It's been a few weeks since you were saved. You've gotten to know Ken and his brother Allen." +
            "\nIt's time to make your seductive move. After breakfast!" },

            { DialogueEvents.GOTO_NEXTNODE, node12.Guid },

            { DialogueEvents.DISPLAY_BACKGROUND, "backgrounds/kens_house" },
        });

        #endregion

        #region Introduction

        var node7 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You were found on the shore of a small island and are saved by a beautiful man named Ken." +
            "\nHe took you back to his hut and nursed you back to health." },

            { DialogueEvents.DISPLAY_BACKGROUND, string.Empty },

            { DialogueEvents.GOTO_NEXTNODE, node11.Guid },
        });

        var node6 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "And before you could understand his mysterious and oddly gamefied instructions, you become unconscious." },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.GOTO_NEXTNODE, node7.Guid },
        });

        var node5 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Hear my voice, do as I plea and I will bring you salvation." +
            "\nI will give you four days to seduce the man of your dreams." +
            "\nIf you fail, I will pull you back to sea with the strength of a hundred waves." },

            { DialogueEvents.SHOW_NAMEPLATE, "Poseidon" },

            { DialogueEvents.GOTO_NEXTNODE, node6.Guid },
        });

        var node4 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Before your mind collapsed from the lack of oxygen, you saw Poseidon, God of the Seas, who spoke to you." },

            { DialogueEvents.GOTO_NEXTNODE, node5.Guid },
        });

        var node3 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "I'm joking around. Between gasping for air and breathing salt water, you began hallucinating." },

            { DialogueEvents.GOTO_NEXTNODE, node4.Guid },
        });

        var node2 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "After your cruise sank, you were saved by a beautiful mermaid with long-flowing hair." +
            "\nHe grabbed you by the waist, pulling you near him and you smelled his scent of coconut oil and fresh shrimp." },

            { DialogueEvents.GOTO_NEXTNODE, node3.Guid },
        });

        var node1 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "It doesn't matter who you are. It doesn't matter who you were." +
            "\nOnly that you decided to take a luxurious cruise on the wrong day at the wrong time." },

            {DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.DISPLAY_BACKGROUND, "backgrounds/under_the_ocean" },

            { DialogueEvents.GOTO_NEXTNODE, node2.Guid },
        });

        #endregion

        headNode = node1.Guid;
        graph = new Dictionary<Guid, DialogueRuntimeNode>
        {
            { node1.Guid, node1 },
            { node2.Guid, node2 },
            { node3.Guid, node3 },
            { node4.Guid, node4 },
            { node5.Guid, node5 },
            { node6.Guid, node6 },
            { node7.Guid, node7 },

            { node11.Guid, node11 },
            { node12.Guid, node12 },
            { node13.Guid, node13 },
            { node14.Guid, node14 },
            { node15.Guid, node15 },
            { node16.Guid, node16 },
            { node17.Guid, node17 },
            { node18.Guid, node18 },
        };
        
    }

    /// <summary>
    /// Create a DialogueData file in project directory.
    /// </summary>
    void InstanciateFile()
    { }
}