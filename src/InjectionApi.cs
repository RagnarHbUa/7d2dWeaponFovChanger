namespace _7d2dFovChanger
{
    public class InjectionApi : IModApi
    {
        void IModApi.InitMod(Mod _modInstance)
        {
            Config.Load(System.IO.Path.Combine(_modInstance.Path, "Config.xml"));

            var harmony = new HarmonyLib.Harmony("Ragnars_FovChanger");
            harmony.PatchAll();
        }
    }
}
