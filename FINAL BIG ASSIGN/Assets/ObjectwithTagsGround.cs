using UnityEngine;
using System.Collections;

public class ObjectwithTagsGround : MonoBehaviour {
    [HideInInspector]
    public PolygonCollider2D[] ground;
    int count;
    //場景中固定物碰撞器子物件
	// Use this for initialization
	 void Awake () {
        count = GameObject.FindGameObjectsWithTag("ground").Length;
        ground = new PolygonCollider2D[count];
        for (int i = 0; i < ground.Length; ++i)
        {
            ground[i] = GameObject.FindGameObjectsWithTag("ground")[i].GetComponent<PolygonCollider2D>();
            //設定具有標籤"ground"的物件其子物件collider2D為ground
        }
	}
	public void ArrayGround(ref PolygonCollider2D [] gr)
    {
        gr = new PolygonCollider2D [count];
        for (int i = 0; i < count; ++i)
        {
            gr[i] = this.ground[i];
        }
    }

	
}
