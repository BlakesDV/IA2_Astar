using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blakes.Astar
{
    [System.Serializable]
    public class Connection
    {
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

        public Route()
        {
            nodes = new List<Cell>();
            sumDistance = 0;
        }

        public Route(List<Cell> nodesToClone, float sumDistanceToCopy)
        {
            nodes = new List<Cell>();
            foreach (Cell cell in nodesToClone)
            {
                nodes.Add(cell);
            }

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
