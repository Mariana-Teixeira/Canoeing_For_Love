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
        var endNode = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "End of Playable" },

            { DialogueEvents.SHOW_NAMEPLATE, "Developers" },

            { DialogueEvents.DISPLAY_BACKGROUND, string.Empty },
        });

        #region Day 1 with Allen

        var node15a1 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You understand the competitive spirit of wanting to be better than everyone else." +
            "\nI respect that!" +
            "\nLet's go buy some shoes, together." },

            { DialogueEvents.GOTO_NEXTNODE, endNode.Guid },
        });

        var node14a1 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "I never thought about that." },

            { DialogueEvents.SHOW_NAMEPLATE, "Allen" },

            { DialogueEvents.DISPLAY_CHARACTER, "characters/allen" },

            { DialogueEvents.GOTO_NEXTNODE, node15a1.Guid },
        });

        var node13a1 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Probably better for your health too." +
            "\nYou glance back at Allen's face." },

            { DialogueEvents.GOTO_NEXTNODE, node14a1.Guid },
        });

        var node12a1 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You glance down towards Allen's shoes, but he is barefoot." +
            "\nYou weren't wrong, running with shoes is probably better for your speed." },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.GOTO_NEXTNODE, node13a1.Guid },
        });

        var node13a0 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Why is everyone so scared of the sun? I don't need sunscreen, I'm not scared of a floating ball of fire." },

            { DialogueEvents.SHOW_NAMEPLATE, "Allen" },

            { DialogueEvents.GOTO_NEXTNODE, endNode.Guid },
        });

        var node12a0 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Allen stops almost immediately after hearing you. He stops so abruptly, you can't help but wonder if his knees are okay." },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.GOTO_NEXTNODE, node13a0.Guid },
        });

        var node11a = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.GOTO_CHOICESPANEL, new[]
                {
                    new DialogueChoices(node12a0.Guid, "Did you remember to put on sunscreen?"),
                    new DialogueChoices(node12a1.Guid, "You could run faster with better running shoes."),
                } },
        });

        var node10aCORRECTION = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "He'll continue to race without glancing at you, unless you say something." },

            { DialogueEvents.DISPLAY_CHARACTER, "characters/allen" },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.GOTO_NEXTNODE, node11a.Guid },
        });

        var node10a = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You smell the river breeze, wood pine and... sweat." +
            "\nYou hold your breath as Allen races pass you down the mountain." },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.DISPLAY_BACKGROUND, "backgrounds/forest" },

            { DialogueEvents.GOTO_NEXTNODE, node10aCORRECTION.Guid },
        });

        #endregion

        #region Day 1 with Ken

        var node15k1 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You will need good luck and a lot of arm strength to row to town without any previous canoeing experience." },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.GOTO_NEXTNODE, endNode.Guid },
        });

        var node14k1 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "I'll prepare one just for you!" },

            { DialogueEvents.SHOW_NAMEPLATE, "Ken" },

            { DialogueEvents.DISPLAY_CHARACTER, "characters/ken" },

            { DialogueEvents.GOTO_NEXTNODE, node15k1.Guid },
        });

        var node13k1 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You've never been on a canoe." },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.GOTO_NEXTNODE, node14k1.Guid },
        });

        var node12k1 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "I'm sorry! I wasn't trying to imply you didn't know how to canoe. I just haven't seen you on a canoe." },

            { DialogueEvents.SHOW_NAMEPLATE, "Ken" },

            { DialogueEvents.DISPLAY_CHARACTER, "characters/ken" },

            { DialogueEvents.GOTO_NEXTNODE, node13k1.Guid },
        });

        var node13k0 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "I'll just finish packing. It'll be quick." },

            { DialogueEvents.SHOW_NAMEPLATE, "Ken" },

            { DialogueEvents.DISPLAY_CHARACTER, "characters/ken" },

            { DialogueEvents.GOTO_NEXTNODE, endNode.Guid },
        });

        var node12k0 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You see a faint blush in Ken's cheek, before he turns his face away from you." },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.GOTO_NEXTNODE, node13k0.Guid },
        });

        var node11kCORRECTION = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable()
        {
            { DialogueEvents.GOTO_CHOICESPANEL, new[]
                {
                    new DialogueChoices(node12k0.Guid, "I'd love to go in your canoe."),
                    new DialogueChoices(node12k1.Guid, "I will go on my own canoe."),
                } },
        });

        var node11k = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Don't actually do that, it's very dangerous. I'll get a two person canoe, just give me a few minutes." },

            { DialogueEvents.GOTO_NEXTNODE, node11kCORRECTION.Guid },
        });

        var node10k = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE,  "Oh? Strap yourself onto my canoe then! Ahah..." },

            { DialogueEvents.SHOW_NAMEPLATE, "Ken" },

            { DialogueEvents.DISPLAY_CHARACTER, "characters/ken" },

            { DialogueEvents.GOTO_NEXTNODE, node11k.Guid },
        });

        #endregion

        #region Day 1 Introduction

        var node190 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.GOTO_CHOICESPANEL, new[]
                {
                    new DialogueChoices(node10k.Guid, "I want to go to the Market with you."),
                    new DialogueChoices(node10a.Guid, "I'm going to go check on Allen."),
                } },
        });

        var node180 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Allen was saying something about going to the mountains for a self-imposed \"manly marathon\". " },

            { DialogueEvents.SHOW_NAMEPLATE, "Ken" },

            { DialogueEvents.DISPLAY_CHARACTER, "characters/ken" },

            { DialogueEvents.GOTO_NEXTNODE, node190.Guid },
        });

        var node170 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "There's never transit on the island, there aren't many motor vehicles around. Nevertheless, he gets ready to hop on his canoe and row away." },

            { DialogueEvents.GOTO_NEXTNODE, node180.Guid },
        });

        var node160 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Even when he needs to go downtown, he always chooses to canoe: there's never transit through the river." },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.GOTO_NEXTNODE, node170.Guid },
        });

        var node150 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Morning! I forgot to buy ingredients to make pancakes for breakfast, so I'm packing my canoe to go to the market." },

            { DialogueEvents.SHOW_NAMEPLATE, "Ken" },

            { DialogueEvents.DISPLAY_CHARACTER, "characters/ken" },

            { DialogueEvents.GOTO_NEXTNODE, node160.Guid },
        });

        var node140 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You hear someone packing a canoe outside the hut and and decide to check who's outside." },

            { DialogueEvents.GOTO_NEXTNODE, node150.Guid },
        });

        var node130 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You are unneccessful, the kitchen is empty. How are you suppose to seduce a handsome fellow with an empty stomach?" },

            { DialogueEvents.GOTO_NEXTNODE, node140.Guid },
        });

        var node120 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You wake up and make your way into Ken's kitchen." +
            "\nThe brothers are nowhere to be seen, so you grab one of the canoe shaped bowls and look for something to eat." },

            { DialogueEvents.GOTO_NEXTNODE, node130.Guid },
        });

        var node110 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "It's been a few weeks since you were saved. You've gotten to know Ken and his brother Allen." +
            "\nIt's time to make your seductive move. After breakfast!" },

            { DialogueEvents.DISPLAY_BACKGROUND, "backgrounds/kens_house" },

            { DialogueEvents.GOTO_NEXTNODE, node120.Guid },

        });

        #endregion

        #region Introduction

        var node70 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "You were found on the shore of a small island and are saved by a beautiful man named Ken." +
            "\nHe took you back to his hut and nursed you back to health." },

            { DialogueEvents.DISPLAY_BACKGROUND, string.Empty },

            { DialogueEvents.GOTO_NEXTNODE, node110.Guid },
        });

        var node60 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "And before you could understand his mysterious and oddly gamefied instructions, you become unconscious." },

            { DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.GOTO_NEXTNODE, node70.Guid },
        });

        var node50 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Hear my voice, do as I plea and I will bring you salvation." +
            "\nI will give you four days to seduce the man of your dreams." +
            "\nIf you fail, I will pull you back to sea with the strength of a hundred waves." },

            { DialogueEvents.SHOW_NAMEPLATE, "Poseidon" },

            { DialogueEvents.GOTO_NEXTNODE, node60.Guid },
        });

        var node40 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "Before your mind collapsed from the lack of oxygen, you saw Poseidon, God of the Seas, who spoke to you." },

            { DialogueEvents.GOTO_NEXTNODE, node50.Guid },
        });

        var node30 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "I'm joking around. Between gasping for air and breathing salt water, you began hallucinating." },

            { DialogueEvents.GOTO_NEXTNODE, node40.Guid },
        });

        var node20 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "After your cruise sank, you were saved by a beautiful mermaid with long-flowing hair." +
            "\nHe grabbed you by the waist, pulling you near him and you smelled his scent of coconut oil and fresh shrimp." },

            { DialogueEvents.GOTO_NEXTNODE, node30.Guid },
        });

        var node10 = new DialogueRuntimeNode(Guid.NewGuid(), new Hashtable
        {
            { DialogueEvents.SHOW_DIALOGUE, "It doesn't matter who you are. It doesn't matter who you were." +
            "\nOnly that you decided to take a luxurious cruise on the wrong day at the wrong time." },

            {DialogueEvents.SHOW_NAMEPLATE, "Narrator" },

            { DialogueEvents.DISPLAY_BACKGROUND, "backgrounds/under_the_ocean" },

            { DialogueEvents.GOTO_NEXTNODE, node20.Guid },
        });

        #endregion

        headNode = node10.Guid;
        graph = new Dictionary<Guid, DialogueRuntimeNode>
        {
            { node10.Guid, node10 },
            { node20.Guid, node20 },
            { node30.Guid, node30 },
            { node40.Guid, node40 },
            { node50.Guid, node50 },
            { node60.Guid, node60 },
            { node70.Guid, node70 },

            { node110.Guid, node110 },
            { node120.Guid, node120 },
            { node130.Guid, node130 },
            { node140.Guid, node140 },
            { node150.Guid, node150 },
            { node160.Guid, node160 },
            { node170.Guid, node170 },
            { node180.Guid, node180 },
            { node190.Guid, node190 },

            { node10k.Guid, node10k },
            { node11k.Guid, node11k },
            { node11kCORRECTION.Guid, node11kCORRECTION },
            { node12k0.Guid, node12k0 },
            { node13k0.Guid, node13k0 },
            { node12k1.Guid, node12k1 },
            { node13k1.Guid, node13k1 },
            { node14k1.Guid, node14k1 },
            { node15k1.Guid, node15k1 },

            { node10a.Guid, node10a },
            { node10aCORRECTION.Guid, node10aCORRECTION },
            { node11a.Guid, node11a },
            { node12a0.Guid, node12a0 },
            { node13a0.Guid, node13a0 },
            { node12a1.Guid, node12a1 },
            { node13a1.Guid, node13a1 },
            { node14a1.Guid, node14a1 },
            { node15a1.Guid, node15a1 },

            { endNode.Guid, endNode }
        };
        
    }
}