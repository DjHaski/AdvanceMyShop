
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
        if (!SemiFunc.RunIsShop() || !SemiFunc.IsMasterClientOrSingleplayer())
            return true;
        
        if (!PluginConfig.individualApply.Value)
        {
            shopWideDiscount = (int) PluginConfig.NextAppliableMultiplier();
            AdvanceMyShop.Logger.LogInfo($"Rolled shop wide multiplier is: {shopWideDiscount}%");
        }
        else
        {
            shopWideDiscount = 0;
            AdvanceMyShop.Logger.LogInfo($"No shop wide multiplier is rolled, individual discounts will be applied instead.");
        }
        return true;
    }
}