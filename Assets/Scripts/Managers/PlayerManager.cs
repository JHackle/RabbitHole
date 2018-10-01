namespace Hackle.Managers
{
    using Hackle.Objects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class PlayerManager : MonoBehaviour
    {
        public HudManager Hud;

        private int capacityMax;
        private int capacity;

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
            foreach (Building building in Buildings())
            {
                Wood += building.ResourcesPerTurn.Wood;
                Food += building.ResourcesPerTurn.Food;
                Gold += building.ResourcesPerTurn.Gold;
            }
        }

        private List<Building> Buildings()
        {
            return Objects.Where(u => u is Building).ToList().Cast<Building>().ToList();
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


        public int Wood
        {
            get
            {
                return Convert.ToInt32(Hud.GetWood());
            }
            set
            {
                Hud.SetWood(value);
            }
        }
        public int Food
        {
            get
            {
                return Convert.ToInt32(Hud.GetFood());
            }
            set
            {
                Hud.SetFood(value);
            }
        }
        public int Gold
        {
            get
            {
                return Convert.ToInt32(Hud.GetGold());
            }
            set
            {
                Hud.SetGold(value);
            }
        }
    }
}
