namespace Hackle.Managers
{
    using Hackle.Objects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    class PlayerManager
    {
        private List<IObject> Units = new List<IObject>();

        public void AddUnit(IObject unit)
        {
            Units.Add(unit);
        }

        public bool CanAnyUnitMove()
        {
            return MovablesWithSteps().Count > 0;
        }

        public void ResetSteps()
        {
            Movables().ForEach(u => (u as IMovable).RestoreSteps());
        }

        /// <summary>
        /// Returns a list of all movable objects of this player.
        /// </summary>
        /// <returns></returns>
        private List<IObject> Movables()
        {
            return Units.Where(u => u is IMovable).ToList();
        }

        /// <summary>
        /// Returns a list of all movable objects which stil can move.
        /// </summary>
        /// <returns></returns>
        private List<IObject> MovablesWithSteps()
        {
            return Movables().Where(u => (u as IMovable).CanMove()).ToList();
        }
    }
}
