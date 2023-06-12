using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject panel;
    public Image[] images;
    public AudioClip soundClip;
    private AudioSource audioSource;
    public GameObject text;

    public Text firstText;
    public Text secondText;
    public Text thirdText;
    public float fadeDuration = 1f;

    #region text with fade


    private IEnumerator ActivateTextWithFade(Text text, float delay)
    {
        yield return new WaitForSeconds(delay);

        Color originalColor = text.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / fadeDuration;
            text.color = Color.Lerp(originalColor, targetColor, normalizedTime);
            yield return null;
        }


    }
    private IEnumerator SwitchSceneAfterDelay(float delay)
    {
       
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("Scene0");
        
    }

    #endregion

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(ActivateTextWithFade(firstText, 3f));
            StartCoroutine(ActivateTextWithFade(secondText, 6f));
            StartCoroutine(ActivateTextWithFade(thirdText, 9f));
            StartCoroutine(SwitchSceneAfterDelay(15f));
            if (text.activeSelf)
            {
                audioSource.volume = 0.3f;
                audioSource.Play();
            }


            foreach (Image image in images)
            {
                image.gameObject.SetActive(false);
                text.gameObject.SetActive(false);
            }

        }

    }
}
