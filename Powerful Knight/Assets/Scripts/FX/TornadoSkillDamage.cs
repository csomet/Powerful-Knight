using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoSkillDamage : MonoBehaviour {

    public LayerMask layer;
    public float radius = 0.85f;
    public float damageCount = 50f;
    public GameObject fireExplosion;
    public float speed = 3.3f;


    private bool collided;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);

    }


    void Update () {
        Move();
        CheckForDamage();
	}



    void Move(){
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));

    }

    void CheckForDamage(){

        Collider[] hit = Physics.OverlapSphere(transform.position, radius, layer);

        foreach (Collider col in hit)
        {

            if (col.gameObject.tag != "Boss")
            {
                col.gameObject.GetComponent<EnemyControlSimplified>().ReceiveDamage(damageCount);
                collided = true;

            }
            else if (col.gameObject.tag == "Boss")
            {

                col.gameObject.GetComponent<BossHealth>().ReceiveDamage(damageCount);
                collided = true;
            }

        }


        if (collided)
        {

            Vector3 temp = transform.position;
            temp.y += +2f;
            Instantiate(fireExplosion, temp, Quaternion.identity);
            Destroy(gameObject);
            enabled = false;
        }
    }
}
