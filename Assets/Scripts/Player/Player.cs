using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rigidBody;
    
    [SerializeField]
    private float _jumpForce = 5.0f;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _speed = 2.75f;
    
    private bool _resetJump;
    private bool _grounded = false;

    private PlayerAnimation _playerAnimation;

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        if (_rigidBody == null)
        {
            Debug.LogError("Rigid Body is NULL.");
        }
        _playerAnimation = GetComponent<PlayerAnimation>();
        if (_playerAnimation == null)
        {
            Debug.LogError("Player animation is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckForAttack();
    }

    private void CheckForAttack()
    {
        if (Input.GetMouseButtonDown(0) && _grounded)
        {
            _playerAnimation.Attack();
        }
    }

    private void Movement()
    {
        var move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnimation.Jump(true);
        }

        _rigidBody.velocity = new Vector2(move * _speed, _rigidBody.velocity.y);

        _playerAnimation.Move(move);
        ChangeDirection(move);
    }

    private bool IsGrounded()
    {
        var hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.85f, _groundLayer.value);
        if (hitInfo.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnimation.Jump(false);
                return true;
            }
        }

        return false;
    }

    private void ChangeDirection(float move)
    {
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage(int damageAmount)
    {
        Debug.Log("Player Damage!");
    }
}
