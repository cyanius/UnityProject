using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Destroy))]
[RequireComponent(typeof(Rigidbody2D))]
public class bulletshot : MonoBehaviour {

    public ForceMode2D FM;
    private GameObject player;
    //物件:玩家
    public float bulletspeed;
    //子彈速度
    private Rigidbody2D RD;
    //發射物件的剛體
    private Vector3 Targetdirection;
    //子彈的目標方向
    private Destroy DS;
    //子彈的銷毀類別
    void Start()
    {
        DS = GetComponent<Destroy>();
        DS.DestroyGround = true;
        DS.DestroyPlayer = true;
        DS.DestroyExplosion = false;
        RD = GetComponent<Rigidbody2D>();
        RD.gravityScale = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
        Targetdirection = (player.transform.position - gameObject.transform.position).normalized;
        //放在這裡的話 子彈不會追蹤玩家
        //放在update裡子彈會追蹤玩家
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        RD.AddForce(Targetdirection * bulletspeed, FM);

        if (gameObject.transform.position.y > 2.5 ||
            gameObject.transform.position.y < -2.5)
        {
            DS.EXDestroy();
        }
        //子彈跑出邊界時刪除子彈
    }
}
