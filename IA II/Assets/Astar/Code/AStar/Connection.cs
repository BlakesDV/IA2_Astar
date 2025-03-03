using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blakes.Graph
{
    [System.Serializable]
    public class Connection
    {
        //Alway two nodes
        [SerializeField] public Node nodeA;
        [SerializeField] public Node nodeB;
        [SerializeField] public float ditanceBetweenNodes;

        #region PublicMethods

        public Node RetreiveOtherNodeThan(Node value)
        {
            if (value == nodeA)
            {
                return nodeB;
            }
            else
            {
                return nodeA;
            }
        }

        #endregion
    }



    [System.Serializable]
    public class Route
    {
        #region Variables

        [SerializeField] public List<Node> nodes;
        [SerializeField] public float sumDistance;

        #endregion

        #region Constructors
        //Generate a contructor to generate a new pointer of this new route

        public Route()
        {
            nodes = new List<Node>();
            sumDistance = 0;
        }

        public Route(List<Node> nodesToClone, float sumDistanceToCopy)
        {
            //Generate a new pointer in the RAM for this NEW collection of nodes
            nodes = new List<Node>();
            //we will retrieve all pointers from the nodes from the "original" list
            foreach (Node node in nodesToClone)
            {
                nodes.Add(node);
            }
            //*nodes != 

            sumDistance = sumDistanceToCopy;
        }

        #endregion

        #region PublicMethods

        public void AddNode(Node nodeValue, float sumValue)
        {
            nodes.Add(nodeValue);
            sumDistance += sumValue;
        }

        public bool ContainsNodeInRoute(Node value)
        {
            foreach (Node node in nodes)
            {
                if (value == node)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
