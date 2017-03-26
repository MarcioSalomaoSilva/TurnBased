using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class One_Stats : MonoBehaviour {
    public static One_Stats instance;
    [HideInInspector]
    int[] stats = new int[11];
    public string thisName;
    [HideInInspector]
    public int level;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public int maxHealth;
    [HideInInspector]
    public int magic;
    [HideInInspector]
    public int maxMagic;
    [HideInInspector]
    public int vigor;
    [HideInInspector]
    public int speed;
    [HideInInspector]
    public int stamina;
    [HideInInspector]
    public int attackPow;
    [HideInInspector]
    public int magicPow;
    [HideInInspector]
    public int defense;
    [HideInInspector]
    public int magDefense;
    [HideInInspector]
    public int magBlock;
    [HideInInspector]
    public int luck;
    void Awake() {
        instance = this;
        GetStats();
    }
    void Update () {
        ClampVariables();
        TurnSpeed();
        if (Input.GetKeyDown("o")) {
            health -= 10;
        }
    }
    public void GetStats() {
        level = Start_Battle.instance.one.level;
        thisName = this.gameObject.ToString();
        stats = GetCharacterStats(Start_Battle.instance.one, Start_Battle.instance.one.level);
        health = stats[0];
        maxHealth = health;
        magic = stats[1];
        maxMagic = magic;
        vigor = stats[2];
        speed = stats[3];
        stamina = stats[4];
        attackPow = stats[5];
        magicPow = stats[6];
        defense = stats[7];
        magDefense = stats[8];
        magBlock = stats[9];
        luck = stats[10];
        CreateTurn();
    }
    void ClampVariables() {
        health = Clamp(health, maxHealth);
        maxHealth = ClampTotal(maxHealth);
        magic = Clamp(magic, maxMagic);
        maxMagic = ClampTotal(maxMagic);
        vigor = ClampStat(vigor);
        speed = ClampStat(speed);
        stamina = ClampStat(stamina);
        attackPow = ClampStat(attackPow);
        magicPow = ClampStat(magicPow);
        defense = ClampStat(defense);
        magDefense = ClampStat(magDefense);
        magBlock = ClampStat(magBlock);
        luck = ClampStat(luck);
    }
    int ClampStat(int var) {
        var = Mathf.Clamp(var, 1, 255);
        return var;
    }
    int ClampLevel(int var) {
        var = Mathf.Clamp(var, 1, 99);
        return var;
    }
    int ClampTotal(int var) {
        var = Mathf.Clamp(var, 0, 9999);
        return var;
    }
    int Clamp(int var, int var2) {
        var = Mathf.Clamp(var, 0, var2);
        return var;
    }
    void CreateTurn() {
        this.gameObject.AddComponent<TBS_Turn>(); 
    }
    void TurnSpeed() {
        this.gameObject.GetComponent<TBS_Turn>().level = level;
        this.gameObject.GetComponent<TBS_Turn>().health = health;
        this.gameObject.GetComponent<TBS_Turn>().maxHealth = maxHealth;
        this.gameObject.GetComponent<TBS_Turn>().magic = magic;
        this.gameObject.GetComponent<TBS_Turn>().maxMagic = maxMagic;
        this.gameObject.GetComponent<TBS_Turn>().vigor = vigor;
        this.gameObject.GetComponent<TBS_Turn>().speed = speed;
        this.gameObject.GetComponent<TBS_Turn>().stamina = stamina;
        this.gameObject.GetComponent<TBS_Turn>().attackPow = attackPow;
        this.gameObject.GetComponent<TBS_Turn>().magicPow = magicPow;
        this.gameObject.GetComponent<TBS_Turn>().defense = defense;
        this.gameObject.GetComponent<TBS_Turn>().magDefense = magDefense;
        this.gameObject.GetComponent<TBS_Turn>().magBlock = magBlock;
        this.gameObject.GetComponent<TBS_Turn>().luck = luck;
    }
    int[] GetCharacterStats(TBS_Classes.Character character, int level) {
        int[] prefabStats = new int[12];
        switch (character.job) {
            case TBS_Classes.Character.Job.Warrior:
                prefabStats = TBS_Classes.Warrior.Stats(level);
                break;
            case TBS_Classes.Character.Job.Fighter:
                prefabStats = TBS_Classes.Fighter.Stats(level);
                break;
            case TBS_Classes.Character.Job.Knight:
                prefabStats = TBS_Classes.Knight.Stats(level);
                break;
            case TBS_Classes.Character.Job.Monk:
                prefabStats = TBS_Classes.Monk.Stats(level);
                break;
            case TBS_Classes.Character.Job.Master:
                prefabStats = TBS_Classes.Master.Stats(level);
                break;
            case TBS_Classes.Character.Job.Thief:
                prefabStats = TBS_Classes.Thief.Stats(level);
                break;
            case TBS_Classes.Character.Job.Ninja:
                prefabStats = TBS_Classes.Ninja.Stats(level);
                break;
            case TBS_Classes.Character.Job.WhiteMage:
                prefabStats = TBS_Classes.WhiteMage.Stats(level);
                break;
            case TBS_Classes.Character.Job.Summoner:
                prefabStats = TBS_Classes.Summoner.Stats(level);
                break;
            case TBS_Classes.Character.Job.BlackMage:
                prefabStats = TBS_Classes.BlackMage.Stats(level);
                break;
            case TBS_Classes.Character.Job.RedMage:
                prefabStats = TBS_Classes.RedMage.Stats(level);
                break;
            default:
                prefabStats = TBS_Classes.Warrior.Stats(level);
                break;
        }
        return prefabStats;
    }
}
