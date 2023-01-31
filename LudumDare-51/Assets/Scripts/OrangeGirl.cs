using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeGirl : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider2D;
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private PurpleGirl _purpleGirl;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float jumpForce;

    private bool inAir;

    public bool isGhost;
    private float followTime;

    [HideInInspector]
    public List<Vector2> positions;

    private int posIndex;

    public bool orangeISDead { get; set; } = false;

    [SerializeField] private ParticleSystem deadParticle;
    

    // Start is called before the first frame update
    private void Start()
    {
        Timer10.OnTimerHit0 += ChangeState;

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        inAir = false;
        followTime = 3f;
        posIndex = 0;

        if (isGhost)
        {
            _capsuleCollider2D.enabled = false;
            _boxCollider2D.enabled = false;
            _rigidbody.isKinematic = true;
        }
    }

    private void OnDestroy()
    {
        Timer10.OnTimerHit0 -= ChangeState;
    }

    private void Update()
    {
        if (!orangeISDead && !_purpleGirl.purpleISdead)
        {
            if (!isGhost && !Timer10.paused)
            {
                _animator.SetBool("Run", false);
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);

                // Move right and left
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    _animator.SetBool("Run", true);
                    _rigidbody.velocity = new Vector2(moveSpeed, _rigidbody.velocity.y);
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    _animator.SetBool("Run", true);
                    _rigidbody.velocity = new Vector2(-moveSpeed, _rigidbody.velocity.y);
                }

                // Jump
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !inAir)
                {
                    inAir = true;
                    AudioManager.Instance.PlaySoundEffect("Jump");
                    _animator.SetTrigger("Jump");
                    _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, jumpForce));
                }

                positions.Add(_rigidbody.position);
            }
            else
            {
                if (!Timer10.paused)
                {
                    followTime -= Time.deltaTime;

                    if (followTime <= 0)
                    {
                        _rigidbody.position = _purpleGirl.positions[posIndex++];
                    }
                }
            }
        }
    }

    public void ChangeState()
    {
        Timer10.paused = true;
        AudioManager.Instance.PlaySoundEffect("Rewind");

        StartCoroutine(Rewind());
    }

    public IEnumerator Rewind()
    {
        if (!isGhost)
        {
            isGhost = !isGhost;
            _capsuleCollider2D.enabled = false;
            _boxCollider2D.enabled = false;

            transform.DOMove(_purpleGirl.transform.position, 1f);
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;
            Color c = _spriteRenderer.color;
            c.a = .5f;
            _spriteRenderer.color = c;
            followTime = 3f;
            posIndex = 0;
            _animator.SetBool("Run", false);
        }
        else
        {
            isGhost = !isGhost;
            _capsuleCollider2D.enabled = true;
            _boxCollider2D.enabled = true;


            Color c = _spriteRenderer.color;
            c.a = 1;
            _spriteRenderer.color = c;
            positions.Clear();
            posIndex = 0;
            _rigidbody.gravityScale = 2;
            inAir = false;
        }

        yield return new WaitForSeconds(1.2f);
        _rigidbody.isKinematic = !_rigidbody.isKinematic;
        Timer10.paused = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        inAir = false;

        if (collision.CompareTag("Obstacle") || collision.CompareTag("DeadWall"))
        {
            if (!orangeISDead && !_purpleGirl.purpleISdead)
                PlayerDead();
        }
        
    }

    private void PlayerDead()
    {
        orangeISDead = true;
        
        AudioManager.Instance.PlaySoundEffect("Spike");

        _spriteRenderer.enabled = false;
        deadParticle.Play();

        StartCoroutine(WaitAndReset());
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1f);
        LevelController.Instance.LoadCurrLevel();
    }
    

    public void Footstep()
    {
        AudioManager.Instance.PlaySoundEffect("Run");
    }
}