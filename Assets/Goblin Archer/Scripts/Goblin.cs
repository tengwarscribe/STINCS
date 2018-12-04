using UnityEngine;
using System.Collections;

public class Goblin : MonoBehaviour
{
    public Transform arrowPrefab;
    public Transform hand;
    public float arrowDelay = 0f;

    public LayerMask ground;
    private Vector2 targetPosition;
    public float speed = 5;
    public float distance = 10;
    public bool lookRight = true;
    public float time = 0;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator makeArrow(float delay, bool right)
    {
        yield return new WaitForSeconds(0);
        var go = Instantiate(arrowPrefab, hand.position, Quaternion.identity);
        go.GetComponent<Arrow>().right = right;
    }

    void Update()
    {
<<<<<<< HEAD
        if (time < 40)
=======
        /*if (time < 150)
>>>>>>> 0e4844454fe2f47f1f6e36a4added09cdb9023e2
        {
            time++;
        }
        else
        {
<<<<<<< HEAD
            time = 0;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.right, distance);
        if (hit.collider.tag == "Player" && time == 40)
=======
            if (Random.Range(0f, 0.25f) > 0.125f)
                animator.SetTrigger("attack");
            StartCoroutine(makeArrow(arrowDelay, lookRight));
        }*/

       /* RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.right, distance);
        if (hit.collider.tag == "Player")
>>>>>>> 0e4844454fe2f47f1f6e36a4added09cdb9023e2
        {
            animator.SetTrigger("attack");
            StartCoroutine(makeArrow(arrowDelay, lookRight));
        }
    }
}