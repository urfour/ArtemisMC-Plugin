using Artemis.Core.Modules;
using Avalonia.Controls.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Artemis.Plugins.Games.Minecraft.DataModels
{
    public class GameInfos
    {
        public GameInfos()
        {
            Player = new PlayerInfos();
            World = new WorldInfos();
        }
        [DataModelProperty(Name = "Player")]
        public PlayerInfos Player { get; set; }
        [DataModelProperty(Name = "World")]
        public WorldInfos World { get; set; }
        public class PlayerInfos
        {
            public class PotionEffects
            {
                public PotionEffects() { }
                public bool BadOmen { get; set; }
                public bool Luck { get; set; }
                public bool Strength { get; set; }
                public bool Regeneration { get; set; }
                public bool MiningFatigue { get; set; }
                public bool Saturation { get; set; }
                public bool Weakness { get; set; }
                public bool FireResistance { get; set; }
                public bool HealthBoost { get; set; }
                public bool NightVision { get; set; }
                public bool BadLuck { get; set; }
                public bool InstantDamage { get; set; }
                public bool Levitation { get; set; }
                public bool VillageHero { get; set; }
                public bool DolphinsGrace { get; set; }
                public bool Poison { get; set; }
                public bool Blindness { get; set; }
                public bool Absorption { get; set; }
                public bool SlowFalling { get; set; }
                public bool Haste { get; set; }
                public bool ConduitPower { get; set; }
                public bool JumpBoost { get; set; }
                public bool Resistance { get; set; }
                public bool WaterBreathing { get; set; }
                public bool InstantHealth { get; set; }
                public bool Invisibility { get; set; }
                public bool Hunger { get; set; }
                public bool MoveSpeed { get; set; }
                public bool Glowing { get; set; }
                public bool MoveSlowdown { get; set; }
                public bool Confusion { get; set; }
                public bool Wither { get; set; }
            }
            public class Armor
            {
                public Armor() { }
                private string Helmet { get; set; }
                private string Chestplate { get; set; }
                private string Leggings { get; set; }
                private string Boots { get; set; }
            }
            public PlayerInfos()
            {
                PlayerEffects = new PotionEffects();
                Armor = new Armor();
            }
            public bool InGame { get; set; }
            public float Health { get; set; }
            public float MaxHealth { get; set; }
            public float Absorption { get; set; }
            public bool IsDead { get; set; }
            public int ArmorPoints { get; set; }
            public int ExperienceLevel { get; set; }
            public float Experience { get; set; }
            public int FoodLevel { get; set; }
            public float SaturationLevel { get; set; }
            public bool IsSneaking { get; set; }
            public bool IsRidingHorse { get; set; }
            public bool IsBurning { get; set; }
            public bool IsInWater { get; set; }
            public PotionEffects PlayerEffects { get; set; }
            public Armor Armor { get; set; }
            public string LeftHandItem { get; set; }
            public string RightHandItem { get; set; }
        }

        public class WorldInfos
        {
            public WorldInfos() 
            {
                Dimension = "";
            }
            public int WorldTime { get; set; }
            public bool IsDayTime { get; set; }
            public bool IsRaining { get; set; }
            public float RainStrength { get; set; }
            public string Dimension { get; set; }
        }
    }
}
