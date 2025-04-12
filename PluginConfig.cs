using System;
using System.Collections.Generic;
using BepInEx.Configuration;

namespace AdvanceMyShop;

internal static class PluginConfig
{
    public static ConfigEntry<float> basePriceMultiplier = null!;
    public static ConfigEntry<int> discountChance = null!;
    public static ConfigEntry<int> overpriceChance = null!;
    public static ConfigEntry<bool> individualApply = null!;
    private static ConfigEntry<string> percentages = null!;
    public static ConfigEntry<float> freeItemChance = null!;
    public static int[] normalizedPercentages = null!;

    public static Dictionary<string, ConfigEntry<float>> itemMultipliers = new();

    public static void Load(ConfigFile config)
    {
        discountChance = config.Bind("General", "DiscountChance", 50, "Chance of a discount to occur. Works shop wide. Set to 0 to disable.");
        overpriceChance = config.Bind("General", "OverpriceChance", 50, "Chance of an overprice to occur. Works shop wide. Set to 0 to disable.");
        individualApply = config.Bind("General", "IndividualApply", false, "If enabled, then each item will have a chance of being overpriced/discounted separately, rather than the whole shop.");
        percentages = config.Bind("General", "Percentages", "5, 10, 15, 30, 50", "List of percentages that will be used in the overpricing/discounts event. Supports only integers.");
        freeItemChance = config.Bind("General", "FreeItemChance", 0.3f, new ConfigDescription("Chance of a free item to occur. Works per item. Set to 0 to disable.", new AcceptableValueRange<float>(0, 100)));

        basePriceMultiplier = config.Bind("Multipliers", "BasePriceMultiplier", 1f, new ConfigDescription("Base price multiplier applied to all items (e.g. price * multiplier). Set to 1 to disable.", new AcceptableValueRange<float>(-10, 10)));
        foreach (var item in Enum.GetValues(typeof(SemiFunc.itemType)))
        {
            itemMultipliers.Add(item.ToString(),
                config.Bind(
                    "Multipliers",
                    $"MultiplierFor_{item}",
                    1f,
                    new ConfigDescription($"Multiplier applied to '{item}' items. Set to 1 to disable.", new AcceptableValueRange<float>(-10, 10))
                )
            );

        }

        normalizedPercentages = Array.ConvertAll(percentages.Value.Split(','), int.Parse);
    }

    public static float GetResultingMultiplier()
    {
        Random random = new();
        if (Utils.GetRandomNumber(1, 100) <= discountChance.Value)
        {
            return normalizedPercentages[random.Next(0, normalizedPercentages.Length - 1)];
        }
        else if (Utils.GetRandomNumber(1, 100) <= overpriceChance.Value)
        {
            return -1 * normalizedPercentages[random.Next(0, normalizedPercentages.Length - 1)];
        }
        else
        {
            return 0;
        }
    }

    public static bool IsItemFree()
    {
        return Utils.GetRandomNumber(1, 100) <= freeItemChance.Value;
    }
}
