using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalScreen : MonoBehaviour
{
    public Text firstText;
    public Text secondText;
    public Text thirdText;
    public Text creditsText;
    public Text titleText;
    public float fadeDuration = 1f;

    private IEnumerator ActivateTextWithFade(Text text, float delay)
    {

        yield return new WaitForSeconds(delay);
        if (delay == 10f)
        {
            firstText.gameObject.SetActive(false);
            secondText.gameObject.SetActive(false);
            thirdText.gameObject.SetActive(false);
        }
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

    void Start()
    {
        StartCoroutine(ActivateTextWithFade(firstText, 1f));
        StartCoroutine(ActivateTextWithFade(secondText, 4f));
        StartCoroutine(ActivateTextWithFade(thirdText, 7f));
        StartCoroutine(ActivateTextWithFade(titleText, 10f));
        StartCoroutine(ActivateTextWithFade(creditsText, 11f));

    }
}
