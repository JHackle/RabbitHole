
namespace Hackle.Factories
{
    using Hackle.Util;
    using Hackle.Objects;
    using System;

    class PriceDictionary
    {
        public static Resources GetPrice(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Knight:
                    return new Resources(0, 10, 5);
                case ObjectType.VillageCenter:
                    return new Resources(100, 30, 50);
                case ObjectType.Farm:
                    return new Resources(20, 0, 0);
                case ObjectType.GoldMine:
                    return new Resources(45, 0, 0);
                case ObjectType.Lumberjack:
                    return new Resources(20, 0, 0);
                default:
                    throw new InvalidOperationException("There is no price for the object type: " + type);
            }
        }
    }
}
