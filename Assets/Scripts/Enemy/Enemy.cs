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
    [SerializeField]
    protected GameObject diamondPrefab;

    protected Animator animator;

    protected Vector3 currentTarget;

    protected bool isHit = false;

    protected Player player;

    protected bool isDead = false;

    public virtual int Health { get; set; }

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (isDead) return;
        
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            Movement();

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > 2.0f)
        {
            isHit = false;
            animator.SetBool("InCombat", false);
        }
    }

    public virtual void Movement()
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

        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        if (animator.GetBool("InCombat"))
            FaceAttacker();
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

    public virtual void Damage(int damageAmount)
    {
        if (isDead) return;

        Health -= damageAmount;

        animator.SetTrigger("Hit");
        isHit = true;
        animator.SetBool("InCombat", true);

        if (Health < 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
            var diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);

            diamond.GetComponent<Diamond>().Diamonds = gems;
            Destroy(gameObject, 6.0f);
        }
    }

    protected virtual void FaceAttacker()
    {
        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x > 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
