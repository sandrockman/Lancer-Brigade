using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class OutOfBounds : MonoBehaviour {

    // Use this for initialization
    void Start () {
        Debug.Log("start");
    }

    // Update is called once per frame
    //void Update () {

    //}


    //Mod by Victor Haskins. Delete if not properly functioning
    /*testing another collider function

    //On collision do something
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit!");
        //if (hitme.tag == "Player")
       // {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}

        
    }

    //Mod by Victor Haskins. Delete if not properly functioning 
    //*/ //end test comment out.

    //Mod function by Victor Haskins. Delete if not properly functioning
    /// <summary>
    /// Change outOfBoundsBox to a trigger collider
    /// Upon activating trigger, do something based on whether it is a player,
    /// or something else
    /// </summary>
    /// <param name="other">Representation of what has entered the trigger.
    /// Only causes effect if it has a collider.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        //suggested to add new if statement to check for enemy and leave out
        //rest of else statement.
        {
            Debug.Log("OutOfBounds has destroyed non-player object.");
            Destroy(other.gameObject);
        }
    }
}
