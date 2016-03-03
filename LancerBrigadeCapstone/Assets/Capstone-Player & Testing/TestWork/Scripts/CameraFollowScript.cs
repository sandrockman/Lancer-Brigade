using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour
{
    [Tooltip("Object to keep track of center point of all players")]
    public Transform PlayersCenterPoint;

    [Tooltip("Start distance from the PlayersCenterPoint object.")]
    public float startDistanceFromFocus = 20f;
    [Tooltip("Angle in degrees from horizontal view. Set between 0 and 90.")]
    public float angleOffset = 60f;
    [Tooltip("Max distance from the PlayersCenterPoint object.")]
    public float maxDistanceFromFocus = 35f;
    [Tooltip("Increment float for steps the camera will move toward/away from center point.")]
    public float zoomDistanceIncrement = 0.1f;
    [Tooltip("offset float from border that camera starts zooming away.")]
    public float zoomBorderOffset = 0.05f;
    [Tooltip("offset float from center that camera starts zooming inward.")]
    public float zoomCenterOffset = 0.2f;
    [Tooltip("Current distance from the PlayersCenterPoint object.")]
    float currentDistanceFromFocus;
    //distance in negative z direction from PlayersCenterPoint.
    float groundDistanceFromPoint;
    //distance in positive y direction from PlayersCenterPoint.
    float cameraHeight;

    //Start Location in camera movement
    Vector3 startLoc;
    //Designated End Location for player movement
    Vector3 endLoc;
    //distance the camera is allowed to move for smoothing effect.
    public float smooth = 5.0f;

    //empty Game Object that will keep track of the player prefabs
    GameObject playerContainer;
    //object to find the camera object this script is attached to.
    Camera camObj;

    // Use this for initialization
    void Start()
    {
        //find the main camera and player container
        camObj = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerContainer = GameObject.Find("PlayerContainer");
        //set current Distance
        currentDistanceFromFocus = startDistanceFromFocus;

        //updates the current distance from the center point
        UpdateDistance();
        //sets current position for camera (this object).
        transform.position = FindPos();
        //set angle of camera to directly face the center point
        transform.LookAt(PlayersCenterPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //updates the current distance from the center point
        UpdateDistance();
        //update end location
        Follow();
    }

    /// <summary>
    /// Sets up new and old positions for the camera by calling the FindPos()
    /// function and moving the camera 
    /// </summary>
    void Follow()
    {
        startLoc = transform.position;
        endLoc = FindPos();
        transform.position = Vector3.Lerp(startLoc, endLoc, Time.deltaTime * smooth);
    }

    /// <summary>
    /// updates the camera's distance from the center point depending on the 
    /// players on the screen.
    /// </summary>
    void UpdateDistance()
    {
        //locks angle if changed out of the game.
        if (angleOffset < 0)
            angleOffset = 0;
        if (angleOffset > 90f)
            angleOffset = 90f;
        //finds the angle in radians
        float tempRad = angleOffset * Mathf.PI / 180f;
        //sets the -z and +y elements of the camera in relation to the center point.
        cameraHeight = Mathf.Sin(tempRad) * currentDistanceFromFocus;
        groundDistanceFromPoint = Mathf.Cos(tempRad) * currentDistanceFromFocus;
        //creates a list of objects held by the player container.
        Transform[] players = playerContainer.GetComponentsInChildren<Transform>();

        //for each player in the list that is found to have the "Player" tag
        foreach (Transform player in players)
        {
            if (player.tag == "Player")
            {
                //grabs the position on the screen, not the world.
                //If the player has reached the limit of the screen's offset,
                //and the distance from the center point is not at its max,
                //increase it by a designer defined element
                Vector3 viewPoint = camObj.WorldToViewportPoint(player.position);
                if (viewPoint.x < zoomBorderOffset)//too far left
                {
                    if (currentDistanceFromFocus < maxDistanceFromFocus)
                    {
                        currentDistanceFromFocus += zoomDistanceIncrement;
                    }
                }
                else if (viewPoint.x > 1 - zoomBorderOffset)//too far right
                {
                    if (currentDistanceFromFocus < maxDistanceFromFocus)
                    {
                        currentDistanceFromFocus += zoomDistanceIncrement;
                    }
                }
                if (viewPoint.y < zoomBorderOffset)//too far down
                {
                    if (currentDistanceFromFocus < maxDistanceFromFocus)
                    {
                        currentDistanceFromFocus += zoomDistanceIncrement;
                    }
                }
                else if (viewPoint.y > 1 - zoomBorderOffset)//too far up
                {
                    if (currentDistanceFromFocus < maxDistanceFromFocus)
                    {
                        currentDistanceFromFocus += zoomDistanceIncrement;
                    }
                }


                //*Zooms into the players if they are both within an offset of the screen
                if (viewPoint.x < 0.5 + zoomCenterOffset &&
                    viewPoint.x > 0.5 - zoomCenterOffset &&
                    viewPoint.y < 0.5 + zoomCenterOffset &&
                    viewPoint.y > 0.5 - zoomCenterOffset &&
                    currentDistanceFromFocus > startDistanceFromFocus)
                    currentDistanceFromFocus -= zoomDistanceIncrement;
              

                //If one player is outside / inside the bounds, then they both are.
                //so this break doen't make it look as choppy.
                break;
            }
        }
    }

    /// <summary>
    /// finds the end location for camera movement.
    /// </summary>
    /// <returns></returns>
    Vector3 FindPos()
    {
        return new Vector3(
            PlayersCenterPoint.position.x,
            (PlayersCenterPoint.position.y + cameraHeight),
            (PlayersCenterPoint.position.z - groundDistanceFromPoint));
    }
}
