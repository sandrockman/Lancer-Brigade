using UnityEngine;
using System.Collections;

public class FillerArrowScript : MonoBehaviour {
    public float speed = 30;
    public XCharacterControllerLancer playerScript;
    public Rigidbody thisRigid;

	// Use this for initialization
	void Start () {
        thisRigid.AddForce(transform.forward * speed *150);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Debug.Log("Player is one more arrow towards being a porcupine.");
        if (other.tag != "RangedRadius" && other.tag != "Untagged")
        { }
        
            StartCoroutine("TimedKill");
    }

    IEnumerator TimedKill()
    {

        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
        
    }
}
