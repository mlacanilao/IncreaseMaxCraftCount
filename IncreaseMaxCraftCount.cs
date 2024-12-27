using BepInEx;
using HarmonyLib;

namespace IncreaseMaxCraftCount
{
    internal static class ModInfo
    {
        internal const string Guid = "omegaplatinum.elin.increasemaxcraftcount";
        internal const string Name = "Increase Max Craft Count";
        internal const string Version = "1.0.0.0";
    }

    [BepInPlugin(GUID: ModInfo.Guid, Name: ModInfo.Name, Version: ModInfo.Version)]
    internal class IncreaseMaxCraftCount : BaseUnityPlugin
    {
        internal static IncreaseMaxCraftCount Instance { get; private set; }

        private void Start()
        {
            Instance = this;

            IncreaseMaxCraftCountConfig.LoadConfig(config: Config);

            var harmony = new Harmony(id: ModInfo.Guid);
            harmony.PatchAll();
        }

        public static void Log(object payload)
        {
            Instance.Logger.LogInfo(data: payload);
        }
    }
    
    [HarmonyPatch(declaringType: typeof(Recipe), methodName: nameof(Recipe.GetMaxCount))]
    internal static class RecipePatch
    {
        [HarmonyPrefix]
        public static bool GetMaxCountPrefix(Recipe __instance, ref int __result)
        {
            int num = IncreaseMaxCraftCountConfig.MaxCraftCount?.Value ?? 999;
            for (int i = 0; i < __instance.ingredients.Count; i++)
            {
                Recipe.Ingredient ingredient = __instance.ingredients[i];
                Thing thing = ingredient.thing;
                int num2 = 0;
                if (!ingredient.optional || thing != null)
                {
                    if (thing != null && !thing.isDestroyed)
                    {
                        num2 = thing.Num / ingredient.req;
                    }

                    if (num2 < num)
                    {
                        num = num2;
                    }
                }
            }

            __result = num;
            return false;
        }
    }
}