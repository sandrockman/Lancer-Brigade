using UnityEngine;
using System.Collections;

public class ScriptDetectionRadius : MonoBehaviour
{
    public GameObject thisParent;
    public ScriptEnemyClass scriptThing;

    public Transform thePlayer;
    public ScriptEnemyMovement eMove;
    public bool isDetecting = false;
    public SphereCollider detectSphere;
    public bool firstAwake = true;
    public Collider colliderHolder;
    //public AttackScript attackThing;

    void Awake()
    {
        firstAwake = true;

    }

    // Use this for initialization
    void Start()
    {
        //set thisparent to be this collider's parent in the hierarchy; i.e. the object that contains the object with this script
        //thisParent = ;
        scriptThing = thisParent.GetComponentInParent<ScriptEnemyClass>();
        eMove = GetComponentInParent<ScriptEnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log ("enemymove" + eMove.enabled);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //if (isDetecting && Vector3.Distance(other.transform.position, eMove.playerLoc) < Vector3.Distance(eMove.moveStart.position, eMove.playerLoc))
            //{

            //    Debug.Log("I see a closer player.");
            //    thePlayer = other.transform;
            //    eMove.playerCollider = other;
            //}
            //call enemymovement coroutine
            //if(thisParent.GetComponentInChildren<FillerAttackScript>().canAttack == true)
            //Debug.Log(thisParent.GetComponentInChildren<FillerAttackScript>().canAttack + "canattack");
            {
                if (scriptThing.isMovementRunning)
                { Debug.Log("I still see a player."); 
                }
                else
                {
                    //StartCoroutine(scriptThing.EnemyBasicMovement());
                    isDetecting = true;
                    Debug.Log("I see a player.");
                    thePlayer = other.transform;
                    eMove.playerCollider = other;
                    Debug.Log(eMove.firstAwake);
                    firstAwake = false;
                    colliderHolder = other;
                    eMove.isMovementRunning = true;
                    eMove.enabled = true;
                    Debug.Log(eMove.firstAwake);
                    //eMove.hasRun = true;

                }

            }
            //else
            //{
            //    Debug.Log("can't attack");
            //    eMove.hasRun = false;
            //}

        }

        
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("I don't see a player anymore.");
            eMove.fracJourney = 0;
            eMove.currentLerpTime = 0;
            eMove.enabled = false;
            //            if(eMove.isLerping)
            //            {
            //				isDetecting = false;
            //                scriptThing.isMovementRunning = false;
            //                scriptThing.eMove.isMovementRunning = false;
            //				eMove.isLerping = false;
            //				scriptThing.eMove.enabled = false;
            //
            //            }

        }
    }


    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.collider.tag == "Separation")
    //    {
    //        print("Points colliding: " + other.contacts.Length);
    //        print("First normal of the point that collide: " + other.contacts[0].normal);
    //    }
    //}
    //void OnTriggerStay(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        if (thisParent.GetComponentInChildren<FillerAttackScript>().canAttack != true)
    //        {
    //            //Debug.Log("can't attack");
    //            eMove.hasRun = false;
    //        }
    //    }
    //}
}
