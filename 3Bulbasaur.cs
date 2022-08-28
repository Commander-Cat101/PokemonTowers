using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using System.Linq;
using static PokemonExpansion.PokemonUI;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Filters;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using BTD_Mod_Helper.Api.Display;
using Assets.Scripts.Unity.Display;
using Bulbasaur.Displays.Projectiles;
using Assets.Scripts.Simulation.Towers.Weapons.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;

namespace Bulbasaur.Displays.Projectiles
{
    public class LeafDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "LeafDisplay");
        }
    }
}
namespace Bulbasaur
{
    public class Bulbasaur : ModTower<PokemonSet>
    {
        public override string BaseTower => "Druid-030";
        public override int Cost => 3190;


        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 0;


        public override bool Use2DModel => true;
        public override string Portrait => "Bulbasaur-Icon";
        public override string Description => "Bulbasaur = GRASS!";
        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            //tower.RemoveBehavior(tower.GetBehaviors<AttackModel>().First(a => a.name.Equals("AttackModel_Attack_")));
            attackModel.weapons[0] = Game.instance.model.GetTowerFromId("DartMonkey-200").GetAttackModel().weapons[0].Duplicate();
            attackModel.weapons[0].Rate = 2f;
            attackModel.weapons[0].projectile.ApplyDisplay<LeafDisplay>();
            attackModel.weapons[0].projectile.GetDamageModel().damage = 2f;

        }
        public override string Get2DTexture(int[] tiers)
        {
            if (tiers[1] < 2)
            {
                return "BulbasaurBaseDisplay";
            }
            if (tiers[1] < 4)
            {
                return "IvysaurDisplay";
            }
            if (tiers[1] == 4)
            {
                return "VenusaurDisplay";
            }
            if (tiers[1] == 5)
            {
                return "VenusaurDisplay";
            }
            return "BulbasaurBaseDisplay";
        }
    }
}
namespace Bulbasaur.Upgrades.MiddlePath
{
    public class RazorLeaf : ModUpgrade<Bulbasaur>
    {
        
        public override string Portrait => "Bulbasaur-Icon";
        public override string Icon => "Bulbasaur-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 950;


        public override string Description => "The user releases sharp-edged leaves to slash the opposing opponents.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].Rate = .8f;
            attackModel.weapons[0].projectile.ApplyDisplay<LeafDisplay>();
            
        }
    }
    public class Ivysaur : ModUpgrade<Bulbasaur>
    {
        public override string Portrait => "Ivysaur-Icon";
        public override string Icon => "Ivysaur-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 1300;


        public override string Description => "Exposure to sunlight adds to its strength. Sunlight also makes the bud on its back grow larger.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].Rate = .6f;
            attackModel.weapons[0].projectile.ApplyDisplay<LeafDisplay>();
            var startOfRoundRateBuffModel = Game.instance.model.GetTower(TowerType.SpikeFactory, 0, 0, 2).GetBehavior<StartOfRoundRateBuffModel>().Duplicate();
            startOfRoundRateBuffModel.modifier = .5f;
            startOfRoundRateBuffModel.duration = 5;
            towerModel.AddBehavior(startOfRoundRateBuffModel);

        }
    }
    public class Growth : ModUpgrade<Bulbasaur>
    {
        public override string Portrait => "Ivysaur-Icon";
        public override string Icon => "Ivysaur-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 1550;


        public override string Description => "The user's body grows all at once, raising the Attack and Speed stats.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].Rate = .5f;
            attackModel.weapons[0].projectile.ApplyDisplay<LeafDisplay>();
            towerModel.GetBehavior<StartOfRoundRateBuffModel>().modifier = .3f;
            towerModel.GetBehavior<StartOfRoundRateBuffModel>().Duration = 7f;
            attackModel.weapons[0].projectile.GetDamageModel().damage *= 1.5f;
        }
    }
    public class Venusaur : ModUpgrade<Bulbasaur>
    {
        public override string Portrait => "Venusaur-Icon";
        public override string Icon => "Venusaur-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 4550;


        public override string Description => "The flower on its back catches the sun's rays. The sunlight is then absorbed and used for energy.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].Rate = .3f;
            attackModel.weapons[0].projectile.ApplyDisplay<LeafDisplay>();
            towerModel.GetBehavior<StartOfRoundRateBuffModel>().modifier = .2f;
            towerModel.GetBehavior<StartOfRoundRateBuffModel>().Duration = 15f;
            attackModel.weapons[0].projectile.GetDamageModel().damage *= 2f;
        }
    }
    public class MegaVenusaur : ModUpgrade<Bulbasaur>
    {
        public override string Portrait => "Venusaur-Icon";
        public override string Icon => "Venusaur-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 156000;


        public override string Description => "Ability: The target is struck with slender, whiplike vines to inflict damage against any moab class bloon INCLUDING B.A.Ds.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].Rate = .05f;
            attackModel.weapons[0].projectile.ApplyDisplay<LeafDisplay>();
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("MonkeyBuccaneer-Paragon").GetBehavior<AbilityModel>().Duplicate());
            towerModel.GetBehavior<AbilityModel>().icon = GetSpriteReference(mod, "Venusaur-Icon");
            towerModel.GetBehavior<AbilityModel>().cooldown = 60f;
            towerModel.GetBehavior<StartOfRoundRateBuffModel>().modifier = .1f;
            towerModel.GetBehavior<StartOfRoundRateBuffModel>().Duration = 30f;
            attackModel.weapons[0].projectile.GetDamageModel().damage *= 2f;

        }
    }
}