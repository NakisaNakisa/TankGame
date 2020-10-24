using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTankController : TankController
{
    static PlayerTankController player = null;
    const float rotationInterval = 50;
    const float rotationChangeTimer = 1;
    const float pcMoveSpeed = 20;
    const float zInput = 1;
    const float shotDelay = 0.8f;
    const float shotRange = 110;
    float currentRotationTimer = 0;
    float currentShotDelay = 0;
    

    private void Start()
    {
        movementSpeed = pcMoveSpeed;
        currentRotationTimer = rotationChangeTimer;
        moveInput = Vector2.one;
        player = FindObjectOfType<PlayerTankController>();
    }

    protected override void Update()
    {
        if (!player)
            return;
        currentRotationTimer -= Time.deltaTime;
        if (currentRotationTimer <= 0)
            CalculateNewRotation();
        base.Update();
        CheckForPlayer();
    }

    void CalculateNewRotation()
    {
        rotationSpeed = Random.Range(-rotationInterval, rotationInterval);
    }

    void CheckForPlayer()
    {
        if(currentShotDelay > 0)
        {
            currentShotDelay -= Time.deltaTime;
            return;
        }
        RaycastHit _hit;
        Ray _ray = new Ray(transform.position + Vector3.up, transform.forward);
        if (Physics.Raycast(_ray, out _hit, shotRange) && _hit.transform == player.transform)
        {
            Shoot();
            currentShotDelay = shotDelay;
        }
    }

    private void OnDestroy()
    {
        FindObjectOfType<GameManager>()?.Victory();
    }
}
