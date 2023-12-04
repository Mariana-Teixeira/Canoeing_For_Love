public class LoadFileCommand : ICommand
{
    DialogueManager treeManager;

    public LoadFileCommand(DialogueManager treeManager)
    {
        this.treeManager = treeManager;
    }

    public void Execute()
    {
        treeManager.LoadGame();
    }
}