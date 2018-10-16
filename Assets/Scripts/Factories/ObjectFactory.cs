namespace Hackle.Factories
{
    using Hackle.Managers;
    using Hackle.Objects;
    using Hackle.Objects.Buildings;
    using Hackle.Objects.Units;
    using System;
    using UnityEngine;

    public class ObjectFactory : MonoBehaviour
    {
        public HudManager HudManager;

        public Transform VillageCenterPrefab;
        public Transform LumberjackPrefab;
        public Transform FarmPrefab;
        public Transform GoldMinePrefab;
        public Transform KnightPrefab;

        private Transform mapHolder;

        private void Start()
        {
            mapHolder = RestoreMapholder();
        }

        public Building CreateBuilding(ObjectType type)
        {
            Transform transform = Instantiate(GetPrefab(type), Vector3.zero, Quaternion.identity).transform;
            transform.parent = mapHolder;
            Building building = transform.gameObject.GetComponent<Building>();
            building.Type = type;
            building.ResourcesPerTurn = GetResources(type);
            building.Capacity = GetCapacity(type);
            HudManager.ChangeCapacity(building.Capacity);
            return building;
        }

        private Util.Resources GetResources(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.VillageCenter:
                    return new Util.Resources(0, 0, 1);
                case ObjectType.Farm:
                    return new Util.Resources(0, 1, 0);
                case ObjectType.GoldMine:
                    return new Util.Resources(0, 0, 2);
                case ObjectType.Lumberjack:
                    return new Util.Resources(3, 0, 0);
                default:
                    throw new InvalidOperationException("There are no resource for this object type: " + type);
            }
        }

        private int GetCapacity(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.VillageCenter:
                    return 5;
                case ObjectType.Farm:
                case ObjectType.GoldMine:
                case ObjectType.Lumberjack:
                    return 0;
                default:
                    throw new InvalidOperationException("There are no resource for this object type: " + type);
            }
        }


        private Transform GetPrefab(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.VillageCenter:
                    return VillageCenterPrefab;
                case ObjectType.Farm:
                    return FarmPrefab;
                case ObjectType.GoldMine:
                    return GoldMinePrefab;
                case ObjectType.Lumberjack:
                    return LumberjackPrefab;
                default:
                    throw new InvalidOperationException("There is no prefab for the given object type: " + type);
            }
        }

        public Knight CreateKnight()
        {
            Transform knightTransform = Instantiate(KnightPrefab, Vector3.zero, Quaternion.identity).transform;
            knightTransform.parent = mapHolder;
            Knight knight = knightTransform.gameObject.GetComponent<Knight>();
            knight.Type = ObjectType.Knight;
            knight.CapacityValue = 1;
            HudManager.ChangeCapacity(knight.CapacityValue);
            return knight;
        }
        private Transform RestoreMapholder()
        {
            string holderName = "Generated Objects";
            if (transform.Find(holderName))
            {
                DestroyImmediate(transform.Find(holderName).gameObject);
            }
            Transform mapHolder = new GameObject(holderName).transform;
            mapHolder.parent = transform;
            return mapHolder;
        }
    }
}
