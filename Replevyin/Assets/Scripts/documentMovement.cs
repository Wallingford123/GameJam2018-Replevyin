using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class documentMovement : MonoBehaviour {

    public float speed;
    Rigidbody2D myRB;
    public float rotation;
    GameObject player;
    public float lifetime;

    public GameObject graphic;
    Vector3 dir;

	void Start () {
        myRB = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, lifetime);
        player = GameObject.Find("Player");
        dir = player.transform.position - this.transform.position;
        dir = dir.normalized;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (this.tag == "RockProjectile")
        {
            myRB.velocity = new Vector2(dir.x * speed, dir.y * speed);
        }
        else myRB.velocity = new Vector2(transform.forward.z * speed, myRB.velocity.y);
      
    }
    private void Update()
    {
        graphic.transform.RotateAround(transform.position, transform.forward, rotation);
    }


}
