using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using System.Linq;
using static PokemonExpansion.PokemonUI;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using Assets.Scripts.Utils;
using Bulbasaur.Displays.Projectiles;
using BTD_Mod_Helper.Api.Enums;
using Pichu;
using Assets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Assets.Scripts.Simulation.Bloons.Behaviors;
using Assets.Scripts.Unity.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api;
using Assets.Scripts.Unity.Towers.Behaviors;

namespace Pidgey
{
    public class PidgeyBaseDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "PidgeyBaseDisplay");
        }
    }
    public class PidgeottoDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "PidgeottoDisplay");
        }
    }
    public class PidgeotDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "PidgeotDisplay");
        }
    }
    public class MegaPidgeotDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "MegaPidgeotDisplay");
        }
    }
    public class ScratchDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "ScratchDisplay");
        }
    }
    public class Pidgey : ModTower<PokemonSet>
    {
        public override string BaseTower => "HeliPilot-210";
        public override int Cost => 750;
        public override string Description => "A common sight in forests and woods. It flaps its wings at ground level to kick up blinding sand.";
        public override string Portrait => "Pidgey-Icon";
        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 0;

        // 2D shit
        public override bool Use2DModel => true;
        public override float PixelsPerUnit => 5f;
        public override string Get2DTexture(int[] tiers)
        {

            return "HeliPadDisplay";
        }

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.GetBehavior<AirUnitModel>().display = ModContent.CreatePrefabReference<PidgeyBaseDisplay>();

            var heliMovementModel = towerModel.GetBehavior<AirUnitModel>().behaviors.First().Cast<HeliMovementModel>();
            heliMovementModel.maxSpeed = 80;
            heliMovementModel.rotationSpeed = 0.16f;
            heliMovementModel.movementForceStart = 600;
            heliMovementModel.movementForceEnd = 300;
            heliMovementModel.movementForceEndSquared = 90000;
            heliMovementModel.brakeForce = 400;
            heliMovementModel.otherHeliRepulsonForce = 15;

            towerModel.range = 22f;
            towerModel.radius = 6;
            towerModel.radiusSquared = 36.0f;

            towerModel.GetBehavior<RectangleFootprintModel>().xWidth = 5;
            towerModel.GetBehavior<RectangleFootprintModel>().yWidth = 5;

            var footPrint = towerModel.GetBehavior<RectangleFootprintModel>();
            towerModel.footprint = footPrint;

            var attackModel = towerModel.GetAttackModel();

            attackModel.RemoveWeapon(attackModel.weapons[1]); // remove quad darts
            attackModel.weapons[0].projectile.ApplyDisplay<ScratchDisplay>();
            attackModel.weapons[0].emission = Game.instance.model.GetTowerFromId("HeliPilot-400").GetAttackModel().weapons[2].emission;
        }
    }
}
namespace Pidgey.upgrades.MiddlePath
{
    public class QuickAttack : ModUpgrade<Pidgey>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Pidgey-Icon";
        public override string Icon => "Pidgey-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 600;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "An extremely fast attack that always strikes first.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var startOfRoundRateBuffModel = Game.instance.model.GetTower(TowerType.SpikeFactory, 0, 0, 2).GetBehavior<StartOfRoundRateBuffModel>().Duplicate();
            startOfRoundRateBuffModel.modifier = .2f;
            startOfRoundRateBuffModel.duration = 10;
            towerModel.AddBehavior(startOfRoundRateBuffModel);
        }
    }
    public class Pidgeotto : ModUpgrade<Pidgey>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Pidgeotto-Icon";
        public override string Icon => "Pidgeotto-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 850;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Very protective of its sprawling territory, this Pokémon will fiercely peck at any intruder.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<AirUnitModel>().display = ModContent.CreatePrefabReference<PidgeottoDisplay>();
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile.GetDamageModel().damage += 1;
            projectile.pierce += 1;
            attackModel.weapons[0].Rate *= .7f;
        }

    }
    public class Agility : ModUpgrade<Pidgey>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Pidgeotto-Icon";
        public override string Icon => "Pidgeotto-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 700;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Very protective of its sprawling territory, this Pokémon will fiercely peck at any intruder.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile.GetDamageModel().damage += 1;
            projectile.pierce += 1;
            var heliMovementModel = towerModel.GetBehavior<AirUnitModel>().behaviors.First().Cast<HeliMovementModel>();
            heliMovementModel.maxSpeed = 100;
            heliMovementModel.brakeForce = 500;
            attackModel.weapons[0].Rate *= .7f;
        }
    }
    public class Pidgeot : ModUpgrade<Pidgey>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Pidgeot-Icon";
        public override string Icon => "Pidgeot-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 2500;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "This Pokémon flies at Mach 2 speed, seeking prey. Its large talons are feared as wicked weapons.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<AirUnitModel>().display = ModContent.CreatePrefabReference<PidgeotDisplay>();
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile.GetDamageModel().damage *= 2;
            var heliMovementModel = towerModel.GetBehavior<AirUnitModel>().behaviors.First().Cast<HeliMovementModel>();
            heliMovementModel.maxSpeed = 120;
            heliMovementModel.brakeForce = 800;
            var WindAttack = Game.instance.model.GetTowerFromId("HeliPilot-030").GetAttackModel(1).Duplicate();
            towerModel.AddBehavior(WindAttack);
        }
    }
    public class MegaPidgeot : ModUpgrade<Pidgey>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "MegaPidgeot-Icon";
        public override string Icon => "MegaPidgeot-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 43200;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "With its muscular strength now greatly increased, it can fly continuously for two weeks without resting.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<AirUnitModel>().display = ModContent.CreatePrefabReference<MegaPidgeotDisplay>();
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            var ShoveModel = Game.instance.model.GetTowerFromId("HeliPilot-003").GetBehavior<MoabShoveZoneModel>().Duplicate();
            ShoveModel.zomgPushSpeedScaleCap = -0.1f;
            ShoveModel.bfbPushSpeedScaleCap = -0.5f;
            ShoveModel.moabPushSpeedScaleCap = -1.5f;
            towerModel.AddBehavior(ShoveModel);
            projectile.GetDamageModel().damage *= 3;
        }
    }
}