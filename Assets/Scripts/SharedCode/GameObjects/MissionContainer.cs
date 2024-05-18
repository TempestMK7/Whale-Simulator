using System;
using System.Collections.Generic;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.GameObjects {

    public class MissionContainer {

        private static readonly MissionInfo[] chapter1 = {
        new MissionInfo(1, 1, HeroEnum.LEPAQUA, HeroEnum.LEPAQUA, HeroEnum.LEPAQUA, HeroEnum.LEPAQUA),
        new MissionInfo(2, 1, HeroEnum.LEPHYTA, HeroEnum.LEPHYTA, HeroEnum.LEPHYTA, HeroEnum.LEPHYTA),
        new MissionInfo(3, 1, HeroEnum.LEPYRA, HeroEnum.LEPYRA, HeroEnum.LEPYRA, HeroEnum.LEPYRA),
        new MissionInfo(4, 1, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN),
        new MissionInfo(5, 1, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING),
        new MissionInfo(6, 1, HeroEnum.DUST_ELEMENTAL, HeroEnum.DUST_ELEMENTAL, HeroEnum.DUST_ELEMENTAL, HeroEnum.DUST_ELEMENTAL),
        new MissionInfo(7, 1, HeroEnum.ARDOWSE, HeroEnum.ARDOWSE, HeroEnum.ARDOWSE, HeroEnum.LEPAQUA, HeroEnum.LEPAQUA, HeroEnum.LEPAQUA),
        new MissionInfo(8, 1, HeroEnum.ARBERRY, HeroEnum.ARBERRY, HeroEnum.ARBERRY, HeroEnum.LEPHYTA, HeroEnum.LEPHYTA, HeroEnum.LEPHYTA),
        new MissionInfo(9, 1, HeroEnum.ARBURN, HeroEnum.ARBURN, HeroEnum.ARBURN, HeroEnum.LEPYRA, HeroEnum.LEPYRA, HeroEnum.LEPYRA),
        new MissionInfo(10, 1, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.ARDOWSE, HeroEnum.LEPAQUA, HeroEnum.ARDOWSE, HeroEnum.LEPAQUA)
    };

        private static readonly MissionInfo[] chapter2 = {
        new MissionInfo(11, 1, HeroEnum.ICECAP, HeroEnum.ICECAP, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN),
        new MissionInfo(12, 1, HeroEnum.BOLTCAP, HeroEnum.BOLTCAP, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING),
        new MissionInfo(13, 1, HeroEnum.MUDCAP, HeroEnum.MUDCAP, HeroEnum.MUDCAP, HeroEnum.DUST_ELEMENTAL, HeroEnum.DUST_ELEMENTAL, HeroEnum.DUST_ELEMENTAL),
        new MissionInfo(14, 1, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.ARDOWSE, HeroEnum.LEPAQUA, HeroEnum.ARDOWSE, HeroEnum.LEPAQUA),
        new MissionInfo(15, 1, HeroEnum.FLORAVULP, HeroEnum.FLORAVULP, HeroEnum.ARBERRY, HeroEnum.LEPHYTA, HeroEnum.ARBERRY, HeroEnum.LEPHYTA),
        new MissionInfo(16, 1, HeroEnum.SCOROVULP, HeroEnum.SCOROVULP, HeroEnum.ARBURN, HeroEnum.LEPYRA, HeroEnum.ARBURN, HeroEnum.LEPYRA),
        new MissionInfo(17, 1, HeroEnum.GLACITAUR, HeroEnum.ICECAP, HeroEnum.ICECAP, HeroEnum.SNOW_MAN, HeroEnum.GLACITAUR, HeroEnum.SNOW_MAN),
        new MissionInfo(18, 1, HeroEnum.ZAPATAUR, HeroEnum.ZAPATAUR, HeroEnum.BOLTCAP, HeroEnum.STATIC_CLING, HeroEnum.BOLTCAP, HeroEnum.STATIC_CLING),
        new MissionInfo(19, 1, HeroEnum.ROCKOTAUR, HeroEnum.MUDCAP, HeroEnum.MUDCAP, HeroEnum.DUST_ELEMENTAL, HeroEnum.ROCKOTAUR, HeroEnum.DUST_ELEMENTAL),
        new MissionInfo(20, 1, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.LEPHYTA, HeroEnum.LEPYRA, HeroEnum.ARBURN, HeroEnum.ARBERRY)
    };

        private static readonly MissionInfo[] chapter3 = {
        new MissionInfo(21, 2, HeroEnum.ARDOWSE, HeroEnum.ICECAP, HeroEnum.SNOW_MAN, HeroEnum.LEPAQUA, HeroEnum.GLACITAUR, HeroEnum.ARDOWSE),
        new MissionInfo(22, 2, HeroEnum.FLORAVULP, HeroEnum.FLORAVULP, HeroEnum.MUDCAP, HeroEnum.LEPHYTA, HeroEnum.ROCKOTAUR, HeroEnum.ARBERRY),
        new MissionInfo(23, 2, HeroEnum.SCOROVULP, HeroEnum.SCOROVULP, HeroEnum.ARBURN, HeroEnum.BOLTCAP, HeroEnum.ZAPATAUR, HeroEnum.STATIC_CLING),
        new MissionInfo(24, 2, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.LEPHYTA, HeroEnum.LEPAQUA, HeroEnum.ARDOWSE, HeroEnum.ARBERRY),
        new MissionInfo(25, 2, HeroEnum.ARBERRY, HeroEnum.FLORAVULP, HeroEnum.ARBURN, HeroEnum.LEPHYTA, HeroEnum.ARBERRY, HeroEnum.SCOROVULP),
        new MissionInfo(26, 2, HeroEnum.ARBURN, HeroEnum.ICECAP, HeroEnum.ARBURN, HeroEnum.SNOW_MAN, HeroEnum.SCOROVULP, HeroEnum.GLACITAUR),
        new MissionInfo(27, 2, HeroEnum.GLACITAUR, HeroEnum.ICECAP, HeroEnum.BOLTCAP, HeroEnum.SNOW_MAN, HeroEnum.ZAPATAUR, HeroEnum.GLACITAUR),
        new MissionInfo(28, 2, HeroEnum.ZAPATAUR, HeroEnum.MUDCAP, HeroEnum.ZAPATAUR, HeroEnum.STATIC_CLING, HeroEnum.ROCKOTAUR, HeroEnum.BOLTCAP),
        new MissionInfo(29, 2, HeroEnum.MUDCAP, HeroEnum.MUDCAP, HeroEnum.ARDOWSE, HeroEnum.LEPAQUA, HeroEnum.ROCKOTAUR, HeroEnum.DUST_ELEMENTAL),
        new MissionInfo(30, 2, HeroEnum.GENERATOR, HeroEnum.FLORAVULP, HeroEnum.MARIVULP, HeroEnum.GENERATOR, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP)
    };

        private static readonly MissionInfo[] chapter4 = {
        new MissionInfo(31, 2, HeroEnum.SPIRIFLOW, HeroEnum.MARIVULP, HeroEnum.LEPAQUA, HeroEnum.ARDOWSE, HeroEnum.SPIRIFLOW, HeroEnum.ARDOWSE),
        new MissionInfo(32, 2, HeroEnum.SPIRIGROW, HeroEnum.FLORAVULP, HeroEnum.LEPHYTA, HeroEnum.ARBERRY, HeroEnum.SPIRIGROW, HeroEnum.ARBERRY),
        new MissionInfo(33, 2, HeroEnum.SPIRIGNITE, HeroEnum.SPIRIGNITE, HeroEnum.ARBURN, HeroEnum.LEPYRA, HeroEnum.SCOROVULP, HeroEnum.LEPYRA),
        new MissionInfo(34, 2, HeroEnum.FREEZER, HeroEnum.ICECAP, HeroEnum.ICECAP, HeroEnum.GLACITAUR, HeroEnum.FREEZER, HeroEnum.SNOW_MAN),
        new MissionInfo(35, 2, HeroEnum.GENERATOR, HeroEnum.BOLTCAP, HeroEnum.BOLTCAP, HeroEnum.ZAPATAUR, HeroEnum.GENERATOR, HeroEnum.STATIC_CLING),
        new MissionInfo(36, 2, HeroEnum.PULVERIZER, HeroEnum.PULVERIZER, HeroEnum.MUDCAP, HeroEnum.DUST_ELEMENTAL, HeroEnum.ROCKOTAUR, HeroEnum.MUDCAP),
        new MissionInfo(37, 2, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.SPIRIFLOW, HeroEnum.GLACITAUR, HeroEnum.ARDOWSE, HeroEnum.FREEZER),
        new MissionInfo(38, 2, HeroEnum.FLORAVULP, HeroEnum.FLORAVULP, HeroEnum.PULVERIZER, HeroEnum.ARBERRY, HeroEnum.SPIRIGROW, HeroEnum.ROCKOTAUR),
        new MissionInfo(39, 2, HeroEnum.SCOROVULP, HeroEnum.SPIRIGNITE, HeroEnum.BOLTCAP, HeroEnum.ARBURN, HeroEnum.GENERATOR, HeroEnum.SCOROVULP),
        new MissionInfo(40, 2, HeroEnum.TERRIKAHT, HeroEnum.TERRIKAHT, HeroEnum.SNOW_MAN, HeroEnum.LEPAQUA, HeroEnum.LEPHYTA, HeroEnum.LEPYRA)
    };

        private static readonly MissionInfo[] chapter5 = {
        new MissionInfo(41, 3, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.ARBERRY),
        new MissionInfo(42, 3, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ARBURN, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP, HeroEnum.SPIRIGROW),
        new MissionInfo(43, 3, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FREEZER, HeroEnum.INFERNIKAHT, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP),
        new MissionInfo(44, 3, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.ROCKOTAUR, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(45, 3, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.TERRIKAHT, HeroEnum.BOLTCAP),
        new MissionInfo(46, 3, HeroEnum.ZEPHYKAHT, HeroEnum.MARIVULP, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR),
        new MissionInfo(47, 3, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.GLACITAUR, HeroEnum.FREEZER, HeroEnum.GLACITAUR),
        new MissionInfo(48, 3, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.MUDCAP, HeroEnum.ROCKOTAUR, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(49, 3, HeroEnum.ZEPHYKAHT, HeroEnum.BOLTCAP, HeroEnum.BOLTCAP, HeroEnum.GENERATOR, HeroEnum.ZEPHYKAHT, HeroEnum.ZAPATAUR),
        new MissionInfo(50, 3, HeroEnum.HYDROKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW)
    };

        private static readonly MissionInfo[] chapter6 = {
        new MissionInfo(51, 3, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.ZAPATAUR, HeroEnum.SPIRIFLOW, HeroEnum.GENERATOR),
        new MissionInfo(52, 3, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW),
        new MissionInfo(53, 3, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FLORAVULP, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP),
        new MissionInfo(54, 3, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ARBURN, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP, HeroEnum.FREEZER),
        new MissionInfo(55, 3, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.FREEZER, HeroEnum.TERRIKAHT, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(56, 3, HeroEnum.ZEPHYKAHT, HeroEnum.PULVERIZER, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.ROCKOTAUR, HeroEnum.ZAPATAUR),
        new MissionInfo(57, 3, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW),
        new MissionInfo(58, 3, HeroEnum.SPIRIGNITE, HeroEnum.SPIRIGNITE, HeroEnum.ARBURN, HeroEnum.SCOROVULP, HeroEnum.INFERNIKAHT, HeroEnum.SCOROVULP),
        new MissionInfo(59, 3, HeroEnum.SPIRIGROW, HeroEnum.BOTANIKAHT, HeroEnum.LEPHYTA, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW),
        new MissionInfo(60, 3, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.ROCKOTAUR, HeroEnum.FREEZER, HeroEnum.GENERATOR)
    };

        private static readonly MissionInfo[] chapter7 = {
        new MissionInfo(61, 4, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SCOROVULP),
        new MissionInfo(62, 4, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.GLACITAUR),
        new MissionInfo(63, 4, HeroEnum.INFERNIKAHT, HeroEnum.PULVERIZER, HeroEnum.ARBURN, HeroEnum.ROCKOTAUR, HeroEnum.INFERNIKAHT, HeroEnum.FREEZER),
        new MissionInfo(64, 4, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.FREEZER, HeroEnum.ZAPATAUR),
        new MissionInfo(65, 4, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.SPIRIFLOW, HeroEnum.TERRIKAHT, HeroEnum.GENERATOR, HeroEnum.ZAPATAUR),
        new MissionInfo(66, 4, HeroEnum.ZEPHYKAHT, HeroEnum.FLORAVULP, HeroEnum.ZEPHYKAHT, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR, HeroEnum.SPIRIGROW),
        new MissionInfo(67, 4, HeroEnum.HYDROKAHT, HeroEnum.CRYOKAHT, HeroEnum.HYDROKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.ARDOWSE),
        new MissionInfo(68, 4, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.MUDCAP, HeroEnum.SPIRIGROW, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(69, 4, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.ZEPHYKAHT, HeroEnum.INFERNIKAHT, HeroEnum.ZAPATAUR, HeroEnum.ARBURN),
        new MissionInfo(70, 4, HeroEnum.CRYOKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.CRYOKAHT, HeroEnum.TERRIKAHT, HeroEnum.INFERNIKAHT)
    };

        private static readonly MissionInfo[] chapter8 = {
        new MissionInfo(71, 4, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.ARBERRY),
        new MissionInfo(72, 4, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ARBURN, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP, HeroEnum.SPIRIGROW),
        new MissionInfo(73, 4, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FREEZER, HeroEnum.INFERNIKAHT, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP),
        new MissionInfo(74, 4, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.ROCKOTAUR, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(75, 4, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.TERRIKAHT, HeroEnum.BOLTCAP),
        new MissionInfo(76, 4, HeroEnum.ZEPHYKAHT, HeroEnum.MARIVULP, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR),
        new MissionInfo(77, 4, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.GLACITAUR, HeroEnum.FREEZER, HeroEnum.GLACITAUR),
        new MissionInfo(78, 4, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.MUDCAP, HeroEnum.ROCKOTAUR, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(79, 4, HeroEnum.ZEPHYKAHT, HeroEnum.BOLTCAP, HeroEnum.BOLTCAP, HeroEnum.GENERATOR, HeroEnum.ZEPHYKAHT, HeroEnum.ZAPATAUR),
        new MissionInfo(80, 4, HeroEnum.HYDROKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW)
    };

        private static readonly MissionInfo[] chapter9 = {
        new MissionInfo(81, 5, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.ZAPATAUR, HeroEnum.SPIRIFLOW, HeroEnum.GENERATOR),
        new MissionInfo(82, 5, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW),
        new MissionInfo(83, 5, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FLORAVULP, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP),
        new MissionInfo(84, 5, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ARBURN, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP, HeroEnum.FREEZER),
        new MissionInfo(85, 5, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.FREEZER, HeroEnum.TERRIKAHT, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(86, 5, HeroEnum.ZEPHYKAHT, HeroEnum.PULVERIZER, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.ROCKOTAUR, HeroEnum.ZAPATAUR),
        new MissionInfo(87, 5, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW),
        new MissionInfo(88, 5, HeroEnum.SPIRIGNITE, HeroEnum.SPIRIGNITE, HeroEnum.ARBURN, HeroEnum.SCOROVULP, HeroEnum.INFERNIKAHT, HeroEnum.SCOROVULP),
        new MissionInfo(89, 5, HeroEnum.SPIRIGROW, HeroEnum.BOTANIKAHT, HeroEnum.LEPHYTA, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW),
        new MissionInfo(90, 5, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.ROCKOTAUR, HeroEnum.FREEZER, HeroEnum.GENERATOR)
    };

        private static readonly MissionInfo[] chapter10 = {
        new MissionInfo(91, 5, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SCOROVULP),
        new MissionInfo(92, 5, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.GLACITAUR),
        new MissionInfo(93, 5, HeroEnum.INFERNIKAHT, HeroEnum.PULVERIZER, HeroEnum.ARBURN, HeroEnum.ROCKOTAUR, HeroEnum.INFERNIKAHT, HeroEnum.FREEZER),
        new MissionInfo(94, 5, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.FREEZER, HeroEnum.ZAPATAUR),
        new MissionInfo(95, 5, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.SPIRIFLOW, HeroEnum.TERRIKAHT, HeroEnum.GENERATOR, HeroEnum.ZAPATAUR),
        new MissionInfo(96, 5, HeroEnum.ZEPHYKAHT, HeroEnum.FLORAVULP, HeroEnum.ZEPHYKAHT, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR, HeroEnum.SPIRIGROW),
        new MissionInfo(97, 5, HeroEnum.HYDROKAHT, HeroEnum.CRYOKAHT, HeroEnum.HYDROKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.ARDOWSE),
        new MissionInfo(98, 5, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.MUDCAP, HeroEnum.SPIRIGROW, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(99, 5, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.ZEPHYKAHT, HeroEnum.INFERNIKAHT, HeroEnum.ZAPATAUR, HeroEnum.ARBURN),
        new MissionInfo(100, 5, HeroEnum.CRYOKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.CRYOKAHT, HeroEnum.TERRIKAHT, HeroEnum.INFERNIKAHT)
    };

        private static readonly MissionInfo[] chapter11 = {
        new MissionInfo(101, 6, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.ARBERRY),
        new MissionInfo(102, 6, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ARBURN, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP, HeroEnum.SPIRIGROW),
        new MissionInfo(103, 6, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FREEZER, HeroEnum.INFERNIKAHT, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP),
        new MissionInfo(104, 6, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.ROCKOTAUR, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(105, 6, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.TERRIKAHT, HeroEnum.BOLTCAP),
        new MissionInfo(106, 6, HeroEnum.ZEPHYKAHT, HeroEnum.MARIVULP, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR),
        new MissionInfo(107, 6, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.GLACITAUR, HeroEnum.FREEZER, HeroEnum.GLACITAUR),
        new MissionInfo(108, 6, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.MUDCAP, HeroEnum.ROCKOTAUR, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(109, 6, HeroEnum.ZEPHYKAHT, HeroEnum.BOLTCAP, HeroEnum.BOLTCAP, HeroEnum.GENERATOR, HeroEnum.ZEPHYKAHT, HeroEnum.ZAPATAUR),
        new MissionInfo(110, 6, HeroEnum.HYDROKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW)
    };

        private static readonly MissionInfo[] chapter12 = {
        new MissionInfo(111, 6, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.ZAPATAUR, HeroEnum.SPIRIFLOW, HeroEnum.GENERATOR),
        new MissionInfo(112, 6, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW),
        new MissionInfo(113, 6, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FLORAVULP, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP),
        new MissionInfo(114, 6, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ARBURN, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP, HeroEnum.FREEZER),
        new MissionInfo(115, 6, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.FREEZER, HeroEnum.TERRIKAHT, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(116, 6, HeroEnum.ZEPHYKAHT, HeroEnum.PULVERIZER, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.ROCKOTAUR, HeroEnum.ZAPATAUR),
        new MissionInfo(117, 6, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW),
        new MissionInfo(118, 6, HeroEnum.SPIRIGNITE, HeroEnum.SPIRIGNITE, HeroEnum.ARBURN, HeroEnum.SCOROVULP, HeroEnum.INFERNIKAHT, HeroEnum.SCOROVULP),
        new MissionInfo(119, 6, HeroEnum.SPIRIGROW, HeroEnum.BOTANIKAHT, HeroEnum.LEPHYTA, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW),
        new MissionInfo(120, 6, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.ROCKOTAUR, HeroEnum.FREEZER, HeroEnum.GENERATOR)
    };

        private static readonly MissionInfo[] chapter13 = {
        new MissionInfo(121, 7, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SCOROVULP),
        new MissionInfo(122, 7, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.GLACITAUR),
        new MissionInfo(123, 7, HeroEnum.INFERNIKAHT, HeroEnum.PULVERIZER, HeroEnum.ARBURN, HeroEnum.ROCKOTAUR, HeroEnum.INFERNIKAHT, HeroEnum.FREEZER),
        new MissionInfo(124, 7, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.FREEZER, HeroEnum.ZAPATAUR),
        new MissionInfo(125, 7, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.SPIRIFLOW, HeroEnum.TERRIKAHT, HeroEnum.GENERATOR, HeroEnum.ZAPATAUR),
        new MissionInfo(126, 7, HeroEnum.ZEPHYKAHT, HeroEnum.FLORAVULP, HeroEnum.ZEPHYKAHT, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR, HeroEnum.SPIRIGROW),
        new MissionInfo(127, 7, HeroEnum.HYDROKAHT, HeroEnum.CRYOKAHT, HeroEnum.HYDROKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.ARDOWSE),
        new MissionInfo(128, 7, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.MUDCAP, HeroEnum.SPIRIGROW, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(129, 7, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.ZEPHYKAHT, HeroEnum.INFERNIKAHT, HeroEnum.ZAPATAUR, HeroEnum.ARBURN),
        new MissionInfo(130, 7, HeroEnum.CRYOKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.CRYOKAHT, HeroEnum.TERRIKAHT, HeroEnum.INFERNIKAHT)
    };

        private static readonly MissionInfo[] chapter14 = {
        new MissionInfo(131, 7, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.ARBERRY),
        new MissionInfo(132, 7, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ARBURN, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP, HeroEnum.SPIRIGROW),
        new MissionInfo(133, 7, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FREEZER, HeroEnum.INFERNIKAHT, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP),
        new MissionInfo(134, 7, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.ROCKOTAUR, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(135, 7, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.TERRIKAHT, HeroEnum.BOLTCAP),
        new MissionInfo(136, 7, HeroEnum.ZEPHYKAHT, HeroEnum.MARIVULP, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR),
        new MissionInfo(137, 7, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.GLACITAUR, HeroEnum.FREEZER, HeroEnum.GLACITAUR),
        new MissionInfo(138, 7, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.MUDCAP, HeroEnum.ROCKOTAUR, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(139, 7, HeroEnum.ZEPHYKAHT, HeroEnum.BOLTCAP, HeroEnum.BOLTCAP, HeroEnum.GENERATOR, HeroEnum.ZEPHYKAHT, HeroEnum.ZAPATAUR),
        new MissionInfo(140, 7, HeroEnum.HYDROKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW)
    };

        private static readonly MissionInfo[] chapter15 = {
        new MissionInfo(141, 8, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.ZAPATAUR, HeroEnum.SPIRIFLOW, HeroEnum.GENERATOR),
        new MissionInfo(142, 8, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW),
        new MissionInfo(143, 8, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FLORAVULP, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP),
        new MissionInfo(144, 8, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ARBURN, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP, HeroEnum.FREEZER),
        new MissionInfo(145, 8, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.FREEZER, HeroEnum.TERRIKAHT, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(146, 8, HeroEnum.ZEPHYKAHT, HeroEnum.PULVERIZER, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.ROCKOTAUR, HeroEnum.ZAPATAUR),
        new MissionInfo(147, 8, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW),
        new MissionInfo(148, 8, HeroEnum.SPIRIGNITE, HeroEnum.SPIRIGNITE, HeroEnum.ARBURN, HeroEnum.SCOROVULP, HeroEnum.INFERNIKAHT, HeroEnum.SCOROVULP),
        new MissionInfo(149, 8, HeroEnum.SPIRIGROW, HeroEnum.BOTANIKAHT, HeroEnum.LEPHYTA, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW),
        new MissionInfo(150, 8, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.ROCKOTAUR, HeroEnum.FREEZER, HeroEnum.GENERATOR)
    };

        private static readonly MissionInfo[] chapter16 = {
        new MissionInfo(151, 8, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SCOROVULP),
        new MissionInfo(152, 8, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.GLACITAUR),
        new MissionInfo(153, 8, HeroEnum.INFERNIKAHT, HeroEnum.PULVERIZER, HeroEnum.ARBURN, HeroEnum.ROCKOTAUR, HeroEnum.INFERNIKAHT, HeroEnum.FREEZER),
        new MissionInfo(154, 8, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.FREEZER, HeroEnum.ZAPATAUR),
        new MissionInfo(155, 8, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.SPIRIFLOW, HeroEnum.TERRIKAHT, HeroEnum.GENERATOR, HeroEnum.ZAPATAUR),
        new MissionInfo(156, 8, HeroEnum.ZEPHYKAHT, HeroEnum.FLORAVULP, HeroEnum.ZEPHYKAHT, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR, HeroEnum.SPIRIGROW),
        new MissionInfo(157, 8, HeroEnum.HYDROKAHT, HeroEnum.CRYOKAHT, HeroEnum.HYDROKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.ARDOWSE),
        new MissionInfo(158, 8, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.MUDCAP, HeroEnum.SPIRIGROW, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(159, 8, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.ZEPHYKAHT, HeroEnum.INFERNIKAHT, HeroEnum.ZAPATAUR, HeroEnum.ARBURN),
        new MissionInfo(160, 8, HeroEnum.CRYOKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.CRYOKAHT, HeroEnum.TERRIKAHT, HeroEnum.INFERNIKAHT)
    };

        private static readonly MissionInfo[] chapter17 = {
        new MissionInfo(161, 9, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.ARBERRY),
        new MissionInfo(162, 9, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ARBURN, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP, HeroEnum.SPIRIGROW),
        new MissionInfo(163, 9, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FREEZER, HeroEnum.INFERNIKAHT, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP),
        new MissionInfo(164, 9, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.ROCKOTAUR, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(165, 9, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.TERRIKAHT, HeroEnum.BOLTCAP),
        new MissionInfo(166, 9, HeroEnum.ZEPHYKAHT, HeroEnum.MARIVULP, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR),
        new MissionInfo(167, 9, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.GLACITAUR, HeroEnum.FREEZER, HeroEnum.GLACITAUR),
        new MissionInfo(168, 9, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.MUDCAP, HeroEnum.ROCKOTAUR, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(169, 9, HeroEnum.ZEPHYKAHT, HeroEnum.BOLTCAP, HeroEnum.BOLTCAP, HeroEnum.GENERATOR, HeroEnum.ZEPHYKAHT, HeroEnum.ZAPATAUR),
        new MissionInfo(170, 9, HeroEnum.HYDROKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW)
    };

        private static readonly MissionInfo[] chapter18 = {
        new MissionInfo(171, 9, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.ZAPATAUR, HeroEnum.SPIRIFLOW, HeroEnum.GENERATOR),
        new MissionInfo(172, 9, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW),
        new MissionInfo(173, 9, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FLORAVULP, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP),
        new MissionInfo(174, 9, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ARBURN, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP, HeroEnum.FREEZER),
        new MissionInfo(175, 9, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.FREEZER, HeroEnum.TERRIKAHT, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(176, 9, HeroEnum.ZEPHYKAHT, HeroEnum.PULVERIZER, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.ROCKOTAUR, HeroEnum.ZAPATAUR),
        new MissionInfo(177, 9, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW),
        new MissionInfo(178, 9, HeroEnum.SPIRIGNITE, HeroEnum.SPIRIGNITE, HeroEnum.ARBURN, HeroEnum.SCOROVULP, HeroEnum.INFERNIKAHT, HeroEnum.SCOROVULP),
        new MissionInfo(179, 9, HeroEnum.SPIRIGROW, HeroEnum.BOTANIKAHT, HeroEnum.LEPHYTA, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW),
        new MissionInfo(180, 9, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.ROCKOTAUR, HeroEnum.FREEZER, HeroEnum.GENERATOR)
    };

        private static readonly MissionInfo[] chapter19 = {
        new MissionInfo(181, 10, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SCOROVULP),
        new MissionInfo(182, 10, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.GLACITAUR),
        new MissionInfo(183, 10, HeroEnum.INFERNIKAHT, HeroEnum.PULVERIZER, HeroEnum.ARBURN, HeroEnum.ROCKOTAUR, HeroEnum.INFERNIKAHT, HeroEnum.FREEZER),
        new MissionInfo(184, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.FREEZER, HeroEnum.ZAPATAUR),
        new MissionInfo(185, 10, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.SPIRIFLOW, HeroEnum.TERRIKAHT, HeroEnum.GENERATOR, HeroEnum.ZAPATAUR),
        new MissionInfo(186, 10, HeroEnum.ZEPHYKAHT, HeroEnum.FLORAVULP, HeroEnum.ZEPHYKAHT, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR, HeroEnum.SPIRIGROW),
        new MissionInfo(187, 10, HeroEnum.HYDROKAHT, HeroEnum.CRYOKAHT, HeroEnum.HYDROKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.ARDOWSE),
        new MissionInfo(188, 10, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.MUDCAP, HeroEnum.SPIRIGROW, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(189, 10, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.ZEPHYKAHT, HeroEnum.INFERNIKAHT, HeroEnum.ZAPATAUR, HeroEnum.ARBURN),
        new MissionInfo(190, 10, HeroEnum.CRYOKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.CRYOKAHT, HeroEnum.TERRIKAHT, HeroEnum.INFERNIKAHT)
    };

        private static readonly MissionInfo[] chapter20 = {
        new MissionInfo(191, 10, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.ARBERRY),
        new MissionInfo(192, 10, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ARBURN, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP, HeroEnum.SPIRIGROW),
        new MissionInfo(193, 10, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FREEZER, HeroEnum.INFERNIKAHT, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP),
        new MissionInfo(194, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.ROCKOTAUR, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(195, 10, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.TERRIKAHT, HeroEnum.BOLTCAP),
        new MissionInfo(196, 10, HeroEnum.ZEPHYKAHT, HeroEnum.MARIVULP, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR),
        new MissionInfo(197, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.GLACITAUR, HeroEnum.FREEZER, HeroEnum.GLACITAUR),
        new MissionInfo(198, 10, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.MUDCAP, HeroEnum.ROCKOTAUR, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(199, 10, HeroEnum.ZEPHYKAHT, HeroEnum.BOLTCAP, HeroEnum.BOLTCAP, HeroEnum.GENERATOR, HeroEnum.ZEPHYKAHT, HeroEnum.ZAPATAUR),
        new MissionInfo(200, 10, HeroEnum.HYDROKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW)
    };

        private static readonly MissionInfo[] chapter21 = {
        new MissionInfo(201, 10, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.ZAPATAUR, HeroEnum.SPIRIFLOW, HeroEnum.GENERATOR),
        new MissionInfo(202, 10, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW),
        new MissionInfo(203, 10, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FLORAVULP, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP),
        new MissionInfo(204, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ARBURN, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP, HeroEnum.FREEZER),
        new MissionInfo(205, 10, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.FREEZER, HeroEnum.TERRIKAHT, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(206, 10, HeroEnum.ZEPHYKAHT, HeroEnum.PULVERIZER, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.ROCKOTAUR, HeroEnum.ZAPATAUR),
        new MissionInfo(207, 10, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW),
        new MissionInfo(208, 10, HeroEnum.SPIRIGNITE, HeroEnum.SPIRIGNITE, HeroEnum.ARBURN, HeroEnum.SCOROVULP, HeroEnum.INFERNIKAHT, HeroEnum.SCOROVULP),
        new MissionInfo(209, 10, HeroEnum.SPIRIGROW, HeroEnum.BOTANIKAHT, HeroEnum.LEPHYTA, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW),
        new MissionInfo(210, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.ROCKOTAUR, HeroEnum.FREEZER, HeroEnum.GENERATOR)
    };

        private static readonly MissionInfo[] chapter22 = {
        new MissionInfo(211, 10, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SCOROVULP),
        new MissionInfo(212, 10, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.GLACITAUR),
        new MissionInfo(213, 10, HeroEnum.INFERNIKAHT, HeroEnum.PULVERIZER, HeroEnum.ARBURN, HeroEnum.ROCKOTAUR, HeroEnum.INFERNIKAHT, HeroEnum.FREEZER),
        new MissionInfo(214, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.FREEZER, HeroEnum.ZAPATAUR),
        new MissionInfo(215, 10, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.SPIRIFLOW, HeroEnum.TERRIKAHT, HeroEnum.GENERATOR, HeroEnum.ZAPATAUR),
        new MissionInfo(216, 10, HeroEnum.ZEPHYKAHT, HeroEnum.FLORAVULP, HeroEnum.ZEPHYKAHT, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR, HeroEnum.SPIRIGROW),
        new MissionInfo(217, 10, HeroEnum.HYDROKAHT, HeroEnum.CRYOKAHT, HeroEnum.HYDROKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.ARDOWSE),
        new MissionInfo(218, 10, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.MUDCAP, HeroEnum.SPIRIGROW, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(219, 10, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.ZEPHYKAHT, HeroEnum.INFERNIKAHT, HeroEnum.ZAPATAUR, HeroEnum.ARBURN),
        new MissionInfo(220, 10, HeroEnum.CRYOKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.CRYOKAHT, HeroEnum.TERRIKAHT, HeroEnum.INFERNIKAHT)
    };

        private static readonly MissionInfo[] chapter23 = {
        new MissionInfo(231, 10, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.ARBERRY),
        new MissionInfo(232, 10, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ARBURN, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP, HeroEnum.SPIRIGROW),
        new MissionInfo(233, 10, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FREEZER, HeroEnum.INFERNIKAHT, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP),
        new MissionInfo(234, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.ROCKOTAUR, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(235, 10, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.TERRIKAHT, HeroEnum.BOLTCAP),
        new MissionInfo(236, 10, HeroEnum.ZEPHYKAHT, HeroEnum.MARIVULP, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR),
        new MissionInfo(237, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.FREEZER, HeroEnum.GLACITAUR, HeroEnum.FREEZER, HeroEnum.GLACITAUR),
        new MissionInfo(238, 10, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.MUDCAP, HeroEnum.ROCKOTAUR, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(239, 10, HeroEnum.ZEPHYKAHT, HeroEnum.BOLTCAP, HeroEnum.BOLTCAP, HeroEnum.GENERATOR, HeroEnum.ZEPHYKAHT, HeroEnum.ZAPATAUR),
        new MissionInfo(240, 10, HeroEnum.HYDROKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW)
    };

        private static readonly MissionInfo[] chapter24 = {
        new MissionInfo(241, 10, HeroEnum.HYDROKAHT, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.ZAPATAUR, HeroEnum.SPIRIFLOW, HeroEnum.GENERATOR),
        new MissionInfo(242, 10, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW),
        new MissionInfo(243, 10, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.FLORAVULP, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGROW, HeroEnum.SCOROVULP),
        new MissionInfo(244, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ARBURN, HeroEnum.GLACITAUR, HeroEnum.SCOROVULP, HeroEnum.FREEZER),
        new MissionInfo(245, 10, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.FREEZER, HeroEnum.TERRIKAHT, HeroEnum.GLACITAUR, HeroEnum.ROCKOTAUR),
        new MissionInfo(246, 10, HeroEnum.ZEPHYKAHT, HeroEnum.PULVERIZER, HeroEnum.ZEPHYKAHT, HeroEnum.GENERATOR, HeroEnum.ROCKOTAUR, HeroEnum.ZAPATAUR),
        new MissionInfo(247, 10, HeroEnum.MARIVULP, HeroEnum.MARIVULP, HeroEnum.HYDROKAHT, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIFLOW),
        new MissionInfo(248, 10, HeroEnum.SPIRIGNITE, HeroEnum.SPIRIGNITE, HeroEnum.ARBURN, HeroEnum.SCOROVULP, HeroEnum.INFERNIKAHT, HeroEnum.SCOROVULP),
        new MissionInfo(249, 10, HeroEnum.SPIRIGROW, HeroEnum.BOTANIKAHT, HeroEnum.LEPHYTA, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW, HeroEnum.SPIRIGROW),
        new MissionInfo(250, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.ROCKOTAUR, HeroEnum.FREEZER, HeroEnum.GENERATOR)
    };

        private static readonly MissionInfo[] chapter25 = {
        new MissionInfo(251, 10, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGNITE, HeroEnum.HYDROKAHT, HeroEnum.SPIRIGROW, HeroEnum.SPIRIFLOW, HeroEnum.SCOROVULP),
        new MissionInfo(252, 10, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.SPIRIGROW, HeroEnum.GLACITAUR),
        new MissionInfo(253, 10, HeroEnum.INFERNIKAHT, HeroEnum.PULVERIZER, HeroEnum.ARBURN, HeroEnum.ROCKOTAUR, HeroEnum.INFERNIKAHT, HeroEnum.FREEZER),
        new MissionInfo(254, 10, HeroEnum.CRYOKAHT, HeroEnum.CRYOKAHT, HeroEnum.PULVERIZER, HeroEnum.GENERATOR, HeroEnum.FREEZER, HeroEnum.ZAPATAUR),
        new MissionInfo(255, 10, HeroEnum.TERRIKAHT, HeroEnum.PULVERIZER, HeroEnum.SPIRIFLOW, HeroEnum.TERRIKAHT, HeroEnum.GENERATOR, HeroEnum.ZAPATAUR),
        new MissionInfo(256, 10, HeroEnum.ZEPHYKAHT, HeroEnum.FLORAVULP, HeroEnum.ZEPHYKAHT, HeroEnum.SPIRIFLOW, HeroEnum.ZAPATAUR, HeroEnum.SPIRIGROW),
        new MissionInfo(257, 10, HeroEnum.HYDROKAHT, HeroEnum.CRYOKAHT, HeroEnum.HYDROKAHT, HeroEnum.FREEZER, HeroEnum.SPIRIFLOW, HeroEnum.ARDOWSE),
        new MissionInfo(258, 10, HeroEnum.BOTANIKAHT, HeroEnum.BOTANIKAHT, HeroEnum.MUDCAP, HeroEnum.SPIRIGROW, HeroEnum.TERRIKAHT, HeroEnum.ROCKOTAUR),
        new MissionInfo(259, 10, HeroEnum.INFERNIKAHT, HeroEnum.SPIRIGNITE, HeroEnum.ZEPHYKAHT, HeroEnum.INFERNIKAHT, HeroEnum.ZAPATAUR, HeroEnum.ARBURN),
        new MissionInfo(260, 10, HeroEnum.CRYOKAHT, HeroEnum.BOTANIKAHT, HeroEnum.ZEPHYKAHT, HeroEnum.CRYOKAHT, HeroEnum.TERRIKAHT, HeroEnum.INFERNIKAHT)
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

        public static List<AccountEquipment> GetStockEquipmentLoadout(AccountHero hero, bool nerfGear) {
            var equipped = new List<AccountEquipment>();
            var baseHero = hero.GetBaseHero();
            var awakeningLevel = nerfGear ? hero.AwakeningLevel - 2 : hero.AwakeningLevel;
            if (awakeningLevel < 1) return equipped;
            if (baseHero.PreferredMainHand != null) equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredMainHand.GetValueOrDefault(), awakeningLevel));
            if (baseHero.PreferredOffHand != null) equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredOffHand.GetValueOrDefault(), awakeningLevel));
            if (baseHero.PreferredTwoHand != null) equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredTwoHand.GetValueOrDefault(), awakeningLevel));
            equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredChest, awakeningLevel));
            equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredLegs, awakeningLevel));
            equipped.Add(new AccountEquipment(hero.GetBaseHero().PreferredHead, awakeningLevel));
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
