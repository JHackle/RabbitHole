namespace Hackle.Managers
{
    using Hackle.Objects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    class PlayerManager
    {
        private int capacityMax;
        private int capacity;
        private int wood;
        private int food;
        private int gold;

        private List<IObject> Objects = new List<IObject>();

        internal void AddObject(IObject obj)
        {
            Objects.Add(obj);
        }

        internal bool CanAnyUnitMove()
        {
            return MovablesWithSteps().Count > 0;
        }

        internal void ResetSteps()
        {
            Movables().ForEach(u => (u as IMovable).RestoreSteps());
        }

        internal void Harvest()
        {

        }

        private List<IObject> Selectables()
        {
            return Objects.Where(u => u is ISelectable).ToList();
        }

        /// <summary>
        /// Returns a list of all movable objects of this player.
        /// </summary>
        /// <returns></returns>
        private List<IObject> Movables()
        {
            return Objects.Where(u => u is IMovable).ToList();
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
