using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Blakes.FSM;
using static IconManager;

namespace Blakes.Astar 
{
    public class Astar : MonoBehaviour
    {
        //[SerializeField] EnemyInteractiveScript_ScriptableObject enemyInteractiveScript;

        #region RuntimeVariables

        [SerializeField] protected CellFSM[,] _2dMatrix;
        protected CellFSM _cell;

        [SerializeField] protected int sizeX = 10;
        [SerializeField] protected int sizeZ = 10;
        [SerializeField] float cellSize = 1.0f;
        [SerializeField] protected GameObject prefabNodeTest;

        #endregion

        #region References


        [SerializeField] protected GameObject initialPosition;
        [SerializeField] protected GameObject finalPosition;

        
        [SerializeField] protected List<Cell> graph;
        [SerializeField] protected List<Cell> nodesContainer;

        #endregion
        
        #region RuntimeVariables

        [SerializeField] protected List<Route> theRoute;

        [SerializeField] protected Cell initialCell;
        [SerializeField] protected Cell finalCell;

        #endregion

        #region GUILayoutButton

        #endregion

        #region LocalMethods

        //Recursive Method
        

        #endregion

        #region RuntimeMethods

        public void ProbeNodes()
        {
            _2dMatrix = new CellFSM[sizeX,sizeZ];
            Vector3 startPosition = transform.position -
                                new Vector3(sizeX * cellSize, 0, sizeZ * cellSize);

            for (int x = 0; x < sizeX; x++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    Vector3 cellSpawnPos = startPosition + new Vector3(x * cellSize, 1f, z * cellSize);
                    GameObject cellInstance = Instantiate(prefabNodeTest, cellSpawnPos, Quaternion.identity);
                    cellInstance.name = $"Node {x} {z}";
                    cellInstance.GetComponent<Cell>().ValidNode();
                    cellInstance.transform.SetParent(this.transform);
                    if (cellInstance.GetComponent<Cell>().isNodeConnectable)
                    {
                        graph.Add(cellInstance.GetComponent<Cell>());
                    }
                    nodesContainer.Add(cellInstance.GetComponent<Cell>());
                }
            }

            GameObject goNode = Instantiate(prefabNodeTest, initialPosition.transform.position, Quaternion.identity);
            goNode.name = $"Node Initial";
            initialCell = goNode.GetComponent<Cell>();
            initialCell.ValidNode();
            goNode.transform.SetParent(this.transform);
            if (initialCell.isNodeConnectable)
            {
                graph.Add(initialCell);
            }
            nodesContainer.Add(initialCell);

            goNode = Instantiate(prefabNodeTest, finalPosition.transform.position, Quaternion.identity);
            goNode.name = $"Node Final";
            finalCell = goNode.GetComponent<Cell>();
            finalCell.ValidNode();
            goNode.transform.SetParent(this.transform);
            if (finalCell.isNodeConnectable)
            {
                graph.Add(finalCell);
            }
            nodesContainer.Add(finalCell);
        }

        public void ConnectionNodes()
        {

            foreach (Cell cell in graph)
            {
                cell.RayCastAndDistanceForAllNodes();
            }

        }

        public void ClearAll()
        {
            foreach (Cell cell in nodesContainer)
            {
                DestroyImmediate(cell.gameObject);
            }
            graph.Clear();
            nodesContainer.Clear();
            theRoute.Clear();
        }


        //public void SetMovementOnSO()
        //{
        //    Route selectedRoute = theRoute[0];
        //    foreach (Node node in selectedRoute.nodes)
        //    {
        //        PatrolScript patrolScript = new PatrolScript();
        //        patrolScript.actionToExecute = Actions.WALK;
        //        patrolScript.speedOrTime = 5f;
        //        patrolScript.destinyVector = node.transform.position;
        //        enemyInteractiveScript.patrolScript.Add(patrolScript);
        //    }
        //}

        #endregion

        #region UnityMethods

        private void Start()
        {
            IconManager.SetIcon(gameObject, LabelIcon.Orange);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Vector3 startPosition = transform.position -
                                    new Vector3(sizeX * cellSize, 0, sizeZ * cellSize);

            for (int x = 0; x < sizeX; x++)
            {
                Vector3 start = startPosition + new Vector3(x * cellSize, 0, 0);
                Vector3 end = start + new Vector3(0, 0, sizeZ * cellSize);
                Gizmos.DrawLine(start, end);
            }

            for (int z = 0; z < sizeZ; z++)
            {
                Vector3 start = startPosition + new Vector3(0, 0, z * cellSize);
                Vector3 end = start + new Vector3(sizeX * cellSize + 1, 0, 0);
                Gizmos.DrawLine(start, end);
            }
        }

        #endregion

        #region GetterSetters

        public List<Cell> GetListOfNodes
        {
            get { return graph; }
        }

        public float GetCellSize
        {
            get { return cellSize; }
        }

        #endregion
    }
}