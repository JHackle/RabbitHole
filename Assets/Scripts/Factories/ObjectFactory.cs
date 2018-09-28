namespace Hackle.Factories
{
    using Hackle.Map;
    using Hackle.Objects;
    using Hackle.Util;
    using UnityEngine;

    public class ObjectFactory : MonoBehaviour
    {
        public Transform VillageCenterPrefab;
        public Transform KnightPrefab;

        private Transform mapHolder;

        private void Start()
        {
            mapHolder = RestoreMapholder();
        }

        public VillageCenter CreateVillageCenter(Coord position)
        {
            Transform villageCenter = Instantiate(VillageCenterPrefab, TileUtil.CoordToPosition(position), Quaternion.identity).transform;
            villageCenter.parent = mapHolder;
            villageCenter.GetComponent<Objects.Object>().Position = position;
            VillageCenter center = villageCenter.gameObject.GetComponent<VillageCenter>();
            center.Type = ObjectType.VillageCenter;
            return center;
        }

        public Knight CreateKnight(Coord position)
        {
            Transform knightTransform = Instantiate(KnightPrefab, TileUtil.CoordToPosition(position), Quaternion.identity).transform;
            knightTransform.parent = mapHolder;
            knightTransform.GetComponent<Objects.Object>().Position = position;
            Knight knight = knightTransform.gameObject.GetComponent<Knight>();
            knight.Type = ObjectType.Knight;
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
