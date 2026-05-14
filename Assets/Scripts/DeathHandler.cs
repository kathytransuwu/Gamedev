using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class DeathHandler : MonoBehaviour
{
    public static DeathHandler Instance;
    public Image fadeImage; //Assign the full screen white UI image in the inspector

    private void Awake()
    {
        Instance = this;
    }

    public void Die()
    {
        StartCoroutine(DeathSequence());
    }

    

    private IEnumerator DeathSequence()
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

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
