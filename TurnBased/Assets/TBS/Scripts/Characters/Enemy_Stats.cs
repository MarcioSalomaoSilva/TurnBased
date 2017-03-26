using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class Enemy_Stats : MonoBehaviour {
    public TBS_Classes.Guard enemy;
    void Awake () {
        CreateTurn();
        enemy.level = 5;
        enemy.health = 40;
        enemy.magic = 15;
        enemy.attackPow = 16;
        enemy.magicPow = 6;
        enemy.defense = 0;
        enemy.magicDefense = 140;
        enemy.magicEvasion = 0;
        enemy.speed = 30;
        enemy.hitRate = 100;
        enemy.evasion = 0;
        enemy.experience = 48;
        enemy.gil = 48;
        TurnSpeed();
    }
    void CreateTurn() {
        this.gameObject.AddComponent<TBS_Turn>();
    }
    void TurnSpeed() {
        this.gameObject.GetComponent<TBS_Turn>().level = enemy.level;
        this.gameObject.GetComponent<TBS_Turn>().health = enemy.health;
        this.gameObject.GetComponent<TBS_Turn>().maxHealth = enemy.health;
        this.gameObject.GetComponent<TBS_Turn>().magic = enemy.magic;
        this.gameObject.GetComponent<TBS_Turn>().maxMagic = enemy.magic;
        this.gameObject.GetComponent<TBS_Turn>().vigor = Random.Range(56, 63);
        this.gameObject.GetComponent<TBS_Turn>().speed = Random.Range(enemy.speed - 9, enemy.speed + 9);
        this.gameObject.GetComponent<TBS_Turn>().attackPow = enemy.attackPow;
        this.gameObject.GetComponent<TBS_Turn>().magicPow = enemy.magicPow;
        this.gameObject.GetComponent<TBS_Turn>().defense = enemy.defense;
    }
}
