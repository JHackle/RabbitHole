namespace Hackle.Factories
{
    using Hackle.Map;
    using Hackle.Objects;
    using Hackle.Util;
    using UnityEngine;

    class ObjectFactory : MonoBehaviour
    {
        private Transform villageCenterPrefab;
        private Transform knightPrefab;
        private Transform mapHolder;

        public void Init(Transform villageCenterPrefab, Transform knightPrefab)
        {
            this.villageCenterPrefab = villageCenterPrefab;
            this.knightPrefab = knightPrefab;
            mapHolder = RestoreMapholder();
        }

        public VillageCenter CreateVillageCenter(Coord position)
        {
            Transform villageCenter = Instantiate(villageCenterPrefab, TileUtil.CoordToPosition(position), Quaternion.identity).transform;
            villageCenter.parent = mapHolder;
            villageCenter.GetComponent<Objects.Object>().Position = position;
            VillageCenter center = villageCenter.gameObject.GetComponent<VillageCenter>();
            center.Type = ObjectType.VillageCenter;
            return center;
        }

        public Knight CreateKnight(Coord position)
        {
            Transform knightTransform = Instantiate(knightPrefab, TileUtil.CoordToPosition(position), Quaternion.identity).transform;
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
