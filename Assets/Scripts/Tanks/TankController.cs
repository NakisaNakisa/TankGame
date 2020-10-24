using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [Header("Tank")]
    [SerializeField]
    Transform shotSocket = null;
    [SerializeField]
    int nrOfShells = 5;
    [SerializeField]
    protected float movementSpeed = 50;
    [SerializeField]
    protected float rotationSpeed = 30;
    [SerializeField]
    GameObject shellTemplate = null;
    ComponentPool<Shell> shellPool = null;
    [SerializeField]
    AudioSource shotSound = null;
    Vector3 upToLerpTo = new Vector3();

    const float gravity = 20;
    const float height = 1.5f;
    const float shotUpAngle = 15;


    public Vector2 moveInput { get; protected set; } = new Vector2();

    private void Awake()
    {
        shellPool = new ComponentPool<Shell>(shellTemplate, nrOfShells, false, "ShellPool");
    }

    protected void Shoot()
    {
        Shell _shell = shellPool.GetComponent();
        if(_shell)
        {
            Vector3 _direction = Quaternion.AngleAxis(shotUpAngle, -transform.right) * transform.forward;
            _shell.Shoot(shotSocket.transform.position, _direction, gameObject);
            shotSound?.Play();
        }
    }

    protected virtual void Update()
    {
        Move();
        Rotate();
        InMapCheck();
        LerpUp();
    }

    void LerpUp()
    {
        Vector3 _newUp = Vector3.Lerp(transform.up, upToLerpTo, 0.3f);
        float _oldY = transform.rotation.eulerAngles.y;
        transform.up = _newUp;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _oldY, transform.rotation.eulerAngles.z);
    }


    void InMapCheck()
    {
        if (!MapGenerator.IsInMap(transform.position.x, transform.position.z))
        {
            transform.position = MapGenerator.GetRandomDropOffPoint();
        }
    }

    void Move()
    {
        GroundedCalculations();
        transform.position += transform.forward * moveInput.y * Time.deltaTime * movementSpeed;
    }

    void GroundedCalculations()
    {
        RaycastHit _hit;
        Ray _ray = new Ray(transform.position + transform.up * height, -transform.up);
        Vector3 _downVector = transform.up * Time.deltaTime * gravity;
        if (Physics.Raycast(_ray, out _hit, _downVector.magnitude + height))
        {
            upToLerpTo = _hit.normal;
            transform.position = _hit.point;
        }
        else
            transform.position -= _downVector;
    }

    void Rotate()
    {
        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime * moveInput.x);
    }
}
