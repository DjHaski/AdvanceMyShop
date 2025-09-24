using HarmonyLib;
using Photon.Pun;

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

        var baseValue = Utils.GetRandomNumber(__instance.itemValueMin, __instance.itemValueMax);
        if (!PluginConfig.disableVanillaPriceMultiplier.Value)
            baseValue *= ShopManager.instance.itemValueMultiplier;
        baseValue = Mathf.Max(1000f, baseValue);

        var finalPrice = (float) __instance.value;
        if (finalPrice == 0)
        {
            finalPrice = Mathf.Ceil(baseValue / 1000);
            switch (__instance.itemType)
            {
                case SemiFunc.itemType.power_crystal:
                    finalPrice = ShopManager.instance.CrystalValueGet(finalPrice); 
                    break;
                case SemiFunc.itemType.healthPack:
                    finalPrice = ShopManager.instance.HealthPackValueGet(finalPrice);
                    break;
                case SemiFunc.itemType.item_upgrade:
                    finalPrice = ShopManager.instance.UpgradeValueGet(finalPrice, __instance.item);
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

        __instance.value = (int) finalPrice;
        if (GameManager.Multiplayer())
        {
            __instance.photonView.RPC("GetValueRPC", RpcTarget.Others, new object[1] { __instance.value });
        }
        return false;
    }
}
