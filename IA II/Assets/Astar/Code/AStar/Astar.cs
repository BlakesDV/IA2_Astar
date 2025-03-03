using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Blakes.FSM;

namespace Blakes.Graph 
{
    public class Astar : MonoBehaviour
    {
        //[SerializeField] EnemyInteractiveScript_ScriptableObject enemyInteractiveScript;

        #region RuntimeVariables

        [SerializeField] protected int sizeX = 10;
        [SerializeField] protected int sizeZ = 10;
        [SerializeField] float cellSize = 1.0f;
        [SerializeField] protected GameObject prefabNodeTest;
        //protected GameObject nodesContainer;

        #endregion

        #region References

        //both will be obtained by calculating the leeser distance between:
        //Avatar VS all nodes
        //Goal VS all nodes
        [SerializeField] protected GameObject initialPosition;
        [SerializeField] protected GameObject finalPosition;

        //The collection of all the nodes
        //Wich every node contains multiple connection
        //defines the graph 
        
        [SerializeField] protected List<Cell> graph;
        [SerializeField] protected List<Cell> nodesContainer;

        #endregion
        
        #region RuntimeVariables

        protected Route initialRoute;
        [SerializeField] protected List<Route> allRoutes;
        [SerializeField] protected List<Route> allValidRoutes;
        [SerializeField] protected List<Route> theRoute;
        //succesfullRoutes
        //truncatedRoutes
        //failledRoutes

        [SerializeField] protected Cell initialCell;
        [SerializeField] protected Cell finalCell;

        #endregion

        #region GUILayoutButton

        public void CalculateAllRoutes()
        {
            initialRoute = new Route();
            initialRoute.AddNode(initialCell, 0);

            allRoutes = new List<Route>();
            allRoutes.Add(initialRoute);
            //Recursive Method
            ExploreBranchTree(initialRoute, initialCell);
        }

        #endregion

        #region LocalMethods

        //Recursive Method
        protected void ExploreBranchTree(Route previousRoute, Cell actualNodeToExplore)
        {
            //Are we in the destiny node
            if (actualNodeToExplore == finalCell)
            {
                allValidRoutes.Add(previousRoute);
                //Break point for recursivity at this level
                return;
            }
            else
            {
                //validate the connections of the actual node
                foreach (Connection connectionOfTheActualNode in actualNodeToExplore.GetConnections)
                {
                    Cell nextNode = connectionOfTheActualNode.RetreiveOtherNodeThan(actualNodeToExplore);

                    if (!previousRoute.ContainsNodeInRoute(nextNode))
                    {
                        //1) Furthe exploration in a branch of the tree
                        //Invocation to itself
                        Route newRoute = new Route(
                            previousRoute.nodes,
                            previousRoute.sumDistance
                            );
                        newRoute.AddNode(
                            nextNode,
                            connectionOfTheActualNode.ditanceBetweenNodes
                            );
                        allRoutes.Add(newRoute); //truncated route
                                                 //Invocation to itself to continue recursivity
                        ExploreBranchTree(newRoute, nextNode);
                    }
                    else
                    {
                        //2) Connection to a previously explored node in the route
                        //Break point for recursivity
                    }
                }
            }
            //Cut the recursivity
        }

        #endregion

        #region RuntimeMethods

        public void ProbeNodes()
        {
            Vector3 startPosition = transform.position -
                                new Vector3(sizeX * cellSize, 0, sizeZ * cellSize);

            for (int x = 0; x < sizeX; x++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    Vector3 nodePosition = startPosition + new Vector3(x * cellSize, 1f, z * cellSize);
                    GameObject nodesInstance = Instantiate(prefabNodeTest, nodePosition, Quaternion.identity);
                    nodesInstance.name = $"Node {x} {z}";
                    nodesInstance.GetComponent<Cell>().ValidNode();
                    nodesInstance.transform.SetParent(this.transform);
                    if (nodesInstance.GetComponent<Cell>().isNodeConnectable)
                    {
                        graph.Add(nodesInstance.GetComponent<Cell>());
                    }
                    nodesContainer.Add(nodesInstance.GetComponent<Cell>());
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
            allRoutes.Clear();
            allValidRoutes.Clear();
            theRoute.Clear();
        }

        public void OptimizeRoute()
        {
            Route TheRealRoute = new Route();
            float theShortestRoute = float.MaxValue;

            foreach (Route route in allValidRoutes)
            {
                if (route.sumDistance < theShortestRoute)
                {
                    theShortestRoute = route.sumDistance;
                    TheRealRoute = route;
                    theRoute.Clear();
                    theRoute.Add(TheRealRoute);
                }
            }
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
        //getter of the list

        #endregion
    }
}