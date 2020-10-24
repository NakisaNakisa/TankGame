using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    const float gravity = 9.81f;
    const float timeScale = 5;
    const float maxLifeTime = 5;
    float lifeTime = 0;
    float shotForcePerSecond = 30;
    float timeSinceShot = 0;
    Vector3 initialVelocity = new Vector3();
    Vector3 initialPosition = new Vector3();
    GameObject parent = null; 

    public void Shoot(Vector3 _position, Vector3 _direction, GameObject _parent)
    {
        timeSinceShot = 0;
        transform.position = _position;
        initialPosition = _position;
        initialVelocity = _direction * shotForcePerSecond;
        parent = _parent;
        lifeTime = maxLifeTime;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            gameObject.SetActive(false);
        timeSinceShot += Time.deltaTime;
        //* x(t) = x0 + 5 \*vx0 \*t
        float _x = initialPosition.x + timeScale * initialVelocity.x * timeSinceShot;
        //* y(t) = y0 + 5 \*vy0 \*t – (5 \*t ) \*(5 \*t ) \*g / 2
        float _y = initialPosition.y + timeScale * initialVelocity.y * timeSinceShot - (timeScale * timeSinceShot) * (timeScale * timeSinceShot) * gravity / 2;
        //* z(t) = z0 + 5 \*vz0 \*t
        float _z = initialPosition.z + timeScale * initialVelocity.z * timeSinceShot;
        Vector3 _nextPosition = new Vector3(_x, _y, _z);
        transform.rotation.SetLookRotation(_nextPosition - transform.position);
        transform.position = _nextPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || other.gameObject == parent)
            return;
        TankHealth _health = other.GetComponent<TankHealth>();
        _health?.Hit();
        gameObject.SetActive(false);
    }

}
