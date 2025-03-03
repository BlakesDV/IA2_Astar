using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Blakes.FSM;

namespace Blakes.Astar
{
    public class Propagation : MonoBehaviour
    {
        protected CellFSM _cell;
        [SerializeField] protected List<CellFSM> route;

        #region PublicMethods

        public void CellToRoute(CellFSM cell)
        {
            
        }

        #endregion
    }
}
