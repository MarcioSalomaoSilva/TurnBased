using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBS_Turn : MonoBehaviour {
    public TBS_TurnManager turnManager;
    public delegate void TurnDelegate();
    public static event TurnDelegate T;
    TBS_HandleTurn attack;
    [HideInInspector]
    public bool enemy;
    public string thisName;
    public int level;
    public int health;
    public int maxHealth;
    public int magic;
    public int maxMagic;
    public int vigor;
    public int speed;
    public int stamina;
    public int attackPow;
    public int magicPow;
    public int defense;
    public int magDefense;
    public int magBlock;
    public int luck;
    Vector3 pos;
    //
    bool test;
    //
    public enum TurnState {
        WaitTurn, AddToList, TakeTurn, ChooseAction, Dead
    }
    public TurnState currentState;
    private Vector3 startPosition;
    //
    void OnEnable() {
        TBS_TurnManager.TMStartBattle += StartBattle;
        TBS_TurnManager.TMTakeAction += TakeAction;
    }
    void Update() {
        ClampVariables();
        this.transform.forward = Camera.main.transform.forward;
        if (enemy) {
            switch (currentState) {
                case TurnState.WaitTurn:
                    break;
                case TurnState.AddToList:
                    break;
                case TurnState.ChooseAction:
                    ChooseAction();
                    Debug.Log("Take action recieved");
                    currentState = TurnState.WaitTurn;
                    break;
                case TurnState.TakeTurn:
                    Debug.Log("Taking turn");
                    TakeTurnEnemy();
                    break;
                case TurnState.Dead:
                    break;
            }
        } else {
            switch (currentState) {
                case TurnState.WaitTurn:
                    break;
                case TurnState.AddToList:
                    break;
                case TurnState.ChooseAction:
                    //GETAction(); // gets the action from gui
                    Debug.Log("Take action recieved");
                    //currentState = TurnState.WaitTurn;
                    break;
                case TurnState.TakeTurn:
                    Debug.Log("Taking turn");
                    TakeTurnPlayer();
                    break;
                case TurnState.Dead:
                    break;
            }
        }
    }
    void StartBattle() {
        if (this.gameObject.tag != "Player") {
            enemy = true;
        } else {
            enemy = false;
        }
        thisName = gameObject.ToString();
        startPosition = this.transform.position;
        currentState = TurnState.WaitTurn;
    }
    void ChooseAction() {
        attack = new TBS_HandleTurn();
        attack.name = thisName;
        attack.Active = this.gameObject;
        pos = new Vector3(attack.Active.transform.position.x + 1, attack.Active.transform.position.y, attack.Active.transform.position.z);
        attack.Target = TBS_TurnManager.instance.playerList[Random.Range(0, TBS_TurnManager.instance.playerList.Count)];
        TBS_TurnManager.instance.CollectActions(attack);
        Debug.Log("The target is "+attack.Target); 
    }
    void TakeAction() {
        //currentState = TurnState.TakeTurn;
    }
    void TakeTurnEnemy() {// this should be a couroutine?
        //move forward
        transform.position = Vector3.MoveTowards(this.transform.position, pos, Time.deltaTime * 5);
        //start animation coroutine

        //deplete health 
        //send to to turn manager
    }
    void TakeTurnPlayer() {// this should be a couroutine?
        //move forward
        transform.position = Vector3.MoveTowards(this.transform.position, pos, Time.deltaTime * 5);
        //start animation coroutine

        //deplete health 
        //send to to turn manager
    }
    //needs methods for attacking defending and row change for now the damage and effects can be calculated using te handler and maybe called by gui if the none of them use spell damage
    //other spells will most likely need to really on scriptable objects in the future with a single spell method in this class/script
    //spells and items from scriptable objects will need to be added to a list and called from gui
    //enemies will need there own functions for attack and spells as well as their own spells and spell list generator for randomization. They will also need some ai conditions (low health mana) in takeenemyturn
    //this script will also need a functions to call sound and animation/texture if it uses scriptable objects
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
}
