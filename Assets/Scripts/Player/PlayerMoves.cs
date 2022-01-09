using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    // moves
    public float moveSpeed;
    private Rigidbody2D rbr2d;
    private float moveVelocity = 0f;
    private Animator animator;
    public bool isFacingRight = true;

    //jump
    public LayerMask whatIsGround;
    private float groundRadius = 0.1f;
    public Transform groundCheck;
    public float jumpForce;
    public bool isGrounded;

    //Kneels
    private float verticalVelocity = 0f;

    //Bombs
    private BombsManager bombsScript;
    public GameObject bombs;

    //Ropes
    public GameObject ropes;
    public AudioClip ropesSound;
    public bool isRope = false;
    public float climbSpeed;

    //knockBack
    public float knockBack = 3f; // force x et  y  = 3
    public float knockBackCount = 0f;// = 0
    public float knockbackLenght = 0.25f;// temps 0.25s
    public bool knockFromRight;


    //Sounds
    public AudioClip jumpSound;
    public AudioClip attackSound;

    //Inventaire
    public GameObject pauseMenu;


    void Start()
    {
        rbr2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pauseMenu.SetActive(false);
        bombsScript = FindObjectOfType<BombsManager>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }

    // 60fps
    void Update()
    {

        //A enelever lors du build android : Application.platform == RuntimePlatform.WindowsEditor
        moveVelocity = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;

        if (moveVelocity > 0)
        {
            MovesRight();
        }
        else if (moveVelocity < 0)
        {
            MovesLeft();
        }
        else
        {
            rbr2d.velocity = new Vector2(rbr2d.velocity.x, rbr2d.velocity.y);
        }

        verticalVelocity = Input.GetAxisRaw("Vertical");
        //A Genoux
        if (verticalVelocity < 0)
        {
            animator.SetBool("Kneel", true);
        }
        else
        {
            animator.SetBool("Kneel", false);
        }




        //Le saut
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }


        //Attack
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Fire2") && bombsScript.nbrBombs > 0)
        {
            DropBombs();

        }

        //Les cordes clics sur la roulette de la souris (fire3)
        if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.R))
        {
            TriggerRopes();
        }

        //Conditions cordes
        //Si tu touche pas le sol + bool is rope est true (Trigger enter en bas de page) + tu appuie sur haut du joystick ou la touche Z
        if (!isGrounded && isRope && verticalVelocity > 0)
        {
            //Le float verticalVelocity init a 0 prend une valeur positive
            rbr2d.velocity = new Vector2(0, climbSpeed);
            //Trigger l'annimation bool Climb de animator
            animator.SetBool("Climb", true);
        }
        else
        {
            isRope = false;
            animator.SetBool("Climb", false);
        }


        //knockback droite et gauche
        if (knockBackCount <= 0)
        {
            rbr2d.velocity = new Vector2(rbr2d.velocity.x, rbr2d.velocity.y);
        }
        else
        {
            if (knockFromRight)
            {
                rbr2d.velocity = new Vector2(-knockBack, knockBack);
            }
            if (!knockFromRight)
            {
                rbr2d.velocity = new Vector2(knockBack, knockBack);
            }
            knockBackCount -= Time.deltaTime;
        }


        animator.SetFloat("Speed", Mathf.Abs(rbr2d.velocity.x));
    }

    public void MovesLeft()
    {
        rbr2d.velocity = new Vector2(-moveSpeed, rbr2d.velocity.y);
        isFacingRight = false;
        FlipSprites();
    }

    public void MovesRight()
    {
        rbr2d.velocity = new Vector2(moveSpeed, rbr2d.velocity.y);
        isFacingRight = true;
        FlipSprites();
    }


    //METHODES

    public void Jump()
    {
        rbr2d.velocity = new Vector2(rbr2d.velocity.x, jumpForce);
        if (jumpSound)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);
        Invoke("StopAttackAnim", 0.2f);
    }

    public void DropBombs()
    {
        if (isFacingRight)
        {

            Instantiate(bombs, new Vector2(transform.position.x + 1f, transform.position.y), Quaternion.identity);
            bombs.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            bombsScript.nbrBombs--;
        }

        if (!isFacingRight)
        {
            Instantiate(bombs, new Vector2(transform.position.x - 1f, transform.position.y), Quaternion.identity);
            bombs.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            bombsScript.nbrBombs--;
        }

    }

    void TriggerRopes()
    {
        Instantiate(ropes, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), Quaternion.identity);
        if (ropesSound)
        {
            AudioSource.PlayClipAtPoint(ropesSound, transform.position);
        }
    }

    void FlipSprites()
    {
        if (isFacingRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (!isFacingRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void StopAttackAnim()
    {
        animator.SetBool("Attack", false);
    }

    //Ouvrir inventaire
    public void OpenPausePanel()
    {
        pauseMenu.SetActive(true);
    }

    public void CloseInventory()
    {
        pauseMenu.SetActive(false);
    }

    //*****************************************************TRIGGER*********************************************************//
    //trigger ENTER : 
    void OnTriggerEnter2D(Collider2D cible)
    {
        if (cible.tag == "PlateForme")
        {
            this.transform.parent = cible.transform;
        }
        //Ropes
        if (cible.CompareTag("Ropes"))
        {
            isRope = true;
        }
    }

    //Trigger STAY : colle au plateforme qui bouge
    void OnTriggerStay2D(Collider2D cible)
    {
        if (cible.tag == "PlateForme")
        {
            this.transform.parent = cible.transform;
        }
    }
    //Trigger EXIT : decolle au plateforme qui bouge
    void OnTriggerExit2D(Collider2D cible)
    {
        if (cible.tag == "PlateForme")
        {
            this.transform.parent = null;
        }
        //Ropes
        if (cible.CompareTag("Ropes"))
        {
            isRope = false;
        }
    }


}
