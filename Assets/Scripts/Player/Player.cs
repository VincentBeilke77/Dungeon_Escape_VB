using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rigidBody;
    
    [SerializeField]
    private float _jumpForce = 5.0f;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _speed = 2.75f;

    private bool _isDead = false;
    private bool _resetJump;
    private bool _grounded = false;

    private PlayerAnimation _playerAnimation;

    public int Health { get; set; }

    [SerializeField]
    private int _diamonds;
    public int Diamonds { 
        get
        {
            return _diamonds;
        } 
        set
        {
            _diamonds = value;
        }
    }

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
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) return;

        Movement();
        CheckForAttack();
        Debug.Log("Diamonds: " + Diamonds);
    }

    private void CheckForAttack()
    {
        if ((Input.GetKeyDown(KeyCode.X) || CrossPlatformInputManager.GetButtonDown("A_Button")) 
            && _grounded)
        {
            _playerAnimation.Attack();
        }
    }

    private void Movement()
    {
        var move = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        Debug.DrawRay(transform.position, Vector2.down * .85f, Color.green);

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) 
            && _grounded)
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
        if (_isDead) return;
        Health -= 1;
        UIManager.Instance.UpdateLives(Health);
        if (Health < 1)
        {
            _isDead = true;
            _playerAnimation.Death();
        }
        // remove 1 health
        // update UI display
        // check for death
        // play death animation
    }

    public void AddDiamonds(int amount)
    {
        Diamonds += amount;
        UIManager.Instance.UpdateDiamondCount(Diamonds);
    }
}
