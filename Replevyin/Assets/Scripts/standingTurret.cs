using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class standingTurret : MonoBehaviour {

    public float range, attackDelay;
    public LayerMask mask;
    public LayerMask playerMask;
    public GameObject player;
    public GameObject rock;
    public Transform shootOrigin;
    float health = 2;
    float timer;

    Animator myAnim;
	// Use this for initialization
	void Start () {
        myAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Physics2D.OverlapCircle(this.transform.position, range, playerMask)){
            Vector2 dir =  player.transform.position - this.transform.position;
            if(dir.x < 0) transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            if (dir.x > 0) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            RaycastHit2D hit = Physics2D.Raycast(shootOrigin.transform.position, dir, range, ~mask);
            if (hit)
            {
                if (hit.transform.tag == "Player")
                {
                    if (timer < Time.time)
                    {
                        timer = Time.time + attackDelay;
                        myAnim.SetTrigger("shoot");
                    }
                }
            }
        }
	}

    public void shoot()
    {
        GameObject lol = Instantiate(rock, shootOrigin.position, shootOrigin.rotation);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "BookProjectile")
        {
            collision.gameObject.GetComponent<ParticleSystem>().Emit(5);
            Destroy(collision.gameObject.GetComponentInChildren<SpriteRenderer>());
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
            Destroy(collision.gameObject, 1f);
            health--;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
