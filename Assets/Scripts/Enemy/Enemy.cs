using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Animator animator;

    protected Vector3 currentTarget;

    public abstract int Health { get; set; }

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            Movement();
    }

    protected virtual void Movement()
    {
        CheckForFlip();

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            animator.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            animator.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

    protected virtual void CheckForFlip()
    {
        if (currentTarget == pointA.position)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (currentTarget == pointB.position)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public abstract void Damage();
}
