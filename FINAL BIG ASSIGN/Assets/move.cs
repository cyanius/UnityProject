using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (PolygonCollider2D))]
[RequireComponent (typeof (EdgeCollider2D))]
[RequireComponent (typeof (ObjectwithTagsGround))]
//要求物件上要有Rigidbody2D及Collider2D的子物件(在unity稱作component)，如果物件沒有unity會自動增加這兩種子物件。
public class move : MonoBehaviour {

    public float moveSpeed ;
    //移動速度
    private float Speed;
    private ObjectwithTagsGround GR;
    private Rigidbody2D Rigidbody ;
    private EdgeCollider2D Trigger;
    //"物件本身的"剛體以及碰撞子物件
    [HideInInspector]
    //[HideInInspector]以下宣告的變數無法在InInspector直接修改，但可由其他函數取得
    public float JumpHeight = 250f;
    //跳躍高度，希望可由其他函數取得
    public int collision;
    //判斷物件是否在地上。
    private float r = 1;
    
    [HideInInspector]
    public float x;
    //比例係數，若玩家被某一種怪物纏住，移動會變慢的係數
    //unity執行一開始會優先選擇執行的函式為awake()，不論這個函式的位置，執行一次
    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Trigger = GetComponent<EdgeCollider2D>();
        //設定變數取得自己的子物件
        Trigger.isTrigger = true;

        GR = GetComponent<ObjectwithTagsGround>();
        
        Speed = moveSpeed;
    }
    //物理效果必使用fixedupdate函式，固定每次執行週期，不會因電腦效能而有差異造成不正常的顯示
    void FixedUpdate()
    {
        Initial();

        Jump();

        Transform();
        
        Gliding();
        
        Dash();
    }
    private void Gliding() //滑翔函式
    {
        Rigidbody.drag = 0;
        //初始值為0，使其為無窮迴圈效果。
        if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && Rigidbody.velocity.y < 0f)
        {
            Rigidbody.drag = 8; //滑翔下降阻力
        }
        //按下shift則空氣阻力變為8f(只有下降時才有，否則不叫做滑翔)
    }
    private void Transform() //移動函式
    {
        float distance = r * moveSpeed * Time.fixedDeltaTime * Input.GetAxis("Horizontal");
        //偵測到輸入平移鍵盤指令或搖桿指令，移動距離 = 速度 * 每一固定時間內跑過禎(frame)量 * 輸入鍵盤訊號(true=1 false=0)
        transform.Translate(Vector2.right * distance);
        //移動物體
    }
    private void Jump()
    {
        collision = 0;
        foreach (PolygonCollider2D i in GR.ground)
        {
            if (Trigger.IsTouching(i) == true)
            {
                ++collision;
            }
        }
        //物件本身有和標籤為ground物體接觸則collision +1
        if (Input.GetKey(KeyCode.Space) && collision > 0)
        {
            Rigidbody.velocity = new Vector2(0, JumpHeight) * Time.fixedDeltaTime * r;
        }
        //條件為按下空白鍵 加上物體和具有other的物件有接觸才能跳躍
        //限制物體不能在空中連續跳
        //按下空白鍵時給予向上之向量，並以每單位時間跑過禎量計算
        
    }
    private void Dash()
    {
        moveSpeed = Speed;
        if(Input.GetKey(KeyCode.F))
        {
            moveSpeed = 2f*Speed;
        }
        //按下F 速度變兩倍
    }
    private void Initial()
    {
        r = 1 / (1 + 0.1f * x);
    }
}
