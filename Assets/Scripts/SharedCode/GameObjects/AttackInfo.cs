using System.Collections.Generic;
using Com.Tempest.Whale.Combat;

namespace Com.Tempest.Whale.GameObjects {

    public enum AttackEnum {

        // No Faction, Physical
        BASIC_PUNCH = 0, // small
        BASIC_KICK = 1, // medium
        CHARGE_RUNNING_PUNCH = 2, // small
        CHARGE_FLYING_KICK = 3, // medium
        CHARGE_RECKLESS_TACKLE = 4, // large

        // No Faction, Magic
        BASIC_ENERGY_SHOT = 10, // small
        BASIC_ENERGY_BEAM = 11, // medium
        CHARGE_LOUD_SCREAM = 12, // small
        CHARGE_PIERCING_SHRIEK = 13, // medium

        // No Faction, Buff
        CHARGE_RALLYING_CHEER = 20, // small
        CHARGE_PLANT_FLAG = 21, // medium

        // No Faction, Debuff
        CHARGE_LOOK_ADORABLE = 30, // small
        CHARGE_INTIMIDATE = 31, // medium

        // Water, Physical
        BASIC_FIN_SLAP = 100, // small
        BASIC_TAIL_SLAP = 101, // medium
        BASIC_BREACHING_CRASH = 102, // large
        CHARGE_SPLASHING_LEAP = 103, // small
        CHARGE_DIVE = 104, // medium
        CHARGE_DEPTH_CHARGE = 105, // large

        // Water, Magic
        BASIC_SPRAY = 110, // small
        BASIC_DELUGE = 111, // medium
        BASIC_TORRENT = 112, // large
        CHARGE_WATER_SHOT = 113, // small
        CHARGE_WATER_GLOBE = 114, // medium
        CHARGE_WATER_CUTTER = 115, // medium
        CHARGE_PRESSURE_JET = 116, // large
        CHARGE_LIQUIFY = 117, // large

        // Water, Physical, Area
        CHARGE_FEEDING_FRENZY = 120, // medium
        CHARGE_SHIPWRECK = 121, // large

        // Water, Magic, Area
        BASIC_SPLASHING_WAVE = 130, // medium
        BASIC_WHIRLPOOL = 131, // large
        CHARGE_TIDAL_WAVE = 132, // medium
        CHARGE_TSUNAMI = 133, // large

        // Water, Healing
        BASIC_HEALING_MIST = 140, // medium, heal 2 targets
        BASIC_HEALING_RAIN = 141, // large, heal all targets
        CHARGE_HEALING_WAVE = 142, // small, heal 1 target
        CHARGE_HEALING_DELUGE = 143, // medium, heal 1 target
        CHARGE_HEALING_TORRENT = 144, // large, heal 1 target
        CHARGE_CLEANSING_MIST = 145, // medium, heal all targets, reduce 1 debuff by 1 turn
        CHARGE_CLEANSING_RAIN = 146, // large, heal all targets, reduce all debuffs by 1 turn

        // Water, Debuff
        CREATE_BOG = 150, // Reduce speed for enemy team

        // Water, Buff
        CHARGE_ENSCALE = 160, // Increase armor
        CHARGE_FAVORABLE_CURRENT = 161, // Increase speed for whole team

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
        CHARGE_BERRY_BLAST = 213, // small
        CHARGE_SALAD_TOSS = 214, // medium
        CHARGE_MOONBEAM = 215, // large

        // Grass, Physical, Area
        BASIC_WHIRLING_BRANCHES = 220, // medium
        BASIC_LOG_ROLL = 221, // large
        CHARGE_NEEDLE_SPRAY = 222, // medium
        CHARGE_STRANGLING_VINES = 223, // large

        // Grass, Magic, Area
        CHARGE_PETAL_STORM = 230, // small
        CHARGE_LEAF_WHIRLWIND = 231, // medium

        // Grass, Healing
        BASIC_REGROW = 240, // small, heal 1 target
        BASIC_REJUVENATE = 241, // medium, heal 2 targets
        BASIC_REVITALIZE = 242, // large, heal 3 targets
        CHARGE_PEACEFUL_MEADOW = 243, // small, heal team
        CHARGE_TRANQUIL_GROVE = 244, // medium, heal team
        CHARGE_SERENE_FOREST = 245, // large, heal team

        // Grass, Buff
        CHARGE_BARKSKIN = 250, // small defense buff, based on user's defense
        CHARGE_THORN_ARMOR = 251, // medium defense buff and physical damage reflection, uses opponent's attack
        CHARGE_SHADY_BRANCHES = 252, // medium protection against team-wide area attacks

        // Grass, Debuff
        CHARGE_INVOKE_ALLERGIES = 260, // small poison
        CHARGE_TOXIC_SPORES = 261, // large poison

        // Fire, Physical
        BASIC_BURNING_FIST = 300, // small
        BASIC_BLAZING_FIST = 301, // medium
        BASIC_JET_TACKLE = 302, // large
        CHARGE_BURNING_STONE = 303, // small
        CHARGE_BURNING_BOULDER = 304, // medium
        CHARGE_METEOR = 305, // large

        // Fire, Magic
        BASIC_SINGE = 310, // small
        BASIC_SCORCH = 311, // medium
        BASIC_IMMOLATE = 312, // large
        CHARGE_BLAZE = 313, // small
        CHARGE_INCINERATE = 314, // medium
        CHARGE_INFERNO = 315, // large

        // Fire, Physical, Area
        CHARGE_ERUPTION = 320, // large

        // Fire, Magic, Area
        BASIC_FIRE_BREATH = 330, // small
        BASIC_LAVA_WAVE = 331, // medium
        BASIC_SEARING_WIND = 332, // large
        CHARGE_TWIN_FLAME = 333, // small
        CHARGE_FIREBALL = 334, // medium
        CHARGE_EXPLOSION = 335, // large

        // Fire, Buff
        BASIC_KINDLE = 340, // medium, gives energy to 2 allies
        CHARGE_STOKE_FLAMES = 341, // medium, gives energy to team
        CHARGE_BURNING_HASTE = 342, // medium, raises offensive stats

        // Fire, Debuff
        CHARGE_ASH_CLOUD = 350, // medium, blinds enemy team
        CHARGE_MELT_ARMOR = 351, // medium, reduces enemy defensive stats

        // Ice, Physical
        BASIC_ICE_CUBE = 400, // small
        BASIC_ICICLE_TOSS = 403, // medium
        BASIC_FROZEN_SLIDE = 402, // large
        CHARGE_FROZEN_FIST, // small
        CHARGE_ICICLE_DROP, // medium
        CHARGE_ICEBERG, // large

        // Ice, Magic
        BASIC_CHILLING_WIND = 410, // small
        BASIC_FREEZING_WIND = 411, // medium
        BASIC_SNOWBLAST = 412, // large
        CHARGE_FROSTBITE = 413, // small
        CHARGE_FREEZE_RAY = 414, // medium
        CHARGE_ABSOLUTE_ZERO = 415, // large

        // Ice, Physical, Area
        BASIC_SNOWBALL = 420, // small
        BASIC_SNOWBALL_STORM = 421, // medium
        BASIC_SUB_ZERO_MACHINE_GUN = 422, // large
        CHARGE_SNOW_DRIFT = 423, // small
        CHARGE_SNOW_SLIDE = 424, // medium
        CHARGE_AVALANCHE = 425, // large

        // Ice, Magic, Area
        CHARGE_SNOWFALL = 430, // medium
        CHARGE_BLIZZARD = 431, // large

        // Ice, Buff
        CHARGE_REFLECTIVE_ARMOR = 440, // medium, grants resistance

        // Ice, Debuff
        CHARGE_WINTER_STORM = 450, // medium, small blind and small chill
        CHARGE_CRYSTALLIZE = 451, //  medium, greatly reduces offensive stats, single target
        CHARGE_FREEZE_EARTH = 452, // medium, reduces offensive stats, team wide

        // Earth, Physical
        BASIC_PEBBLE = 500, // small
        BASIC_JAGGED_ROCK = 501, // medium
        BASIC_BOULDER = 502, // large
        CHARGE_STONE_FIST = 503, // small
        CHARGE_ROLLING_TACKLE = 504, // medium
        CHARGE_SMASH_TO_SMITHEREENS = 505, // large

        // Earth, Magic

        // Earth, Physcial, Area
        BASIC_DUST_STORM = 520, // medium
        BASIC_ROCK_SLIDE = 521, // large
        CHARGE_TREMOR = 522, // medium
        CHARGE_EARTHQUAKE = 523, // large

        // Earth, Magic, Area

        // Earth, 
        CHARGE_HARDEN_FIST = 540, // small, offensive buff
        CHARGE_STRENGTH_OF_EARTH = 541, // medium, offensive buff
        CHARGE_HIGH_GROUND = 542, // large, offensive and defensive buff
        CHARGE_ROCKSKIN = 543, // medium, defensive buff

        // Earth, Debuff
        CHARGE_CHOKING_DUST = 550, // medium, blinds enemy team

        // Electric, Physical

        // Electric, Magic
        BASIC_SPARK = 610, // small
        BASIC_SHOCK = 611, // medium
        BASIC_ZAP = 612, // large
        CHARGE_LIGHTNING_BOLT = 613, // small
        CHARGE_LIGHTNING_BLAST = 614, // medium
        CHARGE_LASER_BEAM = 615, // large

        // Electric, Physcial, Area

        // Electric, Magic, Area
        BASIC_FORKED_LIGHTNING = 630, // medium
        BASIC_CHAIN_LIGHTNING = 631, // large
        CHARGE_ELECTRICAL_STORM = 632, // medium
        CHARGE_TEMPEST = 633, // large

        // Electric, Buff
        CHARGE_OVERCHARGE = 640, // medium, gives energy to team

        // Electric, Debuff
        BASIC_POWER_DRAIN = 650, // medium, steals energy from highest target
        CHARGE_REVERSE_POLARITY = 651, // medium, turns healing into damage
        CHARGE_BRAINSTORM = 652, // medium, stuns with highest energy
        CHARGE_BLINDING_FLASH = 653, // medium, blinds enemy team
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
        public int BaseDamage { get; }
        public int BaseHealing { get; }
        public FactionEnum AttackFaction { get; }
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
            AttackParticleEnum? enemyParticle, ParticleOriginEnum? enemyParticleOrigin, AttackParticleEnum? allyParticle, ParticleOriginEnum? allyParticleOrigin, bool isMelee,
            bool isPhysical, TargetType enemyTargetType, int enemyTargetCount, TargetType allyTargetType, int allyTargetCount, int baseDamage, int baseHealing, FactionEnum attackFaction,
            int attackerEnergyGained, int targetEnergyGained, int allyEnergyGained,
            StatusEnum? targetStatus, double targetStatusValue, int targetStatusDuration, StatusEnum? allyStatus, double allyStatusValue, int allyStatusDuration) {

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

            BaseDamage = baseDamage;
            BaseHealing = baseHealing;
            AttackFaction = attackFaction;

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
            var hasStab = attacker.baseHero.Faction == AttackFaction;
            var hitType = CombatMath.RollHitType(attacker, target);
            var hitEffectivity = CombatMath.GetEffectivity(AttackFaction, target.baseHero.Faction);
            var attackValue = IsPhysical ? attacker.GetModifiedStrength() : attacker.GetModifiedPower();
            var defenseValue = IsPhysical ? target.GetModifiedToughness() : target.GetModifiedResistance();
            var damage = CombatMath.Damage(attackValue, defenseValue, BaseDamage, hasStab, hitType, hitEffectivity);

            var shadyBranches = target.GetStatus(StatusEnum.SHADY_BRANCHES);
            if (shadyBranches != null && EnemyTargetCount > 1) {
                damage *= shadyBranches.value;
            }

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

            // TODO: Figure out status math.
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
                        if (TargetStatus == StatusEnum.CHILL) {
                            return allInstances;
                        }
                        break;
                }

                var inflictedStatus = TargetStatus.GetValueOrDefault();
                var statusValue = TargetStatusValue;
                var statusDuration = TargetStatusDuration;
                switch (inflictedStatus) {
                    case StatusEnum.BURN:
                        var burnPow = attacker.GetModifiedPower();
                        var burnDef = target.GetModifiedResistance();
                        statusValue = CombatMath.Damage(burnPow, burnDef, (int) TargetStatusValue, hasStab, hitType, hitEffectivity);
                        break;
                    case StatusEnum.BLEED:
                        var bleedPow = attacker.GetModifiedStrength();
                        var bleedDef = target.GetModifiedToughness();
                        statusValue = CombatMath.Damage(bleedPow, bleedDef, (int)TargetStatusValue, hasStab, hitType, hitEffectivity);
                        break;
                    case StatusEnum.POISON:
                        var poisonPow = attacker.GetModifiedPower();
                        var poisonDef = target.GetModifiedResistance();
                        statusValue = CombatMath.Damage(poisonPow, poisonDef, (int)TargetStatusValue, hasStab, hitType, hitEffectivity);
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
                    case StatusEnum.ROOT:
                        if (target.HasStatus(StatusEnum.DOWSE)) {
                            statusValue *= 2;
                        }
                        break;
                }

                if (target.baseHero.PassiveAbility == AbilityEnum.MENTAL_GYMNASTICS) {
                    if (inflictedStatus == StatusEnum.POWER_DOWN) {
                        inflictedStatus = StatusEnum.POWER_UP;
                    } else if (inflictedStatus == StatusEnum.STRENGTH_DOWN) {
                        inflictedStatus = StatusEnum.STRENGTH_UP;
                    } else if (inflictedStatus == StatusEnum.SPEED_DOWN) {
                        inflictedStatus = StatusEnum.SPEED_UP;
                    }
                }

                // This is going to break everything.
                if (target.baseHero.PassiveAbility == AbilityEnum.MIRROR_ICE && !IsPhysical) {
                    var statusContainer = new CombatStatus(inflictedStatus, target.combatHeroGuid, attacker.combatHeroGuid, statusValue, statusDuration, AttackFaction);
                    attacker.AddStatus(statusContainer);
                    var mirrorInstance = new CombatStep(null, null, target.combatHeroGuid, attacker.combatHeroGuid);
                    mirrorInstance.AddStatus(statusContainer);
                    allInstances.Add(mirrorInstance);
                } else {
                    var statusContainer = new CombatStatus(inflictedStatus, attacker.combatHeroGuid, target.combatHeroGuid, statusValue, statusDuration, AttackFaction);
                    target.AddStatus(statusContainer);
                    step.AddStatus(statusContainer);
                }
            }

            return allInstances;
        }

        public CombatStep ApplyAttackToAlly(CombatHero attacker, CombatHero ally) {
            var hasStab = attacker.baseHero.Faction == AttackFaction;
            var hitType = CombatMath.RollHitType(attacker);
            var attackValue = IsPhysical ? attacker.GetModifiedStrength() : attacker.GetModifiedPower();
            var healing = CombatMath.Healing(attackValue, attacker.currentLevel, BaseHealing, hasStab, hitType);
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

            // TODO: Figure out status math.
            if (AllyStatus != null) {
                var bestowedStatus = AllyStatus.GetValueOrDefault();
                var statusValue = AllyStatusValue;
                var statusDuration = AllyStatusDuration;
                switch (bestowedStatus) {
                    case StatusEnum.REGENERATION:
                        statusValue = CombatMath.Healing(attackValue, attacker.currentLevel, (int) AllyStatusValue, hasStab, hitType);
                        break;
                    case StatusEnum.LAVA_ARMOR:
                        statusValue = CombatMath.Damage(attacker.GetModifiedPower(), 1, (int) AllyStatusValue, hasStab, hitType, HitEffectivity.NORMAL);
                        break;
                    case StatusEnum.THORN_ARMOR:
                        statusValue = CombatMath.Damage(attacker.GetModifiedStrength(), 1, (int) AllyStatusValue, hasStab, hitType, HitEffectivity.NORMAL);
                        break;
                    default:
                        break;
                }
                var statusContainer = new CombatStatus(bestowedStatus, attacker.combatHeroGuid, ally.combatHeroGuid, statusValue, statusDuration, AttackFaction);
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
                string damageType = IsPhysical ? "physical" : "magic";
                string targetting = GetTargettingTooltip(EnemyTargetType, EnemyTargetCount, false);
                string firstPart = BaseDamage == 0 ? "Targets" : string.Format("Deals base {0} {1} damage to", BaseDamage, damageType);
                output = string.Format("{0} {1}.{2}", firstPart, targetting,
                    GetStatusTooltip(TargetStatus.GetValueOrDefault(), TargetStatusDuration, TargetStatusValue, false));
            }

            if (AllyTargetType != TargetType.NONE) {
                string damageType = IsPhysical ? "physical" : "magic";
                string targetting = GetTargettingTooltip(AllyTargetType, AllyTargetCount, true);
                string firstPart = BaseHealing == 0 ? "Targets" : string.Format("Deals base {0} {1} healing to", BaseHealing, damageType);
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
                case TargetType.FRONT_ROW_FIRST:
                    targetting = string.Format("all {0} on the front row", plural);
                    break;
                case TargetType.BACK_ROW_FIRST:
                    targetting = string.Format("all {0} on the back row", plural);
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

                case StatusEnum.STRENGTH_UP:
                case StatusEnum.STRENGTH_DOWN:
                case StatusEnum.POWER_UP:
                case StatusEnum.POWER_DOWN:
                case StatusEnum.TOUGHNESS_UP:
                case StatusEnum.TOUGHNESS_DOWN:
                case StatusEnum.RESISTANCE_UP:
                case StatusEnum.RESISTANCE_DOWN:
                case StatusEnum.SPEED_UP:
                case StatusEnum.SPEED_DOWN:
                    return GetStatModStatusTooltip(statusType, statusDuration, value, ally);

                case StatusEnum.DOWSE:
                    return string.Format(" Dowses for {0} {1} doubling penalties from Daze (electric only), Chill, and Root.", statusDuration, turnPlural);
                case StatusEnum.CHILL:
                    return string.Format(" Chills for {0} {1} reducing offenses and speed by {2}%.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.DAZE:
                    return string.Format(" Dazes for {0} {1} reducing offenses, crit, and deflection by {2}%.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.BLIND:
                    return string.Format(" Blinds for {0} {1} reducing offenses, crit, and deflection by {2}%.", statusDuration, turnPlural, statusValue);
                case StatusEnum.ROOT:
                    return string.Format(" Entangles for {0} {1} reducing offenses and speed by {2}%.", statusDuration, turnPlural, statusValue);

                // TODO: Figure out status math.
                case StatusEnum.REGENERATION:
                    return string.Format(" Bestows Regeneration for {0} {1}, healing with a base power of {2} per turn.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.THORN_ARMOR:
                    return string.Format(" Bestows Thorn Armor for {0} {1}.  Whenever a hero with thorn armor is attacked, the attacker takes damage with a base strength of {2}.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.LAVA_ARMOR:
                    return string.Format(" Bestows Lava Armor for {0} {1}.  Whenever a hero with lava armor is attacked, the attacker is burned for 2 turns, taking damage with a base power of {2} each turn.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.ICE_ARMOR:
                    return string.Format(" Bestows Ice Armor for {0} {1}.  Whenever a hero with ice armor is attacked, the attacker is chilled by {2}% for 2 turns.",
                        statusDuration, turnPlural, statusValue);
                case StatusEnum.EARTH_ARMOR:
                    return string.Format(" Bestows Earth Armor for {0} {1}, raising toughness by {2}.", statusDuration, turnPlural, statusValue);
                case StatusEnum.SHADY_BRANCHES:
                    return string.Format(" Bestows Shady Branches for {0} {1}, reducing damage taken from area attacks by {2}%.", statusDuration, turnPlural, statusValue);
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

            // No Faction, physical, single target
            attackDict[AttackEnum.BASIC_PUNCH] = new AttackInfo(AttackEnum.BASIC_PUNCH,
                "Basic Punch", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                50, 0, FactionEnum.NONE, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_KICK] = new AttackInfo(AttackEnum.BASIC_KICK,
                "Basic Kick", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                95, 0, FactionEnum.NONE, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_RUNNING_PUNCH] = new AttackInfo(AttackEnum.CHARGE_RUNNING_PUNCH,
                "Running Punch", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                110, 0, FactionEnum.NONE, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_FLYING_KICK] = new AttackInfo(AttackEnum.CHARGE_RUNNING_PUNCH,
                "Running Punch", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 0, TargetType.NONE, 0,
                180, 0, FactionEnum.NONE, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_RECKLESS_TACKLE] = new AttackInfo(AttackEnum.CHARGE_RECKLESS_TACKLE,
                "Reckless Tackle", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 0, TargetType.NONE, 0,
                230, 0, FactionEnum.NONE, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // No Faction, magic, single target
            attackDict[AttackEnum.BASIC_ENERGY_SHOT] = new AttackInfo(AttackEnum.BASIC_ENERGY_SHOT,
                "Energy Shot", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                50, 0, FactionEnum.NONE, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_ENERGY_BEAM] = new AttackInfo(AttackEnum.BASIC_ENERGY_BEAM,
                "Energy Beam", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                95, 0, FactionEnum.NONE, 50, 10, 0,
                null, 0, 0, null, 0, 0);

            // No Faction, magic, area
            attackDict[AttackEnum.CHARGE_LOUD_SCREAM] = new AttackInfo(AttackEnum.CHARGE_LOUD_SCREAM,
                "Energy Beam", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                45, 0, FactionEnum.NONE, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_PIERCING_SHRIEK] = new AttackInfo(AttackEnum.CHARGE_PIERCING_SHRIEK,
                "Energy Beam", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                75, 0, FactionEnum.NONE, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // No Faction, buff
            attackDict[AttackEnum.CHARGE_RALLYING_CHEER] = new AttackInfo(AttackEnum.CHARGE_RALLYING_CHEER,
                "Rallying Cheer", "Icons/RoleSupport", "AttackSounds/BasicMagic",
                null, null, AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, false,
                false, TargetType.NONE, 0, TargetType.FIRST_ALIVE, 5,
                0, 0, FactionEnum.NONE, -100, 0, 15,
                null, 0, 0, StatusEnum.OFFENSE_UP, 0.4, 2);
            attackDict[AttackEnum.CHARGE_RALLYING_CHEER] = new AttackInfo(AttackEnum.CHARGE_PLANT_FLAG,
                "Plant Flag", "Icons/RoleSupport", "AttackSounds/BasicMagic",
                null, null, AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, false,
                false, TargetType.NONE, 0, TargetType.FIRST_ALIVE, 5,
                0, 0, FactionEnum.NONE, -100, 0, 25,
                null, 0, 0, StatusEnum.OFFENSE_UP, 0.5, 3);

            // Water, physical, single target
            attackDict[AttackEnum.BASIC_FIN_SLAP] = new AttackInfo(AttackEnum.BASIC_FIN_SLAP,
                "Fin Slap", "Icons/Attacks/Slap", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                60, 0, FactionEnum.WATER, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_TAIL_SLAP] = new AttackInfo(AttackEnum.BASIC_TAIL_SLAP,
                "Tail Slap", "Icons/Attacks/Slap", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                110, 0, FactionEnum.WATER, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_BREACHING_CRASH] = new AttackInfo(AttackEnum.BASIC_BREACHING_CRASH,
                "Breaching Crash", "Icons/Attacks/WaterSplash", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                150, 0, FactionEnum.WATER, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_SPLASHING_LEAP] = new AttackInfo(AttackEnum.CHARGE_SPLASHING_LEAP,
                "Splashing Leap", "Icons/Attacks/WaterSplash", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                120, 0, FactionEnum.WATER, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_DIVE] = new AttackInfo(AttackEnum.CHARGE_DIVE,
                "Dive", "Icons/Attacks/CloudSwirl", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                200, 0, FactionEnum.WATER, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_DEPTH_CHARGE] = new AttackInfo(AttackEnum.CHARGE_DEPTH_CHARGE,
                "Depth Charge", "Icons/Attacks/CloudSwirl", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                260, 0, FactionEnum.WATER, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // Water, magic, single target
            attackDict[AttackEnum.BASIC_SPRAY] = new AttackInfo(AttackEnum.BASIC_SPRAY,
                "Spray", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                60, 0, FactionEnum.WATER, 50, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);
            attackDict[AttackEnum.BASIC_DELUGE] = new AttackInfo(AttackEnum.BASIC_DELUGE,
                "Deluge", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                110, 0, FactionEnum.WATER, 50, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);
            attackDict[AttackEnum.BASIC_TORRENT] = new AttackInfo(AttackEnum.BASIC_TORRENT,
                "Torrent", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                150, 0, FactionEnum.WATER, 50, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);
            attackDict[AttackEnum.CHARGE_WATER_SHOT] = new AttackInfo(AttackEnum.CHARGE_WATER_SHOT,
                "Water Shot", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                120, 0, FactionEnum.WATER, -100, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);
            attackDict[AttackEnum.CHARGE_WATER_GLOBE] = new AttackInfo(AttackEnum.CHARGE_WATER_GLOBE,
                "Water Globe", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                200, 0, FactionEnum.WATER, -100, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);
            attackDict[AttackEnum.CHARGE_WATER_CUTTER] = new AttackInfo(AttackEnum.CHARGE_WATER_CUTTER,
                "Water Cutter", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
                160, 0, FactionEnum.WATER, -100, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);
            attackDict[AttackEnum.CHARGE_PRESSURE_JET] = new AttackInfo(AttackEnum.CHARGE_PRESSURE_JET,
                "Pressure Jet", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                260, 0, FactionEnum.WATER, -100, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);
            attackDict[AttackEnum.CHARGE_LIQUIFY] = new AttackInfo(AttackEnum.CHARGE_LIQUIFY,
                "Liquify", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.LOWEST_HEALTH, 1, TargetType.NONE, 0,
                210, 0, FactionEnum.WATER, -100, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);

            // Water, physical, area
            attackDict[AttackEnum.CHARGE_FEEDING_FRENZY] = new AttackInfo(AttackEnum.CHARGE_FEEDING_FRENZY,
                "Feeding Frenzy", "Icons/Attacks/CloudSwirl", "AttackSounds/BasicPhysical",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                true, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                75, 0, FactionEnum.WATER, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_SHIPWRECK] = new AttackInfo(AttackEnum.CHARGE_SHIPWRECK,
                "Shipwreck", "Icons/Attacks/CloudSwirl", "AttackSounds/BasicPhysical",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                true, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                100, 0, FactionEnum.WATER, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // Water, magic, area
            attackDict[AttackEnum.BASIC_SPLASHING_WAVE] = new AttackInfo(AttackEnum.BASIC_SPLASHING_WAVE,
                "Splashing Wave", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FRONT_ROW_FIRST, 5, TargetType.NONE, 0,
                65, 0, FactionEnum.WATER, 50, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);
            attackDict[AttackEnum.BASIC_WHIRLPOOL] = new AttackInfo(AttackEnum.BASIC_WHIRLPOOL,
                "Whirlpool", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                90, 0, FactionEnum.WATER, 50, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);
            attackDict[AttackEnum.CHARGE_TIDAL_WAVE] = new AttackInfo(AttackEnum.CHARGE_TIDAL_WAVE,
                "Tidal Wave", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FRONT_ROW_FIRST, 5, TargetType.NONE, 0,
                120, 0, FactionEnum.WATER, -100, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);
            attackDict[AttackEnum.CHARGE_TSUNAMI] = new AttackInfo(AttackEnum.CHARGE_TSUNAMI,
                "Tsunami", "Icons/Attacks/WaterSplash", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                155, 0, FactionEnum.WATER, -100, 10, 0,
                StatusEnum.DOWSE, 1, 1, null, 0, 0);

            // Water, healing
            attackDict[AttackEnum.BASIC_HEALING_MIST] = new AttackInfo(AttackEnum.BASIC_HEALING_MIST,
                "Healing Mist", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 2,
                0, 65, FactionEnum.WATER, 50, 0, 10,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_HEALING_RAIN] = new AttackInfo(AttackEnum.BASIC_HEALING_RAIN,
                "Healing Rain", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 50, FactionEnum.WATER, 50, 0, 10,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_HEALING_WAVE] = new AttackInfo(AttackEnum.CHARGE_HEALING_WAVE,
                "Healing Wave", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 1,
                0, 120, FactionEnum.WATER, -100, 0, 10,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_HEALING_DELUGE] = new AttackInfo(AttackEnum.CHARGE_HEALING_DELUGE,
                "Healing Deluge", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 1,
                0, 200, FactionEnum.WATER, -100, 0, 10,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_HEALING_TORRENT] = new AttackInfo(AttackEnum.CHARGE_HEALING_TORRENT,
                "Healing Torrent", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.WATER, ParticleOriginEnum.ATTACKER, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 1,
                0, 260, FactionEnum.WATER, -100, 0, 10,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_CLEANSING_MIST] = new AttackInfo(AttackEnum.CHARGE_CLEANSING_MIST,
                "Cleansing Mist", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 90, FactionEnum.WATER, -100, 0, 10,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_CLEANSING_RAIN] = new AttackInfo(AttackEnum.CHARGE_CLEANSING_RAIN,
                "Cleansing Rain", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 100, FactionEnum.WATER, -100, 0, 10,
                null, 0, 0, null, 0, 0);

            // Water, debuff
            attackDict[AttackEnum.CREATE_BOG] = new AttackInfo(AttackEnum.CREATE_BOG,
                "Create Bog", "Icons/Attacks/CloudSwirl", "AttackSounds/VaporCloud",
                AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                0, 0, FactionEnum.WATER, -100, 10, 0,
                StatusEnum.SPEED_DOWN, 0.25, 2, null, 0, 0);

            // Water, buff
            attackDict[AttackEnum.CHARGE_ENSCALE] = new AttackInfo(AttackEnum.CHARGE_ENSCALE,
                "Enscale", "Icons/Attacks/WaterScale", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 0, FactionEnum.WATER, -100, 0, 10,
                null, 0, 0, StatusEnum.TOUGHNESS_UP, 0.5, 2);
            attackDict[AttackEnum.CHARGE_FAVORABLE_CURRENT] = new AttackInfo(AttackEnum.CHARGE_FAVORABLE_CURRENT,
                "Favorable Current", "Icons/Attacks/WaterSplash", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.WATER, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 0, FactionEnum.WATER, -100, 0, 10,
                null, 0, 0, StatusEnum.SPEED_UP, 0.25, 0);

            // Grass, physical, single target
            attackDict[AttackEnum.BASIC_PETAL_SLAP] = new AttackInfo(AttackEnum.BASIC_PETAL_SLAP,
                "Petal Slap", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                60, 0, FactionEnum.GRASS, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_BRANCH_SLAP] = new AttackInfo(AttackEnum.BASIC_BRANCH_SLAP,
                "Branch Slap", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                110, 0, FactionEnum.GRASS, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_RAZOR_VINE] = new AttackInfo(AttackEnum.BASIC_RAZOR_VINE,
                "Razor Vine", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                150, 0, FactionEnum.GRASS, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_WEED_WHACKER] = new AttackInfo(AttackEnum.CHARGE_WEED_WHACKER,
                "Weed Whacker", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                120, 0, FactionEnum.GRASS, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_CABER_TOSS] = new AttackInfo(AttackEnum.CHARGE_CABER_TOSS,
                "Caber Toss", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                200, 0, FactionEnum.GRASS, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_TIMBER] = new AttackInfo(AttackEnum.CHARGE_TIMBER,
                "Timber", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                260, 0, FactionEnum.GRASS, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // Grass, magic, single target
            attackDict[AttackEnum.BASIC_NEEDLE_SHOT] = new AttackInfo(AttackEnum.BASIC_NEEDLE_SHOT,
                "Fin Slap", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                60, 0, FactionEnum.GRASS, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_GRAPE_SHOT] = new AttackInfo(AttackEnum.BASIC_GRAPE_SHOT,
                "Tail Slap", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                110, 0, FactionEnum.GRASS, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_COCONUT_CATAPULT] = new AttackInfo(AttackEnum.BASIC_COCONUT_CATAPULT,
                "Breaching Crash", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                150, 0, FactionEnum.GRASS, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_BERRY_BLAST] = new AttackInfo(AttackEnum.CHARGE_BERRY_BLAST,
                "Splashing Leap", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                120, 0, FactionEnum.GRASS, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_SALAD_TOSS] = new AttackInfo(AttackEnum.CHARGE_SALAD_TOSS,
                "Dive", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                200, 0, FactionEnum.GRASS, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_MOONBEAM] = new AttackInfo(AttackEnum.CHARGE_MOONBEAM,
                "Depth Charge", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                260, 0, FactionEnum.GRASS, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // Grass, physical, area
            attackDict[AttackEnum.BASIC_WHIRLING_BRANCHES] = new AttackInfo(AttackEnum.BASIC_WHIRLING_BRANCHES,
                "Whirling Branches", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                true, TargetType.FIRST_ALIVE, 2, TargetType.NONE, 0,
                65, 0, FactionEnum.GRASS, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_LOG_ROLL] = new AttackInfo(AttackEnum.BASIC_LOG_ROLL,
                "Log Roll", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                true, TargetType.FRONT_ROW_FIRST, 5, TargetType.NONE, 0,
                90, 0, FactionEnum.GRASS, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_NEEDLE_SPRAY] = new AttackInfo(AttackEnum.CHARGE_NEEDLE_SPRAY,
                "Needle Spray", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                true, TargetType.FRONT_ROW_FIRST, 5, TargetType.NONE, 0,
                120, 0, FactionEnum.GRASS, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_STRANGLING_VINES] = new AttackInfo(AttackEnum.CHARGE_STRANGLING_VINES,
                "Strangling Vines", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                true, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                105, 0, FactionEnum.GRASS, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // Grass, magic, area
            attackDict[AttackEnum.CHARGE_PETAL_STORM] = new AttackInfo(AttackEnum.CHARGE_PETAL_STORM,
                "Needle Spray", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                80, 0, FactionEnum.GRASS, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_LEAF_WHIRLWIND] = new AttackInfo(AttackEnum.CHARGE_LEAF_WHIRLWIND,
                "Strangling Vines", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                105, 0, FactionEnum.GRASS, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // Grass, healing
            attackDict[AttackEnum.BASIC_REGROW] = new AttackInfo(AttackEnum.BASIC_REGROW,
                "Regrow", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 2,
                0, 25, FactionEnum.GRASS, 50, 0, 10,
                null, 0, 0, StatusEnum.REGENERATION, 25, 1);
            attackDict[AttackEnum.BASIC_REJUVENATE] = new AttackInfo(AttackEnum.BASIC_REJUVENATE,
                "Rejuvenate", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 3,
                0, 25, FactionEnum.GRASS, 50, 0, 10,
                null, 0, 0, StatusEnum.REGENERATION, 25, 4);
            attackDict[AttackEnum.BASIC_REVITALIZE] = new AttackInfo(AttackEnum.BASIC_REVITALIZE,
                "Revitalize", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 25, FactionEnum.GRASS, 50, 0, 10,
                null, 0, 0, StatusEnum.REGENERATION, 25, 4);
            attackDict[AttackEnum.CHARGE_PEACEFUL_MEADOW] = new AttackInfo(AttackEnum.CHARGE_PEACEFUL_MEADOW,
                "Peaceful Meadow", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 25, FactionEnum.GRASS, -100, 0, 10,
                null, 0, 0, StatusEnum.REGENERATION, 25, 2);
            attackDict[AttackEnum.CHARGE_TRANQUIL_GROVE] = new AttackInfo(AttackEnum.CHARGE_TRANQUIL_GROVE,
                "Tranquil Grove", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.ATTACKER, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 35, FactionEnum.GRASS, -100, 0, 10,
                null, 0, 0, StatusEnum.REGENERATION, 35, 4);
            attackDict[AttackEnum.CHARGE_SERENE_FOREST] = new AttackInfo(AttackEnum.CHARGE_SERENE_FOREST,
                "Serene Forest", "Icons/Attacks/HealingWave", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 45, FactionEnum.GRASS, -100, 0, 10,
                null, 0, 0, StatusEnum.REGENERATION, 45, 4);

            // Grass, debuff
            attackDict[AttackEnum.CHARGE_INVOKE_ALLERGIES] = new AttackInfo(AttackEnum.CHARGE_INVOKE_ALLERGIES,
                "Invoke Allergies", "Icons/Attacks/CloudSwirl", "AttackSounds/VaporCloud",
                AttackParticleEnum.GRASS, ParticleOriginEnum.OVERHEAD, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                35, 0, FactionEnum.GRASS, -100, 10, 0,
                StatusEnum.POISON, 35, 4, null, 0, 0);
            attackDict[AttackEnum.CHARGE_TOXIC_SPORES] = new AttackInfo(AttackEnum.CHARGE_TOXIC_SPORES,
                "Toxic Spores", "Icons/Attacks/CloudSwirl", "AttackSounds/VaporCloud",
                AttackParticleEnum.GRASS, ParticleOriginEnum.OVERHEAD, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                45, 0, FactionEnum.GRASS, -100, 10, 0,
                StatusEnum.POISON, 45, 3, null, 0, 0);

            // Grass, buff
            attackDict[AttackEnum.CHARGE_BARKSKIN] = new AttackInfo(AttackEnum.CHARGE_BARKSKIN,
                "Enscale", "Icons/Attacks/WaterScale", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.SELF, 1,
                0, 0, FactionEnum.GRASS, -100, 0, 10,
                null, 0, 0, StatusEnum.TOUGHNESS_UP, 1, 2);
            attackDict[AttackEnum.CHARGE_THORN_ARMOR] = new AttackInfo(AttackEnum.CHARGE_THORN_ARMOR,
                "Enscale", "Icons/Attacks/WaterScale", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 0, FactionEnum.GRASS, -100, 0, 10,
                null, 0, 0, StatusEnum.THORN_ARMOR, 100, 2);
            attackDict[AttackEnum.CHARGE_SHADY_BRANCHES] = new AttackInfo(AttackEnum.CHARGE_SHADY_BRANCHES,
                "Favorable Current", "Icons/Attacks/WaterSplash", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.GRASS, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 0, FactionEnum.GRASS, -100, 0, 10,
                null, 0, 0, StatusEnum.SHADY_BRANCHES, 0.5, 0);

            // Fire, physical, single taget
            attackDict[AttackEnum.BASIC_BURNING_FIST] = new AttackInfo(AttackEnum.BASIC_BURNING_FIST,
                "Burning Fist", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                60, 0, FactionEnum.FIRE, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_BLAZING_FIST] = new AttackInfo(AttackEnum.BASIC_BLAZING_FIST,
                "Blazing Fist", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                110, 0, FactionEnum.FIRE, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.BASIC_JET_TACKLE] = new AttackInfo(AttackEnum.BASIC_JET_TACKLE,
                "Jet Tackle", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                150, 0, FactionEnum.FIRE, 50, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_BURNING_STONE] = new AttackInfo(AttackEnum.CHARGE_BURNING_STONE,
                "Burning Stone", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                120, 0, FactionEnum.FIRE, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_BURNING_BOULDER] = new AttackInfo(AttackEnum.CHARGE_BURNING_BOULDER,
                "Burning Boulder", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                200, 0, FactionEnum.FIRE, -100, 10, 0,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_METEOR] = new AttackInfo(AttackEnum.CHARGE_METEOR,
                "Meteor", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                null, null, null, null, true,
                true, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                260, 0, FactionEnum.FIRE, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // Fire, magic, single target
            attackDict[AttackEnum.BASIC_SINGE] = new AttackInfo(AttackEnum.BASIC_SINGE,
                "Singe", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                30, 0, FactionEnum.FIRE, 50, 10, 0,
                StatusEnum.BURN, 30, 2, null, 0, 0);
            attackDict[AttackEnum.BASIC_SCORCH] = new AttackInfo(AttackEnum.BASIC_SCORCH,
                "Scorch", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                55, 0, FactionEnum.FIRE, 50, 10, 0,
                StatusEnum.BURN, 55, 2, null, 0, 0);
            attackDict[AttackEnum.BASIC_IMMOLATE] = new AttackInfo(AttackEnum.BASIC_IMMOLATE,
                "Immolate", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                75, 0, FactionEnum.FIRE, 50, 10, 0,
                StatusEnum.BURN, 55, 2, null, 0, 0);
            attackDict[AttackEnum.CHARGE_BLAZE] = new AttackInfo(AttackEnum.CHARGE_BLAZE,
                "Blaze", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                60, 0, FactionEnum.FIRE, -100, 10, 0,
                StatusEnum.BURN, 60, 2, null, 0, 0);
            attackDict[AttackEnum.CHARGE_INCINERATE] = new AttackInfo(AttackEnum.CHARGE_INCINERATE,
                "Incinerate", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                100, 0, FactionEnum.FIRE, -100, 10, 0,
                StatusEnum.BURN, 100, 2, null, 0, 0);
            attackDict[AttackEnum.CHARGE_INFERNO] = new AttackInfo(AttackEnum.CHARGE_INFERNO,
                "Inferno", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 1, TargetType.NONE, 0,
                100, 0, FactionEnum.FIRE, -100, 10, 0,
                StatusEnum.BURN, 100, 4, null, 0, 0);

            // Fire, physical, area
            attackDict[AttackEnum.CHARGE_ERUPTION] = new AttackInfo(AttackEnum.CHARGE_ERUPTION,
                "Eruption", "Icons/RoleDamage", "AttackSounds/BasicPhysical",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                true, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                105, 0, FactionEnum.FIRE, -100, 10, 0,
                null, 0, 0, null, 0, 0);

            // Fire, magic, area
            attackDict[AttackEnum.BASIC_FIRE_BREATH] = new AttackInfo(AttackEnum.BASIC_FIRE_BREATH,
                "Fire Breath", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FRONT_ROW_FIRST, 5, TargetType.NONE, 0,
                20, 0, FactionEnum.FIRE, 50, 10, 0,
                StatusEnum.BURN, 20, 2, null, 0, 0);
            attackDict[AttackEnum.BASIC_LAVA_WAVE] = new AttackInfo(AttackEnum.BASIC_LAVA_WAVE,
                "Laval Wave", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FRONT_ROW_FIRST, 5, TargetType.NONE, 0,
                35, 0, FactionEnum.FIRE, 50, 10, 0,
                StatusEnum.BURN, 35, 2, null, 0, 0);
            attackDict[AttackEnum.BASIC_SEARING_WIND] = new AttackInfo(AttackEnum.BASIC_SEARING_WIND,
                "Searing Wind", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                30, 0, FactionEnum.FIRE, 50, 10, 0,
                StatusEnum.BURN, 30, 2, null, 0, 0);
            attackDict[AttackEnum.CHARGE_TWIN_FLAME] = new AttackInfo(AttackEnum.CHARGE_TWIN_FLAME,
                "Twin Flame", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.RANDOM, 2, TargetType.NONE, 0,
                35, 0, FactionEnum.FIRE, -100, 10, 0,
                StatusEnum.BURN, 35, 2, null, 0, 0);
            attackDict[AttackEnum.CHARGE_FIREBALL] = new AttackInfo(AttackEnum.CHARGE_FIREBALL,
                "Fireball", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                40, 0, FactionEnum.FIRE, -100, 10, 0,
                StatusEnum.BURN, 40, 2, null, 0, 0);
            attackDict[AttackEnum.CHARGE_EXPLOSION] = new AttackInfo(AttackEnum.CHARGE_EXPLOSION,
                "Explosion", "Icons/RoleDamage", "AttackSounds/BasicMagic",
                AttackParticleEnum.FIRE, ParticleOriginEnum.ATTACKER, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                45, 0, FactionEnum.FIRE, -100, 10, 0,
                StatusEnum.BURN, 45, 4, null, 0, 0);

            // Fire, buff
            attackDict[AttackEnum.BASIC_KINDLE] = new AttackInfo(AttackEnum.BASIC_KINDLE,
                "Kindle", "Icons/Attacks/WaterScale", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_ENERGY, 2,
                0, 0, FactionEnum.FIRE, 30, 0, 30,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_STOKE_FLAMES] = new AttackInfo(AttackEnum.CHARGE_STOKE_FLAMES,
                "Stoke Flames", "Icons/Attacks/WaterScale", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_ENERGY, 5,
                0, 0, FactionEnum.FIRE, -100, 0, 50,
                null, 0, 0, null, 0, 0);
            attackDict[AttackEnum.CHARGE_BURNING_HASTE] = new AttackInfo(AttackEnum.CHARGE_BURNING_HASTE,
                "Burning Haste", "Icons/Attacks/WaterSplash", "AttackSounds/WaterRenew",
                null, null, AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD, false,
                false, TargetType.NONE, 0, TargetType.LOWEST_HEALTH, 5,
                0, 0, FactionEnum.FIRE, -100, 0, 10,
                null, 0, 0, StatusEnum.SPEED_UP, 0.25, 0);

            // Fire, debuff
            attackDict[AttackEnum.CHARGE_ASH_CLOUD] = new AttackInfo(AttackEnum.CHARGE_ASH_CLOUD,
                "Ash Cloud", "Icons/Attacks/CloudSwirl", "AttackSounds/VaporCloud",
                AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD, null, null, false,
                false, TargetType.FIRST_ALIVE, 5, TargetType.NONE, 0,
                30, 0, FactionEnum.FIRE, -100, 10, 0,
                StatusEnum.BLIND, 0.2, 3, null, 0, 0);
            attackDict[AttackEnum.CHARGE_MELT_ARMOR] = new AttackInfo(AttackEnum.CHARGE_MELT_ARMOR,
                "Melt Armor", "Icons/Attacks/CloudSwirl", "AttackSounds/VaporCloud",
                AttackParticleEnum.FIRE, ParticleOriginEnum.OVERHEAD, null, null, false,
                false, TargetType.FRONT_ROW_FIRST, 5, TargetType.NONE, 0,
                50, 0, FactionEnum.FIRE, -100, 10, 0,
                StatusEnum.DEFENSE_DOWN, 0.4, 3, null, 0, 0);
        }

        public static AttackInfo GetAttackInfo(AttackEnum attack) {
            if (attackDict == null) Initialize();
            return attackDict[attack];
        }
    }
}
