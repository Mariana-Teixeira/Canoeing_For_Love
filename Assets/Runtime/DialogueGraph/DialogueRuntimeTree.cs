using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueTree
{
    // TODO: Command Pattern is specially useful for PlayerNode (because the player must pick a choice before moving to the NextNode).
    public class DialogueRuntimeTree : MonoBehaviour
    {
        DialogueData data;
        DialogueRuntimeNode currentNode;

        public bool GoToOptionA;

        private void Awake()
        {
            LoadData();
            GoToNextNode();
        }

        /// <summary>
        /// Loads a DialogueData file.
        /// </summary>
        void LoadData()
        {
            data = new DialogueData();
            data.graph.TryGetValue(data.headNode, out currentNode);
        }

        /// <summary>
        /// Updates currentNode.
        /// </summary>
        void GoToNextNode()
        {
            if (currentNode.GetType() == typeof(PlayerNode))
            {
                PlayerNode node = (PlayerNode)currentNode;
                Guid nodeGuid =  GoToOptionA ? node.choices[0].NextNodeGUID : node.choices[1].NextNodeGUID;
                LoadPlayerNode(node);
                if (data.graph.TryGetValue(nodeGuid, out currentNode) == true) GoToNextNode();
            }
            else
            {
                NPCNode node = (NPCNode)currentNode;
                LoadNPCNode(node);
                if (data.graph.TryGetValue(node.NextNodeGUID, out currentNode) == true) GoToNextNode();
            }
        }

        /// <summary>
        /// Invoke delegates that Gameplay Components should subscribe to.
        /// </summary>
        /// <param name="node"></param>
        void LoadPlayerNode(PlayerNode node)
        {
            Debug.Log(node.ToString());
        }

        /// <summary>
        /// Invoke delegates that Gameplay Components should be listeners for.
        /// </summary>
        /// <param name="node"></param>
        void LoadNPCNode(NPCNode node)
        {
            Debug.Log(node.ToString());
        }
    }
}