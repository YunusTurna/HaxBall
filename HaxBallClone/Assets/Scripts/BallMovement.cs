using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class BallMovement : NetworkBehaviour
{
    

    [ServerRpc(RequireOwnership = false)]
    void Update()
    {
        transform.position = transform.position;
    }
}
