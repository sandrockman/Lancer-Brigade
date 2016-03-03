using UnityEngine;
using System.Collections;


public class ScriptEnemyMovement : MonoBehaviour
{

    public Transform moveStart;
    public Transform moveEnd;
    public Vector3 playerLoc;
    public float speed = 0.2F;
    private float startTime;
    private float journeyLength;
    public bool isMovementRunning;
    public bool isLerping = false;
    Transform targetStart; //previously llama
    Transform llamatwo;
    public bool hasRun = true;
    public bool firstAwake = true;
    public Transform thisParent;
    public ScriptDetectionRadius detect;
    public ScriptEnemyClass eClass;
    public Vector3 playerExents;
    public Collider playerCollider;
    public Vector3 myExtents;
    public Collider myCollider;
    public SphereCollider attackSphere;

    Vector3 startPos;
    public Vector3 endPos;
    public float fracJourney = 0f;
    float distcovered = 0f;
    public float lerpTime = 1f;
    public float currentLerpTime;


    void Awake()
    {


    }

    void Start()
    {


        this.enabled = false;
    }

    // Runs whenever this script becomes enabled; this will only run when the enemy can detect the player, then sets the variables necessary
    void OnEnable()
    {
        Debug.Log("onenable hasrun" + hasRun);
        //Debug.Log(detect.firstAwake);
        if (detect.firstAwake)
            hasRun = false;
        if (detect.firstAwake != true)
            hasRun = true;
        Debug.Log("llamaduck" + hasRun);
        if (hasRun)
        {
            //if (firstRun) { thisParent = GetComponentInParent<Transform>(); }
            //else
            Debug.Log("ONEnableHasRun");

            eClass = GetComponent<ScriptEnemyClass>();
            detect = GetComponentInChildren<ScriptDetectionRadius>();
            playerCollider = detect.colliderHolder;
            //playerCollider = moveEnd.GetComponentInParent<Collider>();
            moveEnd = playerCollider.transform;
            playerExents = playerCollider.bounds.extents;
            myCollider = GetComponentInParent<Collider>();
            myExtents = myCollider.bounds.extents;
            Debug.Log(playerExents + "playerext");
            Debug.Log(myExtents + "enemyextents");
            Debug.Log(myExtents + playerExents);
            targetStart = thisParent;


            startPos = transform.position;
            endPos = moveEnd.position - (playerExents + myExtents);
            Debug.Log(startPos);
            Debug.Log(endPos);
            attackSphere = eClass.attackRange;
            {
                startTime = Time.time;
                //set end marker to be a the endpoint of a vector with a set magnitude, but a direction of the player object's transform
                playerLoc = moveEnd.position;
                targetStart = moveEnd;
                playerLoc =
                playerLoc = Vector3.ClampMagnitude(playerLoc, speed);

                //llama.position = playerLoc;

                journeyLength = Vector3.Distance(moveStart.position, targetStart.position);
                isLerping = true;



                startPos = transform.position;
                endPos = moveEnd.position - (playerExents + myExtents);
                if (Mathf.Abs(playerExents.y - myExtents.y) < 1)
                    endPos.y = this.gameObject.transform.position.y;
                else
                    endPos.y += playerExents.y - myExtents.y;
                //insert code to set transforms properly?
            }

        }
        else { }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, playerLoc);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasRun)
        {
            //Debug.Log ("islerping" + isLerping);
            Debug.Log("playerpos" + moveEnd.position);
            //Debug.Log ("isenabled" + this.enabled);
            //Debug.Log ("distance p-e" + Mathf.Abs (Vector3.Distance (this.transform.position, moveEnd.transform.position)));
            if (this.enabled)
            {
                //if (firstRun) {
                //	firstRun = false;
                //	this.enabled = false;} 
                //else 
                {
                    if (Mathf.Abs(Vector3.Distance(this.transform.position, moveEnd.transform.position)) > attackSphere.radius)
                    {
                        //Debug.Log ("lerpcheck" + isLerping);
                        if (fracJourney < 1) //is the object at the end of the lerp?
                        {
                            Debug.Log(fracJourney + "fracjourney");
                            Debug.Log("player distance: " + Vector3.Distance(transform.position, moveEnd.position));
                            Debug.Log("endpos" + endPos);
                            Debug.Log("mathtarget " + (moveEnd.position - (playerExents + myExtents)));
                            if (endPos != moveEnd.position - (playerExents + myExtents))
                            {
                                Debug.Log("reset targeter");
                                fracJourney = 0;
                                distcovered = 0;
                                currentLerpTime = 0; //leave this line out for funky teleports
                                startPos = transform.position;
                                endPos = moveEnd.position - (playerExents + myExtents);
                                if (Mathf.Abs(playerExents.y - myExtents.y) < 1)
                                    endPos.y = this.gameObject.transform.position.y;
                                else
                                    endPos.y += playerExents.y - myExtents.y;
                                Debug.Log("Endpos before clamp" + endPos);
                                //endPos = Vector3.Normalize(endPos);
                                //endPos *= speed;

                                Debug.Log("endpos after clamp" + endPos);
                            }
                            currentLerpTime += Time.deltaTime;
                            if (currentLerpTime > lerpTime)
                            {
                                currentLerpTime = lerpTime;
                            }
                            fracJourney = currentLerpTime / lerpTime;
                            transform.position = Vector3.MoveTowards(startPos, endPos, fracJourney * speed);

                        }
                        else
                        {
                            Debug.Log("reached1");
                            //wait until player leaves attack range to reset values
                            //do attack stuff
                            //Debug.Log("player distance: " + Vector3.Distance(transform.position, moveEnd.position));
                            if (Vector3.Distance(transform.position, moveEnd.position) > attackSphere.radius)
                            {
                                Debug.Log("blahblahblah");
                                fracJourney = 0;
                                currentLerpTime = 0;
                                startPos = transform.position;
                                endPos = moveEnd.position - (playerExents + myExtents);
                                if (Mathf.Abs(playerExents.y - myExtents.y) < 1)
                                    endPos.y = this.gameObject.transform.position.y;
                                else
                                    endPos.y += playerExents.y - myExtents.y;
                                //endPos.magnitude = speed;
                            }
                            else { }

                        }
                    }
                }
            }


            //if (isLerping) {
            ////					float distCovered = (Time.time - startTime) * speed; //test for framerate independence or not
            ////					Debug.Log ("distcovered" + distCovered);
            ////					float fracJourney = distCovered / journeyLength;
            ////					Debug.Log ("fracJourney" + fracJourney);
            ////					if (detect.isDetecting == false) {
            ////						fracJourney = 1f;
            ////					}
            //					//if(transform.Translate(playerExents + playerLoc + myExtents)  )
            //					{
            //						playerLoc = moveEnd.position - transform.position;
            //						targetStart = moveEnd;
            //
            //						playerLoc = Vector3.ClampMagnitude(playerLoc, speed);
            //						Debug.Log("enemymovetargetmoved" + playerLoc);
            //					}
            //					Debug.Log("enemymovetarget" + playerLoc);
            //
            //					transform.Translate(playerLoc);
            //
            //					//transform.position = Vector3.Lerp (moveStart.position, llama.position, fracJourney);
            //					Debug.Log ("enemypos" + transform.position);
            //					if(Mathf.Abs(Vector3.Distance(this.transform.position, moveEnd.transform.position)) <= eClass.attackRange){
            //						Debug.Log("tooclose");
            //						isLerping = false;}
            //					if(Mathf.Abs(Vector3.Distance(this.transform.position, moveEnd.transform.position)) > detect.detectSphere.radius){
            //						Debug.Log("toofar");
            //						isLerping = false;}
            ////					if (fracJourney == 1f) {
            ////						isLerping = false;
            ////					}
            //					
            //	}
            //				if (isLerping == false && Mathf.Abs(Vector3.Distance(this.transform.position, moveEnd.transform.position)) > detect.detectSphere.radius) {
            //
            //					Debug.Log ("edgecase");
            //					startTime = Time.time;
            //					//set end marker to be a the endpoint of a vector with a set magnitude, but a direction of the player object's transform
            ////					playerLoc = moveEnd.position;
            ////					playerLoc = Vector3.ClampMagnitude (playerLoc, speed);
            ////					llama = moveEnd;
            ////					llama.position = playerLoc;
            //					playerLoc = moveEnd.position;
            //					targetStart = moveEnd;
            //					
            //					playerLoc = Vector3.ClampMagnitude(playerLoc, speed);
            //					journeyLength = Vector3.Distance (moveStart.position, targetStart.position);
            //					isLerping = true;
            //					this.enabled = false;
            //				}
            //				else if(isLerping == false && Mathf.Abs(Vector3.Distance(this.transform.position, moveEnd.transform.position)) <= eClass.attackRange)
            //				{
            //					isLerping = true;
            //				}


        }
        else hasRun = true;
        Debug.Log(hasRun);
    }
}

//break down into components to insert into main enemy movement based off of boolean checks? No, can call with another script



