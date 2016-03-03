using UnityEngine;
using System.Collections;

/**
 * @author Victor Haskins
 * class PlayerTrackingScript to be placed on an empty gameObject that will
 * find the center point between multiple players and then make that objects 
 * location that point.
 */
public class PlayerTrackingScript : MonoBehaviour {
    [Tooltip("Game Object that holds all the player prefabs on screen.")]
    public GameObject PlayersContainer;
    //Used to find the center from the players' positions
    Vector3 trackingPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //finds the center of the players.
        TrackPlayers();
        //set the position to the new center point.
        transform.position = trackingPosition;
	}

    /// <summary>
    /// Finds the player's center point
    /// </summary>
    void TrackPlayers()
    {
        //set another temp vector3 if problems arise
        Vector3 track = new Vector3(0, 0, 0);
        //
        int trackAvg = 0;
        //provided there is at least one player in the game...
        if (PlayersContainer.transform.childCount > 0)
        {
            //create a list of all the players
            Transform[] players = PlayersContainer.GetComponentsInChildren<Transform>();
            //for every player (only runs once in single player
            foreach (Transform player in players)
            {
                //double check if the object IS a player
                if (player.tag == "Player")
                {
                    //add player position vector
                    track += player.transform.position;
                    //increment average counter to be used later.
                    trackAvg++;
                }
            }
            //find average of all player positions added. there's the center point.
            track.x /= trackAvg;
            track.y /= trackAvg;
            track.z /= trackAvg;
        }
        //set the tracking position to our temp vector3
        trackingPosition = track;
    }
}
