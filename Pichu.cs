using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using static PokemonExpansion.PokemonUI;

namespace Pichu
{
    public class LightningDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "LightningDisplay");
        }
    }
    public class Pichu : ModTower<PokemonSet>
    {
        public override string BaseTower => TowerType.DartMonkey;
        public override int Cost => 700;


        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 0;


        public override bool Use2DModel => true;
        public override string Portrait => "Pichu-Icon";
        public override string Description => "Pichu = ELECTRIC!";
        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile.ApplyDisplay<LightningDisplay>();
            projectile.GetDamageModel().damage = 2;
            attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = (BloonProperties)13;


        }

        public override string Get2DTexture(int[] tiers)
        {
            if (tiers[1] < 2)
            {
                return "PichuBaseDisplay";
            }
            if (tiers[1] < 4)
            {
                return "PikachuDisplay";
            }
            if (tiers[1] == 4)
            {
                return "RaichuDisplay";
            }
            if (tiers[1] == 5)
            {
                return "RaichuDisplay";
            }
            return "PichuBaseDisplay";
        }

    }
}
namespace Pichu.Upgrades.Middlepath
{
    public class ThunderShock : ModUpgrade<Pichu>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Pichu-Icon";
        public override string Icon => "Pichu-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 350;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Shocks the bloons with 100 volts of electricity.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile = Game.instance.model.GetTowerFromId("DartlingGunner-200").GetAttackModel().weapons[0].projectile.Duplicate();
            projectile.ApplyDisplay<LightningDisplay>();
            projectile.GetDamageModel().damage = 2;
            projectile.pierce = 2;
        }

    }
    public class Pikachu : ModUpgrade<Pichu>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Pikachu-Icon";
        public override string Icon => "Pikachu-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 600;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "When several of these Pokémon gather, their electricity could build and cause lightning storms.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            towerModel.range *= 1.5f;
            attackModel.range += 1.5f;
            attackModel.weapons[0].Rate *= .7f;
            projectile.ApplyDisplay<LightningDisplay>();
        }
    }
    public class TailWhip : ModUpgrade<Pichu>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Pikachu-Icon";
        public override string Icon => "Pikachu-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 950;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Hits the opponent with users tail, inflicting damage.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            var Rage = Game.instance.model.GetTowerFromId("Sauda 7").GetAttackModel().Duplicate();
            Rage.range = towerModel.range / 2;
            Rage.name = "Rage_Weapon";
            towerModel.AddBehavior(Rage);
            projectile.ApplyDisplay<LightningDisplay>();
        }
    }
    public class Raichu : ModUpgrade<Pichu>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Raichu-Icon";
        public override string Icon => "Raichu-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 3700;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Its long tail serves as a ground to protect itself from its own high voltage power.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile.GetDamageModel().damage *= 2;
            projectile.pierce *= 4;
            attackModel.weapons[0].Rate *= .6f;
            projectile.AddBehavior(new TrackTargetModel("Testname", 9999999, true, false, 144, false, 300, false, true));
            projectile.ApplyDisplay<LightningDisplay>();
        }
    }
    public class LightScreen : ModUpgrade<Pichu>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "Raichu-Icon";
        public override string Icon => "Raichu-Icon";
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 48000;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "A wondrous wall of light is put up to reduce damage from special attacks for five turns.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile.GetDamageModel().damage *= 10;
            projectile.pierce *= 5;
            attackModel.weapons[0].Rate *= .3f;
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("SuperMonkey-040").GetBehavior<AbilityModel>().Duplicate());
            towerModel.GetBehavior<AbilityModel>().icon = GetSpriteReference(mod, "Raichu-Icon");
        }
    }
}