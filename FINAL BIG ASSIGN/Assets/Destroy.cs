using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CircleCollider2D))]
public class Destroy : MonoBehaviour {
    private CircleCollider2D Destroycollider;
    //自己的碰撞器
    public EdgeCollider2D player;
    //player的碰撞器
	// Use this for initialization
	void Start () 
    {
        Destroycollider = GetComponent<CircleCollider2D>();
        Destroycollider.radius = 0.3f;
        Destroycollider.isTrigger = true;
        //設置自己的碰撞器初始值
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EdgeCollider2D>();
        //取得玩家之碰撞器
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Destroycollider.IsTouching(player))
        {
            EXDestroy();
        }
	}
    public void EXDestroy()
    {
        transform.parent = null;
        Destroy(gameObject);
    }
    //取消父物件，並且刪除自己
}
