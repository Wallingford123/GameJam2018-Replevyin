using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    public static int health = 3;
    public float invTime;
    Rigidbody2D myRB;
    public float jumpForce;
    public float speed;
    bool isDead = false;
    public GameObject failText;
    public GameObject back;

    public bool isOnLadder;

    public Transform topLeft;
    public Transform bottomRight;
    public LayerMask groundLayer;

    Animator myAnim;
    public static bool isGrounded;
    float timer;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody2D>();
        timer = Time.time;
        isGrounded = true;
    }

    void Ups()
    {

        if (GetComponent<SpriteRenderer>().color != Color.white)
        {
            Color col = GetComponent<SpriteRenderer>().color;
            Color col2 = new Vector4(col.r, col.g + Time.deltaTime, col.b + Time.deltaTime, col.a);
            GetComponent<SpriteRenderer>().color = col2;
        }
        if(health <= 0 && !isDead)
        {
            isDead = true;
            Rigidbody2D rig = GetComponent<Rigidbody2D>();
            rig.gravityScale = 0.25f;
            rig.freezeRotation = false;
            rig.angularVelocity = -420;
            this.gameObject.layer = 20;
            rig.velocity = new Vector2(2f, 5);
            failText.SetActive(true);
            back.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            int lol = pickupChild.children.Count;
            for (int i = 0; i < lol; i++)
            {
                pickupChild.children.Remove(pickupChild.children[0]);
            }
            health = 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ups();
        myAnim.SetBool("Grounded", isGrounded);
        myAnim.SetBool("ladder", isOnLadder);
        isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded && timer < Time.time && !shootScript.throwing)
            {
                myRB.AddForce(new Vector2(0, jumpForce));
                timer = Time.time + 0.2f;

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.D) && !shootScript.throwing && !isOnLadder && !isDead)
        {
            myRB.velocity = new Vector2(speed, myRB.velocity.y);

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (Input.GetKey(KeyCode.A) && !shootScript.throwing && !isOnLadder && !isDead)
        {
            myRB.velocity = new Vector2(-speed, myRB.velocity.y);

            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            
        }
        myAnim.SetFloat("Blend", myRB.velocity.x);
        myAnim.SetFloat("Xvelocity", myRB.velocity.y);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "RockProjectile")
        {
            Destroy(collision.gameObject);
            health--;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }


}
