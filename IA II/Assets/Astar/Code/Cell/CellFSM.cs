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
            throw new NotImplementedException();
        }
        private void WeightedStateMethod()
        {
            throw new NotImplementedException();
        }
        private void PropagatedStateMethod()
        {
            throw new NotImplementedException();
        }
        private void DiscardedStateMethod()
        {
            throw new NotImplementedException();
        }
        private void BlockedStateMethod()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region PublicMethods

        public virtual void StateMechanic(CellStates value)
        {
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
