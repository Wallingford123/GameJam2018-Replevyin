using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class levelManager : MonoBehaviour {

    public float childNum;
    float collectedChildren = 0;
    public GameObject winText;
    public GameObject back;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(childNum <= collectedChildren)
        {
            winText.SetActive(true);
            back.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Return) && collectedChildren == childNum){
            int lol = pickupChild.children.Count;
            for (int i = 0; i < lol; i++)
            {
                pickupChild.children.Remove(pickupChild.children[0]);
            }
            movementScript.health = 3;
            if (SceneManager.GetActiveScene().buildIndex == 1) SceneManager.LoadScene(2);
            else SceneManager.LoadScene(0);
        }
	}
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Child")
        {
            collectedChildren++;
            Debug.Log(collectedChildren);
            col.gameObject.GetComponent<childScript>().enabled = false;
            col.gameObject.tag = "Saved";
        }
    }
}
