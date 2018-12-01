using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int secondsToDestroy;
    public WaitForSeconds time;
    public int damage;

    // Use this for initialization
    void Start()
    {
        time = new WaitForSeconds(0.01f);
        StartCoroutine(Trajectory());
    }

    IEnumerator Trajectory()
    {
        yield return time;

        gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        StartCoroutine(Trajectory());
    }

    IEnumerator SelfDestruct(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }

    public int Damage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }
}