using UnityEngine;
using System.Collections;


public class Board : MonoBehaviour {


    public Vector2 Difference;
    public Vector2 Differ;
    public float PeriodTime;
	// Use this for initialization
	void Start () 
    {
        Differ = Difference;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        Difference = Differ;
        if(Time.time % PeriodTime < 0.5*PeriodTime)
        {
            translate(Difference);
        }
        else
        {
            translate(-Difference);
        }
	}
    private void translate(Vector2 Difference)
    {
        Difference *= Time.fixedDeltaTime;
        transform.Translate(Difference);
    }
}
