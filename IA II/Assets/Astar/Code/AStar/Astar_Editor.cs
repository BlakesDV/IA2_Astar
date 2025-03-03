using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Blakes.Graph
{
    [CustomEditor(typeof(Astar))]
    public class Astar_Editor : Editor
    {
        #region RuntimeVariables

        protected Astar _Astar;

        #endregion

        #region unityMethods

        public override void OnInspectorGUI()
        {
            if (_Astar == null)
            {
                _Astar = (Astar)target;
            }
            DrawDefaultInspector();

            if (GUILayout.Button("1) Probe Nodes"))
            {
                _Astar.ProbeNodes();
            }
            if (GUILayout.Button("2) Creat Graph (by connecting the nodes)"))
            {
                _Astar.ConnectionNodes();
            }
            if (GUILayout.Button("3) Calculate all routes"))
            {
                _Astar.CalculateAllRoutes();
            }
            if (GUILayout.Button("4) Calculate Best Route"))
            {
                _Astar.OptimizeRoute();
            }
            //if (GUILayout.Button("5) Set Movement to Agent"))
            //{
            //    _Astar.SetMovementOnSO();
            //}
            if (GUILayout.Button("Clean all previuos calculations"))
            {
                _Astar.ClearAll();
            }
        }

        #endregion

        public Astar SetAstar
        {
            set { _Astar = value; }
        }

        public Astar GetAstar
        {
            get { return _Astar; }
        }

    }
}