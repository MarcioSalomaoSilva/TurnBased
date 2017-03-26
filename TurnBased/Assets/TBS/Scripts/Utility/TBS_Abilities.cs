using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TBS_Abilities : ScriptableObject {
    public string thisName;
    public AudioClip sound;
    public Texture texture;
    public abstract void Initialize();
}
[CreateAssetMenu (menuName = "Abilities/TBS/Attack")]
public class Attack : TBS_Abilities {
    // base power of the attack tat needs to be calculated
    //in this case the attack can be calculated using the handler info and tat calculation can be in the attack method in TBS_turn
    public override void Initialize() {
        //scriptable object needs to have a reference to tbs_turn and any functions that it needs
    }
}
[CreateAssetMenu(menuName = "Abilities/TBS/Defend")]
public class Defend : TBS_Abilities {
    //defend can be calculated in te tbs_turn method (maybe double the defense for that turn using a bool or enum)
    public override void Initialize() {
        //scriptable object needs to have a reference to tbs_turn and any functions that it needs
    }
}
[CreateAssetMenu(menuName = "Abilities/TBS/ChangeRow")]
public class ChangeRow : TBS_Abilities {
    //defend can be calculated in te tbs_turn method (maybe double the defense for that turn using a bool or enum)
    public override void Initialize() {
        //scriptable object needs to have a reference to tbs_turn and any functions that it needs
    }
}
