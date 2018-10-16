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

        public Transform CreateBuilding(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.VillageCenter:
                    return CreateVillageCenter();
                case ObjectType.Lumberjack:
                    return CreateLumberjack();
                case ObjectType.Farm:
                    return CreateFarm();
                case ObjectType.GoldMine:
                    return CreateGoldMine();
                default:
                    throw new InvalidOperationException("The specified type is not a valid building type: " + type);
            }
        }

        private Transform CreateVillageCenter()
        {
            Transform villageCenter = Instantiate(VillageCenterPrefab, Vector3.zero, Quaternion.identity).transform;
            villageCenter.parent = mapHolder;
            VillageCenter center = villageCenter.gameObject.GetComponent<VillageCenter>();
            center.Type = ObjectType.VillageCenter;
            center.ResourcesPerTurn = new Util.Resources(0, 0, 1);
            center.Capacity = 5;
            HudManager.ChangeMaxCapacity(center.Capacity);
            return villageCenter;
        }

        private Transform CreateLumberjack()
        {
            Transform lumberjackTransform = Instantiate(LumberjackPrefab, Vector3.zero, Quaternion.identity).transform;
            lumberjackTransform.parent = mapHolder;
            Lumberjack jack = lumberjackTransform.gameObject.GetComponent<Lumberjack>();
            jack.Type = ObjectType.Lumberjack;
            jack.ResourcesPerTurn = new Util.Resources(5, 0, 0);
            jack.Capacity = 0;
            return lumberjackTransform;
        }

        private Transform CreateFarm()
        {
            Transform farmTransform = Instantiate(FarmPrefab, Vector3.zero, Quaternion.identity).transform;
            farmTransform.parent = mapHolder;
            Farm farm = farmTransform.gameObject.GetComponent<Farm>();
            farm.Type = ObjectType.Farm;
            farm.ResourcesPerTurn = new Util.Resources(0, 5, 0);
            farm.Capacity = 0;
            return farmTransform;
        }

        private Transform CreateGoldMine()
        {
            Transform goldTransform = Instantiate(GoldMinePrefab, Vector3.zero, Quaternion.identity).transform;
            goldTransform.parent = mapHolder;
            GoldMine gold = goldTransform.gameObject.GetComponent<GoldMine>();
            gold.Type = ObjectType.Farm;
            gold.ResourcesPerTurn = new Util.Resources(0, 0, 3);
            gold.Capacity = 0;
            return goldTransform;
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
