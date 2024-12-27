using BepInEx.Configuration;

namespace IncreaseMaxCraftCount
{
    internal static class IncreaseMaxCraftCountConfig
    {
        internal static ConfigEntry<int> MaxCraftCount;

        internal static void LoadConfig(ConfigFile config)
        {
            MaxCraftCount = config.Bind(
                section: ModInfo.Name,
                key: "Max Craft Count",
                defaultValue: 9999,
                description: "Set the maximum number of items that can be crafted in a single action.\n" +
                             "Must be an integer value.\n" +
                             "一度のアクションで作成できるアイテムの最大数を設定します。\n" +
                             "整数値である必要があります。\n" +
                             "设置一次操作中可制作物品的最大数量。\n" +
                             "必须为整数值。"
            );
        }
    }
}