using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerShoot : NetworkBehaviour
{
    bool canShoot = false;
    Rigidbody2D rb;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            gameObject.GetComponent<PlayerShoot>().enabled = false;  
        }
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X));
    }
    void Shoot(Collision other)
    {
        if(canShoot)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce((other.gameObject.transform.position - transform.position) * 100f , ForceMode2D.Impulse);
        }
        else
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    }
    [ServerRpc (RequireOwnership = false)]
    void SetShootServerRpc(Collision other)
    {
        SetShootObserversRpc(other);
    }
    [ObserversRpc]
    void SetShootObserversRpc(Collision other)
    {
        Shoot(other);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Ball")
        {
            canShoot = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Ball")
        {
            canShoot = false;
        }
    }
}

