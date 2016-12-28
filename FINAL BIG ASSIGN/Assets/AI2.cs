using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
//要求物件上要有Rigidbody2D及CircleCollider2D的類別，如果物件沒有unity會自動增加。
public class AI2 : MonoBehaviour
{
    private Transform Mine;
    //自己的transform 類別
    private Vector3 position;
    //目前位置
    public Vector3 originposition;
    //初始之位置
    private GameObject Target;
    //目標物的transform子物件
    public string targetname = "Player";
    //尋找目標的tag
    private Rigidbody2D RD;
    //本身的剛體子物件
    public float AISpeed = 8f;
    //AI的移動速度
    private float Distance;
    //與玩家距離
    public float Limitdistance = 3;
    //跟隨的極限距離
    public float UpdateRate = 0.1f;
    //更新路徑參數頻率，數字越小時更新越快
    public ForceMode2D FM;
    //Addforce所需使用之力量模式
    private float LastTime = -9999;
    //為一判斷時間所用到之變數，初始值為-9999使其必定能夠一開始就執行
    [HideInInspector]
    public CircleCollider2D Col2D;

    //unity一開始會優先選擇執行的函式為awake()，此處start()為第二順位執行
    // Use this for initialization
    void Start()
    {
        RD = GetComponent<Rigidbody2D>();
        //變數設置使變數取得子物件
        RD.drag = 0.25f;
        //設定空氣阻力
        RD.gravityScale = 0f;
        //設定重力大小
        Target = GameObject.FindGameObjectWithTag(targetname);
        Mine = GetComponent<Transform>();
        position = Mine.position;
        //取得自己的座標
        Col2D = GetComponent<CircleCollider2D>();
        Col2D.radius = 1f;
        Col2D.isTrigger = true;
        //取值並設值給collider   
    }
    
    void FixedUpdate() //此函式會一直重複執行
    {
        RD.drag = 0.25f;
        Distance = (Target.transform.position - Mine.position).magnitude;
        if (Time.time - LastTime > UpdateRate && Distance < Limitdistance ) // 距離小於3時開始攻擊
        {
            LastTime = Time.time + Random.value * UpdateRate * 0.5f;
            StartCoroutine(go());
        }
        //Time.time為遊戲執行的時間，Random.value相當於Random.NextDouble
        //Time.time - LastTime = { Time.time(末) - Time.time (初) }- Random.value * UpdateRate * 0.5f
        //故 Time.time(末) - Time.time (初) > UpdateRate * ( 1 + Random.value * 0.5f)
        // 意即 要過了時間為 更新速率 * (1~1.5) 秒 才會再次更新路徑(使AI不會緊緊跟隨)
        if (Target == null)
        {
            return;
        }
        if(Col2D.IsTouching(Target.GetComponent<EdgeCollider2D>()))
        {
            RD.drag = 5f;
        }
        //碰撞玩家時減速
        if(Distance > 1 + Limitdistance)
        {
            RD.drag = 5f;
            StartCoroutine(origin());
        }
        //當過於遠離玩家時，會自發地回到初始位置
    }
    IEnumerator go()
    {
        yield return new WaitForSeconds(Random.value * UpdateRate);
        //等待隨機秒數後繼續
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        // AI要前進的方向
        //path.vectorPath[CurrentPath]為現在應該前進的方向，transform.position為自己的位置
        direction *= AISpeed;
        //乘上AI速度
        RD.AddRelativeForce(direction, FM);
        //施加力以及力的模式
    }
    IEnumerator origin()
    {
        yield return new WaitForSeconds(2f);
        
        if (Distance > Limitdistance)
        {
            originposition =  AISpeed * (position - transform.position).normalized * Time.fixedDeltaTime;
            RD.AddRelativeForce(originposition, FM);
        }
        //等待兩秒後回到原本位置
    }
}

