namespace Hackle.Managers
{
    using Hackle.Objects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    class PlayerManager
    {
        private List<IUnit> Units = new List<IUnit>();

        public void AddUnit(IUnit unit)
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
        private List<IUnit> Movables()
        {
            return Units.Where(u => u is IMovable).ToList();
        }

        /// <summary>
        /// Returns a list of all movable objects which stil can move.
        /// </summary>
        /// <returns></returns>
        private List<IUnit> MovablesWithSteps()
        {
            return Units.Where(u => u is IMovable).Where(u => (u as IMovable).CanMove()).ToList();
        }
    }
}
