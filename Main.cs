
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Map;
using Assets.Scripts.Unity.UI_New.InGame;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using NinjaKiwi.Common;

[assembly: MelonModInfo(typeof(PokemonMod.Main), "Pokemon_Towers", "1.1.0", "Commander__Cat")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace PokemonMod
{
    public class Main : BloonsTD6Mod
    {
        public override void OnApplicationStart()
        {
            //assetBundle = AssetBundle.LoadFromMemory(Resource1.shaders);
            MelonLogger.Msg(System.ConsoleColor.Green, "POKEMON GOTTA CATCH THEM ALL");
        }
    }
}
namespace PokemonExpansion
{
    public class PokemonUI
    {
        public class PokemonSet : ModTowerSet
        {
            public override string DisplayName => "Pokemon";
            public override string Container => "PokemonContainer";
            public override string Button => "PokemonButton";
            public override string ContainerLarge => "PokemonContainer";
            public override string Portrait => "PokemonPortrait";

            
        }
    }
}