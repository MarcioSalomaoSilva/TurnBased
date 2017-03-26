using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//
public class Start_Battle : MonoBehaviour {
    public static Start_Battle instance;
    [Header("Character Positions")]
    public TBS_Classes.Character one;
    public TBS_Classes.Character two;
    public TBS_Classes.Character three;
    public TBS_Classes.Character four;
    [Header("Class Prefabs")]
    public GameObject warrior;
    public GameObject fighter;
    public GameObject knight;
    public GameObject monk;
    public GameObject master;
    public GameObject thief;
    public GameObject ninja;
    public GameObject whiteMage;
    public GameObject summoner;
    public GameObject blackMage;
    public GameObject redMage;
    //
    void Awake() {
        instance = this;
        one.id = 1;
        two.id = 2;
        three.id = 3;
        four.id = 4;
    }
    //
    void Update () {
		if (Input.GetKeyDown("k")) {
            LoadScene();
        }
    }
    void LoadScene() {
        SceneManager.LoadScene("ATB System", LoadSceneMode.Additive);
    }
    void OnValidate() {
        one.level = Mathf.Clamp(one.level, 1, 99);
        two.level = Mathf.Clamp(two.level, 1, 99);
        three.level = Mathf.Clamp(three.level, 1, 99);
        four.level = Mathf.Clamp(four.level, 1, 99);
    }
}
