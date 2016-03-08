using UnityEngine;
using System.Collections;

public class PlayerHoverBob : MonoBehaviour {

    public float bobSpeed = 0.1f;
    public float bobHeight = 0.5f;

    public Transform player;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(player != null)
        {
            float newPos = Mathf.PingPong(Time.time * bobSpeed, bobHeight);
            player.localPosition = Vector3.up * newPos;
        }
	}
}
