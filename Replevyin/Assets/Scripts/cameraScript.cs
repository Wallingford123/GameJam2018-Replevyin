using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    public Rect mapBounds;
    public float verticalBuffer;
    public float horizontalBuffer;
    public GameObject player;
    public float speed;
    Vector2 movePos;

	// Use this for initialization
	void Start () {
        movePos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!shootScript.throwing)
        {
            if (player.transform.position.y > mapBounds.y - mapBounds.height + verticalBuffer && player.transform.position.y < mapBounds.y - verticalBuffer)
            {
                movePos = new Vector3(movePos.x, player.transform.position.y);
            }
            if (player.transform.position.x > mapBounds.x + horizontalBuffer && player.transform.position.x < mapBounds.x + mapBounds.width - horizontalBuffer)
            {
                movePos = new Vector3(player.transform.position.x, movePos.y);
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                new Vector3(movePos.x, this.transform.position.y, this.transform.position.z),
                speed * Time.deltaTime * Vector2.Distance(new Vector2(this.transform.position.x, 0), new Vector2(movePos.x, 0)));

            this.transform.position = Vector3.MoveTowards(this.transform.position,
                new Vector3(this.transform.position.x, movePos.y, this.transform.position.z),
                speed * Time.deltaTime * (Vector2.Distance(new Vector2(0, this.transform.position.y), new Vector2(0, movePos.y)) * 8));

        }
    }
}
