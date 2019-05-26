using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupChild : MonoBehaviour {

    public static List<GameObject> children = new List<GameObject>();
    public GameObject player;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Child")
        {
            GameObject inst = col.gameObject;
            inst.GetComponent<childScript>().enabled = true;
            inst.GetComponent<childWander>().enabled = false;
            if (children.Count == 0)
            {
                inst.GetComponent<childScript>().follow = player;
            }
            else inst.GetComponent<childScript>().follow = children[children.Count - 1];
            inst.GetComponent<childScript>().ID = children.Count;
            children.Add(inst);
            inst.tag = "Follower";
            inst.layer = 12;
            inst.GetComponent<SpriteRenderer>().sortingOrder = -children.Count;
        }
    }
}
