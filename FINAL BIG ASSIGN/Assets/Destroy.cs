using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(ObjectwithTagsGround))]
public class Destroy : MonoBehaviour {
    private CircleCollider2D Destroycollider;
    //自己的碰撞器
    public EdgeCollider2D player;
    //player的碰撞器
    public CircleCollider2D [] Explosion ;
    //爆炸物的碰撞器
    private ObjectwithTagsGround GR;
    //"其他東西的"碰撞器子物件
    public bool DestroyPlayer;
    //碰到玩家是否要消除
    public bool DestroyGround;
    //是否要撞到地板也消除
    private bool colliderGround;
    //撞到地板與否
    public bool DestroyExplosion;
    //是否在爆炸時消除
    private bool colliderExplosion;
    //碰到爆炸與否
    private int count;
    //數量
    private move mov;
    //玩家身上的move
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

        setarray(ref Explosion);
        //設置陣列
        mov = player.gameObject.GetComponent<move>();
	}
	private void setarray(ref CircleCollider2D [] Explosion)
    {
        count = GameObject.FindGameObjectsWithTag("Explosion").Length;
        Explosion = new CircleCollider2D[count];
        for (int i = 0; i < Explosion.Length; ++i)
        {
            Explosion[i] = GameObject.FindGameObjectsWithTag("Explosion")[i].GetComponent<CircleCollider2D>();
            //設定具有標籤"explosion"的物件其子物件collider2D為explosion
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if(Destroycollider.IsTouching(player) && DestroyPlayer == true)
        {
            EXDestroy();
        }
        //碰撞到玩家且許可時會消滅
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
        foreach (CircleCollider2D i in Explosion)
        {
            if(Destroycollider.IsTouching(i) == true)
            {
                colliderExplosion = true;
            }
        }
        //是否遇到爆炸
        if(DestroyExplosion == true && colliderExplosion == true)
        {
            EXDestroy();
            --mov.x;
        }
        //遇到爆炸且許可時消除
	}
    public void EXDestroy()
    {
        transform.parent = null;
        Destroy(gameObject);
    }
    //取消父物件，並且刪除自己
}
