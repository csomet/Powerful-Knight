using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequence : MonoBehaviour {


    public GameObject camera1;
    public GameObject camera2;

	void Start () {

        camera1.SetActive(false);
        camera2.SetActive(true);
        StartCoroutine(StartCamSeq());
	}
	


    IEnumerator StartCamSeq(){

        yield return new WaitForSeconds(10);
        camera1.SetActive(true);
        camera2.SetActive(false);

    }
	
}
