using UnityEngine;
using System.Collections;

public class ScriptDetectionDebugging : MonoBehaviour {

    public SphereCollider attackSphere;
    public SphereCollider detectSphere;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectSphere.radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackSphere.radius);
    }
}
