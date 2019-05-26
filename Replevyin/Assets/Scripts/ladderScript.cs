using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderScript : MonoBehaviour {

    public GameObject player;
    movementScript playerMove;
    public float climbSpeed;
    public Transform TTL, TBR, BTL, BBR;
    public LayerMask mask, mask2;
	// Use this for initialization
	void Start () {
        playerMove = player.GetComponent<movementScript>();
	}
	
	// Update is called once per frame
	void Update () {
        bool lol = Physics2D.OverlapArea(BTL.position, BBR.position, mask);
        if (playerMove.isOnLadder && lol)
        {
            Debug.Log("sdfsdfdsf");
            playerMove.isOnLadder = false;
            player.layer = 11;
            player.GetComponent<Rigidbody2D>().gravityScale = 0.25f;
        }

        if (Input.GetKeyDown(KeyCode.S) && playerMove.isOnLadder == false && Physics2D.OverlapArea(TTL.position, TBR.position, mask2))
        {
            playerMove.isOnLadder = true;
            player.layer = 18;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.transform.position = new Vector2(this.transform.position.x, player.transform.position.y-0.3f);
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rig = player.GetComponent<Rigidbody2D>();
        if (collision.gameObject.tag == "Player")
        {
            movementScript.isGrounded = true;
            if (Input.GetKeyDown(KeyCode.W) && playerMove.isOnLadder == false)
            {
                playerMove.isOnLadder = true;
                player.layer = 18;
                rig.gravityScale = 0;
                player.transform.position = new Vector2(this.transform.position.x, player.transform.position.y + 0.1f);
            }

            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && playerMove.isOnLadder)
            {
                rig.velocity = new Vector2(0, climbSpeed);
            }

            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && playerMove.isOnLadder)
            {
                rig.velocity = new Vector2(0, -climbSpeed);
            }


                if (playerMove.isOnLadder && Input.GetKeyDown(KeyCode.Space))
            {
                playerMove.isOnLadder = false;
                player.layer = 11;
                player.GetComponent<Rigidbody2D>().gravityScale = 0.25f;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("why");
            playerMove.isOnLadder = false;
            player.GetComponent<Rigidbody2D>().gravityScale = 0.25f;
            player.layer = 11;
        }
    }
}
