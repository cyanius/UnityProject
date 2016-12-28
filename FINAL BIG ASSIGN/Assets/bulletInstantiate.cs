using UnityEngine;
using System.Collections;
using System;

public class bulletInstantiate : MonoBehaviour {

    private float LastTime = -9999;
    public float UpdateRate;
    private Vector3 Distance;
    public ForceMode2D FM;
    private int i = 0;
    //第 i 個子彈
    private GameObject player;
    //物件:玩家
    public GameObject[] icebullet = new GameObject[1];
    //發射物件
    private Vector3 bulletposition;
    //子彈初始位置
    public string filename = "ice";
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Distance = player.transform.position - gameObject.transform.position;
        //與玩家距離
        if (Time.time - LastTime > UpdateRate && Distance.magnitude < 5f)
        {
            LastTime = Time.time + UnityEngine.Random.value * UpdateRate * 5f;
            Shot();
        }
        //更新時間及條件

    }

    private void Shot()
    {
        Initial(ref icebullet);
        //給予物件
        bulletposition = gameObject.transform.position + Distance * 0.01f;
        Instantiate(icebullet[i], bulletposition, new Quaternion(0, 0, 0, 0), gameObject.transform);
        //新增子彈物件
        ++i;
    }
    private void Initial(ref GameObject[] icebullet)//給予物件
    {
        int L = i + 1;
        Array.Resize(ref icebullet, L);
        icebullet[i] = (GameObject)Resources.Load("Prefabs/ice");
    }
}

