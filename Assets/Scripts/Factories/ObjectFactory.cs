namespace Hackle.Factories
{
    using Hackle.Managers;
    using Hackle.Objects;
    using Hackle.Objects.Buildings;
    using Hackle.Objects.Units;
    using UnityEngine;

    public class ObjectFactory : MonoBehaviour
    {
        public HudManager HudManager;

        public Transform VillageCenterPrefab;
        public Transform KnightPrefab;

        private Transform mapHolder;

        private void Start()
        {
            mapHolder = RestoreMapholder();
        }

        public VillageCenter CreateVillageCenter()
        {
            Transform villageCenter = Instantiate(VillageCenterPrefab, Vector3.zero, Quaternion.identity).transform;
            villageCenter.parent = mapHolder;
            VillageCenter center = villageCenter.gameObject.GetComponent<VillageCenter>();
            center.Type = ObjectType.VillageCenter;
            center.ResourcesPerTurn = new Util.Resources(0, 0, 1);
            center.Capacity = 5;
            HudManager.ChangeMaxCapacity(center.Capacity);
            return center;
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
