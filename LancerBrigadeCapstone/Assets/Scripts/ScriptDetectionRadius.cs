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
            //call enemymovement coroutine
            if (scriptThing.isMovementRunning)
            { Debug.Log("I still see a player."); }
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

                eMove.enabled = true;
                Debug.Log(eMove.firstAwake);
                //eMove.hasRun = true;

            }

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
}
