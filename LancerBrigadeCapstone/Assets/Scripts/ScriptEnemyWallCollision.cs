using UnityEngine;
using System.Collections;

public class ScriptEnemyWallCollision : MonoBehaviour {
    public ScriptDetectionRadius detect;
    public ScriptEnemyMovement eMove;
    public LayerMask wallMask = 8;
    public bool isWallCollision = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("wallhit" + other.name);
        if (other.tag == "Barrier")
        {
            isWallCollision = true;
            //eMove.isMovementRunning = false;
            //fancy version
            //Debug.Log("eCollideWall");
            if (eMove.isMovementRunning)
            {
                
                RaycastHit wallRay;
                if(Physics.Raycast(this.gameObject.transform.position, eMove.playerLoc, out wallRay, detect.detectSphere.radius))
                {
                    Debug.Log(wallRay.collider + " " + wallRay.distance);
                    Debug.Log(other.tag);
                    if (wallRay.collider.transform.tag == "Barrier")
                        eMove.isMovementRunning = false;
                }
                //Debug.Log("nothittingawall");
                
            }

        }
    }

    //void OnCollisionStay(Collision other)
    //{
    //    //Debug.Log("hitting something");
    //    //Debug.Log(other.collider.name);
    //    if (other.collider.tag == "Barrier")
    //    {
    //        Debug.Log("wallstillhitting");
    //        //fancy version
    //        //Debug.Log("eCollideWall");
    //        //if (eMove.isMovementRunning)
    //        //{
    //        //    RaycastHit wallRay;
    //        //    Physics.Raycast(this.gameObject.transform.position, eMove.playerLoc, out wallRay, detect.detectSphere.radius, wallMask);
    //        //    Debug.Log(wallRay.collider + " " + wallRay.distance);
    //        //    if (wallRay.collider.tag == "Barrier")
    //        //        eMove.isMovementRunning = false;
    //        //}
    //        //else
    //        //{
    //        //    RaycastHit wallRay;
    //        //    Physics.Raycast(this.gameObject.transform.position, eMove.playerLoc, out wallRay, detect.detectSphere.radius, wallMask);
    //        //    if(wallRay.collider.tag != "Barrier")
    //        //    {
    //        //        eMove.enabled = false;
    //        //        eMove.playerCollider = detect.colliderHolder;
    //        //        eMove.isMovementRunning = true;
    //        //        eMove.enabled = true;
    //            //}
    //       // }

    //    }
    //}
}
