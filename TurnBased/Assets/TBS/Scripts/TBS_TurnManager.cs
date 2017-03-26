using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TBS_Manager))]
[RequireComponent(typeof(TBS_GUI))]
[System.Serializable]
public class TBS_HandleTurn {
    public string name;
    public GameObject Active;
    public GameObject Target;
}
public class TBS_TurnManager : MonoBehaviour {
    public static TBS_TurnManager instance;
    public delegate void TurnManagerDelegate();
    public static event TurnManagerDelegate TMStartBattle;
    public static event TurnManagerDelegate TMProcessBattle;
    public static event TurnManagerDelegate TMChooseAction;
    public static event TurnManagerDelegate TMTakeAction;
    public static event TurnManagerDelegate TMEnemyPrompt;
    public static event TurnManagerDelegate TMPlayer;
    public static event TurnManagerDelegate TMPlayerPrompt;
    public enum TurnManager {
        Start, Wait, ChooseAction, TakeAction, PerformAction, End
    }
    TurnManager currentState;
    public List<TBS_HandleTurn> turnListHandler = new List<TBS_HandleTurn>();
    public List<TBS_Turn> totalList = new List<TBS_Turn>();
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> playerList = new List<GameObject>();
    void OnEnable () {
        TBS_GUI.GUIStartBattle += StartTakeAction;
	}
    void Awake() {
        instance = this;
        currentState = TurnManager.Start;
    }
	void Update () {
        switch (currentState) {
            case TurnManager.Start:
                StartBattle();
                currentState = TurnManager.ChooseAction;
                break;
            case TurnManager.Wait:
                //receives from turn
                //remove first item from list
                //repeat take action
                break;
            case TurnManager.ChooseAction:
                totalList[0].currentState = TBS_Turn.TurnState.ChooseAction;
                currentState = TurnManager.Wait;
                Debug.Log("action sent");
                break;
            case TurnManager.TakeAction:
                if (totalList[0].enemy == true) {
                    if (TMEnemyPrompt != null) {
                        TMEnemyPrompt();  
                    }
                    totalList[0].currentState = TBS_Turn.TurnState.TakeTurn;
                } else {
                    if (TMPlayerPrompt != null) {
                        TMPlayerPrompt();
                    }
                    totalList[0].currentState = TBS_Turn.TurnState.TakeTurn;
                }
                break;
            case TurnManager.End:
                break;
        }
    }
    void StartTakeAction() {
        if (totalList[0].enemy == true) {
            currentState = TurnManager.TakeAction;
        } else {
            if (TMPlayer != null) {
                TMPlayer();
            }
            currentState = TurnManager.Wait;
        }
    }
    void StartBattle() {
        GetList();
        if (TMStartBattle != null) {
            TMStartBattle();
        }
    }
    void GetList() {
        playerList.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        enemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        GetSortTotalList();
    }
    void GetSortTotalList() {
        totalList.AddRange(FindObjectsOfType<TBS_Turn>());
        totalList.Sort(SortBySpeed);
        if (totalList.Count < 8) {
            totalList.AddRange(totalList);
        }
    }
    int SortBySpeed(TBS_Turn char1, TBS_Turn char2) {
        return char2.speed.CompareTo(char1.speed);
    }
    public void CollectActions(TBS_HandleTurn input) {
        turnListHandler.Add(input);
    }
}
