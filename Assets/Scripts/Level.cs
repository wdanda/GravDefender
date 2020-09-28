using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private AudioClip loadGameJiggle = null;
    [SerializeField] private float loadGameVolume = 0.8f;
    [SerializeField] private float loadGameDelay = 1.6f;

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
