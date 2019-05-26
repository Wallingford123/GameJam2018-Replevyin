using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderingEnemyScript : MonoBehaviour {

    public GameObject player;
    bool hasCaptive = false;
    GameObject hostage;
    float health = 3;
    float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Follower" && !hasCaptive)
        {
            collision.gameObject.GetComponent<childScript>().follow = this.gameObject;
            collision.gameObject.layer = 15;
            pickupChild.children.Remove(collision.gameObject);
            hasCaptive = true;
            this.gameObject.layer = 16;
            int numb = 0;
            hostage = collision.gameObject;
            foreach (GameObject go in pickupChild.children)
            {
                go.GetComponent<childScript>().ID = numb;
                if (go.GetComponent<childScript>().ID == 0)
                {
                    go.GetComponent<childScript>().follow = player;
                }
                else
                {
                    go.GetComponent<childScript>().follow = pickupChild.children[numb - 1];
                }
                numb++;
            }
        }
        if(collision.transform.tag == "Player" && timer < Time.time)
        {
            movementScript.health--;
            timer = Time.time + collision.gameObject.GetComponent<movementScript>().invTime;
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (collision.transform.tag == "Child" && !hasCaptive)
        {
            hostage = collision.gameObject;
            hasCaptive = true;
            GameObject inst = collision.gameObject;
            inst.GetComponent<childScript>().enabled = true;
            inst.GetComponent<childWander>().enabled = false;
            inst.GetComponent<childScript>().follow = this.gameObject;

            inst.tag = "Captive";
            inst.layer = 15;
            inst.GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
        if(collision.transform.tag == "BookProjectile")
        {
            collision.gameObject.GetComponent<ParticleSystem>().Emit(5);
            Destroy(collision.gameObject.GetComponentInChildren<SpriteRenderer>());
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
            Destroy(collision.gameObject, 1f);
            health--;
            if (health <= 0 && hasCaptive)
            {
                GameObject inst = hostage;
                if (pickupChild.children.Count == 0)
                {
                    inst.GetComponent<childScript>().follow = player;
                }
                else inst.GetComponent<childScript>().follow = pickupChild.children[pickupChild.children.Count - 1];
                inst.GetComponent<childScript>().ID = pickupChild.children.Count;
                pickupChild.children.Add(inst);
                inst.tag = "Follower";
                inst.layer = 12;
                inst.GetComponent<SpriteRenderer>().sortingOrder = -pickupChild.children.Count;
                Destroy(this.gameObject, 0.1f);
            }
            else if (health <= 0) Destroy(this.gameObject, 0.1f);
        }
    }
}
