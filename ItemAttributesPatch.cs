using HarmonyLib;
using Photon.Pun;

using System;
using System.Collections.Generic;

namespace AdvanceMyShop;
[HarmonyPatch(typeof(ItemAttributes), "GetValue")]
[HarmonyPriority(Priority.Last)]
public class ItemAttributesPatch
{
    private static float ApplyMultiplier(float source, float multiplier)
    {
        if (multiplier == 0f)
        {
            return source;
        }
        return source * (1f - multiplier / 100f);
    }

    public static bool Prefix(ItemAttributes __instance)
    {
        if (GameManager.Multiplayer() && !PhotonNetwork.IsMasterClient)
        {
            return true;
        }

        var baseRandomizedPrice = Utils.GetRandomNumber(__instance.itemValueMin, __instance.itemValueMax);
        var finalPrice = (float) __instance.value;
        if (finalPrice == 0)
        {
            finalPrice = (float) Math.Round(ShopManager.instance.itemValueMultiplier * baseRandomizedPrice);
            switch (__instance.itemType)
            {
                case SemiFunc.itemType.power_crystal:
                    finalPrice += finalPrice * ShopManager.instance.crystalValueIncrease * RunManager.instance.levelsCompleted;
                    break;
                case SemiFunc.itemType.healthPack:
                    finalPrice += finalPrice * ShopManager.instance.healthPackValueIncrease * RunManager.instance.levelsCompleted;
                    break;
                case SemiFunc.itemType.player_upgrade:
                    finalPrice += finalPrice * ShopManager.instance.upgradeValueIncrease * StatsManager.instance.GetItemsUpgradesPurchased(__instance.itemName);
                    break;
                default:
                    break;
            }
            finalPrice *= PluginConfig.basePriceMultiplier.Value;
            finalPrice *= PluginConfig.itemMultipliers.GetValueOrDefault(__instance.itemType.ToString())?.Value ?? 1f;
        }
        

        if (PluginConfig.individualApply.Value)
        {
            finalPrice = ApplyMultiplier(finalPrice, PluginConfig.NextAppliableMultiplier());
        }
        else if (ShopManagerPatch.shopWideDiscount != 0)
        {
            finalPrice = ApplyMultiplier(finalPrice, ShopManagerPatch.shopWideDiscount);
        }

        if (PluginConfig.WillBeItemFree())
        {
            finalPrice = 0f;
        }
        else if (PluginConfig.WillBeItemOverpriced())
        {
            finalPrice *= PluginConfig.overpricedItemMultiplier.Value;
        }

        __instance.value = Utils.NormalizePrice(finalPrice);
        if (GameManager.Multiplayer())
        {
            __instance.photonView.RPC("GetValueRPC", RpcTarget.Others, new object[1] { __instance.value });
        }
        return false;
    }
}
