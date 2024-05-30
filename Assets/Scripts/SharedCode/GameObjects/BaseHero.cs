using System;
using System.Collections.Generic;

namespace Com.Tempest.Whale.GameObjects {

    public enum HeroEnum {
        // Water
        LEPAQUA = 1, ARDOWSE = 2, MARIVULP = 3, SPIRIFLOW = 4, HYDROKAHT = 5,
        // Grass
        LEPHYTA = 6, ARBERRY = 7, FLORAVULP = 8, SPIRIGROW = 9, BOTANIKAHT = 10,
        // Fire
        LEPYRA = 11, ARBURN = 12, SCOROVULP = 13, SPIRIGNITE = 14, INFERNIKAHT = 15,
        // Ice
        ARCTIBOAR = 16, ICECAP = 17, GLACITAUR = 18, FREEZER = 19, CRYOKAHT = 20,
        // Earth
        SEISMIBOAR = 21, MUDCAP = 22, ROCKOTAUR = 23, PULVERIZER = 24, TERRIKAHT = 25,
        // Electric
        ELECTIBOAR = 26, BOLTCAP = 27, ZAPATAUR = 28, GENERATOR = 29, ZEPHYKAHT = 30
    }

    public class BaseHero {

        public HeroEnum Hero { get; }
        public string HeroName { get; }
        public string HeroIconPath { get; }
        public string AnimatorPath { get; }
        public string PrefabPath { get; }

        public RoleEnum Role { get; }
        public FactionEnum Faction { get; }
        public int Rarity { get; }

        public double BaseHealth { get; }
        public double BaseStrength { get; }
        public double BasePower { get; }
        public double BaseToughness { get; }
        public double BaseResistance { get; }
        public double BaseSpeed { get; }
        public double BaseCritChance { get; }
        public double BaseDeflectionChance { get; }

        public AbilityEnum PassiveAbility { get; }
        public AttackEnum SimpleBasic { get; }
        public AttackEnum IntermediateBasic { get; }
        public AttackEnum SimpleCharge { get; }
        public AttackEnum IntermediateCharge { get; }
        public AttackEnum[] TeachableBasics { get; }
        public AttackEnum[] TeachableCharges { get; }

        public BaseHero(HeroEnum hero, string heroName, string heroIconPath, string animatorPath, string prefabPath,
            RoleEnum role, FactionEnum faction, int rarity,
            double baseHealth, double baseStrength, double basePower,
            double baseToughness, double baseResistance, double baseSpeed, double baseCritChance, double baseDeflectionChance,
            AbilityEnum passiveAbility, AttackEnum simpleBasic, AttackEnum intermediateBasic, AttackEnum simpleCharge, AttackEnum intermediateCharge,
            AttackEnum[] teachableBasics, AttackEnum[] teachableCharges) {

            Hero = hero;
            HeroName = heroName;
            HeroIconPath = heroIconPath;
            AnimatorPath = animatorPath;
            PrefabPath = prefabPath;

            Role = role;
            Faction = faction;
            Rarity = rarity;

            BaseHealth = baseHealth;
            BaseStrength = baseStrength;
            BasePower = basePower;
            BaseToughness = baseToughness;
            BaseResistance = baseResistance;
            BaseSpeed = baseSpeed;
            BaseCritChance = baseCritChance;
            BaseDeflectionChance = baseDeflectionChance;

            PassiveAbility = passiveAbility;
            SimpleBasic = simpleBasic;
            IntermediateBasic = intermediateBasic;
            SimpleCharge = simpleCharge;
            IntermediateCharge = intermediateCharge;
            TeachableBasics = teachableBasics;
            TeachableCharges = teachableCharges;
        }

        public static BaseHero GetHero(HeroEnum hero) {
            switch (hero) {
                // Water heroes.
                case HeroEnum.LEPAQUA:
                    return new BaseHero(hero, "Lepaqua", "Icons/Element02_256_04", "Characters/WaterOverrideController", "Characters/Lepaqua/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.WATER, 1,
                        65, 90, 55, 65, 60, 90, 0.25, 0,
                        AbilityEnum.NONE, AttackEnum.BASIC_FIN_SLAP, AttackEnum.BASIC_TAIL_SLAP, AttackEnum.CHARGE_SPLASHING_LEAP, AttackEnum.CHARGE_DIVE,
                        new [] { AttackEnum.BASIC_BREACHING_CRASH, AttackEnum.BASIC_KICK, AttackEnum.BASIC_ICICLE_TOSS, AttackEnum.BASIC_FROZEN_SLIDE },
                        new [] { AttackEnum.CHARGE_DEPTH_CHARGE, AttackEnum.CHARGE_FEEDING_FRENZY, AttackEnum.CHARGE_SHIPWRECK, AttackEnum.CHARGE_FAVORABLE_CURRENT, AttackEnum.CHARGE_RENDING_STONE });
                case HeroEnum.ARDOWSE:
                    return new BaseHero(hero, "Ardowse", "Icons/Element02_256_04", "Characters/WaterOverrideController", "Characters/Ardowse/Large/MainPrefab",
                        RoleEnum.PROTECTION, FactionEnum.WATER, 2,
                        85, 50, 75, 80, 80, 50, 0.1, 0.2,
                        AbilityEnum.NONE, AttackEnum.BASIC_SPRAY, AttackEnum.BASIC_DELUGE, AttackEnum.CHARGE_WATER_SHOT, AttackEnum.CHARGE_ENSCALE,
                        new AttackEnum[] { AttackEnum.BASIC_TORRENT, AttackEnum.BASIC_HEALING_MIST, AttackEnum.BASIC_HEALING_RAIN },
                        new AttackEnum[] { AttackEnum.CHARGE_PRESSURE_JET, AttackEnum.CHARGE_CLEANSING_MIST, AttackEnum.CHARGE_WATER_GLOBE, AttackEnum.CREATE_BOG });
                case HeroEnum.MARIVULP:
                    return new BaseHero(hero, "Marivulp", "Icons/Element02_256_04", "Characters/WaterOverrideController", "Characters/Marivulp/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.WATER, 3,
                        75, 55, 90, 60, 70, 75, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_SPRAY, AttackEnum.BASIC_DELUGE, AttackEnum.CHARGE_WATER_SHOT, AttackEnum.CHARGE_WATER_CUTTER,
                        new AttackEnum[] { AttackEnum.BASIC_TORRENT, AttackEnum.BASIC_HEALING_MIST, AttackEnum.BASIC_HEALING_RAIN },
                        new AttackEnum[] { AttackEnum.CHARGE_WATER_GLOBE, AttackEnum.CHARGE_PRESSURE_JET, AttackEnum.CHARGE_LIQUIFY, AttackEnum.CHARGE_HEALING_DELUGE, AttackEnum.CHARGE_HEALING_TORRENT });
                case HeroEnum.SPIRIFLOW:
                    return new BaseHero(hero, "Spiriflow", "Icons/Element02_256_04", "Characters/WaterOverrideController", "Characters/Spiriflow/Large/MainPrefab",
                        RoleEnum.SUPPORT, FactionEnum.WATER, 4,
                        85, 55, 90, 60, 65, 70, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_SPRAY, AttackEnum.BASIC_DELUGE, AttackEnum.CHARGE_HEALING_WAVE, AttackEnum.CHARGE_HEALING_DELUGE,
                        new AttackEnum[] { AttackEnum.BASIC_HEALING_MIST, AttackEnum.BASIC_HEALING_RAIN },
                        new AttackEnum[] { AttackEnum.CHARGE_HEALING_TORRENT, AttackEnum.CHARGE_CLEANSING_MIST, AttackEnum.CHARGE_CLEANSING_RAIN, AttackEnum.CREATE_BOG, AttackEnum.CHARGE_FAVORABLE_CURRENT });
                case HeroEnum.HYDROKAHT:
                    return new BaseHero(hero, "Hydrokaht", "Icons/Element02_256_04", "Characters/Oracle/OracleController", "Characters/Hydrokaht/Large/MainPrefab", 
                        RoleEnum.SUPPORT, FactionEnum.WATER, 5,
                        75, 50, 95, 65, 70, 75, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_SPRAY, AttackEnum.BASIC_SPLASHING_WAVE, AttackEnum.CHARGE_WATER_SHOT, AttackEnum.CHARGE_TIDAL_WAVE,
                        new AttackEnum[] { AttackEnum.BASIC_WHIRLPOOL, AttackEnum.BASIC_HEALING_MIST, AttackEnum.BASIC_HEALING_RAIN },
                        new AttackEnum[] { AttackEnum.CHARGE_TSUNAMI, AttackEnum.CHARGE_HEALING_DELUGE, AttackEnum.CHARGE_HEALING_TORRENT, AttackEnum.CHARGE_CLEANSING_MIST, AttackEnum.CHARGE_CLEANSING_RAIN });

                // Grass heroes.
                case HeroEnum.LEPHYTA:
                    return new BaseHero(hero, "Lephyta", "Icons/Element02_256_10", "Characters/GrassOverrideController", "Characters/Lephyta/Large/MainPrefab",
                        RoleEnum.SUPPORT, FactionEnum.GRASS, 1,
                        70, 90, 55, 65, 60, 85, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_PETAL_SLAP, AttackEnum.BASIC_BRANCH_SLAP, AttackEnum.CHARGE_WEED_WHACKER, AttackEnum.CHARGE_CABER_TOSS,
                        new AttackEnum[] { AttackEnum.BASIC_RAZOR_VINE, AttackEnum.BASIC_WHIRLING_BRANCHES, AttackEnum.BASIC_LOG_ROLL, AttackEnum.BASIC_JAGGED_ROCK, AttackEnum.BASIC_BOULDER, },
                        new AttackEnum[] { AttackEnum.CHARGE_TIMBER, AttackEnum.CHARGE_NEEDLE_SPRAY, AttackEnum.CHARGE_STRANGLING_VINES, AttackEnum.CHARGE_RENDING_STONE });
                case HeroEnum.ARBERRY:
                    return new BaseHero(hero, "Arberry", "Icons/Element02_256_10", "Characters/GrassOverrideController", "Characters/Arberry/Large/MainPrefab",
                        RoleEnum.PROTECTION, FactionEnum.GRASS, 2,
                        90, 50, 70, 85, 75, 55, 0, 0.25,
                        AbilityEnum.NONE, AttackEnum.BASIC_NEEDLE_SHOT, AttackEnum.BASIC_GRAPE_SHOT, AttackEnum.CHARGE_BERRY_BLAST, AttackEnum.CHARGE_THORN_ARMOR,
                        new AttackEnum[] { AttackEnum.BASIC_COCONUT_CATAPULT, AttackEnum.BASIC_REJUVENATE, AttackEnum.BASIC_REVITALIZE },
                        new AttackEnum[] { AttackEnum.CHARGE_MOONBEAM, AttackEnum.CHARGE_INVOKE_ALLERGIES, AttackEnum.CHARGE_TOXIC_SPORES, AttackEnum.CHARGE_SALAD_TOSS, AttackEnum.CHARGE_SHADY_BRANCHES });
                case HeroEnum.FLORAVULP:
                    return new BaseHero(hero, "Floravulp", "Icons/Element02_256_10", "Characters/GrassOverrideController", "Characters/Floravulp/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.GRASS, 3,
                        90, 55, 80, 70, 65, 65, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_NEEDLE_SHOT, AttackEnum.BASIC_GRAPE_SHOT, AttackEnum.CHARGE_BERRY_BLAST, AttackEnum.CHARGE_SALAD_TOSS,
                        new AttackEnum[] { AttackEnum.BASIC_COCONUT_CATAPULT, AttackEnum.BASIC_CHAIN_LIGHTNING },
                        new AttackEnum[] { AttackEnum.CHARGE_MOONBEAM, AttackEnum.CHARGE_LEAF_WHIRLWIND, AttackEnum.CHARGE_INVOKE_ALLERGIES, AttackEnum.CHARGE_TOXIC_SPORES });
                case HeroEnum.SPIRIGROW:
                    return new BaseHero(hero, "Spirigrow", "Icons/Element02_256_10", "Characters/GrassOverrideController", "Characters/Spirigrow/Large/MainPrefab",
                        RoleEnum.SUPPORT, FactionEnum.GRASS, 4,
                        90, 55, 85, 75, 65, 65, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_REGROW, AttackEnum.BASIC_REJUVENATE, AttackEnum.CHARGE_PEACEFUL_MEADOW, AttackEnum.CHARGE_TRANQUIL_GROVE,
                        new AttackEnum[] { AttackEnum.BASIC_REVITALIZE },
                        new AttackEnum[] { AttackEnum.CHARGE_SERENE_FOREST, AttackEnum.CHARGE_CLEANSING_MIST, AttackEnum.CHARGE_CLEANSING_RAIN, AttackEnum.CHARGE_SHADY_BRANCHES, AttackEnum.CHARGE_TOXIC_SPORES });
                case HeroEnum.BOTANIKAHT:
                    return new BaseHero(hero, "Botanikaht", "Icons/Element02_256_10", "Characters/GrassOverrideController", "Characters/Botanikaht/Large/MainPrefab",
                        RoleEnum.PROTECTION, FactionEnum.GRASS, 5,
                        100, 70, 70, 75, 75, 55, 0.1, 0.2,
                        AbilityEnum.NONE, AttackEnum.BASIC_PETAL_SLAP, AttackEnum.BASIC_BRANCH_SLAP, AttackEnum.BASIC_NEEDLE_SHOT, AttackEnum.BASIC_GRAPE_SHOT,
                        new AttackEnum[] { AttackEnum.BASIC_RAZOR_VINE, AttackEnum.BASIC_GRAPE_SHOT, AttackEnum.BASIC_COCONUT_CATAPULT, AttackEnum.BASIC_REJUVENATE, AttackEnum.BASIC_REVITALIZE },
                        new AttackEnum[] { AttackEnum.CHARGE_CABER_TOSS, AttackEnum.CHARGE_TIMBER, AttackEnum.CHARGE_MOONBEAM, AttackEnum.CHARGE_TRANQUIL_GROVE, AttackEnum.CHARGE_SERENE_FOREST });

                // Fire heroes.
                case HeroEnum.LEPYRA:
                    return new BaseHero(hero, "Lepyra", "Icons/Element02_256_01", "Characters/FireOverrideController", "Characters/Lepyra/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.FIRE, 1,
                        55, 85, 80, 50, 55, 95, 0.25, 0,
                        AbilityEnum.NONE, AttackEnum.BASIC_BURNING_FIST, AttackEnum.BASIC_BLAZING_FIST, AttackEnum.CHARGE_BURNING_STONE, AttackEnum.CHARGE_BURNING_BOULDER,
                        new AttackEnum[] { AttackEnum.BASIC_JET_TACKLE, AttackEnum.BASIC_SCORCH, AttackEnum.BASIC_IMMOLATE, AttackEnum.BASIC_ZAP },
                        new AttackEnum[] { AttackEnum.CHARGE_METEOR, AttackEnum.CHARGE_INCINERATE, AttackEnum.CHARGE_INFERNO, AttackEnum.CHARGE_ERUPTION, AttackEnum.CHARGE_RENDING_STONE });
                case HeroEnum.ARBURN:
                    return new BaseHero(hero, "Arburn", "Icons/Element02_256_01", "Characters/FireOverrideController", "Characters/Arburn/Large/MainPrefab",
                        RoleEnum.PROTECTION, FactionEnum.FIRE, 2,
                        80, 50, 70, 70, 80, 70, 0.1, 0.2,
                        AbilityEnum.NONE, AttackEnum.BASIC_SINGE, AttackEnum.BASIC_SCORCH, AttackEnum.CHARGE_BLAZE, AttackEnum.CHARGE_INCINERATE,
                        new AttackEnum[] { AttackEnum.BASIC_IMMOLATE, AttackEnum.BASIC_LAVA_WAVE, AttackEnum.BASIC_SEARING_WIND },
                        new AttackEnum[] { AttackEnum.CHARGE_INFERNO, AttackEnum.CHARGE_STOKE_FLAMES });
                case HeroEnum.SCOROVULP:
                    return new BaseHero(hero, "Scorovulp", "Icons/Element02_256_01", "Characters/FireOverrideController", "Characters/Scorovulp/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.FIRE, 3,
                        75, 55, 90, 60, 65, 80, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_SINGE, AttackEnum.BASIC_SCORCH, AttackEnum.CHARGE_BLAZE, AttackEnum.CHARGE_INCINERATE,
                        new AttackEnum[] { AttackEnum.BASIC_IMMOLATE, AttackEnum.BASIC_LAVA_WAVE, AttackEnum.BASIC_SEARING_WIND, AttackEnum.BASIC_ZAP },
                        new AttackEnum[] { AttackEnum.CHARGE_INFERNO, AttackEnum.CHARGE_FIREBALL, AttackEnum.CHARGE_EXPLOSION, AttackEnum.CHARGE_STRIKE_TWICE });
                case HeroEnum.SPIRIGNITE:
                    return new BaseHero(hero, "Spirignite", "Icons/Element02_256_01", "Characters/FireOverrideController", "Characters/Spirignite/Large/MainPrefab",
                        RoleEnum.SUPPORT, FactionEnum.FIRE, 4,
                        65, 55, 85, 60, 65, 95, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_SINGE, AttackEnum.BASIC_SCORCH, AttackEnum.CHARGE_BLAZE, AttackEnum.CHARGE_INCINERATE,
                        new AttackEnum[] { AttackEnum.BASIC_IMMOLATE, AttackEnum.BASIC_KINDLE, AttackEnum.BASIC_REJUVENATE, AttackEnum.BASIC_REVITALIZE },
                        new AttackEnum[] { AttackEnum.CHARGE_INFERNO, AttackEnum.CHARGE_STOKE_FLAMES, AttackEnum.CHARGE_BURNING_HASTE, AttackEnum.CHARGE_ASH_CLOUD, AttackEnum.CHARGE_MELT_ARMOR });
                case HeroEnum.INFERNIKAHT:
                    return new BaseHero(hero, "Infernikaht", "Icons/Element02_256_01", "Characters/FireOverrideController", "Characters/Infernikaht/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.FIRE, 5,
                        55, 50, 100, 60, 65, 100, 0.25, 0,
                        AbilityEnum.NONE, AttackEnum.BASIC_FIRE_BREATH, AttackEnum.BASIC_LAVA_WAVE, AttackEnum.CHARGE_TWIN_FLAME, AttackEnum.CHARGE_FIREBALL,
                        new AttackEnum[] { AttackEnum.BASIC_SEARING_WIND },
                        new AttackEnum[] { AttackEnum.CHARGE_EXPLOSION });

                // Ice heroes.
                case HeroEnum.ARCTIBOAR:
                    return new BaseHero(hero, "Arctiboar", "Icons/Element02_256_19", "Characters/IceOverrideController", "Characters/Arctiboar/Large/MainPrefab",
                        RoleEnum.PROTECTION, FactionEnum.ICE, 1,
                        85, 70, 50, 70, 90, 60, 0.1, 0.2,
                        AbilityEnum.NONE, AttackEnum.BASIC_ICE_CUBE, AttackEnum.BASIC_ICICLE_TOSS, AttackEnum.CHARGE_FROZEN_FIST, AttackEnum.CHARGE_ICICLE_DROP,
                        new AttackEnum[] { AttackEnum.BASIC_FROZEN_SLIDE, AttackEnum.BASIC_SUB_ZERO_MACHINE_GUN },
                        new AttackEnum[] { AttackEnum.CHARGE_ICEBERG, AttackEnum.CHARGE_CRYSTALLIZE, AttackEnum.CHARGE_ENSCALE });
                case HeroEnum.ICECAP:
                    return new BaseHero(hero, "Icecap", "Icons/Element02_256_19", "Characters/IceOverrideController", "Characters/Icecap/Large/MainPrefab",
                        RoleEnum.SUPPORT, FactionEnum.ICE, 2,
                        80, 55, 90, 65, 75, 60, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_CHILLING_WIND, AttackEnum.BASIC_FREEZING_WIND, AttackEnum.CHARGE_FROSTBITE, AttackEnum.CHARGE_FREEZE_RAY,
                        new AttackEnum[] { AttackEnum.BASIC_SNOWBLAST, AttackEnum.BASIC_POWER_DRAIN },
                        new AttackEnum[] { AttackEnum.CHARGE_ABSOLUTE_ZERO, AttackEnum.CHARGE_SNOWFALL, AttackEnum.CHARGE_BLIZZARD, AttackEnum.CHARGE_FREEZE_EARTH });
                case HeroEnum.GLACITAUR:
                    return new BaseHero(hero, "Glacitaur", "Icons/Element02_256_19", "Characters/IceOverrideController", "Characters/Glacitaur/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.ICE, 3,
                        75, 95, 50, 60, 70, 75, 0.25, 0,
                        AbilityEnum.NONE, AttackEnum.BASIC_ICE_CUBE, AttackEnum.BASIC_ICICLE_TOSS, AttackEnum.CHARGE_SNOW_DRIFT, AttackEnum.CHARGE_SNOW_SLIDE,
                        new AttackEnum[] { AttackEnum.BASIC_FROZEN_SLIDE, AttackEnum.BASIC_SNOWBALL_STORM, AttackEnum.BASIC_SUB_ZERO_MACHINE_GUN, AttackEnum.CHARGE_SMASH_TO_SMITHEREENS },
                        new AttackEnum[] { AttackEnum.CHARGE_ICICLE_DROP, AttackEnum.CHARGE_ICEBERG, AttackEnum.CHARGE_AVALANCHE, AttackEnum.CHARGE_TIMBER, AttackEnum.CHARGE_EARTHQUAKE });
                case HeroEnum.FREEZER:
                    return new BaseHero(hero, "Freezer", "Icons/Element02_256_19", "Characters/IceOverrideController", "Characters/Freezer/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.ICE, 4,
                        85, 80, 50, 70, 80, 60, 0.1, 0.2,
                        AbilityEnum.NONE, AttackEnum.BASIC_ICE_CUBE, AttackEnum.BASIC_ICICLE_TOSS, AttackEnum.CHARGE_FROZEN_FIST, AttackEnum.CHARGE_ICICLE_DROP,
                        new AttackEnum[] { AttackEnum.BASIC_FROZEN_SLIDE, AttackEnum.BASIC_BREACHING_CRASH },
                        new AttackEnum[] { AttackEnum.CHARGE_ICEBERG, AttackEnum.CHARGE_DEPTH_CHARGE });
                case HeroEnum.CRYOKAHT:
                    return new BaseHero(hero, "Cryokaht", "Icons/Element02_256_19", "Characters/IceOverrideController", "Characters/Cryokaht/Large/MainPrefab",
                        RoleEnum.PROTECTION, FactionEnum.ICE, 5,
                        90, 55, 65, 70, 100, 50, 0, 0.25,
                        AbilityEnum.NONE, AttackEnum.BASIC_CHILLING_WIND, AttackEnum.BASIC_FREEZING_WIND, AttackEnum.CHARGE_FROSTBITE, AttackEnum.CHARGE_REFLECTIVE_ARMOR,
                        new AttackEnum[] { AttackEnum.BASIC_SNOWBLAST },
                        new AttackEnum[] { AttackEnum.CHARGE_WINTER_STORM, AttackEnum.CHARGE_CRYSTALLIZE, AttackEnum.CHARGE_FREEZE_EARTH });

                // Earth heroes.
                case HeroEnum.SEISMIBOAR:
                    return new BaseHero(hero, "Seismiboar", "Icons/Element02_256_22", "Characters/EarthOverrideController", "Characters/Seismiboar/Large/MainPrefab",
                        RoleEnum.PROTECTION, FactionEnum.EARTH, 1,
                        95, 65, 50, 90, 65, 60, 0, 0.25,
                        AbilityEnum.NONE, AttackEnum.BASIC_PEBBLE, AttackEnum.BASIC_JAGGED_ROCK, AttackEnum.CHARGE_STONE_FIST, AttackEnum.CHARGE_FALLING_ROCK_TRAP,
                        new AttackEnum[] { AttackEnum.BASIC_BOULDER },
                        new AttackEnum[] { AttackEnum.CHARGE_SMASH_TO_SMITHEREENS, AttackEnum.CHARGE_ROLLING_TACKLE, AttackEnum.CHARGE_GIFT_OF_EARTH, AttackEnum.CHARGE_SHATTER_GLASS });
                case HeroEnum.MUDCAP:
                    return new BaseHero(hero, "Mudcap", "Icons/Element02_256_22", "Characters/EarthOverrideController", "Characters/Mudcap/Large/MainPrefab",
                        RoleEnum.SUPPORT, FactionEnum.EARTH, 2,
                        80, 75, 75, 70, 55, 75, 0.1, 0.2,
                        AbilityEnum.NONE, AttackEnum.BASIC_PEBBLE, AttackEnum.BASIC_JAGGED_ROCK, AttackEnum.CHARGE_STONE_FIST, AttackEnum.CHARGE_FALLING_ROCK_TRAP,
                        new AttackEnum[] { AttackEnum.BASIC_BOULDER, AttackEnum.BASIC_GRAPE_SHOT, AttackEnum.BASIC_COCONUT_CATAPULT, AttackEnum.BASIC_REJUVENATE, AttackEnum.BASIC_REVITALIZE },
                        new AttackEnum[] { AttackEnum.CHARGE_RENDING_STONE, AttackEnum.CHARGE_PETAL_STORM, AttackEnum.CHARGE_LEAF_WHIRLWIND, AttackEnum.CHARGE_TRANQUIL_GROVE, AttackEnum.CHARGE_SERENE_FOREST });
                case HeroEnum.ROCKOTAUR:
                    return new BaseHero(hero, "Rockotaur", "Icons/Element02_256_22", "Characters/EarthOverrideController", "Characters/Rockotaur/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.EARTH, 3,
                        80, 95, 50, 75, 55, 70, 0.25, 0,
                        AbilityEnum.NONE, AttackEnum.BASIC_PEBBLE, AttackEnum.BASIC_JAGGED_ROCK, AttackEnum.CHARGE_STONE_FIST, AttackEnum.CHARGE_ROLLING_TACKLE,
                        new AttackEnum[] { AttackEnum.BASIC_BOULDER, AttackEnum.BASIC_DUST_STORM, AttackEnum.BASIC_ROCK_SLIDE },
                        new AttackEnum[] { AttackEnum.CHARGE_SMASH_TO_SMITHEREENS, AttackEnum.CHARGE_TREMOR, AttackEnum.CHARGE_EARTHQUAKE, AttackEnum.CHARGE_BURNING_BOULDER, AttackEnum.CHARGE_METEOR });
                case HeroEnum.PULVERIZER:
                    return new BaseHero(hero, "Pulverizer", "Icons/Element02_256_22", "Characters/EarthOverrideController", "Characters/Pulverizer/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.EARTH, 4,
                        85, 95, 50, 70, 65, 60, 0.1, 0.2,
                        AbilityEnum.NONE, AttackEnum.BASIC_PEBBLE, AttackEnum.BASIC_JAGGED_ROCK, AttackEnum.CHARGE_STONE_FIST, AttackEnum.CHARGE_ROLLING_TACKLE,
                        new AttackEnum[] { AttackEnum.BASIC_BOULDER },
                        new AttackEnum[] { AttackEnum.CHARGE_SMASH_TO_SMITHEREENS });
                case HeroEnum.TERRIKAHT:
                    return new BaseHero(hero, "Terrikaht", "Icons/Element02_256_22", "Characters/EarthOverrideController", "Characters/Terrikaht/Large/MainPrefab",
                        RoleEnum.PROTECTION, FactionEnum.EARTH, 5,
                        90, 60, 50, 100, 75, 60, 0, 0.25,
                        AbilityEnum.NONE, AttackEnum.BASIC_PEBBLE, AttackEnum.BASIC_JAGGED_ROCK, AttackEnum.CHARGE_HARDEN_FIST, AttackEnum.CHARGE_GIFT_OF_EARTH,
                        new AttackEnum[] { AttackEnum.BASIC_BOULDER },
                        new AttackEnum[] { AttackEnum.CHARGE_HIGH_GROUND, AttackEnum.CHARGE_CHOKING_DUST, AttackEnum.CHARGE_SHATTER_GLASS });

                // Electric heroes.
                case HeroEnum.ELECTIBOAR:
                    return new BaseHero(hero, "Electiboar", "Icons/Element02_256_16", "Characters/ElectricOverrideController", "Characters/Electiboar/Large/MainPrefab",
                        RoleEnum.PROTECTION, FactionEnum.ELECTRIC, 1,
                        80, 50, 75, 60, 85, 75, 0.1, 0.2,
                        AbilityEnum.NONE, AttackEnum.BASIC_SPARK, AttackEnum.BASIC_SHOCK, AttackEnum.CHARGE_LIGHTNING_BOLT, AttackEnum.CHARGE_LIGHTNING_BLAST,
                        new AttackEnum[] { AttackEnum.BASIC_ZAP },
                        new AttackEnum[] { AttackEnum.CHARGE_LASER_BEAM, AttackEnum.CHARGE_BRAINSTORM, AttackEnum.CHARGE_SHATTER_GLASS });
                case HeroEnum.BOLTCAP:
                    return new BaseHero(hero, "Boltcap", "Icons/Element02_256_16", "Characters/ElectricOverrideController", "Characters/Boltcap/Large/MainPrefab",
                        RoleEnum.SUPPORT, FactionEnum.ELECTRIC, 2,
                        65, 55, 85, 60, 70, 90, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_SPARK, AttackEnum.BASIC_POWER_DRAIN, AttackEnum.CHARGE_LIGHTNING_BOLT, AttackEnum.CHARGE_LIGHTNING_BLAST,
                        new AttackEnum[] { AttackEnum.BASIC_SHOCK, AttackEnum.BASIC_ZAP },
                        new AttackEnum[] { AttackEnum.CHARGE_LASER_BEAM, AttackEnum.CHARGE_REVERSE_POLARITY, AttackEnum.CHARGE_BURNING_HASTE });
                case HeroEnum.ZAPATAUR:
                    return new BaseHero(hero, "Zapataur", "Icons/Element02_256_16", "Characters/ElectricOverrideController", "Characters/Zapataur/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 3,
                        70, 50, 90, 60, 70, 85, 0.25, 0,
                        AbilityEnum.NONE, AttackEnum.BASIC_SPARK, AttackEnum.BASIC_SHOCK, AttackEnum.CHARGE_LIGHTNING_BOLT, AttackEnum.CHARGE_LIGHTNING_BLAST,
                        new AttackEnum[] { AttackEnum.BASIC_ZAP, AttackEnum.BASIC_FORKED_LIGHTNING, AttackEnum.BASIC_CHAIN_LIGHTNING },
                        new AttackEnum[] { AttackEnum.CHARGE_LASER_BEAM, AttackEnum.CHARGE_ELECTRICAL_STORM, AttackEnum.CHARGE_TEMPEST, AttackEnum.CHARGE_STRIKE_TWICE });
                case HeroEnum.GENERATOR:
                    return new BaseHero(hero, "Generator", "Icons/Element02_256_16", "Characters/ElectricOverrideController", "Characters/Generator/Large/MainPrefab",
                        RoleEnum.SUPPORT, FactionEnum.ELECTRIC, 4,
                        75, 50, 75, 70, 80, 75, 0.1, 0.2,
                        AbilityEnum.NONE, AttackEnum.BASIC_SPARK, AttackEnum.BASIC_SHOCK, AttackEnum.CHARGE_LIGHTNING_BOLT, AttackEnum.CHARGE_LIGHTNING_BLAST,
                        new AttackEnum[] { AttackEnum.BASIC_ZAP, AttackEnum.BASIC_POWER_DRAIN },
                        new AttackEnum[] { AttackEnum.CHARGE_LASER_BEAM, AttackEnum.CHARGE_OVERCHARGE });
                case HeroEnum.ZEPHYKAHT:
                    return new BaseHero(hero, "Zephykaht", "Icons/Element02_256_16", "Characters/ElectricOverrideController", "Characters/Zephykaht/Large/MainPrefab",
                        RoleEnum.DAMAGE, FactionEnum.ELECTRIC, 5,
                        60, 50, 90, 60, 70, 100, 0.2, 0.1,
                        AbilityEnum.NONE, AttackEnum.BASIC_SPARK, AttackEnum.BASIC_SHOCK, AttackEnum.CHARGE_LIGHTNING_BOLT, AttackEnum.CHARGE_LIGHTNING_BLAST,
                        new AttackEnum[] { AttackEnum.BASIC_ZAP, AttackEnum.BASIC_FORKED_LIGHTNING, AttackEnum.BASIC_CHAIN_LIGHTNING, AttackEnum.BASIC_KINDLE },
                        new AttackEnum[] { AttackEnum.CHARGE_LASER_BEAM, AttackEnum.CHARGE_ELECTRICAL_STORM, AttackEnum.CHARGE_TEMPEST, AttackEnum.CHARGE_STRIKE_TWICE, AttackEnum.CHARGE_BRAINSTORM, AttackEnum.CHARGE_OVERCHARGE });

                default:
                    return new BaseHero(hero, "Unknown", "Icons/icon_gem", "Characters/FacelessOverrideController", null,
                        RoleEnum.DAMAGE, FactionEnum.WATER, 1,
                        50, 50, 50, 50, 50, 50, 0, 0,
                        AbilityEnum.NONE, AttackEnum.BASIC_PUNCH, AttackEnum.BASIC_KICK, AttackEnum.CHARGE_RUNNING_PUNCH, AttackEnum.CHARGE_FLYING_KICK,
                        new AttackEnum[] { },
                        new AttackEnum[] { });
            }
        }

        public static double GetBigStatGain(double baseStat) {
            return ((baseStat - 50.0) / 20.0) + 1.5;
        }
    }

    public class BaseHeroContainer {

        private static Dictionary<HeroEnum, BaseHero> allHeroes;
        public static List<HeroEnum> rarityOne;
        public static List<HeroEnum> rarityTwo;
        public static List<HeroEnum> rarityThree;
        public static List<HeroEnum> rarityFour;
        public static List<HeroEnum> rarityFive;

        public static List<HeroEnum> allTanks;
        public static List<HeroEnum> allDamage;
        public static List<HeroEnum> allSupports;

        public static void Initialize() {
            allHeroes = new Dictionary<HeroEnum, BaseHero>();
            rarityOne = new List<HeroEnum>();
            rarityTwo = new List<HeroEnum>();
            rarityThree = new List<HeroEnum>();
            rarityFour = new List<HeroEnum>();
            rarityFive = new List<HeroEnum>();
            allTanks = new List<HeroEnum>();
            allDamage = new List<HeroEnum>();
            allSupports = new List<HeroEnum>();

            foreach (HeroEnum hero in Enum.GetValues(typeof(HeroEnum))) {
                var b = BaseHero.GetHero(hero);
                allHeroes[hero] = b;
                switch (b.Rarity) {
                    case 1:
                        rarityOne.Add(hero);
                        break;
                    case 2:
                        rarityTwo.Add(hero);
                        break;
                    case 3:
                        rarityThree.Add(hero);
                        break;
                    case 4:
                        rarityFour.Add(hero);
                        break;
                    case 5:
                        rarityFive.Add(hero);
                        break;
                }

                switch (b.Role) {
                    case RoleEnum.PROTECTION:
                        allTanks.Add(hero);
                        break;
                    case RoleEnum.DAMAGE:
                        allDamage.Add(hero);
                        break;
                    case RoleEnum.SUPPORT:
                        allSupports.Add(hero);
                        break;
                }
            }
        }

        public static BaseHero GetBaseHero(HeroEnum hero) {
            if (allHeroes == null || allHeroes.Count == 0) {
                Initialize();
            }
            return allHeroes[hero];
        }

        public static HeroEnum ChooseRandomHero(Random rand) {
            if (allHeroes == null || allHeroes.Count == 0) {
                Initialize();
            }
            double roll = rand.NextDouble();
            if (roll <= 0.3) {
                return ChooseHeroFromList(rand, rarityOne);
            } else if (roll <= 0.6) {
                return ChooseHeroFromList(rand, rarityTwo);
            } else if (roll <= 0.8) {
                return ChooseHeroFromList(rand, rarityThree);
            } else if (roll <= 0.95) {
                return ChooseHeroFromList(rand, rarityFour);
            } else {
                return ChooseHeroFromList(rand, rarityFive);
            }
        }

        public static HeroEnum ChooseHeroFromList(Random rand, List<HeroEnum> choices) {
            int choice = rand.Next(choices.Count);
            return choices[choice];
        }

        public static HeroEnum ChooseRandomHero(RoleEnum role, Random rand) {
            if (allHeroes == null || allHeroes.Count == 0) {
                Initialize();
            }
            switch (role) {
                case RoleEnum.PROTECTION:
                    return ChooseHeroFromList(rand, allTanks);
                case RoleEnum.DAMAGE:
                    return ChooseHeroFromList(rand, allDamage);
                case RoleEnum.SUPPORT:
                    return ChooseHeroFromList(rand, allSupports);
                default:
                    return ChooseHeroFromList(rand, allDamage);
            }
        }
    }
}