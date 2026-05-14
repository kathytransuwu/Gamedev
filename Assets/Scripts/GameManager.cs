using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Image fadeImage;
    private int EnemyCount;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyDied()
    {
        EnemyCount--;
        if (EnemyCount == 0)
        {
            StartCoroutine(WinSequence());
        }
    }

    IEnumerator WinSequence()
    {
        //Fade to white
        float fadeDuration = 1f;
        float elapsedTime = 0f;
        Color initialColor = fadeImage.color;
        Color targetColor = Color.white;

        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = targetColor;

        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
