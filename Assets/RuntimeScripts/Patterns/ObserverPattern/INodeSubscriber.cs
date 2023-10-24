using DialogueTree;

public interface INodeSubscriber
{
    public void OnNotifyNPC(NPCNode node);
    public void OnNotifyPlayer(PlayerNode node);
}
