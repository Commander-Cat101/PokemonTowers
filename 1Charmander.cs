using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Charmander.Displays.Projectiles;
using Assets.Scripts.Unity;
using System.Linq;
using Assets.Scripts.Models.Towers.Behaviors.Abilities;
using PokemonExpansion;
using static PokemonExpansion.PokemonUI;
using System.Collections.Generic;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Simulation.Towers;
using Harmony;
using Assets.Scripts.Unity.Audio;
using UnityEngine;
using Assets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;

namespace Charmander.Displays.Projectiles
{
        public class FireballDisplay : ModDisplay
        {
            public override string BaseDisplay => Generic2dDisplay;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                Set2DTexture(node, Name);
            }
        }
        public class BlueFireballDisplay : ModDisplay
        {
            public override string BaseDisplay => Generic2dDisplay;

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                Set2DTexture(node, Name);
            }
        }
    }
    namespace Charmander
    {
    public class Charmander : ModTower<PokemonSet>
        {
            public override string BaseTower => TowerType.DartMonkey;
            public override int Cost => 400;


            public override int TopPathUpgrades => 0;
            public override int MiddlePathUpgrades => 5;
            public override int BottomPathUpgrades => 0;


            public override bool Use2DModel => true;
            public override string Portrait => "Charmander-Icon";
            public override string Description => "Charmander = FIRE!";
            public override void ModifyBaseTowerModel(TowerModel towerModel)
            {
                var attackModel = towerModel.GetAttackModel();


                var projectile = attackModel.weapons[0].projectile;
                projectile.ApplyDisplay<FireballDisplay>();
                attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Purple;


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
                    return "CharmanderBaseDisplay";
                }
                if (tiers[1] < 4)
                {
                    return "CharmeleonDisplay";
                }
                if (tiers[1] == 4)
                {
                    return "CharizardDisplay";
                }
                if (tiers[1] == 5)
                {
                    return "MegaCharizardXDisplay";
                }
                if (tiers[2] != 0)
                {
                    return "MegaCharizardYDisplay";
                }
                return "CharmanderBaseDisplay";
            }

        }
    }
    namespace Charmander.Upgrades.MiddlePath
    {
        public class Flamethrower : ModUpgrade<Charmander>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
            public override string Portrait => "Charmander-Icon";
            public override string Icon => "Charmander-Icon";
            public override int Path => MIDDLE;
            public override int Tier => 1;
            public override int Cost => 750;

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

            public override string Description => "The target is scorched with an intense blast of fire.";

            public override void ApplyUpgrade(TowerModel tower)
            {
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate -= .3f;

                }
                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.pierce += 1;
                    projectile.GetDamageModel().damage += 1;
                }

            }

        }
        public class Charmeleon : ModUpgrade<Charmander>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
            public override string Portrait => "Charmeleon-Icon";
            public override int Path => MIDDLE;
            public override int Tier => 2;
            public override int Cost => 950;

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

            public override string Description => "When it swings its burning tail, it elevates the temperature to unbearably high levels.";

            public override void ApplyUpgrade(TowerModel tower)
            {
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate -= .1f;

                }
                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.pierce += 1;
                    projectile.GetDamageModel().damage += 3;
                }

            }
        }
        public class DragonRage : ModUpgrade<Charmander>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
            public override string Portrait => "Charmeleon-Icon";
            public override string Icon => "Charmeleon-Icon";
            public override int Path => MIDDLE;
            public override int Tier => 3;
            public override int Cost => 1700;

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

            public override string Description => "This attack hits the target with a shock wave of pure rage.";

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var Rage = Game.instance.model.GetTowerFromId("Sauda").GetAttackModel().Duplicate();
                Rage.range = towerModel.range / 2;
                Rage.name = "Rage_Weapon";
                towerModel.AddBehavior(Rage);
            }
        }
        public class Charizard : ModUpgrade<Charmander>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
            public override string Portrait => "Charizard-Icon";
            public override int Path => MIDDLE;
            public override int Tier => 4;
            public override int Cost => 3500;

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

            public override string Description => "Breathing intense, hot flames, it can melt almost anything. Its breath inflicts terrible pain on enemies.";

            public override void ApplyUpgrade(TowerModel tower)
            {
            foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.pierce += 3;
                    projectile.GetDamageModel().damage *= 3;
                }
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate -= .3f;

                }

            }
        }
        public class MegaCharizardX : ModUpgrade<Charmander>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
            public override string Portrait => "MegaCharizardX-Icon";
            public override int Path => MIDDLE;
            public override int Tier => 5;
            public override int Cost => 113500;

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

            public override string Description => "Charizard-X flies around the sky in search of powerful opponents. It breathes fire of such great heat that it melts anything.";

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var attackModel = towerModel.GetAttackModel();
                var projectile = attackModel.weapons[0].projectile;
                attackModel.weapons[0].projectile.pierce *= 10;
                attackModel.weapons[0].projectile.GetDamageModel().damage *= 8;
                attackModel.weapons[0].Rate = .03f;
                towerModel.range *= 1.5f;
                attackModel.range += 1.5f;
                var path = towerModel.IsUpgradeUnlocked(1, 4);
                var middletier = towerModel.GetUpgradeLevel(1);
                towerModel.AddBehavior(Game.instance.model.GetTowerFromId("WizardMonkey-050").GetBehavior<AbilityModel>().Duplicate());
                towerModel.GetBehavior<AbilityModel>().icon = GetSpriteReference(mod, "MegaCharizardX-Icon");


            }
        }
    }
/*namespace Charmander.Upgrades.BottomPath
{
    public class MegaCharizardY : ModUpgrade<Charmander>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "MegaCharizardY-Icon";
        public override int Path => BOTTOM;
        public override int Tier => 1;
        public override int Cost => 113500;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Ability: Charizard-Y flies around the sky in search of powerful opponents. It breathes fire of such great heat that it melts anything.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("WizardMonkey-050").GetBehavior<AbilityModel>().Duplicate());
            towerModel.GetBehavior<AbilityModel>().icon = GetSpriteReference(mod, "MegaCharizardX-Icon");
            attackModel.weapons[0].projectile.pierce *= 3;
            attackModel.weapons[0].projectile.GetDamageModel().damage *= 4;
            attackModel.weapons[0].Rate -= .1f;
            var path = towerModel.IsUpgradeUnlocked(1, 4);
            var middletier = towerModel.GetUpgradeLevel(1);
            if (path == false)
            {
                towerModel.SetTiers(0, middletier, 0);

            }
        }
    }
}
namespace Charmander.Upgrades.TopPath
{
    public class MegaCharizardX : ModUpgrade<Charmander>
    {
        // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
        // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";
        public override string Portrait => "MegaCharizardX-Icon";
        public override int Path => TOP;
        public override int Tier => 1;
        public override int Cost => 113500;

        // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

        public override string Description => "Charizard-X flies around the sky in search of powerful opponents. It breathes fire of such great heat that it melts anything.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            attackModel.weapons[0].projectile.pierce *= 10;
            attackModel.weapons[0].projectile.GetDamageModel().damage *= 8;
            attackModel.weapons[0].Rate = .03f;
            towerModel.range *= 1.5f;
            attackModel.range += 1.5f;
            var path = towerModel.IsUpgradeUnlocked(1, 4);
            var middletier = towerModel.GetUpgradeLevel(1);
            if (path == false)
            {
                towerModel.SetTiers(0, middletier, 0);
            }


        }
    }
}*/


