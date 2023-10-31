using System;

namespace DialogueTree
{
    public class DialogueRuntimeNode
    {
        public Guid Guid;
        public string displayName;
        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public DialogueRuntimeNode(Guid myGuid, string characterName)
        {
            this.Guid = myGuid;
            this.displayName = characterName;
        }
    }

    public class PlayerNode : DialogueRuntimeNode
    {
        public DialogueChoices[] Choices { get; private set; }

        public PlayerNode(Guid myGuid, string playerName, DialogueChoices[] choices) : base(myGuid, playerName)
        {
            this.Choices = choices;
        }
    }

    public class NPCNode : DialogueRuntimeNode
    {
        public Guid NextNodeGUID { get; private set; }
        string characterDialogue;
        string imagePath;

        string soundPath;

        string backgroundPath;

        CameraEvents cameraE;

        float textSpeed;

        public string CharacterDialogue
        {
            get
            {
                return characterDialogue;
            }
        }
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
        }

        public string SoundPath
        {
            get
            {
                return soundPath;
            }
        }

        public string BackgroundPath
        {
            get
            {
                return backgroundPath;
            }
        }

        public float TextSpeed
        {
            get
            {
                return textSpeed;
            }
        }

        public CameraEvents CameraE
        {
            get
            {
                return cameraE;
            }
        }
        

        public NPCNode(Guid myGuid, Guid nextNodeGuid, string npcName, string npcDialogue, string imagePath = "", string soundPath =  "", string backgroundPath = "", float textSpeed = 0.3f, CameraEvents cameraE = CameraEvents.NORMAL) : base(myGuid, npcName)
        {
            this.NextNodeGUID = nextNodeGuid;
            this.characterDialogue = npcDialogue;
            this.imagePath = imagePath;
            this.backgroundPath = backgroundPath;
            this.soundPath = soundPath;
            this.cameraE = cameraE;
            this.textSpeed = textSpeed;
        }
    }

    public struct DialogueChoices
    {
        public Guid NextNodeGUID { get; private set; }
        public string choiceDialogue;

        public DialogueChoices(Guid nextNode, string dialogue)
        {
            this.NextNodeGUID = nextNode;
            this.choiceDialogue = dialogue;
        }
    }
}