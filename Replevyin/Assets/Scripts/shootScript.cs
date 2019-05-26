using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootScript : MonoBehaviour {

    [HideInInspector]
    public GameObject child;
    public GameObject document;
    public GameObject player;
    public Transform shootOrigin;
    public Transform throwOrigin;
    public float forceMultiplier;
    public float maxForce;
    public float minForce;
    public float throwDelay;
    public static bool throwing;
    LineRenderer forceLine;
    Vector2 lineClick;
    Ray ray, ray2;
    public float distanceCheck;
    float timer;

    Vector2 direction;
    float force;


    void Start () {
        forceLine = player.GetComponent<LineRenderer>();
        child = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (child != null)
        {
            lineClick = child.transform.position;
            forceLine.SetPosition(0, lineClick);
            ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            forceLine.SetPosition(1, new Vector2(lineClick.x + (ray.origin.x - ray2.origin.x) * 10, lineClick.y + (ray.origin.y - ray2.origin.y) * 10));
        }
        if ((ray.origin - ray2.origin).normalized.x < 0 && throwing)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            child.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            child.transform.position = throwOrigin.position;
        }
        else if (throwing)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            child.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            child.transform.position = throwOrigin.position;
        }

        if (Input.GetMouseButtonDown(0) && this.gameObject.layer == 11)
        {
            Instantiate(document, shootOrigin.position, shootOrigin.rotation);
        }
        if (Input.GetMouseButtonDown(1) && timer < Time.time && movementScript.isGrounded && this.gameObject.layer == 11)
        {
            if (pickupChild.children.Count > 0 && Vector2.Distance(player.transform.position, pickupChild.children[0].transform.position) < distanceCheck)
            {
                foreach (GameObject go in pickupChild.children)
                {
                    if ((int)go.GetComponent<childScript>().ID - 1 >= 1)
                    {
                        go.GetComponent<childScript>().follow = pickupChild.children[(int)go.GetComponent<childScript>().ID - 1];
                        go.GetComponent<childScript>().ID -= 1;
                    }
                    else if ((int)go.GetComponent<childScript>().ID == 1)
                    {
                        go.GetComponent<childScript>().follow = player;
                        go.GetComponent<childScript>().ID -= 1;
                    }
                }
                if (pickupChild.children.Count >= 1)
                {
                    throwing = true;
                    player.GetComponent<Animator>().SetBool("isThrowing", true);
                    child = pickupChild.children[0];
                    pickupChild.children.Remove(pickupChild.children[0]);
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    lineClick = child.transform.position;
                    forceLine.SetPosition(0, lineClick);
                    ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    forceLine.SetPosition(1, new Vector2(lineClick.x - (ray.origin.x - ray2.origin.x) * 10, lineClick.y - (ray.origin.y - ray2.origin.y) * 10));
                    forceLine.enabled = true;
                    child.GetComponent<childScript>().enabled = false;
                    child.transform.position = throwOrigin.position;
                    child.GetComponent<Animator>().SetBool("isThrown", true);
                    child.GetComponent<Rigidbody2D>().gravityScale = 0;
                    child.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    child.GetComponent<Animator>().SetFloat("movement", child.GetComponent<Rigidbody2D>().velocity.x);

                }
                else child = null;
            }
        }
        if (Input.GetMouseButtonUp(1) && child != null && timer < Time.time && movementScript.isGrounded)
        {
            direction = (ray.origin - ray2.origin).normalized;
            force = Vector2.Distance(ray.origin, ray2.origin);
            player.GetComponent<Animator>().SetBool("isThrowing", false);
        
        }
    }

    public void ThrowChild()
    {
        
        if (force > maxForce) force = maxForce;
        if (force < minForce) force = minForce;
        if (direction == new Vector2(0, 0)) direction = new Vector2(0, 1);
        child.GetComponent<Rigidbody2D>().velocity = new Vector2((force * forceMultiplier) * direction.x, (force * forceMultiplier) * direction.y);
        child.tag = "Child";
        child.GetComponent<childScript>().timer = Time.time + 0.1f;
        child.layer = 13;
        child.GetComponent<Rigidbody2D>().gravityScale = 0.25f;
        forceLine.enabled = false;
        timer = Time.time + 1;
    }

    public void unlockMovement()
    {
        throwing = false;
        child = null;
    }
}
