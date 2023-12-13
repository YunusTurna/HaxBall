using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Header("Player Movement")]
    public float speed = 5f;

    [Header("Player Shooting")]
    public float shootPower = 5f;
    public bool canShoot = false;


    //////////////////////////

    private GameObject ball;
    private Rigidbody2D rb;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
      if (!IsOwner)  return; 
        Movement();
        ReadyForShoot();
        Shoot();
        
    }
    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized;

        rb.velocity = movement * speed;

    }
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.X) && canShoot)
        {
            ball.GetComponent<Rigidbody2D>().AddForce((ball.transform.position - this.gameObject.transform.position) * shootPower , ForceMode2D.Impulse);
        }

    }
    void ReadyForShoot()
    {
        if (Input.GetKey(KeyCode.X))
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            canShoot = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            canShoot = false;
        }
    }

}
