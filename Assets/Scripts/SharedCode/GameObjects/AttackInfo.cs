using System.Collections.Generic;
using Com.Tempest.Whale.Combat;

namespace Com.Tempest.Whale.GameObjects {

    public enum AttackEnum {

        // No Faction, Physical
        BASIC_PUNCH = 0,
        BASIC_KICK = 1,
        CHARGE_RUNNING_PUNCH = 2,
        CHARGE_FLYING_KICK = 3,
        CHARGE_RECKLESS_TACKLE = 4,

        // No Faction, Magic
        BASIC_ENERGY_SHOT = 10,
        BASIC_ENERGY_BEAM = 11,
        CHARGE_PIERCING_SHRIEK = 12,

        // No Faction, Buff
        CHARGE_RALLYING_CHEER = 20,
        CHARGE_TAKE_COVER = 21,

        // No Faction, Debuff
        CHARGE_LOOK_ADORABLE = 30,
        CHARGE_INTIMIDATE = 31,

        // Water, Physical
        BASIC_FIN_SLAP = 100, // small
        BASIC_TAIL_SLAP = 101, // medium
        BASIC_BREACHING_CRASH = 102, // large
        CHARGE_SPLASHING_LEAP = 103, // small
        CHARGE_DIVE = 104, // medium

        // Water, Magic
        BASIC_SPRAY = 110, // small
        BASIC_DELUGE = 111, // medium
        BASIC_TORRENT = 112, // large
        CHARGE_WATER_SHOT = 113, // small
        CHARGE_WATER_GLOBE = 114, // medium
        CHARGE_PRESSURE_JET = 115, // large

        // Water, Physical, Area
        CHARGE_FEEDING_FRENZY = 120, // medium
        CHARGE_SHIPWRECK = 121, // large

        // Water, Magic, Area
        BASIC_SPLASHING_WAVE = 130, // medium
        BASIC_WHIRLPOOL = 131, // large
        CHARGE_TSUNAMI = 132, // large
        
        // Water, Debuff
        CREATE_BOG = 140, // Reduce speed for enemy team

        // Water, Buff
        BASIC_HEALING_MIST = 150, // Small heal on 2 targets
        BASIC_HEALING_RAIN = 151, // Small heal on all targets
        CHARGE_SHROUD_IN_MIST = 152, // Reduce crit chance
        CHARGE_HEALING_WAVE = 153, // Big heal single target
        CHARGE_CLEANSING_WAVE = 154, // Remove debuffs single target
        CHARGE_CLEANSING_RAIN = 155, // Reduce debuff timers by 1 all targets
        CHARGE_ENSCALE = 156, // Increase armor
        CHARGE_FAVORABLE_CURRENT = 157, // Increase speed for whole team

        // Grass, Physical
        BASIC_PETAL_SLAP = 200, // small
        BASIC_BRANCH_SLAP = 201, // medium
        BASIC_RAZOR_VINE = 202, // large
        CHARGE_WEED_WHACKER = 203, // small
        CHARGE_CABER_TOSS = 204, // medium
        CHARGE_TIMBER = 205, // large

        // Grass, Magic
        BASIC_NEEDLE_SHOT = 210, // small
        BASIC_GRAPE_SHOT = 211, // medium
        BASIC_COCONUT_CATAPULT = 212, // large
        CHARGE_DRAIN_LIFE = 213, // medium
        CHARGE_MOONBEAM = 214, // large

        // Grass, Physical, Area
        BASIC_WHIRLING_BRANCHES = 220, // medium
        BASIC_LOG_ROLL = 221, // large
        CHARGE_NEEDLE_SPRAY = 222, // medium
        CHARGE_STRANGLING_VINES = 223, // large

        // Grass, Magic, Area
        CHARGE_PETAL_STORM = 230, // small
        CHARGE_LEAF_WHIRLWIND = 231, // medium

        // Grass, Buff
        BASIC_REGROW = 240, // large heal single target
        CHARGE_TAKE_ROOT = 241, // medium heal self
        CHARGE_TRANQUIL_GROVE = 242, // large heal team wide
        CHARGE_BARKSKIN = 243, // small defense buff, based on user's defense
        CHARGE_THORN_ARMOR = 244, // medium defense buff and physical damage reflection, uses opponent's attack
        CHARGE_SHADY_BRANCHES = 245, // medium protection against team-wide area attacks

        // Grass, Debuff
        INVOKE_ALLERGIES = 250, // small poison
        TOXIC_SPORES = 251, // large poison, uses magic

        // Fire, Physical
        BASIC_BURNING_FIST = 300, // small
        BASIC_BLAZING_FIST = 301, // medium
        BASIC_JET_TACKLE = 302, // large
        CHARGE_BURNING_BOULDER = 303, // medium

        // Fire, Magic
        BASIC_SINGE = 310, // small
        BASIC_SCORCH = 311, // medium
        BASIC_IMMOLATE = 312, // large
        CHARGE_BLAZE = 313, // small
        CHARGE_INCINERATE = 314, // medium
        CHARGE_INFERNO = 315, // large

        // Fire, Physical, Area
        CHARGE_BURNING_PEBBLES = 320, // small
        CHARGE_METEOR_SHOWER = 321, // medium
        CHARGE_ERUPTION = 322, // large

        // Fire, Magic, Area
        BASIC_FIRE_BREATH = 330, // small
        BASIC_LAVA_WAVE = 331, // medium
        BASIC_SEARING_WIND = 332, // large
        CHARGE_TWIN_FLAME = 333, // small
        CHARGE_FIREBALL = 334, // medium
        CHARGE_EXPLOSION = 335, // large

        // Fire, Buff
        CHARGE_FIRE_OF_YOUTH = 340, // small, raises speed
        CHARGE_BURNING_HASTE = 341, // medium, raises offensive stats
        CHARGE_RECKLESS_ABANDON = 342, // large, raises all offensive stats, lowers defensive stats
        CHARGE_KINDLE = 343, // medium, gives energy to team

        // Fire, Debuff
        CHARGE_ASH_CLOUD = 350, // medium, blinds enemy team
        CHARGE_MELT_ARMOR = 351, // medium, reduces enemy defensive stats

        // Ice, Physical
        ICE_CUBE = 400,
        SNOWBALL = 401,
        SUB_ZERO_MACHINE_GUN = 402,
        ICICLE = 403,

        // Ice, Magic
        CHILLING_WIND = 410,
        FREEZE_RAY = 411,
        ABSOLUTE_ZERO = 412,

        // Ice, Physical, Area
        SNOW_DRIFT = 420,
        SNOW_SLIDE = 421,
        AVALANCHE = 422,

        // Ice, Magic, Area
        SNOWFALL = 430,
        BLIZZARD = 431,

        // Ice, Buff
        REFLECTIVE_ARMOR = 440,

        // Ice, Debuff
        WINTER_STORM = 450,
        CRYSTALLIZE = 451,
        FREEZE_EARTH = 452,

        // Earth, Physical
        PEBBLE,
        BOULDER,
        ROCK_FIST,
        ROLLING_TACKLE,
        JAGGED_ROCKS,
        SMASH,

        // Earth, Magic
        MUD_SHOT,
        MUD_BLAST,

        // Earth, Physcial, Area
        ROCK_SLIDE,
        TREMOR,
        EARTHQUAKE,

        // Earth, Magic, Area
        DUST_STORM,
        TWISTER,

        // Earth, Buff
        ROCKSKIN,
        HIGH_GROUND,
        STRENGTH_OF_EARTH,
        HARDEN_FIST,
        JAGGED_TEETH,

        // Earth, Debuff
        CHOKING_DUST,

        // Electric, Physical
        ELECTRIC_CHARGE,

        // Electric, Magic
        ZAP,
        SPARK,
        SHOCK,
        LIGHTNING_BOLT,
        LASER_BEAM,

        // Electric, Physcial, Area

        // Electric, Magic, Area
        FORKED_LIGHTNING,
        ELECTRICAL_STORM,
        TEMPEST,

        // Electric, Buff
        POLARIZE,
        ILLUMINATE,
        OVERCHARGE,
        REVERSE_POLARITY,

        // Electric, Debuff
        BRAINSTORM,
        DEAFENING_THUNDER,
        POWER_DRAIN,
        BLINDING_FLASH,
    }

    public class AttackInfo {

        public AttackEnum Attack { get; }
        public string AttackName { get; }
        public string AttackIconPath { get; }
        public string AttackSoundPath { get; }
        public AttackParticleEnum? EnemyParticle { get; }
        public ParticleOriginEnum? EnemyParticleOrigin { get; }
        public AttackParticleEnum? AllyParticle { get; }
        public ParticleOriginEnum? AllyParticleOrigin { get; }
        public bool IsMelee { get; }
        public bool IsPhysical { get; }
        public TargetType EnemyTargetType { get; }
        public int EnemyTargetCount { get; }
        public TargetType AllyTargetType { get; }
        public int AllyTargetCount { get; }
        public double BaseDamage { get; }
        public double BaseHealing { get; }
        public int AttackerEnergyGained { get; }
        public int TargetEnergyGained { get; }
        public int AllyEnergyGained { get; }
        public StatusEnum? TargetStatus { get; }
        public double TargetStatusValue { get; }
        public int TargetStatusDuration { get; }
        public StatusEnum? AllyStatus { get; }
        public double AllyStatusValue { get; }
        public int AllyStatusDuration { get; }

        public AttackInfo(AttackEnum attack, string attackName, string attackIcon, string attackSound,
            AttackParticleEnum? enemyParticle, ParticleOriginEnum? enemyParticleOrigin, AttackParticleEnum? allyParticle, ParticleOriginEnum? allyParticleOrigin,
            bool isMelee, bool isPhysical,
            TargetType enemyTargetType, int enemyTargetCount, TargetType allyTargetType, int allyTargetCount,
            double damageMultiplier, double healingMultiplier,
            int attackerEnergyGained, int targetEnergyGained, int allyEnergyGained,
            StatusEnum? targetStatus, double targetStatusValue, int targetStatusDuration,
            StatusEnum? allyStatus, double allyStatusValue, int allyStatusDuration) {

            Attack = attack;
            AttackName = attackName;
            AttackIconPath = attackIcon;
            AttackSoundPath = attackSound;

            EnemyParticle = enemyParticle;
            EnemyParticleOrigin = enemyParticleOrigin;
            AllyParticle = allyParticle;
            AllyParticleOrigin = allyParticleOrigin;

            IsMelee = isMelee;
            IsPhysical = isPhysical;

            EnemyTargetType = enemyTargetType;
            EnemyTargetCount = enemyTargetCount;
            AllyTargetType = allyTargetType;
            AllyTargetCount = allyTargetCount;

            BaseDamage = damageMultiplier;
            BaseHealing = healingMultiplier;

            AttackerEnergyGained = attackerEnergyGained;
            TargetEnergyGained = targetEnergyGained;
            AllyEnergyGained = allyEnergyGained;

            TargetStatus = targetStatus;
            TargetStatusValue = targetStatusValue;
            TargetStatusDuration = targetStatusDuration;

            AllyStatus = allyStatus;
            AllyStatusValue = allyStatusValue;
            AllyStatusDuration = allyStatusDuration;
        }

        public List<CombatStep> ApplyAttackToEnemy(CombatHero attacker, CombatHero target) {
            var hitType = CombatMath.RollHitType(attacker, target);
            var hitEffectivity = CombatMath.GetEffectivity(attacker, target);
            var attackValue = IsPhysical ? attacker.GetModifiedAttack() : attacker.GetModifiedMagic();
            var defenseValue = IsPhysical ? target.GetModifiedDefense() : target.GetModifiedReflection();
            var damage = CombatMath.Damage(attackValue, defenseValue, hitType, hitEffectivity);
            damage *= DamageMultiplier;
            target.currentHealth -= damage;
            target.currentEnergy += TargetEnergyGained;

            var step = new CombatStep(Attack, null, attacker.combatHeroGuid, target.combatHeroGuid);
            step.damage = damage;
            step.targetEnergy = TargetEnergyGained;
            step.hitType = hitType;
            var allInstances = new List<CombatStep>();
            allInstances.Add(step);

            // If the target died from this attack, bail before applying status.
            if (!target.IsAlive()) {
                target.currentHealth = 0;
                target.currentEnergy = 0;
                target.currentStatus.Clear();

                step.wasFatal = true;
                return allInstances;
            }

            if (TargetStatus != null) {
                switch (target.baseHero.PassiveAbility) {
                    case AbilityEnum.WATER_BODY:
                        if (TargetStatus == StatusEnum.BURN) {
                            return allInstances;
                        }
                        break;
                    case AbilityEnum.HOT_BLOODED:
                        if (TargetStatus == StatusEnum.POISON) {
                            return allInstances;
                        }
                        break;
                    case AbilityEnum.COLD_BLOODED:
                        if (TargetStatus == StatusEnum.CHILL || TargetStatus == StatusEnum.FREEZE) {
                            return allInstances;
                        }
                        break;
                }

                var inflictedStatus = TargetStatus.GetValueOrDefault();
                var statusValue = TargetStatusValue;
                var statusDuration = TargetStatusDuration;
                switch (inflictedStatus) {
                    case StatusEnum.BURN:
                        statusValue *= attacker.GetModifiedMagic();
                        break;
                    case StatusEnum.BLEED:
                        statusValue *= attacker.GetModifiedAttack();
                        break;
                    case StatusEnum.POISON:
                        statusValue *= attacker.GetModifiedMagic();
                        break;
                    case StatusEnum.CHILL:
                        if (target.HasStatus(StatusEnum.DOWSE)) {
                            statusValue *= 2;
                        }
                        break;
                    case StatusEnum.DAZE:
                        if (target.HasStatus(StatusEnum.DOWSE) && attacker.baseHero.Faction == FactionEnum.ELECTRIC) {
                            statusValue *= 2;
                        }
                        break;
                }

                if (target.baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                    if (inflictedStatus == StatusEnum.MAGIC_DOWN) {
                        inflictedStatus = StatusEnum.MAGIC_UP;
                    } else if (inflictedStatus == StatusEnum.SPEED_DOWN) {
                        inflictedStatus = StatusEnum.SPEED_UP;
                    }
                }

                // This is going to break everything.
                if (target.baseHero.PassiveAbility == AbilityEnum.MIRROR_ICE && !IsPhysical) {
                    var statusContainer = new CombatStatus(inflictedStatus, target.combatHeroGuid, attacker.combatHeroGuid, statusValue, statusDuration);
                    attacker.AddStatus(statusContainer);
                    var mirrorInstance = new CombatStep(null, null, target.combatHeroGuid, attacker.combatHeroGuid);
                    mirrorInstance.AddStatus(statusContainer);
                    allInstances.Add(mirrorInstance);
                } else {
                    var statusContainer = new CombatStatus(inflictedStatus, attacker.combatHeroGuid, target.combatHeroGuid, statusValue, statusDuration);
                    target.AddStatus(statusContainer);
                    step.AddStatus(statusContainer);
                }
            }

            return allInstances;
        }

        public CombatStep ApplyAttackToAlly(CombatHero attacker, CombatHero ally) {
            var attackValue = IsPhysical ? attacker.GetModifiedAttack() : attacker.GetModifiedMagic();
            var healing = attackValue * HealingMultiplier;
            healing = ally.ReceiveHealing(healing);
            ally.currentEnergy += AllyEnergyGained;

            var step = new CombatStep(Attack, null, attacker.combatHeroGuid, ally.combatHeroGuid);
            step.healing = healing;
            step.targetEnergy = AllyEnergyGained;

            // If the ally died from this attack somehow, bail before applying status.
            if (!ally.IsAlive()) {
                ally.currentHealth = 0;
                ally.currentEnergy = 0;
                ally.currentStatus.Clear();

                step.wasFatal = true;
                return step;
            }

            if (AllyStatus != null) {
                var bestowedStatus = AllyStatus.GetValueOrDefault();
                var statusValue = AllyStatusValue;
                var statusDuration = AllyStatusDuration;
                switch (bestowedStatus) {
                    case StatusEnum.REGENERATION:
                        statusValue *= attacker.GetModifiedMagic();
                        break;
                    case StatusEnum.LAVA_ARMOR:
                        statusValue *= attacker.GetModifiedMagic();
                        break;
                    case StatusEnum.THORN_ARMOR:
                        statusValue *= attacker.GetModifiedAttack();
                        break;
                    default:
                        break;
                }
                var statusContainer = new CombatStatus(bestowedStatus, attacker.combatHeroGuid, ally.combatHeroGuid, statusValue, statusDuration);
                ally.AddStatus(statusContainer);
                step.AddStatus(statusContainer);
            }

            if (attacker.baseHero.PassiveAbility == AbilityEnum.CLEANSING_RAIN) {
                var newStatus = new List<CombatStatus>();
                foreach (CombatStatus status in ally.currentStatus) {
                    var statusDisplay = StatusInfoContainer.GetStatusInfo(status.status);
                    if (status.turnsRemaining == CombatStatus.INDEFINITE) {
                        newStatus.Add(status);
                    } else if (statusDisplay.IsBeneficial) {
                        newStatus.Add(status);
                    } else {
                        status.turnsRemaining -= 1;
                        if (status.turnsRemaining > 0) {
                            newStatus.Add(status);
                        }
                    }
                }
                ally.currentStatus = newStatus;
            }

            return step;
        }

        public string GetTooltip() {
            string output = "";

            if (EnemyTargetType != TargetType.NONE) {
                string damageMultiplier = (DamageMultiplier * 100).ToString("0");
                string damageType = IsPhysical ? "attack" : "magic";
                string targetting = GetTargettingTooltip(EnemyTargetType, EnemyTargetCount, false);
                string firstPart = DamageMultiplier == 0 ? "Targets" : string.Format("Deals damage equal to {0}% of {1} to", damageMultiplier, damageType);
                output = string.Format("{0} {1}.{2}", firstPart, targetting,
                    GetStatusTooltip(TargetStatus.GetValueOrDefault(), TargetStatusDuration, TargetStatusValue, false));
            }

            if (AllyTargetType != TargetType.NONE) {
                string healMultiplier = (HealingMultiplier * 100).ToString("0");
                string damageType = IsPhysical ? "attack" : "magic";
                string targetting = GetTargettingTooltip(AllyTargetType, AllyTargetCount, true);
                string firstPart = HealingMultiplier == 0 ? "Targets" : string.Format("Restores health equal to {0}% of {1} to", healMultiplier, damageType);
                string allyTip = string.Format("{0} {1}.{2}", firstPart, targetting,
                    GetStatusTooltip(AllyStatus.GetValueOrDefault(), AllyStatusDuration, AllyStatusValue, true));

                if (output.Length == 0) output = allyTip;
                else output += "  " + allyTip;
            }

            return output;
        }

        private string GetTargettingTooltip(TargetType targetType, int targetCount, bool ally) {
            string singular = ally ? "ally" : "enemy";
            string plural = ally ? "allies" : "enemies";
            string targetting;
            switch (targetType) {
                case TargetType.FIRST_ALIVE:
                    string targettingSuffix = targetCount != 1 ? string.Format("{0} {1}", targetCount, plural) : singular;
                    targetting = string.Format("the first {0}", targettingSuffix);
                    break;
                case TargetType.LOWEST_HEALTH:
                    string count = targetCount != 1 ? string.Format("{0} {1}", targetCount, plural) : singular;
                    targetting = string.Format("the {0} with the lowest health", count);
                    break;
                case TargetType.HIGHEST_HEALTH:
                    count = targetCount != 1 ? string.Format("{0} {1}", targetCount, plural) : singular;
                    targetting = string.Format("the {0} with the highest health", count);
                    break;
                case TargetType.LOWEST_ENERGY:
                    count = targetCount != 1 ? string.Format("{0} {1}", targetCount, plural) : singular;
                    targetting = string.Format("the {0} with the lowest energy", count);
                    break;
                case TargetType.HIGHEST_ENERGY:
                    count = targetCount != 1 ? string.Format("{0} {1}", targetCount, plural) : singular;
                    targetting = string.Format("the {0} with the highest energy", count);
                    break;
                case TargetType.SELF:
                    return "self";
                case TargetType.RANDOM:
                default:
                    count = targetCount != 1 ? plural : singular;
                    targetting = string.Format("{0} {1} at random", targetCount, count);
                    break;
            }
            return targetting;
        }

        private string GetStatusTooltip(StatusEnum statusType, int statusDuration, double value, bool ally) {
            string turnPlural = statusDuration != 1 ? "turns" : "turn";
            string statusValue = (value * 100).ToString("0");
            switch (statusType) {
                case StatusEnum.BURN:
                case StatusEnum.BLEED:
                case StatusEnum.POISON:
                    return GetEnemyDamageStatusTooltip(statusType, statusDuration, value);

                case StatusEnum.ATTACK_UP:
                case StatusEnum.ATTACK_DOWN:
                case StatusEnum.MAGIC_UP:
                case StatusEnum.MAGIC_DOWN:
                case StatusEnum.DEFENSE_UP:
                case StatusEnum.DEFENSE_DOWN:
                case StatusEnum.REFLECTION_UP:
                case StatusEnum.REFLECTION_DOWN:
                case StatusEnum.SPEED_UP:
                case StatusEnum.SPEED_DOWN:
                    return GetStatModStatusTooltip(statusType, statusDuration, value, ally);

                case StatusEnum.CHILL:
                    return string.Format(" Chills for {0} {1} reducing attack and speed by {2}%.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.DAZE:
                    return string.Format(" Dazes for {0} {1} reducing magic, crit, and deflection by {2}%.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.FREEZE:
                    return string.Format(" Freezes for {0} {1} preventing all attacks.", statusDuration, turnPlural);
                case StatusEnum.STUN:
                    return string.Format(" Stuns for {0} {1} preventing all attacks.", statusDuration, turnPlural);
                case StatusEnum.BLIND:
                    return string.Format(" Blinds for {0} {1} preventing ranged attacks.", statusDuration, turnPlural);
                case StatusEnum.ROOT:
                    return string.Format(" Entangles for {0} {1} preventing melee attacks.", statusDuration, turnPlural);
                case StatusEnum.DOWSE:
                    return string.Format(" Dowses for {0} {1}.  Dowsed targets receive double penalties from Daze and Chill.", statusDuration, turnPlural);

                case StatusEnum.REGENERATION:
                    return string.Format(" Bestows Regeneration for {0} {1}, healing for {2}% of magic each turn.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.THORN_ARMOR:
                    return string.Format(" Bestows Thorn Armor for {0} {1}.  Whenever a hero with thorn armor is attacked, the attacker takes damage equal to {2}% of attack.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.LAVA_ARMOR:
                    return string.Format(" Bestows Lava Armor for {0} {1}.  Whenever a hero with lava armor is attacked, the attacker is burned for 2 turns, taking {2}% of magic each turn.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.ICE_ARMOR:
                    return string.Format(" Bestows Ice Armor for {0} {1}.  Whenever a hero with ice armor is attacked, the attacker is chilled for 2 turns, reducing speed by {2}%.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.EARTH_ARMOR:
                    return string.Format(" Bestows Earth Armor for {0} {1}, raising defense by {2}% and reflection by {3}%.", statusDuration, turnPlural, statusValue, (value * 100 / 2).ToString("0"));
                default:
                    return "";
            }
        }

        private string GetEnemyDamageStatusTooltip(StatusEnum statusType, int duration, double value) {
            string statusName = StatusInfoContainer.GetStatusInfo(statusType).StatusName;
            string turnPlural = duration != 1 ? "turns" : "turn";
            string damageType = statusType == StatusEnum.BLEED ? "attack" : "magic";
            string damageAmount = (value * 100).ToString("0");
            return string.Format(" Inflicts the {0} status for {1} {2}, dealing {3}% of {4} per turn.", statusName, duration, turnPlural, damageAmount, damageType);
        }

        private string GetStatModStatusTooltip(StatusEnum statusType, int duration, double value, bool ally) {
            string inflictWord = ally ? "Bestows" : "Inflicts";
            string statusAmount = (value * 100).ToString("0");
            string statusName = StatusInfoContainer.GetStatusInfo(statusType).StatusName;
            string turnPlural = duration != 1 ? "turns" : "turn";
            return string.Format(" {0} {1}% {2} for {3} {4}.", inflictWord, statusAmount, statusName, duration, turnPlural);
        }
    }

    public class AttackInfoContainer {

        private static Dictionary<AttackEnum, AttackInfo> attackDict;

        public static void Initialize() {
            if (attackDict != null) return;
            attackDict = new Dictionary<AttackEnum, AttackInfo>();

            // These are all basic attacks.
            attackDict[AttackEnum.BASIC_PHYSICAL] = new AttackInfo(
                AttackEnum.BASIC_PHYSICAL, "Basic Physical", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1, 0, 25, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_MAGIC] = new AttackInfo(
                AttackEnum.BASIC_MAGIC, "Basic Magic", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1, 0, 25, 10, 0,
                null, 0, 0, null, 0, 0);

            // Water
            attackDict[AttackEnum.VAPOR_CLOUD] = new AttackInfo(
                AttackEnum.VAPOR_CLOUD, "Vapor Cloud", "Icons/Attacks/CloudSwirl", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, false,
                TargetType.FIRST_ALIVE, 2, TargetType.NONE, 0,
                0.6, 0, 25, 10, 0,
                StatusEnum.DOWSE, 0, 2, null, 0, 0);
            attackDict[AttackEnum.FISH_SLAP] = new AttackInfo(
                AttackEnum.FISH_SLAP, "Fish Slap", "Icons/Attacks/Slap", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.2, 0, 25, 10, 0,
                StatusEnum.DEFENSE_DOWN, 0.2, 2, null, 0, 0);
            attackDict[AttackEnum.HEALING_MIST] = new AttackInfo(
                AttackEnum.HEALING_MIST, "Healing Mist", "Icons/Attacks/WaterSplash", "AttackSounds/WaterRenew",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER,
                false, true, false, false,
                TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 2,
                0, 1, 25, 0, 0,
                null, 0, 0, StatusEnum.REGENERATION, 0.2, 2);
            attackDict[AttackEnum.CRYSTAL_SMASH] = new AttackInfo(
                AttackEnum.CRYSTAL_SMASH, "Crystal Smash", "Icons/Attacks/CloudSwirl", "AttackSounds/VaporCloud",
                null, null, null, null,
                true, true, false, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.2, 0, 25, 10, 0,
                StatusEnum.DAZE, 0.2, 1, null, 0, 0);

            // Grass
            attackDict[AttackEnum.PETAL_SLAP] = new AttackInfo(
                AttackEnum.PETAL_SLAP, "Petal Slap", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.5, 0, 25, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.NEEDLE_STAB] = new AttackInfo(
                AttackEnum.NEEDLE_STAB, "Needle Stab", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, false,
                TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
                1, 0, 25, 10, 0,
                StatusEnum.POISON, 0.4, 2, null, 0, 0);
            attackDict[AttackEnum.MOONLIGHT] = new AttackInfo(
                AttackEnum.MOONLIGHT, "Moonlight", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER,
                false, true, false, false,
                TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 3,
                0, 1, 25, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BRANCH_SLAM] = new AttackInfo(
                AttackEnum.BRANCH_SLAM, "Branch Slam", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1, 0, 25, 10, 0,
                StatusEnum.DAZE, 0.4, 1, null, 0, 0);

            // Fire
            attackDict[AttackEnum.FIRE_BOLT] = new AttackInfo(
                AttackEnum.FIRE_BOLT, "Fire Bolt", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null,
                true, false, false, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.2, 0, 25, 10, 0,
                StatusEnum.BURN, 0.2, 2, null, 0, 0);
            attackDict[AttackEnum.SCORCH] = new AttackInfo(
                AttackEnum.SCORCH, "Scorch", "Icons/RoleDamage", "AttackSounds/Scorch",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, false,
                TargetType.FIRST_ALIVE, 2, TargetType.NONE, 0,
                0.5, 0, 25, 10, 0,
                StatusEnum.BURN, 0.2, 2, null, 0, 0);
            attackDict[AttackEnum.FIRE_PUNCH] = new AttackInfo(
                AttackEnum.FIRE_PUNCH, "Fire Punch", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, false, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1, 0, 25, 10, 0,
                StatusEnum.BURN, 0.2, 2, null, 0, 0);

            // Ice
            attackDict[AttackEnum.ICE_PUNCH] = new AttackInfo(
                AttackEnum.ICE_PUNCH, "Ice Slam", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.2, 0, 25, 10, 0,
                StatusEnum.CHILL, 0.2, 1, null, 0, 0);
            attackDict[AttackEnum.ICICLE_THROW] = new AttackInfo(
                AttackEnum.ICICLE_THROW, "Icicle Throw", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER, null, null,
                false, true, true, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.3, 0, 25, 10, 0,
                StatusEnum.CHILL, 0.1, 1, null, 0, 0);
            attackDict[AttackEnum.SNOWY_WIND] = new AttackInfo(
                AttackEnum.SNOWY_WIND, "Snowy Wind", "Icons/RoleDamage", "AttackSounds/SnowyWind2",
                AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, false,
                TargetType.RANDOM, 2, TargetType.NONE, 0,
                0.5, 0, 25, 10, 0,
                StatusEnum.CHILL, 0.2, 1, null, 0, 0);

            // Earth
            attackDict[AttackEnum.TWISTER] = new AttackInfo(
                AttackEnum.TWISTER, "Twister", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.2, 0, 25, 10, 0,
                StatusEnum.DEFENSE_DOWN, 0.2, 2, null, 0, 0);
            attackDict[AttackEnum.ROCK_SLAM] = new AttackInfo(
                AttackEnum.ROCK_SLAM, "Rock Slam", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.2, 0, 25, 10, 0,
                StatusEnum.DAZE, 0.2, 1, null, 0, 0);
            attackDict[AttackEnum.GRAVEL_SHOT] = new AttackInfo(
                AttackEnum.GRAVEL_SHOT, "Pebble Toss", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.EARTH, ParticleOriginEnum.ATTACKER, null, null,
                false, true, true, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.4, 0, 25, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.AXE_SLASH] = new AttackInfo(
                AttackEnum.AXE_SLASH, "Axe Slash", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.2, 0, 25, 10, 0,
                StatusEnum.DEFENSE_DOWN, 0.2, 2, null, 0, 0);

            // Electric
            attackDict[AttackEnum.SPARK] = new AttackInfo(
                AttackEnum.SPARK, "Spark", "Icons/RoleDamage", "AttackSounds/Spark",
                null, null, null, null,
                true, false, false, false,
                TargetType.FIRST_ALIVE, 2, TargetType.NONE, 0,
                0.5, 0, 25, 10, 0,
                StatusEnum.DAZE, 0.2, 1, null, 0, 0);
            attackDict[AttackEnum.ENERGY_DRAIN] = new AttackInfo(
                AttackEnum.ENERGY_DRAIN, "Energy Drain", "Icons/RoleDamage", "AttackSounds/EnergyDrain",
                AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER,
                false, true, false, false,
                TargetType.FIRST_ALIVE, 1, TargetType.RANDOM, 1,
                1, 0, 25, -25, 25,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.LIGHTNING_BOLT] = new AttackInfo(
                AttackEnum.LIGHTNING_BOLT, "Lightning Bolt", "Icons/RoleDamage", "AttackSounds/LightningBolt",
                AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, false,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                1.2, 0, 25, 10, 0,
                StatusEnum.DAZE, 0.2, 1, null, 0, 0);
            attackDict[AttackEnum.FORKED_LIGHTNING] = new AttackInfo(
                AttackEnum.FORKED_LIGHTNING, "Forked Lightning", "Icons/RoleDamage", "AttackSounds/LightningBolt",
                AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, false,
                TargetType.RANDOM, 2, TargetType.NONE, 0,
                0.5, 0, 25, 10, 0,
                StatusEnum.DAZE, 0.2, 1, null, 0, 0);

            // These are all special attacks.
            attackDict[AttackEnum.SPECIAL_PHYSICAL] = new AttackInfo(
                AttackEnum.SPECIAL_PHYSICAL, "Special Physical", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, true,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                4, 0, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.SPECIAL_MAGIC] = new AttackInfo(
                AttackEnum.SPECIAL_MAGIC, "Special Magic", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, true,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                4, 0, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // Water
            attackDict[AttackEnum.WATER_SHOT] = new AttackInfo(
                AttackEnum.WATER_SHOT, "Water Shot", "Icons/Attacks/WaterSwirl", "AttackSounds/BasicMagic",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, true,
                TargetType.RANDOM, 2, TargetType.NONE, 0,
                2, 0, -100, 10, 0,
                StatusEnum.DOWSE, 0, 2, null, 0, 0);
            attackDict[AttackEnum.DRENCHING_WAVE] = new AttackInfo(
                AttackEnum.DRENCHING_WAVE, "Drenching Wave", "Icons/Attacks/Trident", "AttackSounds/BasicMagic",
                AttackParticleEnum.ELECTRIC, ParticleOriginEnum.OVERHEAD, null, null,
                false, true, false, true,
                TargetType.RANDOM, 3, TargetType.NONE, 0,
                1, 0, -100, 10, 0,
                StatusEnum.REFLECTION_DOWN, 0.5, 3, null, 0, 0);
            attackDict[AttackEnum.ENSCALE_TEAM] = new AttackInfo(
                AttackEnum.ENSCALE_TEAM, "Enscale Team", "Icons/Attacks/WaterScale", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, true,
                TargetType.FIRST_ALIVE, 1, TargetType.RANDOM, 5,
                2, 0, -100, 10, 0,
                StatusEnum.DEFENSE_DOWN, 0.2, 2, StatusEnum.DEFENSE_UP, 0.3, 3);
            attackDict[AttackEnum.TSUNAMI] = new AttackInfo(
                AttackEnum.TSUNAMI, "Tsunami", "Icons/Attacks/Rainstorm", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD, null, null,
                false, true, false, true,
                TargetType.RANDOM, 5, TargetType.NONE, 0,
                0.8, 0, -100, 10, 0,
                StatusEnum.DOWSE, 0, 2, null, 0, 0);
            attackDict[AttackEnum.HEALING_WAVE] = new AttackInfo(
                AttackEnum.HEALING_WAVE, "Healing Wave", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD, AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD,
                false, false, false, true,
                TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 1, -100, 0, 0,
                null, 0, 0, StatusEnum.REGENERATION, 0.5, 2);

            // Grass
            attackDict[AttackEnum.HEALING_SUN] = new AttackInfo(
                AttackEnum.HEALING_SUN, "Healing Sun", "Icons/RoleSupport", "AttackSounds/BasicMagic",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.OVERHEAD,
                false, false, false, true,
                TargetType.NONE, 0, TargetType.RANDOM, 5,
                0, 1, -100, 0, 0,
                null, 0, 0, StatusEnum.REGENERATION, 0.2, 5);
            attackDict[AttackEnum.WEED_WHACKER] = new AttackInfo(
                 AttackEnum.WEED_WHACKER, "Weed Whacker", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                 null, null, null, null,
                 true, false, true, true,
                 TargetType.LOWEST_HEALTH, 2, TargetType.NONE, 0,
                 1.5, 0, -100, 10, 0,
                 StatusEnum.POISON, 1, 2, null, 0, 0);
            attackDict[AttackEnum.ENTANGLING_ROOTS] = new AttackInfo(
                 AttackEnum.ENTANGLING_ROOTS, "Entangling Roots", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                 null, null, null, null,
                 true, false, true, true,
                 TargetType.RANDOM, 5, TargetType.NONE, 0,
                 0.6, 0, -100, 10, 0,
                 StatusEnum.ROOT, 0.5, 2, null, 0, 0);
            attackDict[AttackEnum.RITUAL_OF_THE_SUN] = new AttackInfo(
                 AttackEnum.RITUAL_OF_THE_SUN, "Ritual of the Sun", "Icons/RoleSupport", "AttackSounds/BasicMagic",
                 null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER,
                 false, false, false, true,
                 TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 3,
                 0, 2, -100, 0, 0,
                 null, 0, 0, StatusEnum.REGENERATION, 0.5, 3);
            attackDict[AttackEnum.GIFT_OF_THORNS] = new AttackInfo(
                AttackEnum.GIFT_OF_THORNS, "Gift of Thorns", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER,
                false, false, true, true,
                TargetType.NONE, 0, TargetType.RANDOM, 5,
                0, 0, -100, 0, 0,
                null, 0, 0, StatusEnum.THORN_ARMOR, 0.2, 3);

            // Fire
            attackDict[AttackEnum.TWIN_FLAME] = new AttackInfo(
                AttackEnum.TWIN_FLAME, "Twin Flame", "Icons/RoleDamage", "AttackSounds/Scorch",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, true,
                TargetType.LOWEST_HEALTH, 2, TargetType.NONE, 0,
                2, 0, -100, 10, 0,
                StatusEnum.BURN, 0.5, 2, null, 0, 0);
            attackDict[AttackEnum.TURN_UP_THE_HEAT] = new AttackInfo(
                AttackEnum.TURN_UP_THE_HEAT, "Turn Up The Heat", "Icons/RoleSupport", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD, AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD,
                false, false, false, true,
                TargetType.RANDOM, 5, TargetType.RANDOM, 5,
                0, 0, -100, 0, 0,
                StatusEnum.BURN, 0.25, 2, StatusEnum.SPEED_UP, 0.2, 2);
            attackDict[AttackEnum.IMMOLATE] = new AttackInfo(
                AttackEnum.IMMOLATE, "Immolate", "Icons/RoleDamage", "AttackSounds/Scorch",
                AttackParticleEnum.FIRE, ParticleOriginEnum.TARGET, null, null,
                false, true, false, true,
                TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                2, 0, -100, 10, 0,
                StatusEnum.BURN, 2, 3, null, 0, 0);
            attackDict[AttackEnum.GIFT_OF_LAVA] = new AttackInfo(
                AttackEnum.GIFT_OF_LAVA, "Gift of Lava", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                null, null, AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD,
                false, false, false, true,
                TargetType.NONE, 0, TargetType.RANDOM, 10,
                0, 0, -100, 0, 0,
                null, 0, 0, StatusEnum.LAVA_ARMOR, 0.3, 3);
            attackDict[AttackEnum.FIRE_STORM] = new AttackInfo(
                AttackEnum.FIRE_STORM, "Fire Storm", "Icons/RoleDamage", "AttackSounds/Scorch",
                AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD, null, null,
                false, true, false, true,
                TargetType.RANDOM, 10, TargetType.NONE, 0,
                0.6, 0, -100, 10, 0,
                StatusEnum.BURN, 0.4, 2, null, 0, 0);

            // Ice
            attackDict[AttackEnum.CHILLY_WIND] = new AttackInfo(
                AttackEnum.CHILLY_WIND, "Chilly Wind", "Icons/RoleDamage", "AttackSounds/SnowyWind2",
                AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER, null, null,
                false, true, true, true,
                TargetType.RANDOM, 3, TargetType.NONE, 0,
                1.2, 0, -100, 10, 0,
                StatusEnum.CHILL, 0.5, 2, null, 0, 0);
            attackDict[AttackEnum.ENCASE_IN_ICE] = new AttackInfo(
                AttackEnum.ENCASE_IN_ICE, "Encase in Ice", "Icons/RoleDamage", "AttackSounds/FrozenMirror",
                null, null, AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER,
                false, false, true, true,
                TargetType.NONE, 0, TargetType.SELF, 1,
                0, 0, -100, 0, 0,
                null, 0, 0, StatusEnum.ICE_ARMOR, 1, 3);
            attackDict[AttackEnum.FLINGING_SPREE] = new AttackInfo(
                AttackEnum.FLINGING_SPREE, "Flinging Spree", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER, null, null,
                false, true, true, true,
                TargetType.LOWEST_HEALTH, 2, TargetType.NONE, 0,
                2.5, 0, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BLIZZARD] = new AttackInfo(
                AttackEnum.BLIZZARD, "Blizzard", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.ICE, ParticleOriginEnum.OVERHEAD, null, null,
                false, true, false, true,
                TargetType.RANDOM, 5, TargetType.NONE, 0,
                0.6, 0, -100, 10, 0,
                StatusEnum.CHILL, 0.5, 2, null, 0, 0);
            attackDict[AttackEnum.GIFT_OF_ICE] = new AttackInfo(
                AttackEnum.GIFT_OF_ICE, "Gift of Ice", "Icons/RoleDamage", "AttackSounds/FrozenMirror",
                null, null, AttackParticleEnum.ICE, ParticleOriginEnum.ATTACKER,
                false, false, false, true,
                TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 10,
                0, 0, -100, 0, 0,
                null, 0, 0, StatusEnum.ICE_ARMOR, 0.5, 2);

            // Earth
            attackDict[AttackEnum.DUST_STORM] = new AttackInfo(
                AttackEnum.DUST_STORM, "Dust Storm", "Icons/RoleDamage", "AttackSounds/BasePhysical",
                AttackParticleEnum.EARTH, ParticleOriginEnum.OVERHEAD, null, null,
                false, true, true, true,
                TargetType.RANDOM, 5, TargetType.NONE, 0,
                0.6, 0, -100, 10, 0,
                StatusEnum.BLIND, 0.5, 2, null, 0, 0);
            attackDict[AttackEnum.ENCASE_IN_ROCK] = new AttackInfo(
                AttackEnum.ENCASE_IN_ROCK, "Encase in Rock", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                null, null, AttackParticleEnum.EARTH, ParticleOriginEnum.ATTACKER,
                false, false, true, true,
                TargetType.NONE, 0, TargetType.SELF, 1,
                0, 0, -100, 0, 0,
                null, 0, 0, StatusEnum.EARTH_ARMOR, 1.5, 3);
            attackDict[AttackEnum.PEBBLE_SHOWER] = new AttackInfo(
                AttackEnum.PEBBLE_SHOWER, "Pebble Shower", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.EARTH, ParticleOriginEnum.OVERHEAD, null, null,
                false, true, true, true,
                TargetType.RANDOM, 5, TargetType.NONE, 0,
                0.6, 0, -100, 10, 0,
                StatusEnum.BLIND, 0.5, 1, null, 0, 0);
            attackDict[AttackEnum.GIFT_OF_EARTH] = new AttackInfo(
                AttackEnum.GIFT_OF_EARTH, "Gift of Earth", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                null, null, AttackParticleEnum.EARTH, ParticleOriginEnum.ATTACKER,
                false, false, true, true,
                TargetType.NONE, 0, TargetType.RANDOM, 5,
                0, 0, -100, 0, 0,
                null, 0, 0, StatusEnum.EARTH_ARMOR, 0.5, 3);
            attackDict[AttackEnum.SPLIT_SKULL] = new AttackInfo(
                AttackEnum.SPLIT_SKULL, "Split Skull", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null,
                true, false, true, true,
                TargetType.FIRST_ALIVE, 1, TargetType.SELF, 1,
                3, 0, -100, 10, 0,
                StatusEnum.BLEED, 1, 2, StatusEnum.ATTACK_UP, 0.4, CombatStatus.INDEFINITE);

            // Electric
            attackDict[AttackEnum.FLASH_OF_LIGHT] = new AttackInfo(
                AttackEnum.FLASH_OF_LIGHT, "Flash of Light", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, true,
                TargetType.RANDOM, 3, TargetType.NONE, 0,
                1, 0, -100, 10, 0,
                StatusEnum.BLIND, 0.6, 1, null, 0, 0);
            attackDict[AttackEnum.CHARGE_TEAM] = new AttackInfo(
                AttackEnum.CHARGE_TEAM, "Charge Team", "Icons/RoleSupport", "AttackSounds/BasicMagic",
                null, null, AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER,
                false, false, false, true,
                TargetType.NONE, 0, TargetType.LOWEST_ENERGY, 3,
                0, 0, -100, 0, 40,
                null, 0, 0, StatusEnum.MAGIC_UP, 0.5, 3);
            attackDict[AttackEnum.OVERCHARGED_BOLT] = new AttackInfo(
                AttackEnum.OVERCHARGED_BOLT, "Overcharged Bolt", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, true,
                TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
                5, 0, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.LIGHTNING_FLASH] = new AttackInfo(
                AttackEnum.LIGHTNING_FLASH, "Lightning Flash", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.ELECTRIC, ParticleOriginEnum.OVERHEAD, null, null,
                false, true, false, true,
                TargetType.RANDOM, 3, TargetType.NONE, 0,
                1.2, 0, -100, 10, 0,
                StatusEnum.BLIND, 0.5, 1, null, 0, 0);
            attackDict[AttackEnum.BRAIN_STORM] = new AttackInfo(
                AttackEnum.BRAIN_STORM, "Brain Storm", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.ELECTRIC, ParticleOriginEnum.ATTACKER, null, null,
                false, true, false, true,
                TargetType.RANDOM, 5, TargetType.NONE, 0,
                0.6, 0, -100, 10, 0,
                StatusEnum.DAZE, 0.4, 1, null, 0, 0);
        }

        public static AttackInfo GetAttackInfo(AttackEnum attack) {
            if (attackDict == null) Initialize();
            return attackDict[attack];
        }
    }
}
