using UnityEngine;
using System.Collections;

public class PlayerStatScript : MonoBehaviour {

    public enum StatusEffects{NONE, DAZED, PARALYZED, POISONED, BLEEDING};

    public float Health { get; private set; }
    public float AttackStrength { get; private set; }
    public StatusEffects Status { get; private set; }

    public float initialHealth = 10;
    public float maxHealth = 10;
    public float initAtkStr = 1;
    public StatusEffects initStat = StatusEffects.NONE;
    public bool healthBoost = false;

    // Use this for initialization
    void Awake () {
        Health = initialHealth;
        Status = initStat;
        AttackStrength = initAtkStr;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Health <= 0)
        {
            DeathFunction();
        }
        if(!healthBoost)
        {
            if(Health > maxHealth)
            {
                Health = maxHealth;
            }
        }
	}

    void DeathFunction()
    {
        //death script
    }

    void TakeDamage(float dmg)
    {
        Health -= dmg;
    }

    void TakeHealing(float hlth)
    {
        Health += hlth;
    }

    void ChangeStatus(StatusEffects newStat)
    {
        Status = newStat;
    }
}
