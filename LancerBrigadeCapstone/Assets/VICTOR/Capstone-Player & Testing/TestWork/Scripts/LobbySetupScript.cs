using UnityEngine;
using System.Collections;

/**
 *@author Victor Haskins
 *class LobbySetupScript Keeps track of the players and will modify the screen
 *as players are added/removed.
 */
public class LobbySetupScript : MonoBehaviour {

    public GameObject player1Join;
    public GameObject player2Join;
    public GameObject startCommand;

    /// <summary>
    /// At start, automatically sets both players to false as well as
    /// the start command
    /// </summary>
	// Use this for initialization
	void Start () {
        player1Join.SetActive(false);
        player2Join.SetActive(false);
        startCommand.SetActive(false);
	}

	/// <summary>
    /// 
    /// </summary>
	// Update is called once per frame
	void Update () {
	    switch(StaticSpawnController.GetSetPlayers())
        {
            case 0://neither player is active. all items are blanked out
                player1Join.SetActive(false);
                player2Join.SetActive(false);
                startCommand.SetActive(false);
                break;
            case 1://player one is active while player 2 is not
                player1Join.SetActive(true);
                player2Join.SetActive(false);
                startCommand.SetActive(true);
                break;
            case 2://player two is active while player 1 is not
                player1Join.SetActive(false);
                player2Join.SetActive(true);
                startCommand.SetActive(true);
                break;
            case 3://player one and two are both active.
                player1Join.SetActive(true);
                player2Join.SetActive(true);
                startCommand.SetActive(true);
                break;
            default:
                break;

        }
	}
}
