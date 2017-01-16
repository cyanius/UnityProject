using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider2D))]
public class Final : MonoBehaviour {

    private EdgeCollider2D player;
    private PolygonCollider2D mine;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EdgeCollider2D>();
        mine = GetComponent<PolygonCollider2D>();
        mine.isTrigger = true;
	}
	
	// Update is called once per frame
	void FixedUpdate() 
    {
        if(mine.IsTouching(player))
        {
            Vector3 a = new Vector3(0, -1, 0);
            transform.Translate(a*Time.fixedDeltaTime);
        }
	
	}
}
