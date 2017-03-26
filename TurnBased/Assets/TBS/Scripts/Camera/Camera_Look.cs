using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Look : MonoBehaviour {
    //
    public static Camera_Look instance;
	// 
	void Awake () {
        instance = this;
        this.transform.LookAt(GameObject.Find("Camera Target").transform);
	}
    //
	void Update () {
		
	}
}
