using UnityEngine;
using System.Collections;

public class ScriptEnemyClass : MonoBehaviour
{
    //insert tooltip here: "Which type of enemy is this?"
    public enum EnemyType {BasicMelee, BasicRanged, MidMelee, MidRanged, Boss };
    //insert tooltip: "How far away can this enemy detect the player?"
    public int sightRange;
    //tooltip: "How far away can the enemy strike the player?"
    public SphereCollider attackRange;
    //tooltip
    public Animation attackAnimation;

    public Animation deathAnimation;

    public int attackDamage;

    public int health = 1;

    public int startingHealth = 100;

    public int armor;

    public float speed;
    //tooltip: "How many frames of delay does this enemy have before it can react to the player?"
    public int reactionTime;
    //tooltip: "Can this enemy cancel its animations?"
    public bool canFrameCancel;

    //public Projectile[10];

    public ScriptDetectionRadius detector;
    public ScriptEnemyMovement eMove;

    //inform designers to not fuck with these variable
    public bool isMovementRunning = false;
    public bool isAttackRunning = false;


	// Use this for initialization
	void Start () {
        eMove = GetComponent<ScriptEnemyMovement>();
        detector = GetComponentInChildren<ScriptDetectionRadius>();
		health = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		//wtf, this is instantly triggering...
//		if (health <= 0)
//			Destroy (this.gameObject);
	}

    

    public IEnumerator EnemyBasicMovement()
    {
        Debug.Log("EnemyBasicMovement");
        if(isMovementRunning != true && eMove.isMovementRunning != true)
        {
            eMove.enabled = true;
        }
        //includes functionality of movement
        isMovementRunning = true;
        //this.gameObject.transform.Translate(detector.thePlayer.position);
        //do the movmement with lerping instead
        eMove.isMovementRunning = true;
        return null;
    }

    public IEnumerator EnemyBasicAttack()
    {
        //includes functionality of attacking
        isAttackRunning = true;
        return null;
    }

    public void EnemyRemoval()
    {
        //includes functionality of removing dead enemies from the game
    }


}
