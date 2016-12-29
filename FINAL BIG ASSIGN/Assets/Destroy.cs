using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(ObjectwithTagsGround))]
public class Destroy : MonoBehaviour {
    private CircleCollider2D Destroycollider;
    //自己的碰撞器
    public EdgeCollider2D player;
    //player的碰撞器
    private ObjectwithTagsGround GR;
    //"其他東西的"碰撞器子物件
    public bool DestroyGround;
    //是否要撞到地板也消除
    private bool colliderGround;
    //撞到地板與否
	// Use this for initialization
	void Start () 
    {
        Destroycollider = GetComponent<CircleCollider2D>();
        Destroycollider.radius = 0.3f;
        Destroycollider.isTrigger = true;
        //設置自己的碰撞器初始值
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EdgeCollider2D>();
        //取得玩家之碰撞器
        GR = GetComponent<ObjectwithTagsGround>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Destroycollider.IsTouching(player))
        {
            EXDestroy();
        }
        //碰撞到玩家會消滅
        foreach (PolygonCollider2D i in GR.ground)
        {
            if (Destroycollider.IsTouching(i) == true)
            {
                colliderGround = true;
            }
        }
        //是否碰撞到地板
        if(DestroyGround == true && colliderGround == true)
        {
            EXDestroy();
        }
        //碰撞地板且許可時消除
	}
    public void EXDestroy()
    {
        transform.parent = null;
        Destroy(gameObject);
    }
    //取消父物件，並且刪除自己
}
