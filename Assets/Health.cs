
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hitPoints;
    public float timeToDie;
    public GameObject spawn;
    private int maxHealth;


    // Use this for initialization
    void Start()
    {
        maxHealth = hitPoints;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (hitPoints <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(timeToDie);

        //Destroy(gameObject);

        hitPoints = maxHealth;

        Spawn respawn = spawn.GetComponent<Spawn>();
        respawn.Respawn();
    }

    public int HitPoints
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

    public GameObject Spawn
    {
        get { return spawn; }
        set
        {
            spawn = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Projectile otherProjectile = collision.gameObject.GetComponent<Projectile>();

            //Debug.Log("I am colliding with another object!");

            hitPoints -= otherProjectile.Damage;

            Destroy(otherProjectile.gameObject);
        }
    }

}