using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float hitPoints;
    public float timeToDie;
    private float maxHealth;
    public Slider healthbar;

    // Use this for initialization
    void Start()
    {
        maxHealth = hitPoints;
        healthbar.value = CalculateHealth(); //connects the in game health to UI 
    }

    private void Update()
    {
        //This is a dealing damage test code
        if (Input.GetKeyDown(KeyCode.X))
            DealDamage(10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (hitPoints <= 0)
        {
            StartCoroutine(Death());
        }
    }

    void DealDamage(float damageValue)
    {
        hitPoints -= damageValue;
        healthbar.value = CalculateHealth();
        if (hitPoints <= 0)
            Death();
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(timeToDie);
        hitPoints = 0;
        Destroy(gameObject);
        Debug.Log("You Died");
    }

    float CalculateHealth()
    {
        return hitPoints / maxHealth; //calculates health from max health. Returns in decimals representing percentage. 99/100 = .99
    }

    public float HitPoints
    {
        get { return hitPoints; }
        set
        {
            hitPoints += value;
            if (hitPoints > maxHealth)
            {
                hitPoints = maxHealth;
            }
        }
    }

}

