using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TBS_TurnManager))]
[RequireComponent(typeof(TBS_Manager))]
[System.Serializable]
public class GUI_Enemies {
    public List<Rect> enemyGUIList = new List<Rect>();
}
public class TBS_GUI : MonoBehaviour {
    public static TBS_GUI instance;
    public delegate void GUIManagerDelegate();
    public static event GUIManagerDelegate GUIStartBattle;
    public enum TurnManager {
        Start, Wait, Player, Enemy, End
    }
    public TurnManager currentState;
    TBS_HandleTurn attack;
    public Texture whiteBox;
    public Texture whiteArrow;
    [HideInInspector]
    bool start;
    bool attackClicked;
    List<Rect> enemyGUIList = new List<Rect>();
    List<Rect> playerGUIName = new List<Rect>();
    List<Rect> playerGUIHealth = new List<Rect>();
    List<Rect> playerGUIMagic = new List<Rect>();
    List<Rect> playerGUIMagicBar = new List<Rect>();
    List<Rect> playerGUIHealthBar = new List<Rect>();
    List<Rect> turnArrowGUI = new List<Rect>();
    List<Rect> turnOrderGUI = new List<Rect>();
    Rect turn;
    float healthBar1;
    float magicBar1;
    float healthBar2;
    float magicBar2;
    float healthBar3;
    float magicBar3;
    float healthBar4;
    float magicBar4;
    float boxSize;
    float screenBoxPos;
    float offset;
    //
    void OnEnable() {
        TBS_TurnManager.TMStartBattle += StartBattle;
        TBS_TurnManager.TMEnemyPrompt += TMEnemyPrompt;
        TBS_TurnManager.TMPlayer += TMPlayer;
    }
    void StartBattle() {
        instance = this;
        GetSetVariables();
        AddToLists();
        currentState = TurnManager.Start;
    }
    void TMEnemyPrompt() {
        currentState = TurnManager.Enemy;
    }
    void TMPlayer() {
        currentState = TurnManager.Wait;
    }
    void OnGUI() {
        switch (currentState) {
            case TurnManager.Start:
                TurnOrderGUI();
                StaticGUI();
                StartGUI();
                LabelGUI();
                StartCoroutine(StartBattleGUI());
                break;
            case TurnManager.Wait:
                TurnOrderGUI();
                StaticGUI();
                PlayerMenuGUI();
                LabelGUI();
                break;
            case TurnManager.Player:
                TurnOrderGUI();
                StaticGUI();
                PlayerGUIPrompt();
                LabelGUI();
                break;
            case TurnManager.Enemy:
                TurnOrderGUI();
                StaticGUI();
                EnemyGUIPrompt();
                LabelGUI();
                break;
            case TurnManager.End:
                break;
        }
        Debug.Log(currentState);
    }
    void StartGUI() {
        if (start) {
            GUI.Box(new Rect(Screen.width / 2 - (Screen.width / 7) / 2, Screen.height / 2 - (Screen.height / 20) / 2, Screen.width / 7, Screen.height / 25), "Start Battle");
        }
    }
    void StaticGUI() {
        GetSetVariables();
        //bottom window
        GUI.Box(new Rect(0, Screen.height - Screen.height / 6, Screen.width, Screen.height / 6), "");
        //enemy window
        GUI.Box(new Rect(0, Screen.height - Screen.height / 6, Screen.width / 4, Screen.height / 5), "Enemies");
        //command window
        GUI.Box(new Rect(Screen.width / 4, Screen.height - Screen.height / 6, Screen.width / 4, Screen.height / 5), "");
        //player stat window
        GUI.Box(new Rect(Screen.width / 2, Screen.height - Screen.height / 6, (Screen.width / 2), Screen.height / 6), "");//box
        //player stat titles
        GUI.Label(new Rect(Screen.width / 2 + Screen.width / 70, Screen.height - Screen.height / 6, (Screen.width / 5) - Screen.width / 70, Screen.height / 30), "Character");
        GUI.Label(new Rect((Screen.width / 2) + (Screen.width / 5), Screen.height - Screen.height / 6, (Screen.width / 13), Screen.height / 30), "Health");
        GUI.Label(new Rect((Screen.width / 2) + (Screen.width / 5) * 1.4f, Screen.height - Screen.height / 6, (Screen.width / 5), Screen.height / 30), "Mana");
        GUI.Label(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6, Screen.width / 7, Screen.height / 30), "Stat Bars");//bar
    }
    void LabelGUI() {
        for (int i = 0; i < TBS_TurnManager.instance.playerList.Count; i++) {
            //characters sorted by height
            GUI.Label(playerGUIName[i], TBS_TurnManager.instance.playerList[i].GetComponent<TBS_Turn>().thisName);
            //player health
            GUI.Label(playerGUIHealth[i], TBS_TurnManager.instance.playerList[i].GetComponent<TBS_Turn>().health + " / " + TBS_TurnManager.instance.playerList[i].GetComponent<TBS_Turn>().maxHealth);
            //player mana
            GUI.Label(playerGUIMagic[i], TBS_TurnManager.instance.playerList[i].GetComponent<TBS_Turn>().magic + " / " + TBS_TurnManager.instance.playerList[i].GetComponent<TBS_Turn>().maxMagic);
            //bars
            GUI.Box(playerGUIMagicBar[i], "");
            GUI.Box(playerGUIHealthBar[i], "");
        }
        for (int i = 0; i < TBS_TurnManager.instance.enemyList.Count; i++) {
            GUI.Label(enemyGUIList[i], TBS_TurnManager.instance.enemyList[i].gameObject.ToString());
        }
        //health and magic bars
        Color savedGUI = GUI.color;
        GUI.color = new Color(Color.green.r, Color.green.b, Color.green.g, 0.3f);
        GUI.DrawTexture(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 + Screen.height / 240, (Screen.width / 7 - Screen.width / 140) * (healthBar1), Screen.height / 60 - Screen.height / 240), whiteBox);
        GUI.DrawTexture(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 2 + Screen.height / 240, (Screen.width / 7 - Screen.width / 140) * (healthBar2), Screen.height / 60 - Screen.height / 240), whiteBox);
        GUI.DrawTexture(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 3 + Screen.height / 240, (Screen.width / 7 - Screen.width / 140) * (healthBar3), Screen.height / 60 - Screen.height / 240), whiteBox);
        GUI.DrawTexture(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 4 + Screen.height / 240, (Screen.width / 7 - Screen.width / 140) * (healthBar4), Screen.height / 60 - Screen.height / 240), whiteBox);
        GUI.color = new Color(Color.blue.r, Color.blue.b, Color.blue.g, 0.3f);
        GUI.DrawTexture(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 + Screen.height / 60, (Screen.width / 7 - Screen.width / 140) * (magicBar1), Screen.height / 60 - Screen.height / 240), whiteBox);
        GUI.DrawTexture(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 2 + Screen.height / 60, (Screen.width / 7 - Screen.width / 140) * (magicBar2), Screen.height / 60 - Screen.height / 240), whiteBox);
        GUI.DrawTexture(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 3 + Screen.height / 60, (Screen.width / 7 - Screen.width / 140) * (magicBar3), Screen.height / 60 - Screen.height / 240), whiteBox);
        GUI.DrawTexture(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 4 + Screen.height / 60, (Screen.width / 7 - Screen.width / 140) * (magicBar4), Screen.height / 60 - Screen.height / 240), whiteBox);
        GUI.color = savedGUI;
    }
    void TurnOrderGUI() {
        GUI.Box(turn, "Turn Order");
        for (int i = 0; i < 8; i++) {
            GUI.Box(turnOrderGUI[i], TBS_TurnManager.instance.totalList[i].ToString());
        }
    }
    void PlayerMenuGUI() {
        bool ready = false;
        GUI.DrawTexture(turnArrowGUI[0], whiteArrow);
        GUI.Box(new Rect(Screen.width / 4, Screen.height - Screen.height / 6, Screen.width / 4, Screen.height / 5), TBS_TurnManager.instance.totalList[0].thisName);
        if (GUI.Button(new Rect(Screen.width / 4, Screen.height - Screen.height / 6 + (Screen.height / 6) / 5, Screen.width / 8, (Screen.height / 6) / 5), "Attack")) {
            attackClicked = true;
        }
        if (attackClicked) {
            for (int i = 0; i < TBS_TurnManager.instance.enemyList.Count; i++) {
                if (GUI.Button(enemyGUIList[i], "")) {
                    Debug.Log(TBS_TurnManager.instance.enemyList[i].ToString() + " is selected");
                    ready = true;
                }
            }
            for (int i = 0; i < TBS_TurnManager.instance.enemyList.Count; i++) {
                GUI.Label(enemyGUIList[i], TBS_TurnManager.instance.enemyList[i].gameObject.ToString());
            }
        }
        if (GUI.Button(new Rect(Screen.width / 4, Screen.height - Screen.height / 6 + ((Screen.height / 6) / 5) * 2, Screen.width / 8, (Screen.height / 6) / 5), "")) {
            attackClicked = false;
        }
        if (GUI.Button(new Rect(Screen.width / 4, Screen.height - Screen.height / 6 + ((Screen.height / 6) / 5) * 3, Screen.width / 8, (Screen.height / 6) / 5), "")) {
            attackClicked = false;
        }
        if (GUI.Button(new Rect(Screen.width / 4, Screen.height - Screen.height / 6 + ((Screen.height / 6) / 5) * 4, Screen.width / 8, (Screen.height / 6) / 5), "")) {
            attackClicked = false;
        }
        if(GUI.Button(new Rect(Screen.width / 4 + Screen.width / 8, Screen.height - Screen.height / 6 + (Screen.height / 6) / 5, Screen.width / 8, (Screen.height / 6) / 5), "Defend")){
            attackClicked = false;
        }
        if(GUI.Button(new Rect(Screen.width / 4 + Screen.width / 8, Screen.height - Screen.height / 6 + ((Screen.height / 6) / 5) * 2, Screen.width / 8, (Screen.height / 6) / 5), "Item")) {
            attackClicked = false;
        }
        if(GUI.Button(new Rect(Screen.width / 4 + Screen.width / 8, Screen.height - Screen.height / 6 + ((Screen.height / 6) / 5) * 3, Screen.width / 8, (Screen.height / 6) / 5), "Change Row")){
            attackClicked = false;
        }
        if(GUI.Button(new Rect(Screen.width / 4 + Screen.width / 8, Screen.height - Screen.height / 6 + ((Screen.height / 6) / 5) * 4, Screen.width / 8, (Screen.height / 6) / 5), "Skip Turn")) {
            attackClicked = false;
        }
        //Name backgrounds
        GUI.Box(new Rect(Screen.width / 2, Screen.height - Screen.height / 6 + Screen.height / 30, Screen.width / 2, Screen.height / 30), "");
        //GUI.Box(new Rect(Screen.width / 2, Screen.height - Screen.height / 6 + Screen.height / 30 * 2, Screen.width / 2, Screen.height / 30), "");
        //GUI.Box(new Rect(Screen.width / 2, Screen.height - Screen.height / 6 + Screen.height / 30 * 3, Screen.width / 2, Screen.height / 30), "");
        //GUI.Box(new Rect(Screen.width / 2, Screen.height - Screen.height / 6 + Screen.height / 30 * 4, Screen.width / 2, Screen.height / 30), "");
        if (ready) {
            currentState = TurnManager.Player;
        }
    }
    void PlayerGUIPrompt() {
        GUI.Box(new Rect(Screen.width / 3, 0, Screen.width - (Screen.width / 3) * 2, Screen.height / 25), TBS_TurnManager.instance.totalList[0].name);
        GUI.Box(new Rect(Screen.width / 3, 0, Screen.width - (Screen.width / 3) * 2, Screen.height / 25), TBS_TurnManager.instance.totalList[0].name);
        GUI.DrawTexture(turnArrowGUI[0], whiteArrow);
    }
    void EnemyGUIPrompt() {
        GUI.Box(new Rect(Screen.width / 3, 0, Screen.width - (Screen.width / 3) * 2, Screen.height / 25), TBS_TurnManager.instance.totalList[0].name);
        GUI.Box(new Rect(Screen.width / 3, 0, Screen.width - (Screen.width / 3) * 2, Screen.height / 25), TBS_TurnManager.instance.totalList[0].name);
        for (int i = 0; i < TBS_TurnManager.instance.enemyList.Count; i++) {
            if (TBS_TurnManager.instance.totalList[0].gameObject.ToString() == TBS_TurnManager.instance.enemyList[i].gameObject.ToString()) {
                GUI.Box(enemyGUIList[i], "");
            }
        }
        GUI.DrawTexture(turnArrowGUI[0], whiteArrow);
            for (int i = 0; i < TBS_TurnManager.instance.playerList.Count; i++) {
                if (TBS_TurnManager.instance.turnListHandler[0].Target.gameObject.ToString() == TBS_TurnManager.instance.playerList[i].gameObject.ToString()) {
                    GUI.DrawTexture(turnArrowGUI[i], whiteArrow);
                }
            
        }
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(TBS_TurnManager.instance.turnListHandler[0].Target.transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.Box(new Rect(screenPosition.x - 20, screenPosition.y - 40, 80, 20), "test");
    }
    void GetSetVariables() {
        healthBar1 = (float)One_Stats.instance.gameObject.GetComponent<TBS_Turn>().health / One_Stats.instance.maxHealth;
        magicBar1 = (float)One_Stats.instance.gameObject.GetComponent<TBS_Turn>().magic / One_Stats.instance.maxMagic;
        healthBar2 = (float)Two_Stats.instance.gameObject.GetComponent<TBS_Turn>().health / Two_Stats.instance.maxHealth;
        magicBar2 = (float)Two_Stats.instance.gameObject.GetComponent<TBS_Turn>().magic / Two_Stats.instance.maxMagic;
        healthBar3 = (float)Three_Stats.instance.gameObject.GetComponent<TBS_Turn>().health / Three_Stats.instance.maxHealth;
        magicBar3 = (float)Three_Stats.instance.gameObject.GetComponent<TBS_Turn>().magic / Three_Stats.instance.maxMagic;
        healthBar4 = (float)Four_Stats.instance.gameObject.GetComponent<TBS_Turn>().health / Four_Stats.instance.maxHealth;
        magicBar4 = (float)Four_Stats.instance.gameObject.GetComponent<TBS_Turn>().magic / Four_Stats.instance.maxMagic;
    }
    void AddToLists() {
        boxSize = ((Screen.height - Screen.height / 6) / 8);
        screenBoxPos = (Screen.height / 30) + (boxSize / 3);
        offset = (Screen.height / 30 / 8);
        if (TBS_TurnManager.instance.enemyList.Count > 4) {
            enemyGUIList.Add(new Rect(Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30, Screen.width / 8 - Screen.width / 70, Screen.height / 30));
            enemyGUIList.Add(new Rect(Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30 * 2, Screen.width / 8 - Screen.width / 35, Screen.height / 30));
            enemyGUIList.Add(new Rect(Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30 * 3, Screen.width / 8 - Screen.width / 35, Screen.height / 30));
            enemyGUIList.Add(new Rect(Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30 * 4, Screen.width / 8 - Screen.width / 35, Screen.height / 30));
            enemyGUIList.Add(new Rect(Screen.width / 8, Screen.height - Screen.height / 6 + Screen.height / 30, Screen.width / 8, Screen.height / 30));
            enemyGUIList.Add(new Rect(Screen.width / 8, Screen.height - Screen.height / 6 + Screen.height / 30 * 2, Screen.width / 8, Screen.height / 30));
            enemyGUIList.Add(new Rect(Screen.width / 8, Screen.height - Screen.height / 6 + Screen.height / 30 * 3, Screen.width / 8, Screen.height / 30));
            enemyGUIList.Add(new Rect(Screen.width / 8, Screen.height - Screen.height / 6 + Screen.height / 30 * 4, Screen.width / 8, Screen.height / 30));
        } else {
            enemyGUIList.Add(new Rect(Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30, Screen.width / 4 - Screen.width / 35, Screen.height / 30));
            enemyGUIList.Add(new Rect(Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30 * 2, Screen.width / 4 - Screen.width / 35, Screen.height / 30));
            enemyGUIList.Add(new Rect(Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30 * 3, Screen.width / 4 - Screen.width / 35, Screen.height / 30));
            enemyGUIList.Add(new Rect(Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30 * 4, Screen.width / 4 - Screen.width / 35, Screen.height / 30));
        }
        playerGUIName.Add(new Rect(Screen.width / 2 + Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30, Screen.width / 5 - Screen.width / 70, Screen.height / 30));
        playerGUIName.Add(new Rect(Screen.width / 2 + Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30 * 2, Screen.width / 5 - Screen.width / 70, Screen.height / 30));
        playerGUIName.Add(new Rect(Screen.width / 2 + Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30 * 3, Screen.width / 5 - Screen.width / 70, Screen.height / 30));
        playerGUIName.Add(new Rect(Screen.width / 2 + Screen.width / 70, Screen.height - Screen.height / 6 + Screen.height / 30 * 4, Screen.width / 5 - Screen.width / 70, Screen.height / 30));
        playerGUIHealth.Add(new Rect((Screen.width / 2) + (Screen.width / 5), Screen.height - Screen.height / 6 + Screen.height / 30, (Screen.width / 13), Screen.height / 30));
        playerGUIHealth.Add(new Rect((Screen.width / 2) + (Screen.width / 5), Screen.height - Screen.height / 6 + Screen.height / 30 * 2, (Screen.width / 13), Screen.height / 30));
        playerGUIHealth.Add(new Rect((Screen.width / 2) + (Screen.width / 5), Screen.height - Screen.height / 6 + Screen.height / 30 * 3, (Screen.width / 13), Screen.height / 30));
        playerGUIHealth.Add(new Rect((Screen.width / 2) + (Screen.width / 5), Screen.height - Screen.height / 6 + Screen.height / 30 * 4, (Screen.width / 13), Screen.height / 30));
        playerGUIMagic.Add(new Rect((Screen.width / 2) + (Screen.width / 5) * 1.4f, Screen.height - Screen.height / 6 + Screen.height / 30, (Screen.width / 13), Screen.height / 30));
        playerGUIMagic.Add(new Rect((Screen.width / 2) + (Screen.width / 5) * 1.4f, Screen.height - Screen.height / 6 + Screen.height / 30 * 2, (Screen.width / 13), Screen.height / 30));
        playerGUIMagic.Add(new Rect((Screen.width / 2) + (Screen.width / 5) * 1.4f, Screen.height - Screen.height / 6 + Screen.height / 30 * 3, (Screen.width / 13), Screen.height / 30));
        playerGUIMagic.Add(new Rect((Screen.width / 2) + (Screen.width / 5) * 1.4f, Screen.height - Screen.height / 6 + Screen.height / 30 * 4, (Screen.width / 13), Screen.height / 30));
        playerGUIHealthBar.Add(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 + Screen.height / 240, Screen.width / 7 - Screen.width / 140, Screen.height / 60 - Screen.height / 240));
        playerGUIHealthBar.Add(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 2 + Screen.height / 240, Screen.width / 7 - Screen.width / 140, Screen.height / 60 - Screen.height / 240));
        playerGUIHealthBar.Add(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 3 + Screen.height / 240, Screen.width / 7 - Screen.width / 140, Screen.height / 60 - Screen.height / 240));
        playerGUIHealthBar.Add(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 4 + Screen.height / 240, Screen.width / 7 - Screen.width / 140, Screen.height / 60 - Screen.height / 240));
        playerGUIMagicBar.Add(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 + Screen.height / 60, Screen.width / 7 - Screen.width / 140, Screen.height / 60 - Screen.height / 240));
        playerGUIMagicBar.Add(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 2 + Screen.height / 60, Screen.width / 7 - Screen.width / 140, Screen.height / 60 - Screen.height / 240));
        playerGUIMagicBar.Add(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 3 + Screen.height / 60, Screen.width / 7 - Screen.width / 140, Screen.height / 60 - Screen.height / 240));
        playerGUIMagicBar.Add(new Rect(Screen.width - Screen.width / 7, Screen.height - Screen.height / 6 + Screen.height / 30 * 4 + Screen.height / 60, Screen.width / 7 - Screen.width / 140, Screen.height / 60 - Screen.height / 240));
        turn = new Rect(Screen.width - Screen.width / 7, 0, Screen.width / 7, Screen.height - Screen.height / 6);
        turnOrderGUI.Add(new Rect(Screen.width - Screen.width / 7, Screen.height / 30, Screen.width / 7, (Screen.height - Screen.height / 6) / 8 - offset));
        turnOrderGUI.Add(new Rect(Screen.width - Screen.width / 7, (Screen.height / 30) + boxSize - offset * 2, Screen.width / 7, (Screen.height - Screen.height / 6) / 8));
        turnOrderGUI.Add(new Rect(Screen.width - Screen.width / 7, (Screen.height / 30) + boxSize * 2 - offset * 3, Screen.width / 7, (Screen.height - Screen.height / 6) / 8));
        turnOrderGUI.Add(new Rect(Screen.width - Screen.width / 7, (Screen.height / 30) + boxSize * 3 - offset * 4, Screen.width / 7, (Screen.height - Screen.height / 6) / 8));
        turnOrderGUI.Add(new Rect(Screen.width - Screen.width / 7, (Screen.height / 30) + boxSize * 4 - offset * 5, Screen.width / 7, (Screen.height - Screen.height / 6) / 8));
        turnOrderGUI.Add(new Rect(Screen.width - Screen.width / 7, (Screen.height / 30) + boxSize * 5 - offset * 6, Screen.width / 7, (Screen.height - Screen.height / 6) / 8));
        turnOrderGUI.Add(new Rect(Screen.width - Screen.width / 7, (Screen.height / 30) + boxSize * 6 - offset * 7, Screen.width / 7, (Screen.height - Screen.height / 6) / 8));
        turnOrderGUI.Add(new Rect(Screen.width - Screen.width / 7, (Screen.height / 30) + boxSize * 7 - offset * 8, Screen.width / 7, (Screen.height - Screen.height / 6) / 8));
        turnArrowGUI.Add(new Rect(Screen.width - Screen.width / 6, screenBoxPos, boxSize / 3, boxSize / 3));
        turnArrowGUI.Add(new Rect(Screen.width - Screen.width / 6, screenBoxPos + boxSize - offset, boxSize / 3, boxSize / 3 - offset));
        turnArrowGUI.Add(new Rect(Screen.width - Screen.width / 6, screenBoxPos + boxSize * 2 - offset * 2, boxSize / 3, boxSize / 3 - offset));
        turnArrowGUI.Add(new Rect(Screen.width - Screen.width / 6, screenBoxPos + boxSize * 3 - offset * 3, boxSize / 3, boxSize / 3 - offset));
        turnArrowGUI.Add(new Rect(Screen.width - Screen.width / 6, screenBoxPos + boxSize * 4 - offset * 4, boxSize / 3, boxSize / 3 - offset));
        turnArrowGUI.Add(new Rect(Screen.width - Screen.width / 6, screenBoxPos + boxSize * 5 - offset * 5, boxSize / 3, boxSize / 3 - offset));
        turnArrowGUI.Add(new Rect(Screen.width - Screen.width / 6, screenBoxPos + boxSize * 6 - offset * 6, boxSize / 3, boxSize / 3 - offset));
        turnArrowGUI.Add(new Rect(Screen.width - Screen.width / 6, screenBoxPos + boxSize * 7 - offset * 7, boxSize / 3, boxSize / 3 - offset));
    } 
    IEnumerator StartBattleGUI() {
        yield return new WaitForSeconds(1);
        start = true;
        yield return new WaitForSeconds(4);
        start = false;
        if (GUIStartBattle != null) {
            GUIStartBattle();
        }
    }
}
