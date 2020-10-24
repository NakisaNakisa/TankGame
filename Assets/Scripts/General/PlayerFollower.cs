using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    const float lerpRate = 0.4f;
    PlayerTankController player = null;

    private void Start()
    {
        player = FindObjectOfType<PlayerTankController>();
    }

    private void Update()
    {
        transform.forward = Vector3.Lerp(transform.forward, player.transform.forward, lerpRate);
        transform.position = Vector3.Lerp(transform.position, player.transform.position, lerpRate);
    }

}
