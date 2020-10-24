using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;
    int currentHealth = 0;
    [SerializeField]
    AudioSource hitSound = null;
    [SerializeField]
    AudioSource destroySound = null;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Hit()
    {
        if (currentHealth < 1)
            return;
        --currentHealth;
        if (currentHealth > 0)
            hitSound.Play();
        else
            Destroy();
    }

    void Destroy()
    {
        destroySound.Play();
        StartCoroutine(C_DestroyAfterExposionSound());
    }

    IEnumerator C_DestroyAfterExposionSound()
    {
        Destroy(GetComponent<TankController>());
        yield return new WaitForSeconds(destroySound.clip.length);
        Destroy(gameObject);
    }
}
