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


    //On collision do something
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit!");
        //if (hitme.tag == "Player")
       // {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}

        
    }
}
