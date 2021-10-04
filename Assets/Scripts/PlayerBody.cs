using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour, IPlayerBodyCallback {

    private Rigidbody2D rb;
    private Animator anim;
    public GameObject die;
    private AudioSource src;
    private bool facingRight = true;
    private int playerLayer;

    //---------------Speed---------------
    public float speedInGround;
    public float speedInWater;
    private float speed;

    //------------Jump forces------------
    public float jumpForceAir;
    public float jumpForceWater;
    private float jumpForce;

    //---------------Melee---------------
    private Collider2D physicMelee;
    private bool attacking = false;
    private float attackTimer = 0f;
    private float attackCd = 0.33f;

    //---------------Shoot---------------
    public GameObject bullet;
    public Vector3 correction;
    private bool shooting = false;
    public float shootTimer = 0f;
    public static float shootCd = 3f;
    public float shootBlock;
    private float multiShootTime;
    public GameObject bulletImage;

    //--------------Under Water--------------
    public static float breatheTime = 30f;
    public float breathe;

    //--------------Special--------------
    public float specialTimer;
    public static float specialSetTime = 10f;
    private Collider2D physicsSelf;
    private Collider2D physicsStomp;
    private Collider2D physicsSpin;

    private string flyAnimation;

    //--------------Hurt--------------
    public int hP;
    private SpriteRenderer cNormal;
    public float hurtTime;
    private bool hurtState = false;

    //--------Invencibility-----------
    public float invincibilitySetTime;
    private float invincibilityTimer;
    private GameObject shield;

    public bool winState;

    void Start ()
    {
        if (Game.diffLevel == 2)
        {
            hP = 1;
        }
        else
        {
            hP = 3;
        }

        playerLayer = gameObject.layer;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        src = GetComponent<AudioSource>();
        cNormal = GetComponent<SpriteRenderer>();

        physicsSelf = GetComponent<Collider2D>();
        physicMelee = transform.GetChild(0).GetComponent<Collider2D>();
        physicsStomp = transform.GetChild(1).GetComponent<Collider2D>();

        shield = transform.GetChild(2).gameObject;
        shield.SetActive(false);

        if (playerLayer == 8)
            physicsSpin = transform.GetChild(3).GetComponent<Collider2D>();

        specialTimer = 0f;
        breathe = breatheTime;
        speed = speedInGround;
        jumpForce = jumpForceAir;

        flyAnimation = "Fly";

        winState = false;
        multiShootTime = 0f;

        bulletImage.SetActive(false);

        invincibilityTimer = 0f;
    }

    void Update()
    {
        //Timer de ataque
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            attacking = false;
            physicMelee.enabled = false;
            anim.SetBool("attacking", false);
        }

        if(multiShootTime > 0)
        {
            multiShootTime -= Time.deltaTime;
        }
        else
        {
            bulletImage.SetActive(false);
        }

        //1 = Cooldown de disparo. 2 = Timer de detencion
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        //Si pasaron _shootBlock_ segundos desde que se disparo la bala, habilitar devuelta el disparo
        if ((multiShootTime <= 0 && (shootTimer < (shootCd - shootBlock))) || (multiShootTime > 0 && shootTimer < 0))
        {
            shooting = false;

            if(playerLayer == 8)
                anim.SetBool("shooting", false);
        }

        if(specialTimer > 0)
        {
            specialTimer -= Time.deltaTime;
        }

        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else
        {
            shield.SetActive(false);
        }
    }

    /*===================================================== MAIN FUNCTIONS ================================================*/

    /*---------------------------------Move---------------------------------*/
    public void Move(float hAxis)
    {
        Vector2 moveSpeed;

        moveSpeed.x = hAxis * speed;
        moveSpeed.y = rb.velocity.y;

        //Si se esta atacando o disparando en tierra, se detiene. De lo contrario, seguira moviendose
        if(rb.velocity.y == 0 && (attacking || shooting))
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = moveSpeed;

        //Flipea personaje si va hacia un lado o al otro
        if (moveSpeed.x > 0 && !facingRight)
            FlipCharacterX();
        else if (moveSpeed.x < 0 && facingRight)
            FlipCharacterX();

        //Si no esta atacando o disparando en tierra, reproduce la animacion Idle
        if (moveSpeed.y == 0 && !attacking && !shooting)
        {
            anim.Play("Ground");

            if (playerLayer == 8)
                SpinJump(false);
            else if (playerLayer == 9)
                FlyJump(false);

            if (!physicsStomp.enabled)
                physicsStomp.enabled = true;
        }

        //Pasa valores de velocidad horizontal y vertical. Para animacion de caminata y salto
        anim.SetFloat("velocityX", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetFloat("velocityY", GetComponent<Rigidbody2D>().velocity.y);
    }

    /*---------------------------------Jump Player 1---------------------------------*/
    public void Jump1()
    {
        //Se bloquea el salto si se esta atacando o disparando
        if (rb.velocity.y == 0 && !(attacking || shooting))
        {
            Impulse(jumpForce);

            if (specialTimer <= 0)
            {
                anim.Play("Jump");
                src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(0));
            }
            else
            {
                anim.Play("Spin Jump");
                SpinJump(true);
                src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(0));
            }
            
        }
    }

    public void Jump2()
    {
        if(rb.velocity.y == 0 && !(attacking || shooting))
        {
            Impulse(jumpForce);

            anim.Play("Jump");
            src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(0));
        }
        else if(specialTimer > 0 && rb.velocity.y != 0 && !(attacking || shooting))
        {
            Impulse(5);
            FlyJump(true);
        }
    }

    /*--------------------------------Attack--------------------------------*/
    public void Attack()
    {
        if (!shooting && !attacking)
        {
            //Estado de animacion de ataque
            anim.SetBool("attacking", true);

            //Si es el jugador 1 0 2. El jugador 2 no tiene animacion de ataque aereo
            if (playerLayer == 8)
            {
                //Si esta en tierra, usa una animacion. Si esta en aire usa otra
                if (rb.velocity.y == 0)
                {
                    anim.Play("Attack");
                    src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(16));
                }
                else
                {
                    anim.Play("Air Attack");
                    src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(17));
                }
            }
            else if(playerLayer == 9)
            {
                anim.Play("Attack");
                src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(20));
            }

            //Activa Timer de ataque
            attacking = true;
            attackTimer = attackCd;
            physicMelee.enabled = true;
        }
    }

    /*--------------------------------Shoot--------------------------------*/
    public void Shoot()
    {
        if (Game.diffLevel < 1)
        {
            if (shootTimer <= 0 && !attacking)
            {
                //Habria que poner aca un seleccionador de jugador. Jugador 2 no tiene una segunda animacion de disparo o de ataque aereo
                if (playerLayer == 8)
                {
                    anim.SetBool("shooting", true);
                    if (rb.velocity.y == 0)
                    {
                        anim.Play("Ground Shoot");
                    }
                    if (rb.velocity.y != 0)
                    {
                        anim.SetBool("attacking", true);
                        anim.Play("Air Attack");
                        src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(17));
                    }
                }
                else if (playerLayer == 9)
                {
                    anim.SetBool("attacking", true);
                    anim.Play("Attack");
                    attacking = true;
                    attackTimer = attackCd;
                }
                //===============================

                //Activa timer de disparo
                shooting = true;

                if (multiShootTime <= 0)
                    shootTimer = shootCd;
                else
                    shootTimer = 0.5f;

                //Instancia la bala
                GameObject playerBullet = GameObject.Instantiate(bullet);
                playerBullet.transform.position = transform.position + correction;
                playerBullet.transform.right = transform.right;

                src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(playerLayer + 1));
            }
        }
    }

    /*--------------------------Damage---------------------------*/
    public void Damage(bool takeHp)
    {
        if (physicsSelf.enabled || physicsSpin.enabled)
        {
            if (takeHp)
            {
                hurtState = true;
                hP--;
            }

            Impulse(jumpForce);

            if (hP > 0)
            {
                FlyJump(false);
                physicsStomp.enabled = false;
                anim.Play("Hit");

                if(takeHp)
                    src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(4));
                else
                    src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(21));

                StartCoroutine(HitBlink(15));
            }
            else
            {
                Die();
            }
        }
    }

    //===================================================== OTHER FUNCTIONS ==================================================

    //--------------------------Facing Character--------------------------
    public void FlipCharacterX()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        facingRight = !facingRight;
    }

    //--------------------------Impulse-----------------------------------
    public void Impulse(float jumpImpulse)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
    }

    //--------------------------Water Settings----------------------------
    public void WaterSettings(bool isInWater)
    {
        if (isInWater)
        {
            rb.drag = 6;
            speed = speedInWater;
            jumpForce = jumpForceWater;
            breathe -= Time.deltaTime;
            flyAnimation = "Swim";

            if(breathe <= 0)
                Die();
        }
        else
        {
            rb.drag = 0;
            speed = speedInGround;
            jumpForce = jumpForceAir;
            breathe = breatheTime;
            flyAnimation = "Fly";
        }
    }

    //--------------------------Spin Jump---------------------------------
    public void SpinJump(bool isEnabled)
    {
        if (isEnabled)
        {
            physicsStomp.enabled = false;
            physicsSelf.enabled = false;
            physicsSpin.enabled = true;
        }
        else
        {
            physicsStomp.enabled = true;
            physicsSelf.enabled = true;
            physicsSpin.enabled = false;
        }
    }

    //--------------------------Fly Jump---------------------------------
    public void FlyJump(bool isEnabled)
    {
        if(isEnabled)
        {
            rb.gravityScale = 0.75f;
            anim.SetBool("flying", true);
            anim.Play(flyAnimation);
        }
        else
        {
            rb.gravityScale = 4f;
            anim.SetBool("flying", false);
        }
    }

    //--------------------------Blink Coroutine---------------------------------
    private IEnumerator HitBlink(int numBlinks)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            if (i % 2 == 0)
                cNormal.color = new Color(1, 1, 1, 0);
            else
                cNormal.color = new Color(1, 1, 1, 1);

            yield return new WaitForSeconds(hurtTime / (numBlinks * 2f));
        }
        cNormal.color = new Color(1, 1, 1, 1);
        hurtState = false;
    }

    //--------------------------------Die---------------------------------------
    public void Die()
    {
        GameObject dieJump = GameObject.Instantiate(die);
        dieJump.transform.position = transform.position;
        gameObject.SetActive(false);
    }
    //===================================================== COLLISIONS =======================================================

    //Va OnCollisionStay para recibir daño inmediatamente despues de salir del estado lastimado (cuando parpadea) si esta tocando un enemigo.
    public void OnCollisionStay2D(Collision2D collision)
    {
        //Si colisiona con un enemigo
        if ((collision.gameObject.layer == 12 || collision.gameObject.layer == 15 || collision.gameObject.layer == 25) && invincibilityTimer <= 0)
        {
            if (!hurtState)
            {
                Damage(true);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 10)
        {
            Damage(false);
        }

        if (collision.gameObject.layer == 16)
        {
            winState = true;
        }

        if (collision.gameObject.tag == "Jump")
        {
            Destroy(collision.gameObject);
            Impulse(35);
            src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(5));
        }

        if (collision.gameObject.tag == "Health")
        {
            Destroy(collision.gameObject);
            hP++;
            src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(7));
        }

        if (collision.gameObject.tag == "Time")
        {
            Destroy(collision.gameObject);
            Game.currentTime = Game.currentTime + 20;
            src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(6));
        }

        if (collision.gameObject.tag == "Special")
        {
            Destroy(collision.gameObject);
            specialTimer = specialSetTime;
            src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(11));
        }

        if(collision.gameObject.tag == "Fast Shoots")
        {
            Destroy(collision.gameObject);
            shootTimer = 0f;
            multiShootTime = 10f;
            bulletImage.SetActive(true);
            src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(11));
        }

        if(collision.gameObject.tag == "Invincibility")
        {
            Destroy(collision.gameObject);
            invincibilityTimer = invincibilitySetTime;
            shield.SetActive(true);
            src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(22));
        }

        if (collision.gameObject.layer == 17)
        {
            Die();
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.gameObject.layer == 14 && invincibilityTimer <= 0)
        {
            if (!hurtState)
            {
                Damage(true);
            }
        }
        
        //Mientras este sumergido en agua
        if (collision.gameObject.layer == 4)
        {
            WaterSettings(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //Cuando sale del agua
        if (collision.gameObject.layer == 4)
        {
            WaterSettings(false);
        }
    }
}
