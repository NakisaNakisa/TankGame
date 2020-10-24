using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTankController : TankController
{
    public void OnPlayerMove(InputAction.CallbackContext _context)
    {
        moveInput = _context.ReadValue<Vector2>();
    }

    public void OnPlayerShot(InputAction.CallbackContext _context)
    {
        if(_context.performed)
            Shoot();
    }

    private void OnDestroy()
    {
        FindObjectOfType<GameManager>()?.GameOver();
    }
}
