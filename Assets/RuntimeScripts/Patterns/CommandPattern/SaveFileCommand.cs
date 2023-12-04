public class SaveFileCommand : ICommand
{
    DialogueManager treeManager;

    public SaveFileCommand(DialogueManager treeManager)
    {
        this.treeManager = treeManager;
    }

    public void Execute()
    {
        treeManager.SaveGame();
    }
}