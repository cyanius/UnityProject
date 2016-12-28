using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (Rigidbody2D))]
public class AI3 : MonoBehaviour {

    private float LastTime = -99999;
    public float UpdateRate;
    //更新(平均時間) 越小越快
    private Transform player;
    private float Distance;
    //與玩家的距離
    private Vector3 TargetPosition; 
    //AI目標座標
    private Vector3 Direction; 
    //AI目標座標之方向
    private Rigidbody2D RD;
    //自己的剛體
    public float AISpeed;
    public ForceMode2D FM;
    //力模式以及AI速度
    public float R_range ;
    public float Y_range ;
    public float HeightRange ;
    //移動目標 範圍
    //為一長條區域及扇形的交集(由以下之程式碼設計)
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //另一種寫法 private Gameobject player;
        //           player = GameObject.FindGameObjectWithTag("Player")
        //           player.transform.position // (獲取玩家位置)
        RD = GetComponent<Rigidbody2D>();
        RD.gravityScale = 0f;
        //取得值
        if(HeightRange + Y_range < R_range)
        {
            R_range = (float)Math.Pow((HeightRange + Y_range), 2);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        RD.drag = 0.5f;
        if(player == null)
        {
            return;
        }
        //若沒有玩家則返回，以免出錯
        Distance = (player.position - gameObject.transform.position).magnitude;
        //與玩家距離
        if (Time.time - LastTime > UpdateRate && Distance < 5f) 
        {
            LastTime = Time.time + UnityEngine.Random.value * UpdateRate * 5f;
            NextPosition();
        }
        //更新時間及條件
        Direction = (TargetPosition - gameObject.transform.position).normalized;
        Direction *= AISpeed;
        RD.AddForce(Direction, FM);
        //施加力使其朝特定方向移動
	}
    private void NextPosition()
    {
        TargetPosition.y = player.position.y + HeightRange + UnityEngine.Random.value * Y_range;
        //y座標 為玩家上方一個長條range 高為Y_Range
        int x;
        x = plusminus();
        TargetPosition.x = player.position.x + x * UnityEngine.Random.value * (float)Math.Sqrt(R_range - Math.Pow((TargetPosition.y-player.position.y), 2));
        //x = 玩家位置 +- (R_range-y^2)^0.5;
        //  = (R_range - (HeightRange + UnityEngine.Random.value * Y_range)^2 )^0.5
    }
    private int plusminus () // 隨機產生+1或-1
    {
        int x;
        float y;
        y = UnityEngine.Random.value;
        if(y>0.5f)
        {
            x = +1;
            return x;
        }
        else
        {
            x = -1;
            return x;
        }
    }
}
