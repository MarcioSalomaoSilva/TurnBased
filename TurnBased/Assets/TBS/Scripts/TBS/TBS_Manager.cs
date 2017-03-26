using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TBS_TurnManager))]
[RequireComponent(typeof(TBS_GUI))]
public class TBS_Manager : MonoBehaviour {
    public static TBS_Manager instance;
    [Header("Variables")]
    int enemyCount;
    int levelCount;
    [Header ("Enemy Position Prefabs")]
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public GameObject enemyPrefab4;
    public GameObject enemyPrefab5;
    [Header("Level Prefabs")]
    public GameObject forest;
    public GameObject desert;
    public GameObject city;
    [Header("Player Prefabs")]
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public GameObject character4;
    [Header("Other Prefabs")]
    public GameObject characterPositions;
    public GameObject cameraRig;
    [HideInInspector]
    GameObject front;
    [HideInInspector]
    GameObject back;
    //
    void OnEnable () {
        //Start_Battle.OnPressed += CreateCharacters;
    }
    void Awake () {
		instance = this;
        GetOtherPrefabs();
        GetLevelPrefab();
        GetEnemyRandom();
        CreateCharacters();
    }
    //
    void GetEnemyRandom() {
        enemyCount = Random.Range(1, 5);
        switch (enemyCount) {
            case 1:
                enemyPrefab1 = Instantiate(enemyPrefab1, transform.position, this.transform.rotation, this.transform);
                break;
            case 2:
                enemyPrefab2 = Instantiate(enemyPrefab2, transform.position, this.transform.rotation, this.transform);
                break;
            case 3:
                enemyPrefab3 = Instantiate(enemyPrefab3, transform.position, this.transform.rotation, this.transform);
                break;
            case 4:
                enemyPrefab4 = Instantiate(enemyPrefab4, transform.position, this.transform.rotation, this.transform);
                break;
            case 5:
                enemyPrefab5 = Instantiate(enemyPrefab5, transform.position, this.transform.rotation, this.transform);
                break;
        }
    }
    //
    GameObject GetLevelPrefab() {
        GameObject curLevel;
        levelCount = Random.Range(1, 3);
        switch (levelCount) {
            case 1:
                curLevel = Instantiate(forest, Vector3.zero, this.transform.rotation, this.transform);
                break;
            case 2:
                curLevel = Instantiate(desert, Vector3.zero, this.transform.rotation, this.transform);
                break;
            case 3:
                curLevel = Instantiate(city, Vector3.zero, this.transform.rotation, this.transform);
                break;
            default:
                curLevel = Instantiate(forest, Vector3.zero, this.transform.rotation, this.transform);
                break;
        }
        return curLevel;
    }
    void GetOtherPrefabs() {
        characterPositions = Instantiate(characterPositions, Vector3.zero, this.transform.rotation, this.transform);
        cameraRig = Instantiate(cameraRig, Vector3.zero, this.transform.rotation, this.transform);
    }
    void CreateCharacters() {
        GameObject characters = new GameObject("Characters");
        characters.transform.position = Vector3.zero;
        characters.transform.parent = this.transform;
        if (Start_Battle.instance.one.id == 1) {
            character1 = Instantiate(GetCharacterPrefab(Start_Battle.instance.one), Vector3.zero, this.transform.rotation, characters.transform);
            character1.transform.position = GetRow(Start_Battle.instance.one.row, "Position (1)");
            character1.AddComponent<One_Stats>();
        }
        if (Start_Battle.instance.two.id == 2) {
            character2 = Instantiate(GetCharacterPrefab(Start_Battle.instance.two), Vector3.zero, this.transform.rotation, characters.transform);
            character2.transform.position = GetRow(Start_Battle.instance.two.row, "Position (2)");
            character2.AddComponent<Two_Stats>();
        }
        if (Start_Battle.instance.three.id == 3) {
            character3 = Instantiate(GetCharacterPrefab(Start_Battle.instance.three), Vector3.zero, this.transform.rotation, characters.transform);
            character3.transform.position = GetRow(Start_Battle.instance.three.row, "Position (3)");
            character3.AddComponent<Three_Stats>();
        }
        if (Start_Battle.instance.four.id == 4) {
            character4 = Instantiate(GetCharacterPrefab(Start_Battle.instance.four), Vector3.zero, this.transform.rotation, characters.transform);
            character4.transform.position = GetRow(Start_Battle.instance.four.row, "Position (4)");
            character4.AddComponent<Four_Stats>();
        }
    }
    GameObject GetCharacterPrefab(TBS_Classes.Character character) {
        GameObject prefab;
        switch (character.job) {
            case TBS_Classes.Character.Job.Warrior:
                prefab = Start_Battle.instance.warrior;
                break;
            case TBS_Classes.Character.Job.Fighter:
                prefab = Start_Battle.instance.fighter;
                break;
            case TBS_Classes.Character.Job.Knight:
                prefab = Start_Battle.instance.knight;
                break;
            case TBS_Classes.Character.Job.Monk:
                prefab = Start_Battle.instance.monk;
                break;
            case TBS_Classes.Character.Job.Master:
                prefab = Start_Battle.instance.master;
                break;
            case TBS_Classes.Character.Job.Thief:
                prefab = Start_Battle.instance.thief;
                break;
            case TBS_Classes.Character.Job.Ninja:
                prefab = Start_Battle.instance.ninja;
                break;
            case TBS_Classes.Character.Job.WhiteMage:
                prefab = Start_Battle.instance.whiteMage;
                break;
            case TBS_Classes.Character.Job.Summoner:
                prefab = Start_Battle.instance.summoner;
                break;
            case TBS_Classes.Character.Job.BlackMage:
                prefab = Start_Battle.instance.blackMage;
                break;
            case TBS_Classes.Character.Job.RedMage:
                prefab = Start_Battle.instance.redMage;
                break;
            default:
                prefab = Start_Battle.instance.warrior;
                break;
        }
        return prefab;
    }
    Vector3 GetRow(TBS_Classes.Character.Row _row, string posName) {
        Vector3 pos;
        front = GameObject.Find("Front");
        back = GameObject.Find("Back");
        switch (_row) {
            case TBS_Classes.Character.Row.Front:
                pos = AssignRow(posName, front);
                break;
            case TBS_Classes.Character.Row.Back:
                pos = AssignRow(posName, back);
                break;
            default:
                pos = AssignRow(posName, front);
                break;
        }
        return pos;
    }
    public Vector3 AssignRow(string pos, GameObject obj) {
        Vector3 row = obj.transform.FindChild(pos).position;
        return row;
    }
}
