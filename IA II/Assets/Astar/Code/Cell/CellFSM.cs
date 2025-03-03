using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blakes.Astar
{
    #region Enums

    public enum CellStates
    {
        UNDEFINED,
        WEIGHTED,
        PROPAGATED,
        DISCARDED,
        BLOCKED,
    }

    #endregion

    #region Structs

    [System.Serializable]
    public struct MaterialCellState
    {
        public Material UNDEFINED;
        public Material WEIGHTED;
        public Material PROPAGATED;
        public Material DISCARDED;
        public Material BLOCKED;
    }
    [System.Serializable]
    public struct CellReferences
    {
        [SerializeField] public MaterialCellState matCellState;
        [SerializeField] public GameObject _state;
        [SerializeField] public GameObject _pos;
        [SerializeField] public Astar _aStar;
        [SerializeField] public MeshRenderer _meshrenderer;
    }

    #endregion

    public class CellFSM : MonoBehaviour
    {
        #region References

        [SerializeField] protected CellReferences _cellReferences;

        #endregion

        #region RuntimeVars

        [SerializeField] protected int weight;
        protected CellStates _currentState;

        #endregion

        #region StateMethods
        
        private void UndefinedStateMethod()
        {
            IconManager.SetIcon(_cellReferences._state, IconManager.LabelIcon.Gray);
            IconManager.SetIcon(_cellReferences._pos, IconManager.LabelIcon.Gray);
            _cellReferences._meshrenderer.material = _cellReferences.matCellState.UNDEFINED;
        }
        private void WeightedStateMethod()
        {
            IconManager.SetIcon(_cellReferences._state, IconManager.LabelIcon.Blue);
            IconManager.SetIcon(_cellReferences._pos, IconManager.LabelIcon.Blue);
            _cellReferences._meshrenderer.material = _cellReferences.matCellState.WEIGHTED;
        }
        private void PropagatedStateMethod()
        {
            IconManager.SetIcon(_cellReferences._state, IconManager.LabelIcon.Green);
            IconManager.SetIcon(_cellReferences._pos, IconManager.LabelIcon.Green);
            _cellReferences._meshrenderer.material = _cellReferences.matCellState.PROPAGATED;
        }
        private void DiscardedStateMethod()
        {
            IconManager.SetIcon(_cellReferences._state, IconManager.LabelIcon.Orange);
            IconManager.SetIcon(_cellReferences._pos, IconManager.LabelIcon.Orange);
            _cellReferences._meshrenderer.material = _cellReferences.matCellState.DISCARDED;
        }
        private void BlockedStateMethod()
        {
            IconManager.SetIcon(_cellReferences._state, IconManager.LabelIcon.Red);
            IconManager.SetIcon(_cellReferences._pos, IconManager.LabelIcon.Red);
            _cellReferences._meshrenderer.material = _cellReferences.matCellState.BLOCKED;
        }

        #endregion

        #region PublicMethods

        public virtual void StateMechanic(CellStates value)
        {
            _currentState = value;
            _cellReferences._state.name = _currentState.ToString();
            switch (value)
            {
                case CellStates.UNDEFINED:
                    UndefinedStateMethod();
                    break;
                case CellStates.WEIGHTED:
                    WeightedStateMethod();
                    break;
                case CellStates.PROPAGATED:
                    PropagatedStateMethod();
                    break;
                case CellStates.DISCARDED:
                    DiscardedStateMethod();
                    break;
                case CellStates.BLOCKED:
                    BlockedStateMethod();
                    break;
            }
        }

        #endregion

        #region GettersAndSetters

        public Astar SetAStar
        {
            set { _cellReferences._aStar = value; }
        }
        #endregion
    }
}
