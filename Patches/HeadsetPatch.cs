﻿using Comfort.Common;
using EFT;
using EFT.InventoryLogic;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using HeadsetClass = HeadphonesItemClass;

namespace BobbysMusicPlayer.Patches
{
    public class HeadsetPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player), nameof(Player.UpdatePhones));
        }

        [PatchPostfix]
        private static void Postfix(Player __instance)
        {
            if (__instance == Singleton<GameWorld>.Instance.MainPlayer)
            {
                InventoryEquipment equipment = __instance.Equipment;
                CompoundItem headwear = equipment.GetSlot(EquipmentSlot.Headwear).ContainedItem as CompoundItem;
                HeadsetClass headset = (equipment.GetSlot(EquipmentSlot.Earpiece).ContainedItem as HeadsetClass) ?? ((headwear != null) ? headwear.GetAllItemsFromCollection().OfType<HeadsetClass>().FirstOrDefault<HeadsetClass>() : null);
                if (headset != null)
                {
                    Plugin.headsetMultiplier = Plugin.HeadsetMultiplier.Value;
                }
                else
                {
                    Plugin.headsetMultiplier = 1f;
                }
            }
        }
    }
}