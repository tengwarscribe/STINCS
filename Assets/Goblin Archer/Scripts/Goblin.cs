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
        if (time < 25)
        {
            time++;
        }
        else
        {
            time = 0;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.right, distance);
        if (hit.collider.tag == "Player" && time == 25)
        {
            animator.SetTrigger("attack");
            StartCoroutine(makeArrow(arrowDelay, lookRight));
        }
    }
}