using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour {
    

    public LayerMask layer;
    public float radius = 0.5f;
    public float damageCount = 10f;

    private bool collided;



	void Update () {

        Collider[] hit = Physics.OverlapSphere(transform.position, radius, layer);
		
        foreach(Collider col in hit){

            if (col.gameObject.tag != "Boss")
            {
                col.gameObject.GetComponent<EnemyControlSimplified>().ReceiveDamage(damageCount);
                collided = true;

            } else if (col.gameObject.tag == "Boss"){

                col.gameObject.GetComponent<BossHealth>().ReceiveDamage(damageCount);
                collided = true;
            }

        }


        if (collided){
            enabled = false;
        }



	}
}
