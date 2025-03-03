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

    public class CellFSM : MonoBehaviour
    {

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
    }
}
