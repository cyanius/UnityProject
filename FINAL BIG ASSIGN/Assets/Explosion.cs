using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CircleCollider2D))]
public class Explosion : MonoBehaviour {

    private Animator anim;
    private CircleCollider2D parameter;
    //自己的碰撞器
    private EdgeCollider2D player;
    //玩家的碰撞器
    private Rigidbody2D RD;
    public float Destroytime = 0.45f;
    //約為0.3333/動畫播放速度
    private SpriteRenderer texture;
	// Use this for initialization
	void Start () 
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        //一開始不讓動畫執行
        texture = GetComponent<SpriteRenderer>();
        texture.enabled = false;
        parameter = GetComponent<CircleCollider2D>();
        parameter.radius = 1.2f;
        parameter.offset = new Vector2(0.1f , 0.3f) ; //要設新值 必須用new
        parameter.isTrigger = true;
        //設置初始值(此處為已測試過最佳值)
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EdgeCollider2D>();
        //獲得玩家碰撞器資訊
        RD = GetComponent<Rigidbody2D>();
	}
	IEnumerator End ()
        {
            yield return new WaitForSeconds(Destroytime);
            Destroy(gameObject);
        }
    //等待x秒後刪除物件
	// Update is called once per frame
	void Update () 
    {
        if(parameter.IsTouching(player))
        {
            texture.enabled = true;
            anim.enabled = true;
            RD.velocity = new Vector2(0, 0);
            StartCoroutine("End");
        }
	}
}
