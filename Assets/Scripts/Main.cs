using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Main : MonoBehaviour {

    public float speed = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, speed * Time.deltaTime));
	}
}
