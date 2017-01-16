using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof (Destroy))]
public class stick : MonoBehaviour {

    private CircleCollider2D CC2D;
    //自己的碰撞器
    private EdgeCollider2D player;
    //玩家的碰撞器
    private Rigidbody2D RD;
    //自己的剛體
    private AI A;
    //自己的AI
    private Vector3 DIS;
    //和玩家距離向量
    private GameObject obj;
    //物件:玩家
    private bool i = false;
    //判斷是否黏在玩家身上的變數
    private move mov;
    //玩家的move
    private Destroy destroy;
	// Use this for initialization
	void Start () 
    {
        destroy = GetComponent<Destroy>();
        destroy.enabled = false;
        //先關閉消滅機制
        destroy.DestroyExplosion = true;
        destroy.DestroyGround = false;
        destroy.DestroyPlayer = false;

        CC2D = GetComponent<CircleCollider2D>();
        CC2D.radius = 0.25f;
        CC2D.isTrigger = true;
        //碰撞器設初始值
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EdgeCollider2D>();
        RD = GetComponent<Rigidbody2D>();
        A = GetComponent<AI>();
        //取得個變數值
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if(i == true)
        {
            Position();
            destroy.enabled = true;
        }
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            obj = other.gameObject;
            mov = obj.GetComponent<move>();

            A.enabled = false;
            RD.gravityScale = 0f;
            //停用物件身上的類別，並去除重力影響
            gameObject.transform.parent = other.gameObject.transform;
            
            if(i == false)
            {
                ++mov.x;
                //x為黏在玩家身上東西數量，越多時在move腳本中
                //玩家移動及跳躍越難
                DIS = gameObject.transform.position - obj.gameObject.transform.position;
            }
            i = true;
        }
        //若撞擊到玩家會使得物體黏在玩家身上
    }
    private void Position()//物體隨玩家移動
    {
        gameObject.transform.position = (obj.gameObject.transform.position + DIS);
    }
}
