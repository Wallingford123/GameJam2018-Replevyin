using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childWander : MonoBehaviour {
    public Transform left, right;
    Vector3 currentPlace;
    Vector3 localLeft, localRight;
    public float maxDelay, minDelay;
    float delay;
    int speed = 2;
    Animator myAnim;

	// Use this for initialization
	void Start () {
        myAnim = GetComponent<Animator>();
        localLeft = left.transform.position;
        localRight = right.transform.position;
        currentPlace = localRight;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x < localLeft.x + 0.1f && currentPlace == localLeft)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            currentPlace = localRight;
            delay = Time.time + Random.Range(minDelay, maxDelay);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (this.transform.position.x > localRight.x - 0.1f && currentPlace == localRight)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            currentPlace = localLeft;
            delay = Time.time + Random.Range(minDelay, maxDelay);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (delay < Time.time && currentPlace == localRight)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (delay < Time.time && currentPlace == localLeft)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        myAnim.SetFloat("movement", this.GetComponent<Rigidbody2D>().velocity.x);
    }
}
