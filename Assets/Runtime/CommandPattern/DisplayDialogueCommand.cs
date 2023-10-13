using UnityEngine;

public class DisplayDialogueCommand : ICommand
{
    string dialogue;

    public DisplayDialogueCommand(string dialogue) => this.dialogue = dialogue;

    public void Execute()
    {
        Debug.Log(dialogue);
    }
}
