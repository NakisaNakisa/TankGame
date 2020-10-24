using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerTemplate = null;
    [SerializeField]
    GameObject enemyTemplate = null;
    [SerializeField]
    int playerAmount = 1;
    [SerializeField]
    int enemyAmount = 4;
    [SerializeField]
    AudioSource victorySound = null;
    [SerializeField]
    AudioSource gameOverSound = null;
    [SerializeField]
    AudioClip explosionClip = null;

    private void Awake()
    {
        for (int i = 0; i < enemyAmount; ++i)
        {
            Instantiate(enemyTemplate).transform.position = -Vector3.one;
        }
        for (int i = 0; i < playerAmount; ++i)
        {
            Instantiate(playerTemplate).transform.position = -Vector3.one;
        }
    }

    public void GameOver()
    {
        StartCoroutine(C_GameOver());
    }

    public void Victory()
    {
        --enemyAmount;
        if (enemyAmount < 1)
            StartCoroutine(C_Victory());
    }

    IEnumerator C_Victory()
    {
        yield return new WaitForSeconds(explosionClip.length);
        victorySound?.Play();
        yield return new WaitForSeconds(victorySound.clip.length);
        SceneManager.LoadScene(0);
    }

    IEnumerator C_GameOver()
    {
        yield return new WaitForSeconds(explosionClip.length);
        gameOverSound?.Play();
        yield return new WaitForSeconds(gameOverSound.clip.length);
        SceneManager.LoadScene(0);
    }
}
