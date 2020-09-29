using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AudioClip loadGameJiggle = null;
    [SerializeField] private float loadGameVolume = 0.8f;
    [SerializeField] private float loadGameDelay = 1.6f;

    private ScoreManager scoreManager = null;
    private HealthManager healthManager = null;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        healthManager = FindObjectOfType<HealthManager>();
        if (scoreManager == null)
        {
            Debug.LogError("No scoreManager found");
        }
        if (healthManager == null)
        {
            Debug.LogError("No healthManager found");
        }
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        if (loadGameJiggle)
        {
            AudioSource.PlayClipAtPoint(loadGameJiggle,
                Camera.main.transform.position,
                loadGameVolume);
        }
        scoreManager.ResetScore();
        healthManager.ResetHealth();
        StartCoroutine(WaitAndLoadScene(1, loadGameDelay));
    }

    public void LoadGameOver()
    {
        var waitTime = 1f;
        StartCoroutine(WaitAndLoadScene(2, waitTime));
        StartCoroutine(WaitAndDestroyMusic(waitTime));
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndLoadScene(int sceneNumber, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneNumber);
    }

    private IEnumerator WaitAndDestroyMusic(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        var bg = FindObjectOfType<MusicPlayer>();
        if (bg)
        {
            Destroy(bg.gameObject);
        }
    }

}
