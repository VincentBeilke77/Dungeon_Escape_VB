using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Animator _swordAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Player animator is NULL.");
        }

        _swordAnimator = transform.GetChild(1).GetComponent<Animator>();
        if (_swordAnimator == null)
        {
            Debug.LogError("Player sword animator is NULL.");
        }
    }

    public void Move(float move)
    {
        _animator.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump)
    {
        _animator.SetBool("Jumping", jump);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        _swordAnimator.SetTrigger("SwordAttack");
    }
}
