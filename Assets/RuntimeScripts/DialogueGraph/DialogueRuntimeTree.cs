using System;
using UnityEngine;

namespace DialogueTree
{
    public class DialogueRuntimeTree
    {
        DialogueData data;
        DialogueRuntimeNode currentNode;

        public DialogueRuntimeNode CurrentNode { get { return currentNode; } }

        public DialogueRuntimeTree() => data = new DialogueData();

        public void GoToNextNode()
        {
            if (currentNode == null)
            {
                Debug.Log("Setting Head Node");
                data.graph.TryGetValue(data.headNode, out currentNode);
            }
            else
            {
                Debug.Log("Setting Next Node");
                NPCNode node = (NPCNode)currentNode;
                data.graph.TryGetValue(node.NextNodeGUID, out currentNode);
            }
        }

        public void GoToHeadNode()
        {
            Debug.Log("Setting Head Node");
            data.graph.TryGetValue(data.headNode, out currentNode);
        }

        public void GoToNextNode(Guid nextNode)
        {
            Debug.Log("Iterating through the Node Tree");
            data.graph.TryGetValue(nextNode, out currentNode);
        }
    }
}