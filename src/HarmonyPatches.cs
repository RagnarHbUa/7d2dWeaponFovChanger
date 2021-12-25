using System;
using UnityEngine;
using HarmonyLib;

namespace _7d2dFovChanger
{
    [HarmonyPatch(typeof(vp_FPWeapon))]
    [HarmonyPatch("Start")]
    public class FovValueChanger
    {
        static void Postfix(vp_FPWeapon __instance)
        {
            // I don't actually use these values because the game overrides them for non-gun items. RenderingFieldOfView is set to 45 from scripts inside resources.assets such as WeaponBow, WeaponDefault ...
            __instance.RenderingFieldOfView = Config.weapon_fov;
            __instance.originalRenderingFieldOfView = Config.weapon_fov;
        }
    }

    [HarmonyPatch(typeof(vp_FPWeapon))]
    [HarmonyPatch("UpdateZoom")]
    public class FovCheckRemover
    {
        static bool Prefix(vp_FPWeapon __instance, float ___m_FinalZoomTime, bool ___m_Wielded, GameObject ___m_WeaponCamera)
        {
            if (!___m_Wielded)
                return false;

            __instance.RenderingZoomDamping = Mathf.Max(__instance.RenderingZoomDamping, 0.01f);
            float num = 1f - (___m_FinalZoomTime - Time.time) / __instance.RenderingZoomDamping;
            if (___m_WeaponCamera != null)
            {
                ___m_WeaponCamera.GetComponent<Camera>().fieldOfView = Mathf.SmoothStep(___m_WeaponCamera.gameObject.GetComponent<Camera>().fieldOfView, Config.weapon_fov, num * 15f);
            }

            return false;
        }
    }
}
