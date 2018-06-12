using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour {



    private PlayerHealth playerHealth;


	
	void Awake () {

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}




    private void OnEnable()
    {
        playerHealth.activeShield = true;
    }


    private void OnDisable()
    {
        playerHealth.activeShield = false;
    }
}
