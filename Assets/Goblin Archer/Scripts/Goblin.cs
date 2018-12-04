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
        /*if (time < 150)
        {
            time = time + 1;
        }
        else if (time == 150)
        {
            if (Random.Range(0f, 0.25f) > 0.125f)
                animator.SetTrigger("attack");
            StartCoroutine(makeArrow(arrowDelay, lookRight));
        }*/

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.right, distance);
        if (hit.collider.tag == "Player")
        {
            animator.SetTrigger("attack");
            StartCoroutine(makeArrow(arrowDelay, lookRight));
        }

        /*if (targetPosition.x > transform.position.x && !lookRight)
            Flip();
        if (targetPosition.x < transform.position.x && lookRight)
            Flip();

        var p = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //Vector3 vel = targetPosition - transform.position;
        //vel = Vector3.ClampMagnitude(vel, speed * Time.deltaTime);
        //transform.position += vel;

        animator.SetFloat("speed", (transform.position - p).magnitude / Time.deltaTime);*/
    }

    public void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        lookRight = !lookRight;
    }
}