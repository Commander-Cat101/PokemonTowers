using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using System.Linq;
using static PokemonExpansion.PokemonUI;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Filters;
using BTD_Mod_Helper.Api.Display;
using Assets.Scripts.Unity.Display;
using Squirtle.Displays.Projectiles;
using Assets.Scripts.Models.Map;

namespace Squirtle.Displays.Projectiles
{
    public class WaterSprayDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    
}
namespace Squirtle
{
    public class Squirtle : ModTower<PokemonSet>
    {
        public override string BaseTower => TowerType.DartMonkey;
        public override int Cost => 620;


        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 0;


        public override bool Use2DModel => true;

        public override string Portrait => "Squirtle-Icon";
        public override string Description => "Squirtle = WATER!";
        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
            towerModel.range += 20;
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            attackModel.range += 20;
            attackModel.weapons[0].Rate = 1f;
            attackModel.weapons[0].projectile.GetDamageModel().damage = 3;
            projectile.ApplyDisplay<WaterSprayDisplay>();
            attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Lead;
            UnhollowerBaseLib.Il2CppStructArray<AreaType> areaTypes = new UnhollowerBaseLib.Il2CppStructArray<AreaType>(2);
            towerModel.areaTypes = areaTypes;
            towerModel.areaTypes[0] = AreaType.land;
            towerModel.areaTypes[1] = AreaType.water;
        }
        public override string Get2DTexture(int[] tiers)
        {
            if (tiers[1] < 2)
            {
                return "SquirtleBaseDisplay";
            }
            if (tiers[1] < 4)
            {
                return "WartortleDisplay";
            }
            if (tiers[1] == 4)
            {
                return "BlastoiseDisplay";
            }
            if (tiers[1] == 5)
            {
                return "BlastoiseDisplay";
            }
            return "SquirtleBaseDisplay";
        }
    }
}
namespace Squirtle.Upgrades.MiddlePath
{
    public class WaterPulse : ModUpgrade<Squirtle>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Squirtle-Icon";
        public override string Icon => "Squirtle-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 550;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "The user attacks the target with a pulsing blast of water. This may also confuse the target.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();

            var WindModel = Game.instance.model.GetTowerFromId("NinjaMonkey-010").GetWeapon().projectile.GetBehavior<WindModel>().Duplicate();
            WindModel.chance = 0.05f;
            WindModel.distanceMin = 5f;
            WindModel.distanceMax = 50f;
            WindModel.speedMultiplier = 2.5f;
            attackModel.weapons[0].projectile.AddBehavior(WindModel);

            attackModel.weapons[0].projectile.pierce += 1;
            attackModel.weapons[0].projectile.GetDamageModel().damage += 1;
            attackModel.weapons[0].Rate = .9f;
        }
    }
    public class Wartortle : ModUpgrade<Squirtle>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Wartortle-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 800;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Often hides in water to stalk unwary prey. For swimming fast, it moves its ears to maintain balance.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();

            attackModel.weapons[0].projectile.GetBehavior<WindModel>().chance = 0.1f;
            attackModel.weapons[0].Rate = 0.7f;

        }
    }
    public class RapidSpin : ModUpgrade<Squirtle>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Wartortle-Icon";
        public override string Icon => "Wartortle-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 1350;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "A spin attack that can also eliminate such moves as Bind, Wrap, Leech Seed, and Spikes.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();

            attackModel.weapons[0].projectile.GetBehavior<WindModel>().chance = 0.15f;
            attackModel.weapons[0].Rate = 0.5f;

        }
    }
    public class Blastoise : ModUpgrade<Squirtle>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Blastoise-Icon";
        public override string Icon => "Blastoise-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 3150;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "A brutal Pokémon with pressurized water jets on its shell. They are used for high speed tackles.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();

            attackModel.weapons[0].projectile.GetBehavior<WindModel>().chance = 0.2f;
            attackModel.weapons[0].Rate = 0.3f;
            attackModel.weapons[0].projectile.GetDamageModel().damage += 2;
            attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
        }
    }
    public class MegaBlastoise : ModUpgrade<Squirtle>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "MegaBlastoise-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 63150;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "The foe is blasted by a huge volume of water launched under great pressure.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();

            attackModel.weapons[0].projectile.GetBehavior<WindModel>().chance = 0.02f;
            attackModel.weapons[0].projectile.GetBehavior<WindModel>().affectMoab = true;
            attackModel.weapons[0].Rate = 0.08f;
            attackModel.weapons[0].projectile.GetDamageModel().damage *= 6;
        }
    }
}