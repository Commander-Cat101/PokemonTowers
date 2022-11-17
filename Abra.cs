using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using System.Linq;
using static PokemonExpansion.PokemonUI;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Charmander.Displays.Projectiles;
using Assets.Scripts.Unity.Towers.Emissions;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using BTD_Mod_Helper.Api.Enums;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using System.Xml.Linq;
using Assets.Scripts.Models.Towers.Behaviors;

public class ShockWaveDisplay : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, "ShockWaveDisplay");
    }
}
namespace Abra
{
    public class Abra : ModTower<PokemonSet>
    {
        public override string BaseTower => TowerType.DartMonkey;
        public override int Cost => 700;


        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 0;


        public override bool Use2DModel => true;
        public override string Portrait => "Abra-Icon";
        public override string Description => "Abra = PSYCHIC";
        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.RemoveBehavior<AttackModel>();
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("SniperMonkey-100").GetAttackModel().Duplicate());
            var projectile = attackModel.weapons[0].projectile;
            attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = (BloonProperties)8;
            towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));

        }
        /*public override IEnumerable<int[]> TowerTiers()
        {
            yield return new[] { 0, 0, 0 };
            for (var i = 1; i < 4; i++)
            {
                yield return new[] { 0, i, 0 };
                yield return new[] { 1, 4, 0 };
                yield return new[] { 0, 4, 1 };
            }
        }*/

        public override string Get2DTexture(int[] tiers)
        {
            if (tiers[1] < 2)
            {
                return "AbraBaseDisplay";
            }
            if (tiers[1] < 4)
            {
                return "KadabraDisplay";
            }
            if (tiers[1] == 4)
            {
                return "AlakazamDisplay";
            }
            if (tiers[1] == 5)
            {
                return "MegaAlakazamDisplay";
            }
            return "CharmanderBaseDisplay";
        }

    }
}
namespace Abra.Upgrades.Middlepath
{
    public class Physhock : ModUpgrade<Abra>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Abra-Icon";
        public override string Icon => "Abra-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 750;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "The user materializes an odd psychic wave to attack the target.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            var WindModel = Game.instance.model.GetTowerFromId("NinjaMonkey-010").GetWeapon().projectile.GetBehavior<WindModel>().Duplicate();
            WindModel.chance = .50f;
            WindModel.distanceMin = 5f;
            WindModel.distanceMax = 10f;
            WindModel.speedMultiplier = 1f;
            attackModel.weapons[0].projectile.AddBehavior(WindModel);
        }

    }
    public class Kadabra : ModUpgrade<Abra>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Kadabra-Icon";
        public override string Icon => "Kadabra-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 800;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "It possesses strong spiritual power. The more danger it faces, the stronger its psychic power.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            attackModel.weapons[0].Rate *= .5f;
        }

    }
    public class Psychic : ModUpgrade<Abra>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Kadabra-Icon";
        public override string Icon => "Kadabra-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 1400;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "The target is hit by a strong telekinetic force.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            var towersniper = Game.instance.model.GetTowerFromId("TackShooter-300").GetAttackModel().Duplicate();
            towersniper.range = towerModel.range / 2;
            towersniper.name = "Pulse_Tower";
            towersniper.weapons[0].Rate = 2f;
            towersniper.weapons[0].projectile.ApplyDisplay<ShockWaveDisplay>();
            var WindModel = Game.instance.model.GetTowerFromId("NinjaMonkey-010").GetWeapon().projectile.GetBehavior<WindModel>().Duplicate();
            WindModel.chance = .50f;
            WindModel.distanceMin = 5f;
            WindModel.distanceMax = 25f;
            WindModel.speedMultiplier = 1f;
            towersniper.weapons[0].projectile.AddBehavior(WindModel);
            towerModel.AddBehavior(towersniper);
        }

    }
    public class Alakazam : ModUpgrade<Abra>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Alakazam-Icon";
        public override string Icon => "Alakazam-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 5200;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Its brain can outperform a super-computer. Its intelligence quotient is said to be 5,000.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            attackModel.weapons[0].Rate *= .4f;
            projectile.GetDamageModel().damage *= 8;
            projectile.GetBehavior<WindModel>().affectMoab = true;
        }

    }
    public class MegaAlakazam : ModUpgrade<Abra>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "MegaAlakazam-Icon";
        public override string Icon => "MegaAlakazam-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 160000;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "It sends out psychic power from the red organ on its forehead to foresee its opponents’ every move.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            var windmodel = projectile.GetBehavior<WindModel>();
            windmodel.distanceMin = 100f;
            windmodel.distanceMax = 250f;
            attackModel.weapons[0].Rate *= .1f;
            foreach (var attacks in towerModel.GetAttackModels())
            {
                if (attacks.name.Contains("Pulse"))
                {
                    attacks.weapons[0].projectile.GetDamageModel().damage *= 25;
                    attacks.weapons[0].Rate *= .1f;
                }

            }
        }

    }
}