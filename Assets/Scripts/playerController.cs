using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
     public Animator Animator;
    [SerializeField]
    float Jump_Power;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private ScoreManager Score;
    Rigidbody2D RB2d;
    public int playerHealth = 5;
    [SerializeField]
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private LayerMask platformLayerMask;
    [SerializeField]
    private BoxCollider2D platformCollider;
    [SerializeField]
    private float fallingThreshold; 


    private float distanceToGround;
    private static playerController instance;
    public static playerController Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        //lastYPosition = transform.position.y;
        RB2d = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        PlayingAnimation(Horizontal);
        PlayerMovement(Horizontal);
        stoplooping();

        if (IsGrounded())
        {
            Animator.SetBool("IsGrounded", true);
        }
        else 
        { 
            Animator.SetBool("IsGrounded", false);
            CheckFalling();    
        }

    }

    public void Collectable_PickedUp()
    {
        Debug.Log("Collected");
        Score.Update_Score(10);
    }

    void PlayerMovement(float Horizonatal)
    {
        Vector2 pos = transform.position;
        pos.x += Horizonatal * Speed * Time.deltaTime;
        transform.position = pos;
    }

    private bool IsGrounded()
    {
        float ExtraHeightChcek = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size - new Vector3(0.25f,0f,0f), 0f, Vector2.down, ExtraHeightChcek, platformLayerMask);
        Color Boxcolor;
        if (raycastHit.collider != null)
        {
            Boxcolor = Color.green;
        }
        else
        {
            Boxcolor = Color.red;
            distanceToGround = Mathf.Abs(raycastHit.point.y - boxCollider2D.bounds.center.y);
        }

        // drawing an BoxCast visual to see in the scene whats going on
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector3.down * (boxCollider2D.bounds.extents.y + ExtraHeightChcek), Boxcolor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector3.down * (boxCollider2D.bounds.extents.y + ExtraHeightChcek), Boxcolor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y + ExtraHeightChcek), Vector3.right * (boxCollider2D.bounds.extents.x * 2f), Boxcolor);
      
        return raycastHit.collider != null; //these returns if Boxcast is hitting our ground or not(T/F) 
    }

    private void CheckFalling()
    {
            if (RB2d.velocity.y < 0) //player falling down 
            {
                FallingAnim();
            }
    }

    private void FallingAnim()
    {
        Animator.SetTrigger("Falling");
    }

    private void PlayingAnimation(float Horizonatl) {
        
        Animator.SetFloat("Speed", Mathf.Abs(Horizonatl));

        //IsGrounded() is called before checking for the jump key. C# implements "shortcutting"
        //which means that if you have 2 AND conditions in an if statement and it fails the first one, it won't even check for the second one.
        //If the IsGrounded() is in the second one, then it won't draw the raycast.
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.SFXSounds(SoundTypes.Jump);
            Animator.SetTrigger("IsJumping");
            RB2d.AddForce(new Vector2(0f, Jump_Power), ForceMode2D.Impulse);
            Debug.Log("Jumping trrigered");
        }
        
        if (IsGrounded() && Input.GetKeyDown(KeyCode.LeftControl)) //CrouchDown
        {
            Animator.SetBool("IsCrouching", true);         
        }
        if (IsGrounded() && Input.GetKeyUp(KeyCode.LeftControl)) //CrouchUp when key is released
        {
            Animator.SetBool("IsCrouching", false);
        }

        Vector3 scale = transform.localScale;
        if (Horizonatl < 0) // pressed A/ right arrow 
        {
            if (IsGrounded() && Input.GetKeyDown(KeyCode.A))
            {
                SoundManager.Instance.MovementSound(SoundTypes.PlayerMovement, true);
                Debug.Log("Playing movemnt with a");
            }
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (Horizonatl > 0)  // pressed D/left arrow
        {
            if (Input.GetKeyDown(KeyCode.D) && IsGrounded())
            {
                SoundManager.Instance.MovementSound(SoundTypes.PlayerMovement, true);
                Debug.Log("Playing movemnt with d");
            }

            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

    }
    public void ThrowbackFromEnemycollision()
    {
        Vector3 pos = transform.position;
        pos.x -= 3f;
        transform.position = pos;
    }
    private void stoplooping()
    {
        if ( Input.GetKeyUp(KeyCode.D) || !IsGrounded() || Input.GetKeyUp(KeyCode.A))
        {
            SoundManager.Instance.MovementSound(SoundTypes.PlayerMovement, false);
           
        }
        
    }

    public void PlayerDied()
    {
        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead");
            //death anim
            UI_Manager.Instance.GameOver();
            SoundManager.Instance.LowVolSFXSounds(SoundTypes.GameOver);
        }
    }
}

        
