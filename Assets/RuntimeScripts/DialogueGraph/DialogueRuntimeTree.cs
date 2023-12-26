using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DialogueTree
{
    public class DialogueRuntimeTree
    {
        public DialogueData data;
        DialogueRuntimeNode currentNode;

        public DialogueRuntimeNode CurrentNode { get { return currentNode; } }

        public DialogueRuntimeTree() => data = new DialogueData();

        public void GoToHeadNode(Guid headNode)
        {
            Debug.Log("Setting Head Node");
            data.graph.TryGetValue(headNode, out currentNode);
            
        }

        public void GoToNextNode(Guid nextNode)
        {
            Debug.Log("Iterating through the Node Tree");
            data.graph.TryGetValue(nextNode, out currentNode);
        }

        public void GoToCredits()
        {
            SceneManager.LoadScene(3);
        }
    }
}