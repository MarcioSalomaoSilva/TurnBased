using System.Collections;
using System.Collections.Generic;
//
public class TBS_Classes {
    //
    [System.Serializable]
    public class Ability {
        public string name;
        public string description;
        public int baseDamageAmount;
    }
    [System.Serializable]
    public class Attack : Ability {
        public int CalculateDamage(int Attack) {
            return Attack;
        }
    }
    [System.Serializable]
    public class Character {
        public int id;
        public int level;
        public enum Job {
            Warrior, Fighter, Knight, Monk, Master, Thief, Ninja, WhiteMage, Summoner, BlackMage, RedMage
        }
        public enum Row {
            Front, Back, empty
        }
        public Job job;
        public Row row;
        public Character(int id, int _level, Job _job, Row _row) {
            level = _level;
            job = _job;
            row = _row;
        } 
    }
    [System.Serializable]
    public class Guard {
        public int level = 5;
        public int health = 40;
        public int magic = 15;
        public int attackPow = 16;
        public int magicPow = 6;
        public int defense = 0;
        public int magicDefense = 140;
        public int magicEvasion = 0;
        public int speed = 30;
        public int hitRate = 100;
        public int evasion = 0;
        public int experience = 48;
        public int gil = 48;
        public Guard(int _level, int _health, int _magic, int _attackPow, int _magicPow, int _defense, int _magicDefense, int _magicEvasion, int _speed, int _hitRate, int _evasion, int _experience, int _gil) {
            level = _level;
            health = _health;
            magic = _magic;
            attackPow = _attackPow;
            magicPow = _magicPow;
            defense = _defense;
            magicDefense = _magicDefense;
            magicEvasion = _magicEvasion;
            speed = _speed;
            hitRate = _hitRate;
            evasion = _evasion;
            experience = _experience;
            gil = _gil;
        }
    }
    //starting Stats. These get multiplied by the level to determine things like damage.
    //
    public static class Warrior {// celes
        public static int[] Stats(int level) {
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(40, level); //vigor
            stats[3] = CalculateSpeedStat(34, level); //speed
            stats[4] = CalculateStaminaStat(31, level); //stamina
            stats[5] = CalculateAttackPowStat(36, level); //attack pow
            stats[6] = CalculateMagicPowStat(16, level); //magic pow
            stats[7] = CalculateDefenseStat(44, level); //defnse
            stats[8] = CalculateMagDefenseStat(31, level); //magic defense
            stats[9] = CalculateMagBlockStat(9, level); //magic block
            stats[10] = CalculateMagBlockStat(7, level); //luck
            return stats;
        }
    }
    public static class Fighter {//edgar
        public static int[] Stats(int level) {
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(39, level); //vigor
            stats[3] = CalculateSpeedStat(30, level); //speed
            stats[4] = CalculateStaminaStat(34, level); //stamina
            stats[5] = CalculateAttackPowStat(29, level); //attack pow
            stats[6] = CalculateMagicPowStat(20, level); //magic pow
            stats[7] = CalculateDefenseStat(48, level); //defnse
            stats[8] = CalculateMagDefenseStat(22, level); //magic defense
            stats[9] = CalculateMagBlockStat(1, level);//magic block
            stats[10] = CalculateMagBlockStat(4, level); //luck
            return stats;
        }
    }
    public static class Knight {//cyan
        public static int[] Stats(int level) {
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(40, level); //vigor
            stats[3] = CalculateSpeedStat(28, level); //speed
            stats[4] = CalculateStaminaStat(33, level); //stamina
            stats[5] = CalculateAttackPowStat(25, level); //attack pow
            stats[6] = CalculateMagicPowStat(25, level); //magic pow
            stats[7] = CalculateDefenseStat(50, level); //defnse
            stats[8] = CalculateMagDefenseStat(20, level); //magic defense
            stats[9] = CalculateMagBlockStat(1, level); //magic block
            stats[10] = CalculateMagBlockStat(6, level); //luck
            return stats;
        }
    }
    public static class Monk {//sabin
        public static int[] Stats(int level) { 
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(47, level); //vigor
            stats[3] = CalculateSpeedStat(37, level); //speed
            stats[4] = CalculateStaminaStat(39, level); //stamina
            stats[5] = CalculateAttackPowStat(28, level); //attack pow
            stats[6] = CalculateMagicPowStat(26, level); //magic pow
            stats[7] = CalculateDefenseStat(53, level); //defnse
            stats[8] = CalculateMagDefenseStat(21, level); //magic defense
            stats[9] = CalculateMagBlockStat(4, level); //magic block
            stats[10] = CalculateMagBlockStat(12, level); //luck
            return stats;
        }
    }
    public static class Master {// setzer
        public static int[] Stats(int level) {
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(36, level); //vigor
            stats[3] = CalculateSpeedStat(32, level); //speed
            stats[4] = CalculateStaminaStat(32, level); //stamina
            stats[5] = CalculateAttackPowStat(29, level); //attack pow
            stats[6] = CalculateMagicPowStat(18, level); //magic pow
            stats[7] = CalculateDefenseStat(50, level); //defnse
            stats[8] = CalculateMagDefenseStat(26, level); //magic defense
            stats[9] = CalculateMagBlockStat(1, level); //magic block
            stats[10] = CalculateMagBlockStat(9, level); //luck
            return stats;
        }
    }
    public static class Thief {//locke
        public static int[] Stats(int level) {
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(37, level); //vigor
            stats[3] = CalculateSpeedStat(40, level); //speed
            stats[4] = CalculateStaminaStat(13, level);  //stamina
            stats[5] = CalculateAttackPowStat(28, level); //attack pow
            stats[6] = CalculateMagicPowStat(14, level); //magic pow
            stats[7] = CalculateDefenseStat(46, level); //defnse
            stats[8] = CalculateMagDefenseStat(23, level); //magic defense
            stats[9] = CalculateMagBlockStat(2, level); //magic block
            stats[10] = CalculateMagBlockStat(15, level); //luck
            return stats;
        }
    }
    public static class Ninja {//shadow
        public static int[] Stats(int level) {
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(39, level); //vigor
            stats[3] = CalculateSpeedStat(38, level); //speed
            stats[4] = CalculateStaminaStat(30, level); //stamina
            stats[5] = CalculateAttackPowStat(33, level); //attack pow
            stats[6] = CalculateMagicPowStat(23, level); //magic pow
            stats[7] = CalculateDefenseStat(47, level); //defnse
            stats[8] = CalculateMagDefenseStat(25, level); //magic defense
            stats[9] = CalculateMagBlockStat(9, level); //magic block
            stats[10] = CalculateMagBlockStat(18, level); //luck
            return stats;
        }
    }
    public static class WhiteMage {//gogo
        public static int[] Stats(int level) {
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(31, level); //vigor
            stats[3] = CalculateSpeedStat(30, level); //speed
            stats[4] = CalculateStaminaStat(20, level); //stamina
            stats[5] = CalculateAttackPowStat(35, level); //attack pow
            stats[6] = CalculateMagicPowStat(16, level); //magic pow
            stats[7] = CalculateDefenseStat(39, level); //defnse
            stats[8] = CalculateMagDefenseStat(30, level); //magic defense
            stats[9] = CalculateMagBlockStat(8, level); //magic block
            stats[10] = CalculateMagBlockStat(10, level); //luck
            return stats;
        }
    }
    public static class Summoner {//terra
        public static int[] Stats(int level) {
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(31, level); ; //vigor
            stats[3] = CalculateSpeedStat(33, level); //speed
            stats[4] = CalculateStaminaStat(28, level); //stamina
            stats[5] = CalculateAttackPowStat(39, level); //attack pow
            stats[6] = CalculateMagicPowStat(12, level); //magic pow
            stats[7] = CalculateDefenseStat(42, level); //defnse
            stats[8] = CalculateMagDefenseStat(33, level); //magic defense
            stats[9] = CalculateMagBlockStat(7, level); //magic block
            stats[10] = CalculateMagBlockStat(5, level); //luck
            return stats;
        }
    }
    public static class BlackMage {//strago
        public static int[] Stats(int level) {
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(28, level); //vigor
            stats[3] = CalculateSpeedStat(25, level); //speed
            stats[4] = CalculateStaminaStat(19, level); //stamina
            stats[5] = CalculateAttackPowStat(34, level); //attack pow
            stats[6] = CalculateMagicPowStat(10, level); //magic pow
            stats[7] = CalculateDefenseStat(33, level); //defnse
            stats[8] = CalculateMagDefenseStat(27, level); //magic defense
            stats[9] = CalculateMagBlockStat(7, level); //magic block
            stats[10] = CalculateMagBlockStat(6, level); //luck
            return stats;
        }
    }
    public static class RedMage {//relm
        public static int[] Stats(int level) {
            int[] stats = new int[11];
            stats[0] = CalculateHealthStat(11, level); //health
            stats[1] = CalculateMagicStat(4, level); //magic
            stats[2] = CalculateVigorStat(26, level); //vigor
            stats[3] = CalculateSpeedStat(34, level); //speed
            stats[4] = CalculateStaminaStat(22, level); //stamina
            stats[5] = CalculateAttackPowStat(44, level); //attack pow
            stats[6] = CalculateMagicPowStat(11, level); //magic pow
            stats[7] = CalculateDefenseStat(35, level); //defnse
            stats[8] = CalculateMagDefenseStat(30, level); //magic defense
            stats[9] = CalculateMagBlockStat(9, level); //magic block
            stats[10] = CalculateMagBlockStat(13, level); //luck
            return stats;
        }
    }
    //For calculating stats with current level
    public static int CalculateHealthStat(int health, int level) {
        int cur = health + level;
        return cur;
    }
    public static int CalculateMagicStat(int magic, int level) {
        int cur = magic + level;
        return cur;
    }
    public static int CalculateVigorStat(int vigor, int level) {
        int cur = vigor + level;
        return cur;
    }
    public static int CalculateSpeedStat(int speed, int level) {
        int cur = speed + level;
        return cur;
    }
    public static int CalculateStaminaStat(int stamina, int level) {
        int cur = stamina + level;
        return cur;
    }
    public static int CalculateAttackPowStat(int attackPow, int level) {
        int cur = attackPow + level;
        return cur;
    }
    public static int CalculateMagicPowStat(int magicPow, int level) {
        int cur = magicPow + level;
        return cur;
    }
    public static int CalculateDefenseStat(int defense, int level) {
        int cur = defense + level;
        return cur;
    }
    public static int CalculateMagDefenseStat(int magicDefense, int level) {
        int cur = magicDefense + level;
        return cur;
    }
    public static int CalculateMagBlockStat(int magicBlock, int level) {
        int cur = magicBlock + level;
        return cur;
    }
    public static int CalculateLuckStat(int luck, int level) {
        int cur = luck + level;
        return cur;
    }
    //For Calculating level from experience
    //int CalculateLevel(int experience) {
    //    int level = 
    //    return;
    //}
}
