using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public GameController gameController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "ball") {
            if(name == "Gate.A") {
                gameController.IncreaseScoreB();
            } else {
                gameController.IncreaseScoreA();
            }
            gameController.Initialize();
        }
    }
}
