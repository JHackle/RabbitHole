namespace Hackle.Factories
{
    using Hackle.Map;
    using Hackle.Objects;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class MenuItemFactory : MonoBehaviour
    {
        public Transform BuildMenuContent;

        public GameObject MenuItem;

        public Sprite Lumberjack;
        public Sprite GoldMine;
        public Sprite Farm;

        internal void CreateMenuItems(ISelectable selected)
        {
            switch (selected.Type)
            {
                case ObjectType.GrassTile:
                    CreateItem(selected, Farm, "Farm");
                    break;
                case ObjectType.DesertTile:
                    CreateItem(selected, Farm, "Farm");
                    CreateItem(selected, Lumberjack, "Lumberjack");
                    CreateItem(selected, GoldMine, "Gold Mine");
                    break;
                case ObjectType.MountainTile:
                    CreateItem(selected, GoldMine, "Gold Mine");
                    break;
                case ObjectType.WaterTile:
                    break;
                case ObjectType.ForestTile:
                    CreateItem(selected, Lumberjack, "Lumberjack");
                    break;
                case ObjectType.Knight:
                    break;
                case ObjectType.VillageCenter:
                    break;
            }
        }

        private void CreateItem(ISelectable selected, Sprite sprite, string label)
        {
            // if this is a tile without building
            if ((selected is Tile) && !((selected as Tile).HasBuilding()))
            {
                GameObject item = Instantiate(MenuItem);
                item.transform.Find("Image").GetComponent<Image>().sprite = sprite;
                item.transform.Find("Text").GetComponent<Text>().text = label;
                item.transform.SetParent(BuildMenuContent.transform, false);
            }
        }
    }
}
