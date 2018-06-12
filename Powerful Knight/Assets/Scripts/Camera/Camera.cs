using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {


    public float followHeight = 6f;
    public float followDistance = 6f;

    private Transform player;

    private float targetHeight;
    private float currentHeight;
    private float currentRotation;


	void Awake () {

        player = GameObject.FindWithTag("Player").transform;

	}

    void Update () {

      
            targetHeight = player.position.y + followHeight;
            currentRotation = transform.eulerAngles.y;
            currentHeight = Mathf.Lerp(transform.position.y, targetHeight, 0.9f * Time.deltaTime);
            Quaternion euler = Quaternion.Euler(0f, currentRotation, 0f);
            Vector3 targetPos = player.position - (euler * Vector3.forward) * followDistance;
            targetPos.y = currentHeight;

            //Set the camera position:
            transform.position = targetPos;
            transform.LookAt(player);

        
     
	}
}
