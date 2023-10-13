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
    }
}