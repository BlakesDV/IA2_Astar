using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blakes.Graph
{
    [System.Serializable]
    public class Connection
    {
        //Alway two nodes
        [SerializeField] public Cell nodeA;
        [SerializeField] public Cell nodeB;
        [SerializeField] public float ditanceBetweenNodes;

        #region PublicMethods

        public Cell RetreiveOtherNodeThan(Cell value)
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

        [SerializeField] public List<Cell> nodes;
        [SerializeField] public float sumDistance;

        #endregion

        #region Constructors
        //Generate a contructor to generate a new pointer of this new route

        public Route()
        {
            nodes = new List<Cell>();
            sumDistance = 0;
        }

        public Route(List<Cell> nodesToClone, float sumDistanceToCopy)
        {
            //Generate a new pointer in the RAM for this NEW collection of nodes
            nodes = new List<Cell>();
            //we will retrieve all pointers from the nodes from the "original" list
            foreach (Cell cell in nodesToClone)
            {
                nodes.Add(cell);
            }
            //*nodes != 

            sumDistance = sumDistanceToCopy;
        }

        #endregion

        #region PublicMethods

        public void AddNode(Cell nodeValue, float sumValue)
        {
            nodes.Add(nodeValue);
            sumDistance += sumValue;
        }

        public bool ContainsNodeInRoute(Cell value)
        {
            foreach (Cell cell in nodes)
            {
                if (value == cell)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
