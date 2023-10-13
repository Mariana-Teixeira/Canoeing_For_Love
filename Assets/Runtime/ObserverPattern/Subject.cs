using DialogueTree;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver obs) => observers.Add(obs);
    public void RemoveObserver(IObserver obs) => observers.Remove(obs);
    protected void NotifyObserver() => observers.ForEach(obs => obs.OnNotify());
}
