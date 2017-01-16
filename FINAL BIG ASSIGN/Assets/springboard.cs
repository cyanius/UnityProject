using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (PolygonCollider2D))]
public class springboard : MonoBehaviour {

    private PolygonCollider2D PC2D;
    //自己的碰撞器
    private Rigidbody2D RD;
    //自己的剛體
    private PolygonCollider2D player;
    //玩家的碰撞器
    private Transform  initial;
    //初始位置
    public ForceMode2D FM;
    public Vector3 V;
    public int a,b,c,d,e,f;

	// Use this for initialization
	void Start () 
    {
        PC2D = GetComponent<PolygonCollider2D>();
        RD = GetComponent<Rigidbody2D>();
        PC2D.isTrigger = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PolygonCollider2D>();
        initial = gameObject.GetComponent<Transform>();
        V = new Vector3(0,1,0);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (PC2D.IsTouching(player) == true)
        {
            downward();
            ++a;
        }
        else
        {
            if(gameObject.transform.position.y > initial.position.y)
            {
                downward();
                ++b;
            }
            else if(gameObject.transform.position.y < initial.position.y)
            {
                upward();
                ++c;
            }
            else
            {
                ++d;
                return;
            }

        }
	}
    private void downward()
    {
        RD.AddForce(-V, FM);
        ++e;
    }
    private void upward()
    {
        RD.AddForce(V, FM);
        ++f;
    }
}
