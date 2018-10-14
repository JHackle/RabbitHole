namespace Hackle.Factories
{
    using Hackle.Managers;
    using Hackle.Map;
    using Hackle.Objects;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// This class is responsible to create items in the build menu. The items itself depend on the
    /// Selectable which is currently selected.
    /// </summary>
    public class MenuItemFactory : MonoBehaviour
    {
        public GameManager GameManager;
        public SelectionManager SelectionManager;
        public Transform BuildMenuContent;

        public GameObject MenuItem;

        public Sprite Lumberjack;
        public Sprite GoldMine;
        public Sprite Farm;

        /// <summary>
        /// Creates all available items to the build menu. This depends on the selected object.
        /// </summary>
        internal void CreateMenuItems()
        {
            ISelectable selected = SelectionManager.SelectedUnit<ISelectable>();
            switch (selected.Type)
            {
                case ObjectType.GrassTile:
                    CreateItem(selected, ObjectType.Farm);
                    break;

                case ObjectType.DesertTile:
                    CreateItem(selected, ObjectType.Farm);
                    CreateItem(selected, ObjectType.Lumberjack);
                    CreateItem(selected, ObjectType.GoldMine);
                    break;

                case ObjectType.MountainTile:
                    CreateItem(selected, ObjectType.GoldMine);
                    break;

                case ObjectType.WaterTile:
                    break;

                case ObjectType.ForestTile:
                    CreateItem(selected, ObjectType.Lumberjack);
                    break;

                case ObjectType.Knight:
                    break;

                case ObjectType.VillageCenter:
                    break;
            }
        }

        private void CreateItem(ISelectable selected, ObjectType type)
        {
            // if this is a tile without building
            if ((selected is Tile) && !((selected as Tile).HasBuilding()))
            {
                GameObject item = Instantiate(MenuItem);
                item.transform.Find("Image").GetComponent<Image>().sprite = GetSprite(type);
                item.transform.Find("Text").GetComponent<Text>().text = GetLabel(type);
                item.transform.SetParent(BuildMenuContent.transform, false);
                item.GetComponent<Hackle.Objects.Object>().Type = type;
                item.GetComponent<Button>().onClick.AddListener(delegate { GameManager.ClickMenuItem(item); });
            }
        }

        private Sprite GetSprite(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Farm:
                    return Farm;
                case ObjectType.GoldMine:
                    return GoldMine;
                case ObjectType.Lumberjack:
                    return Lumberjack;
                default:
                    throw new InvalidOperationException("There is no build menu sprite for the given object type: " + type);
            }
        }

        private string GetLabel(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Farm:
                    return "Farm";
                case ObjectType.GoldMine:
                    return "Gold Mine";
                case ObjectType.Lumberjack:
                    return "Lumberjack";
                default:
                    throw new InvalidOperationException("There is no build menu label for the given object type: " + type);
            }
        }
    }
}