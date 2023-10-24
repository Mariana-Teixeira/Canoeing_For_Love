using DialogueTree;
using System.Collections.Generic;
using UnityEngine;

public class NodePublisher : MonoBehaviour
{
    List<INodeSubscriber> observers = new List<INodeSubscriber>();

    public void AddObserver(INodeSubscriber obs) => observers.Add(obs);
    public void RemoveObserver(INodeSubscriber obs) => observers.Remove(obs);
    protected void NotifyObserver(DialogueRuntimeNode node)
    {
        observers.ForEach
        (
            obs =>
            {
                if (node.GetType() == typeof(NPCNode))
                {
                    NPCNode npc = (NPCNode)node;
                    obs.OnNotifyNPC(npc);
                }
                else if (node.GetType() == typeof(PlayerNode))
                {
                    PlayerNode player = (PlayerNode)node;
                    obs.OnNotifyPlayer(player);
                }
            }
        );
    }
}
