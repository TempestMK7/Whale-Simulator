using System;
using System.Collections.Generic;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.GameObjects {

    public class MissionContainer {

        private static readonly MissionInfo[] chapter1 = {
        new MissionInfo(1, 1, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD),
        new MissionInfo(2, 1, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER),
        new MissionInfo(3, 1, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN),
        new MissionInfo(4, 1, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN),
        new MissionInfo(5, 1, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING),
        new MissionInfo(6, 1, HeroEnum.DUST_ELEMENTAL, HeroEnum.DUST_ELEMENTAL, HeroEnum.DUST_ELEMENTAL, HeroEnum.DUST_ELEMENTAL),
        new MissionInfo(7, 1, HeroEnum.RAIN_MAN, HeroEnum.RAIN_MAN, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD),
        new MissionInfo(8, 1, HeroEnum.BUSH_WHACKER, HeroEnum.BUSH_WHACKER, HeroEnum.BUSH_WHACKER, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER),
        new MissionInfo(9, 1, HeroEnum.EMBER, HeroEnum.EMBER, HeroEnum.EMBER, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN),
        new MissionInfo(10, 1, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD)
    };

        private static readonly MissionInfo[] chapter2 = {
        new MissionInfo(11, 1, HeroEnum.ICE_CUBE, HeroEnum.ICE_CUBE, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN),
        new MissionInfo(12, 1, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING),
        new MissionInfo(13, 1, HeroEnum.BOULDER, HeroEnum.BOULDER, HeroEnum.BOULDER, HeroEnum.DUST_ELEMENTAL, HeroEnum.DUST_ELEMENTAL, HeroEnum.DUST_ELEMENTAL),
        new MissionInfo(14, 1, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD),
        new MissionInfo(15, 1, HeroEnum.BALL_OF_ROOTS, HeroEnum.BALL_OF_ROOTS, HeroEnum.BUSH_WHACKER, HeroEnum.HAPPY_FLOWER, HeroEnum.BUSH_WHACKER, HeroEnum.HAPPY_FLOWER),
        new MissionInfo(16, 1, HeroEnum.TORCH, HeroEnum.TORCH, HeroEnum.EMBER, HeroEnum.CANDLE_MAN, HeroEnum.EMBER, HeroEnum.CANDLE_MAN),
        new MissionInfo(17, 1, HeroEnum.ICICLE_FLINGER, HeroEnum.ICE_CUBE, HeroEnum.ICE_CUBE, HeroEnum.SNOW_MAN, HeroEnum.ICICLE_FLINGER, HeroEnum.SNOW_MAN),
        new MissionInfo(18, 1, HeroEnum.SPARK_ELEMENTAL, HeroEnum.SPARK_ELEMENTAL, HeroEnum.BATTERY, HeroEnum.STATIC_CLING, HeroEnum.BATTERY, HeroEnum.STATIC_CLING),
        new MissionInfo(19, 1, HeroEnum.PEBBLE_FLINGER, HeroEnum.BOULDER, HeroEnum.BOULDER, HeroEnum.DUST_ELEMENTAL, HeroEnum.PEBBLE_FLINGER, HeroEnum.DUST_ELEMENTAL),
        new MissionInfo(20, 1, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.HAPPY_FLOWER, HeroEnum.CANDLE_MAN, HeroEnum.EMBER, HeroEnum.BUSH_WHACKER)
    };

        private static readonly MissionInfo[] chapter3 = {
        new MissionInfo(21, 2, HeroEnum.RAIN_MAN, HeroEnum.ICE_CUBE, HeroEnum.SNOW_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.ICICLE_FLINGER, HeroEnum.RAIN_MAN),
        new MissionInfo(22, 2, HeroEnum.BALL_OF_ROOTS, HeroEnum.BALL_OF_ROOTS, HeroEnum.BOULDER, HeroEnum.HAPPY_FLOWER, HeroEnum.PEBBLE_FLINGER, HeroEnum.BUSH_WHACKER),
        new MissionInfo(23, 2, HeroEnum.TORCH, HeroEnum.TORCH, HeroEnum.EMBER, HeroEnum.BATTERY, HeroEnum.SPARK_ELEMENTAL, HeroEnum.STATIC_CLING),
        new MissionInfo(24, 2, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.HAPPY_FLOWER, HeroEnum.VAPOR_CLOUD, HeroEnum.RAIN_MAN, HeroEnum.BUSH_WHACKER),
        new MissionInfo(25, 2, HeroEnum.BUSH_WHACKER, HeroEnum.BALL_OF_ROOTS, HeroEnum.EMBER, HeroEnum.HAPPY_FLOWER, HeroEnum.BUSH_WHACKER, HeroEnum.TORCH),
        new MissionInfo(26, 2, HeroEnum.EMBER, HeroEnum.ICE_CUBE, HeroEnum.EMBER, HeroEnum.SNOW_MAN, HeroEnum.TORCH, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(27, 2, HeroEnum.ICICLE_FLINGER, HeroEnum.ICE_CUBE, HeroEnum.BATTERY, HeroEnum.SNOW_MAN, HeroEnum.SPARK_ELEMENTAL, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(28, 2, HeroEnum.SPARK_ELEMENTAL, HeroEnum.BOULDER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.STATIC_CLING, HeroEnum.PEBBLE_FLINGER, HeroEnum.BATTERY),
        new MissionInfo(29, 2, HeroEnum.BOULDER, HeroEnum.BOULDER, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.PEBBLE_FLINGER, HeroEnum.DUST_ELEMENTAL),
        new MissionInfo(30, 2, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BALL_OF_ROOTS, HeroEnum.FEESH, HeroEnum.LIGHTNING_WRAITH, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH)
    };

        private static readonly MissionInfo[] chapter4 = {
        new MissionInfo(31, 2, HeroEnum.MIST_CALLER, HeroEnum.FEESH, HeroEnum.VAPOR_CLOUD, HeroEnum.RAIN_MAN, HeroEnum.MIST_CALLER, HeroEnum.RAIN_MAN),
        new MissionInfo(32, 2, HeroEnum.DRYAD, HeroEnum.BALL_OF_ROOTS, HeroEnum.HAPPY_FLOWER, HeroEnum.BUSH_WHACKER, HeroEnum.DRYAD, HeroEnum.BUSH_WHACKER),
        new MissionInfo(33, 2, HeroEnum.LAVA_GOLEM, HeroEnum.LAVA_GOLEM, HeroEnum.EMBER, HeroEnum.CANDLE_MAN, HeroEnum.TORCH, HeroEnum.CANDLE_MAN),
        new MissionInfo(34, 2, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICE_CUBE, HeroEnum.ICE_CUBE, HeroEnum.ICICLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.SNOW_MAN),
        new MissionInfo(35, 2, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.SPARK_ELEMENTAL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.STATIC_CLING),
        new MissionInfo(36, 2, HeroEnum.LIVING_WALL, HeroEnum.LIVING_WALL, HeroEnum.BOULDER, HeroEnum.DUST_ELEMENTAL, HeroEnum.PEBBLE_FLINGER, HeroEnum.BOULDER),
        new MissionInfo(37, 2, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.MIST_CALLER, HeroEnum.ICICLE_FLINGER, HeroEnum.RAIN_MAN, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(38, 2, HeroEnum.BALL_OF_ROOTS, HeroEnum.BALL_OF_ROOTS, HeroEnum.LIVING_WALL, HeroEnum.BUSH_WHACKER, HeroEnum.DRYAD, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(39, 2, HeroEnum.TORCH, HeroEnum.LAVA_GOLEM, HeroEnum.BATTERY, HeroEnum.EMBER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.TORCH),
        new MissionInfo(40, 2, HeroEnum.EARTHZERKER, HeroEnum.EARTHZERKER, HeroEnum.SNOW_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.HAPPY_FLOWER, HeroEnum.CANDLE_MAN)
    };

        private static readonly MissionInfo[] chapter5 = {
        new MissionInfo(41, 3, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.BUSH_WHACKER),
        new MissionInfo(42, 3, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.EMBER, HeroEnum.DRYAD, HeroEnum.TORCH, HeroEnum.DRYAD),
        new MissionInfo(43, 3, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.INFERNOMANCER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH),
        new MissionInfo(44, 3, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.PEBBLE_FLINGER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(45, 3, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.EARTHZERKER, HeroEnum.BATTERY),
        new MissionInfo(46, 3, HeroEnum.NEUROMANCER, HeroEnum.FEESH, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(47, 3, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(48, 3, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BOULDER, HeroEnum.PEBBLE_FLINGER, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(49, 3, HeroEnum.NEUROMANCER, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.LIGHTNING_WRAITH, HeroEnum.NEUROMANCER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(50, 3, HeroEnum.ORACLE, HeroEnum.ANGERY_TREANT, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD)
    };

        private static readonly MissionInfo[] chapter6 = {
        new MissionInfo(51, 3, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.SPARK_ELEMENTAL, HeroEnum.MIST_CALLER, HeroEnum.LIGHTNING_WRAITH),
        new MissionInfo(52, 3, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD),
        new MissionInfo(53, 3, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BALL_OF_ROOTS, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD, HeroEnum.TORCH),
        new MissionInfo(54, 3, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.EMBER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(55, 3, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.EARTHZERKER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(56, 3, HeroEnum.NEUROMANCER, HeroEnum.LIVING_WALL, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.PEBBLE_FLINGER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(57, 3, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER),
        new MissionInfo(58, 3, HeroEnum.LAVA_GOLEM, HeroEnum.LAVA_GOLEM, HeroEnum.EMBER, HeroEnum.TORCH, HeroEnum.INFERNOMANCER, HeroEnum.TORCH),
        new MissionInfo(59, 3, HeroEnum.DRYAD, HeroEnum.ANGERY_TREANT, HeroEnum.HAPPY_FLOWER, HeroEnum.DRYAD, HeroEnum.DRYAD, HeroEnum.DRYAD),
        new MissionInfo(60, 3, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.NEUROMANCER, HeroEnum.PEBBLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.LIGHTNING_WRAITH)
    };

        private static readonly MissionInfo[] chapter7 = {
        new MissionInfo(61, 4, HeroEnum.ORACLE, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.TORCH),
        new MissionInfo(62, 4, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(63, 4, HeroEnum.INFERNOMANCER, HeroEnum.LIVING_WALL, HeroEnum.EMBER, HeroEnum.PEBBLE_FLINGER, HeroEnum.INFERNOMANCER, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(64, 4, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(65, 4, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.MIST_CALLER, HeroEnum.EARTHZERKER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(66, 4, HeroEnum.NEUROMANCER, HeroEnum.BALL_OF_ROOTS, HeroEnum.NEUROMANCER, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.DRYAD),
        new MissionInfo(67, 4, HeroEnum.ORACLE, HeroEnum.REFLECTOR, HeroEnum.ORACLE, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.RAIN_MAN),
        new MissionInfo(68, 4, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BOULDER, HeroEnum.DRYAD, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(69, 4, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.NEUROMANCER, HeroEnum.INFERNOMANCER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.EMBER),
        new MissionInfo(70, 4, HeroEnum.REFLECTOR, HeroEnum.ANGERY_TREANT, HeroEnum.NEUROMANCER, HeroEnum.REFLECTOR, HeroEnum.EARTHZERKER, HeroEnum.INFERNOMANCER)
    };

        private static readonly MissionInfo[] chapter8 = {
        new MissionInfo(71, 4, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.BUSH_WHACKER),
        new MissionInfo(72, 4, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.EMBER, HeroEnum.DRYAD, HeroEnum.TORCH, HeroEnum.DRYAD),
        new MissionInfo(73, 4, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.INFERNOMANCER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH),
        new MissionInfo(74, 4, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.PEBBLE_FLINGER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(75, 4, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.EARTHZERKER, HeroEnum.BATTERY),
        new MissionInfo(76, 4, HeroEnum.NEUROMANCER, HeroEnum.FEESH, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(77, 4, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(78, 4, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BOULDER, HeroEnum.PEBBLE_FLINGER, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(79, 4, HeroEnum.NEUROMANCER, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.LIGHTNING_WRAITH, HeroEnum.NEUROMANCER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(80, 4, HeroEnum.ORACLE, HeroEnum.ANGERY_TREANT, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD)
    };

        private static readonly MissionInfo[] chapter9 = {
        new MissionInfo(81, 5, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.SPARK_ELEMENTAL, HeroEnum.MIST_CALLER, HeroEnum.LIGHTNING_WRAITH),
        new MissionInfo(82, 5, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD),
        new MissionInfo(83, 5, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BALL_OF_ROOTS, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD, HeroEnum.TORCH),
        new MissionInfo(84, 5, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.EMBER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(85, 5, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.EARTHZERKER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(86, 5, HeroEnum.NEUROMANCER, HeroEnum.LIVING_WALL, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.PEBBLE_FLINGER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(87, 5, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER),
        new MissionInfo(88, 5, HeroEnum.LAVA_GOLEM, HeroEnum.LAVA_GOLEM, HeroEnum.EMBER, HeroEnum.TORCH, HeroEnum.INFERNOMANCER, HeroEnum.TORCH),
        new MissionInfo(89, 5, HeroEnum.DRYAD, HeroEnum.ANGERY_TREANT, HeroEnum.HAPPY_FLOWER, HeroEnum.DRYAD, HeroEnum.DRYAD, HeroEnum.DRYAD),
        new MissionInfo(90, 5, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.NEUROMANCER, HeroEnum.PEBBLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.LIGHTNING_WRAITH)
    };

        private static readonly MissionInfo[] chapter10 = {
        new MissionInfo(91, 5, HeroEnum.ORACLE, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.TORCH),
        new MissionInfo(92, 5, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(93, 5, HeroEnum.INFERNOMANCER, HeroEnum.LIVING_WALL, HeroEnum.EMBER, HeroEnum.PEBBLE_FLINGER, HeroEnum.INFERNOMANCER, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(94, 5, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(95, 5, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.MIST_CALLER, HeroEnum.EARTHZERKER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(96, 5, HeroEnum.NEUROMANCER, HeroEnum.BALL_OF_ROOTS, HeroEnum.NEUROMANCER, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.DRYAD),
        new MissionInfo(97, 5, HeroEnum.ORACLE, HeroEnum.REFLECTOR, HeroEnum.ORACLE, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.RAIN_MAN),
        new MissionInfo(98, 5, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BOULDER, HeroEnum.DRYAD, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(99, 5, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.NEUROMANCER, HeroEnum.INFERNOMANCER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.EMBER),
        new MissionInfo(100, 5, HeroEnum.REFLECTOR, HeroEnum.ANGERY_TREANT, HeroEnum.NEUROMANCER, HeroEnum.REFLECTOR, HeroEnum.EARTHZERKER, HeroEnum.INFERNOMANCER)
    };

        private static readonly MissionInfo[] chapter11 = {
        new MissionInfo(101, 6, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.BUSH_WHACKER),
        new MissionInfo(102, 6, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.EMBER, HeroEnum.DRYAD, HeroEnum.TORCH, HeroEnum.DRYAD),
        new MissionInfo(103, 6, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.INFERNOMANCER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH),
        new MissionInfo(104, 6, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.PEBBLE_FLINGER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(105, 6, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.EARTHZERKER, HeroEnum.BATTERY),
        new MissionInfo(106, 6, HeroEnum.NEUROMANCER, HeroEnum.FEESH, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(107, 6, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(108, 6, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BOULDER, HeroEnum.PEBBLE_FLINGER, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(109, 6, HeroEnum.NEUROMANCER, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.LIGHTNING_WRAITH, HeroEnum.NEUROMANCER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(110, 6, HeroEnum.ORACLE, HeroEnum.ANGERY_TREANT, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD)
    };

        private static readonly MissionInfo[] chapter12 = {
        new MissionInfo(111, 6, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.SPARK_ELEMENTAL, HeroEnum.MIST_CALLER, HeroEnum.LIGHTNING_WRAITH),
        new MissionInfo(112, 6, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD),
        new MissionInfo(113, 6, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BALL_OF_ROOTS, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD, HeroEnum.TORCH),
        new MissionInfo(114, 6, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.EMBER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(115, 6, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.EARTHZERKER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(116, 6, HeroEnum.NEUROMANCER, HeroEnum.LIVING_WALL, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.PEBBLE_FLINGER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(117, 6, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER),
        new MissionInfo(118, 6, HeroEnum.LAVA_GOLEM, HeroEnum.LAVA_GOLEM, HeroEnum.EMBER, HeroEnum.TORCH, HeroEnum.INFERNOMANCER, HeroEnum.TORCH),
        new MissionInfo(119, 6, HeroEnum.DRYAD, HeroEnum.ANGERY_TREANT, HeroEnum.HAPPY_FLOWER, HeroEnum.DRYAD, HeroEnum.DRYAD, HeroEnum.DRYAD),
        new MissionInfo(120, 6, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.NEUROMANCER, HeroEnum.PEBBLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.LIGHTNING_WRAITH)
    };

        private static readonly MissionInfo[] chapter13 = {
        new MissionInfo(121, 7, HeroEnum.ORACLE, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.TORCH),
        new MissionInfo(122, 7, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(123, 7, HeroEnum.INFERNOMANCER, HeroEnum.LIVING_WALL, HeroEnum.EMBER, HeroEnum.PEBBLE_FLINGER, HeroEnum.INFERNOMANCER, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(124, 7, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(125, 7, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.MIST_CALLER, HeroEnum.EARTHZERKER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(126, 7, HeroEnum.NEUROMANCER, HeroEnum.BALL_OF_ROOTS, HeroEnum.NEUROMANCER, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.DRYAD),
        new MissionInfo(127, 7, HeroEnum.ORACLE, HeroEnum.REFLECTOR, HeroEnum.ORACLE, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.RAIN_MAN),
        new MissionInfo(128, 7, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BOULDER, HeroEnum.DRYAD, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(129, 7, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.NEUROMANCER, HeroEnum.INFERNOMANCER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.EMBER),
        new MissionInfo(130, 7, HeroEnum.REFLECTOR, HeroEnum.ANGERY_TREANT, HeroEnum.NEUROMANCER, HeroEnum.REFLECTOR, HeroEnum.EARTHZERKER, HeroEnum.INFERNOMANCER)
    };

        private static readonly MissionInfo[] chapter14 = {
        new MissionInfo(131, 7, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.BUSH_WHACKER),
        new MissionInfo(132, 7, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.EMBER, HeroEnum.DRYAD, HeroEnum.TORCH, HeroEnum.DRYAD),
        new MissionInfo(133, 7, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.INFERNOMANCER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH),
        new MissionInfo(134, 7, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.PEBBLE_FLINGER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(135, 7, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.EARTHZERKER, HeroEnum.BATTERY),
        new MissionInfo(136, 7, HeroEnum.NEUROMANCER, HeroEnum.FEESH, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(137, 7, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(138, 7, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BOULDER, HeroEnum.PEBBLE_FLINGER, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(139, 7, HeroEnum.NEUROMANCER, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.LIGHTNING_WRAITH, HeroEnum.NEUROMANCER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(140, 7, HeroEnum.ORACLE, HeroEnum.ANGERY_TREANT, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD)
    };

        private static readonly MissionInfo[] chapter15 = {
        new MissionInfo(141, 8, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.SPARK_ELEMENTAL, HeroEnum.MIST_CALLER, HeroEnum.LIGHTNING_WRAITH),
        new MissionInfo(142, 8, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD),
        new MissionInfo(143, 8, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BALL_OF_ROOTS, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD, HeroEnum.TORCH),
        new MissionInfo(144, 8, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.EMBER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(145, 8, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.EARTHZERKER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(146, 8, HeroEnum.NEUROMANCER, HeroEnum.LIVING_WALL, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.PEBBLE_FLINGER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(147, 8, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER),
        new MissionInfo(148, 8, HeroEnum.LAVA_GOLEM, HeroEnum.LAVA_GOLEM, HeroEnum.EMBER, HeroEnum.TORCH, HeroEnum.INFERNOMANCER, HeroEnum.TORCH),
        new MissionInfo(149, 8, HeroEnum.DRYAD, HeroEnum.ANGERY_TREANT, HeroEnum.HAPPY_FLOWER, HeroEnum.DRYAD, HeroEnum.DRYAD, HeroEnum.DRYAD),
        new MissionInfo(150, 8, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.NEUROMANCER, HeroEnum.PEBBLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.LIGHTNING_WRAITH)
    };

        private static readonly MissionInfo[] chapter16 = {
        new MissionInfo(151, 8, HeroEnum.ORACLE, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.TORCH),
        new MissionInfo(152, 8, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(153, 8, HeroEnum.INFERNOMANCER, HeroEnum.LIVING_WALL, HeroEnum.EMBER, HeroEnum.PEBBLE_FLINGER, HeroEnum.INFERNOMANCER, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(154, 8, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(155, 8, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.MIST_CALLER, HeroEnum.EARTHZERKER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(156, 8, HeroEnum.NEUROMANCER, HeroEnum.BALL_OF_ROOTS, HeroEnum.NEUROMANCER, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.DRYAD),
        new MissionInfo(157, 8, HeroEnum.ORACLE, HeroEnum.REFLECTOR, HeroEnum.ORACLE, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.RAIN_MAN),
        new MissionInfo(158, 8, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BOULDER, HeroEnum.DRYAD, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(159, 8, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.NEUROMANCER, HeroEnum.INFERNOMANCER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.EMBER),
        new MissionInfo(160, 8, HeroEnum.REFLECTOR, HeroEnum.ANGERY_TREANT, HeroEnum.NEUROMANCER, HeroEnum.REFLECTOR, HeroEnum.EARTHZERKER, HeroEnum.INFERNOMANCER)
    };

        private static readonly MissionInfo[] chapter17 = {
        new MissionInfo(161, 9, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.BUSH_WHACKER),
        new MissionInfo(162, 9, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.EMBER, HeroEnum.DRYAD, HeroEnum.TORCH, HeroEnum.DRYAD),
        new MissionInfo(163, 9, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.INFERNOMANCER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH),
        new MissionInfo(164, 9, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.PEBBLE_FLINGER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(165, 9, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.EARTHZERKER, HeroEnum.BATTERY),
        new MissionInfo(166, 9, HeroEnum.NEUROMANCER, HeroEnum.FEESH, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(167, 9, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(168, 9, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BOULDER, HeroEnum.PEBBLE_FLINGER, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(169, 9, HeroEnum.NEUROMANCER, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.LIGHTNING_WRAITH, HeroEnum.NEUROMANCER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(170, 9, HeroEnum.ORACLE, HeroEnum.ANGERY_TREANT, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD)
    };

        private static readonly MissionInfo[] chapter18 = {
        new MissionInfo(171, 9, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.SPARK_ELEMENTAL, HeroEnum.MIST_CALLER, HeroEnum.LIGHTNING_WRAITH),
        new MissionInfo(172, 9, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD),
        new MissionInfo(173, 9, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BALL_OF_ROOTS, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD, HeroEnum.TORCH),
        new MissionInfo(174, 9, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.EMBER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(175, 9, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.EARTHZERKER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(176, 9, HeroEnum.NEUROMANCER, HeroEnum.LIVING_WALL, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.PEBBLE_FLINGER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(177, 9, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER),
        new MissionInfo(178, 9, HeroEnum.LAVA_GOLEM, HeroEnum.LAVA_GOLEM, HeroEnum.EMBER, HeroEnum.TORCH, HeroEnum.INFERNOMANCER, HeroEnum.TORCH),
        new MissionInfo(179, 9, HeroEnum.DRYAD, HeroEnum.ANGERY_TREANT, HeroEnum.HAPPY_FLOWER, HeroEnum.DRYAD, HeroEnum.DRYAD, HeroEnum.DRYAD),
        new MissionInfo(180, 9, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.NEUROMANCER, HeroEnum.PEBBLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.LIGHTNING_WRAITH)
    };

        private static readonly MissionInfo[] chapter19 = {
        new MissionInfo(181, 10, HeroEnum.ORACLE, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.TORCH),
        new MissionInfo(182, 10, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(183, 10, HeroEnum.INFERNOMANCER, HeroEnum.LIVING_WALL, HeroEnum.EMBER, HeroEnum.PEBBLE_FLINGER, HeroEnum.INFERNOMANCER, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(184, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(185, 10, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.MIST_CALLER, HeroEnum.EARTHZERKER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(186, 10, HeroEnum.NEUROMANCER, HeroEnum.BALL_OF_ROOTS, HeroEnum.NEUROMANCER, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.DRYAD),
        new MissionInfo(187, 10, HeroEnum.ORACLE, HeroEnum.REFLECTOR, HeroEnum.ORACLE, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.RAIN_MAN),
        new MissionInfo(188, 10, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BOULDER, HeroEnum.DRYAD, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(189, 10, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.NEUROMANCER, HeroEnum.INFERNOMANCER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.EMBER),
        new MissionInfo(190, 10, HeroEnum.REFLECTOR, HeroEnum.ANGERY_TREANT, HeroEnum.NEUROMANCER, HeroEnum.REFLECTOR, HeroEnum.EARTHZERKER, HeroEnum.INFERNOMANCER)
    };

        private static readonly MissionInfo[] chapter20 = {
        new MissionInfo(191, 10, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.BUSH_WHACKER),
        new MissionInfo(192, 10, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.EMBER, HeroEnum.DRYAD, HeroEnum.TORCH, HeroEnum.DRYAD),
        new MissionInfo(193, 10, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.INFERNOMANCER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH),
        new MissionInfo(194, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.PEBBLE_FLINGER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(195, 10, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.EARTHZERKER, HeroEnum.BATTERY),
        new MissionInfo(196, 10, HeroEnum.NEUROMANCER, HeroEnum.FEESH, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(197, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(198, 10, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BOULDER, HeroEnum.PEBBLE_FLINGER, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(199, 10, HeroEnum.NEUROMANCER, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.LIGHTNING_WRAITH, HeroEnum.NEUROMANCER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(200, 10, HeroEnum.ORACLE, HeroEnum.ANGERY_TREANT, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD)
    };

        private static readonly MissionInfo[] chapter21 = {
        new MissionInfo(201, 10, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.SPARK_ELEMENTAL, HeroEnum.MIST_CALLER, HeroEnum.LIGHTNING_WRAITH),
        new MissionInfo(202, 10, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD),
        new MissionInfo(203, 10, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BALL_OF_ROOTS, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD, HeroEnum.TORCH),
        new MissionInfo(204, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.EMBER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(205, 10, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.EARTHZERKER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(206, 10, HeroEnum.NEUROMANCER, HeroEnum.LIVING_WALL, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.PEBBLE_FLINGER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(207, 10, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER),
        new MissionInfo(208, 10, HeroEnum.LAVA_GOLEM, HeroEnum.LAVA_GOLEM, HeroEnum.EMBER, HeroEnum.TORCH, HeroEnum.INFERNOMANCER, HeroEnum.TORCH),
        new MissionInfo(209, 10, HeroEnum.DRYAD, HeroEnum.ANGERY_TREANT, HeroEnum.HAPPY_FLOWER, HeroEnum.DRYAD, HeroEnum.DRYAD, HeroEnum.DRYAD),
        new MissionInfo(210, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.NEUROMANCER, HeroEnum.PEBBLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.LIGHTNING_WRAITH)
    };

        private static readonly MissionInfo[] chapter22 = {
        new MissionInfo(211, 10, HeroEnum.ORACLE, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.TORCH),
        new MissionInfo(212, 10, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(213, 10, HeroEnum.INFERNOMANCER, HeroEnum.LIVING_WALL, HeroEnum.EMBER, HeroEnum.PEBBLE_FLINGER, HeroEnum.INFERNOMANCER, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(214, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(215, 10, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.MIST_CALLER, HeroEnum.EARTHZERKER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(216, 10, HeroEnum.NEUROMANCER, HeroEnum.BALL_OF_ROOTS, HeroEnum.NEUROMANCER, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.DRYAD),
        new MissionInfo(217, 10, HeroEnum.ORACLE, HeroEnum.REFLECTOR, HeroEnum.ORACLE, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.RAIN_MAN),
        new MissionInfo(218, 10, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BOULDER, HeroEnum.DRYAD, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(219, 10, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.NEUROMANCER, HeroEnum.INFERNOMANCER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.EMBER),
        new MissionInfo(220, 10, HeroEnum.REFLECTOR, HeroEnum.ANGERY_TREANT, HeroEnum.NEUROMANCER, HeroEnum.REFLECTOR, HeroEnum.EARTHZERKER, HeroEnum.INFERNOMANCER)
    };

        private static readonly MissionInfo[] chapter23 = {
        new MissionInfo(231, 10, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.BUSH_WHACKER),
        new MissionInfo(232, 10, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.EMBER, HeroEnum.DRYAD, HeroEnum.TORCH, HeroEnum.DRYAD),
        new MissionInfo(233, 10, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.INFERNOMANCER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH),
        new MissionInfo(234, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.PEBBLE_FLINGER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(235, 10, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.EARTHZERKER, HeroEnum.BATTERY),
        new MissionInfo(236, 10, HeroEnum.NEUROMANCER, HeroEnum.FEESH, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(237, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(238, 10, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BOULDER, HeroEnum.PEBBLE_FLINGER, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(239, 10, HeroEnum.NEUROMANCER, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.LIGHTNING_WRAITH, HeroEnum.NEUROMANCER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(240, 10, HeroEnum.ORACLE, HeroEnum.ANGERY_TREANT, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD)
    };

        private static readonly MissionInfo[] chapter24 = {
        new MissionInfo(241, 10, HeroEnum.ORACLE, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.SPARK_ELEMENTAL, HeroEnum.MIST_CALLER, HeroEnum.LIGHTNING_WRAITH),
        new MissionInfo(242, 10, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD),
        new MissionInfo(243, 10, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.BALL_OF_ROOTS, HeroEnum.INFERNOMANCER, HeroEnum.DRYAD, HeroEnum.TORCH),
        new MissionInfo(244, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.EMBER, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(245, 10, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.EARTHZERKER, HeroEnum.ICICLE_FLINGER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(246, 10, HeroEnum.NEUROMANCER, HeroEnum.LIVING_WALL, HeroEnum.NEUROMANCER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.PEBBLE_FLINGER, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(247, 10, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.ORACLE, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER, HeroEnum.MIST_CALLER),
        new MissionInfo(248, 10, HeroEnum.LAVA_GOLEM, HeroEnum.LAVA_GOLEM, HeroEnum.EMBER, HeroEnum.TORCH, HeroEnum.INFERNOMANCER, HeroEnum.TORCH),
        new MissionInfo(249, 10, HeroEnum.DRYAD, HeroEnum.ANGERY_TREANT, HeroEnum.HAPPY_FLOWER, HeroEnum.DRYAD, HeroEnum.DRYAD, HeroEnum.DRYAD),
        new MissionInfo(250, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.NEUROMANCER, HeroEnum.PEBBLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.LIGHTNING_WRAITH)
    };

        private static readonly MissionInfo[] chapter25 = {
        new MissionInfo(251, 10, HeroEnum.ORACLE, HeroEnum.LAVA_GOLEM, HeroEnum.ORACLE, HeroEnum.DRYAD, HeroEnum.MIST_CALLER, HeroEnum.TORCH),
        new MissionInfo(252, 10, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.DRYAD, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(253, 10, HeroEnum.INFERNOMANCER, HeroEnum.LIVING_WALL, HeroEnum.EMBER, HeroEnum.PEBBLE_FLINGER, HeroEnum.INFERNOMANCER, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(254, 10, HeroEnum.REFLECTOR, HeroEnum.REFLECTOR, HeroEnum.LIVING_WALL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(255, 10, HeroEnum.EARTHZERKER, HeroEnum.LIVING_WALL, HeroEnum.MIST_CALLER, HeroEnum.EARTHZERKER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.SPARK_ELEMENTAL),
        new MissionInfo(256, 10, HeroEnum.NEUROMANCER, HeroEnum.BALL_OF_ROOTS, HeroEnum.NEUROMANCER, HeroEnum.MIST_CALLER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.DRYAD),
        new MissionInfo(257, 10, HeroEnum.ORACLE, HeroEnum.REFLECTOR, HeroEnum.ORACLE, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.MIST_CALLER, HeroEnum.RAIN_MAN),
        new MissionInfo(258, 10, HeroEnum.ANGERY_TREANT, HeroEnum.ANGERY_TREANT, HeroEnum.BOULDER, HeroEnum.DRYAD, HeroEnum.EARTHZERKER, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(259, 10, HeroEnum.INFERNOMANCER, HeroEnum.LAVA_GOLEM, HeroEnum.NEUROMANCER, HeroEnum.INFERNOMANCER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.EMBER),
        new MissionInfo(260, 10, HeroEnum.REFLECTOR, HeroEnum.ANGERY_TREANT, HeroEnum.NEUROMANCER, HeroEnum.REFLECTOR, HeroEnum.EARTHZERKER, HeroEnum.INFERNOMANCER)
    };

        private static readonly MissionInfo[][] allChapters = {
        chapter1, chapter2, chapter3, chapter4, chapter5, chapter6, chapter7, chapter8, chapter9, chapter10,
        chapter11, chapter12, chapter13, chapter14, chapter15, chapter16, chapter17, chapter18, chapter19, chapter20,
        chapter21, chapter22, chapter23, chapter24, chapter25 };

        public static MissionInfo GetMission(int chapter, int mission) {
            if (chapter > allChapters.Length) return null;
            return allChapters[chapter - 1][mission - 1];
        }

        public static GenerationInfo GetGenerationInfo(AccountState currentState) {
            return new GenerationInfo(currentState.CurrentChapter);
        }

        public static List<AccountEquipment> GetMissionEquipmentLoadout(AccountHero hero) {
            var equipped = new List<AccountEquipment>();
            var baseHero = hero.GetBaseHero();
            if (baseHero.PreferredMainHand != null) equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredMainHand.GetValueOrDefault(), hero.AwakeningLevel));
            if (baseHero.PreferredOffHand != null) equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredOffHand.GetValueOrDefault(), hero.AwakeningLevel));
            if (baseHero.PreferredTwoHand != null) equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredTwoHand.GetValueOrDefault(), hero.AwakeningLevel));
            equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredChest, hero.AwakeningLevel));
            equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredLegs, hero.AwakeningLevel));
            equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredHead, hero.AwakeningLevel));
            return equipped;
        }
    }

    public class MissionInfo {

        public int HeroLevel { get; }
        public int HeroAwakening { get; }
        public HeroEnum FaceOfMission { get; }

        public HeroEnum[] MissionHeroes { get; }

        public MissionInfo(int heroLevel, int heroAwakening, HeroEnum faceOfMission, params HeroEnum[] missionHeroes) {
            HeroLevel = heroLevel;
            HeroAwakening = heroAwakening;
            FaceOfMission = faceOfMission;
            MissionHeroes = missionHeroes;
        }

        public PotentialRewardsContainer RewardsForMission() {
            var heroRequirement = LevelContainer.HeroExperienceRequirement(HeroLevel);
            return new PotentialRewardsContainer(1, heroRequirement * 3, heroRequirement * 2, 200 * HeroAwakening, HeroAwakening, (HeroAwakening / 2) + 1);
        }
    }

    public struct GenerationInfo {

        public int GoldPerMinute { get; }
        public int SoulsPerMinute { get; }
        public int ExperiencePerMinute { get; }

        public GenerationInfo(int chapter) {
            GoldPerMinute = 20 * chapter;
            SoulsPerMinute = 10 * chapter;
            ExperiencePerMinute = 10 * chapter;
        }

        public static double GenerationPerMillisecond(int generation) {
            return generation / (60.0 * 1000.0);
        }
    }
}
