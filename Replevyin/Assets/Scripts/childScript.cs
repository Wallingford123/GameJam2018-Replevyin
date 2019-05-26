using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childScript : MonoBehaviour {
    public int ID = 999;
    public GameObject follow;
    public float buffer;
    public float delay;
    int speed = 2;
    bool jumping = false;
    Animator myAnim;
    [HideInInspector]
    public float timer;

    public Transform topLeft;
    public Transform bottomRight;
    public LayerMask groundLayer;
    bool isGrounded;


    // Use this for initialization
    void Start () {
        timer = Time.time;
        isGrounded = true;
        myAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, groundLayer);
        if (isGrounded && !jumping && !shootScript.throwing && this.gameObject.layer != 15)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumping = true;
                timer = Time.time + ID * Time.deltaTime * delay;
            }
        }

        if (timer < Time.time && jumping)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
            jumping = false;
        }

        if (this.transform.position.x - follow.transform.position.x <= -buffer)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, this.GetComponent<Rigidbody2D>().velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (follow.transform.position.x - this.transform.position.x <= -buffer)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, this.GetComponent<Rigidbody2D>().velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        myAnim.SetFloat("movement", this.GetComponent<Rigidbody2D>().velocity.x);
        myAnim.SetFloat("xVelocity", this.GetComponent<Rigidbody2D>().velocity.y);
        myAnim.SetBool("Grounded", isGrounded);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(this.gameObject.layer == 13)
        {
            if (collision.gameObject.layer == 9 && timer < Time.time)
            {
                this.gameObject.layer = 10;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                myAnim.SetFloat("movement", this.GetComponent<Rigidbody2D>().velocity.x);
                myAnim.SetBool("isThrown", false);
            }
        }
    }
}
