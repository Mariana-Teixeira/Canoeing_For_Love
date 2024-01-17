using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DialogueTree
{
    public class DialogueRuntimeTree
    {
        /// <value> Contains the graph of the dialogue tree. </value>
        public DialogueData data;
        /// <value> Current Node of the DialogueData graph contains current Dialogue Events. </value>
        DialogueRuntimeNode currentNode;

        public DialogueRuntimeNode CurrentNode { get { return currentNode; } }

        public DialogueRuntimeTree() => data = new DialogueData();

        /// <summary>
        /// Update current node to GUID argument.
        /// </summary>
        public void GoToHeadNode(Guid headNode)
        {
            Debug.Log("Setting Head Node");
            data.graph.TryGetValue(headNode, out currentNode);
            
        }

        /// <summary>
        /// Update current node to GUID argument.
        /// </summary>
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