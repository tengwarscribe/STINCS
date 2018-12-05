using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public Vector3 v = new Vector3(5, 3, 0);
	public Vector3 a = new Vector3(0, -10, 0);
    public bool right = true;
    public int damage;

    void Start () {
		Destroy(this.gameObject, 10);
        if (!right)
            v.x = -v.x;
	}

	void Update () {
	
		transform.position += v*Time.deltaTime;
		v += a * Time.deltaTime;
        
        transform.rotation = Quaternion.LookRotation(v, new Vector3(0,0,-1));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health otherProjectile = collision.gameObject.GetComponent<Health>();

            Debug.Log("I am colliding with another object!");

            if (otherProjectile.hitPoints <= 80)
            {
                otherProjectile.hitPoints -= 20;
            }

            else if (otherProjectile.hitPoints > 80)
            {
                otherProjectile.hitPoints = 100;
            }

            Destroy(gameObject);
        }
    }
}
