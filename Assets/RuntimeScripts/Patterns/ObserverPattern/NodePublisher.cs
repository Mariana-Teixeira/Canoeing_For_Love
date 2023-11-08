using DialogueTree;
using System.Collections.Generic;
using UnityEngine;

public class NodePublisher : MonoBehaviour
{
    List<INodeSubscriber> observers = new List<INodeSubscriber>();

    public void AddObserver(INodeSubscriber obs) => observers.Add(obs);
    public void RemoveObserver(INodeSubscriber obs) => observers.Remove(obs);
    public void NotifyObserver(DialogueRuntimeNode node) => observers.ForEach(obs => obs.OnNotifyNode(node));
}
