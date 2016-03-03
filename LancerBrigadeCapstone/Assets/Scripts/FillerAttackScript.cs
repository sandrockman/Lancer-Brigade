using UnityEngine;
using System.Collections;

public class FillerAttackScript : MonoBehaviour {
    public GameObject arrow;
    public int aimTime = 2;
    public int waitTime = 5;
    public bool canAttack = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && canAttack)
        {
            StartCoroutine("Attack");
            canAttack = false;
            gameObject.GetComponentInParent<ScriptEnemyMovement>().enabled = false;
        }
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(aimTime);
        Vector3 arrowShot = transform.TransformDirection(Vector3.forward);
        //arrowShot = transform.InverseTransformPoint(arrowShot);
        Debug.Log(GetComponentInParent<Rigidbody>().name);
        Instantiate(arrow, gameObject.GetComponentInParent<Rigidbody>().position + transform.forward, this.gameObject.GetComponentInParent<Rigidbody>().rotation);
        yield return new WaitForSeconds(waitTime);
        canAttack = true;
        gameObject.GetComponentInParent<ScriptEnemyMovement>().enabled = true;
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if(other.tag == "Player") //add in check for if this enemy can animcancel
    //    {
    //        //StopCoroutine("Attack");
    //        //canAttack = true;
    //        //gameObject.GetComponentInParent<ScriptEnemyMovement>().enabled = true; use this line for animatino cancelling enemies
    //    }
    //}
}
