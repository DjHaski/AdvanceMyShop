
using System;
using HarmonyLib;
using UnityEngine.UIElements;

namespace AdvanceMyShop;
[HarmonyPatch(typeof(ShopManager), "ShopInitialize")]
class ShopManagerPatch
{
    public static int shopWideDiscount = 0;
    public static bool Prefix(ShopManager __instance)
    {
        if (SemiFunc.IsMasterClientOrSingleplayer() && SemiFunc.RunIsShop() && !PluginConfig.individualApply.Value)
        {
            shopWideDiscount = (int) PluginConfig.GetResultingMultiplier();
            AdvanceMyShop.Logger.LogInfo($"Rolled shop wide multiplier is: {shopWideDiscount}%");
        }
        else
        {
            shopWideDiscount = 0;
            AdvanceMyShop.Logger.LogInfo($"No shop wide multiplier is rolled.");
        }
        return true;
    }
}