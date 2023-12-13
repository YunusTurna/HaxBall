using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Connection;

public class PlayerController : NetworkBehaviour
{
    [Header("Player Movement")]
    public float speed = 5f;



    private GameObject ball;
    private Rigidbody2D rb;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        rb = GetComponent<Rigidbody2D>();
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            gameObject.GetComponent<PlayerController>().enabled = false;  
        }
    }

    private void Update()
    {
        Movement();
        ReadyForShoot();
        
        
    }
    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized;

        rb.velocity = movement * speed;

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

}
