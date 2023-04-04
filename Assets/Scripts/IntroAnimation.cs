using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroAnimation : MonoBehaviour
{
    public Image[] introImages;
    public Text[] introTexts;
    public float animationDuration = 10f;
    public float textDuration = 2f;
    public float imageDuration = 2f;

    private bool skipIntro = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipIntro = true;
        }
    }

    IEnumerator Start()
    {
        for (int i = 0; i < introImages.Length; i++)
        {
            Image introImage = introImages[i];
            Text introText = introTexts[i];

            introImage.gameObject.SetActive(true);
            introText.gameObject.SetActive(true);

            // Fade in text
            Color textColor = introText.color;
            float textTimer = 0f;
            while (textTimer < textDuration)
            {
                introText.color = Color.Lerp(Color.clear, textColor, textTimer / textDuration);

                introText.rectTransform.anchoredPosition += Vector2.up * (Time.deltaTime / textDuration) * introText.rectTransform.rect.height;

                textTimer += Time.deltaTime;

                if (skipIntro)
                {
                    introImage.color = Color.clear;
                    introText.color = Color.clear;
                    break;
                }

                yield return null;
            }

            // Wait for imageDuration seconds
            float imageTimer = 0f;
            while (imageTimer < imageDuration)
            {
                imageTimer += Time.deltaTime;

                if (skipIntro)
                {
                    introImage.color = Color.clear;
                    introText.color = Color.clear;
                    break;
                }

                yield return null;
            }

            // Fade out text and image
            Color imageColor = introImage.color;
            float timer = 0f;
            while (timer < animationDuration)
            {
                introImage.color = Color.Lerp(imageColor, Color.clear, timer / animationDuration);
                introText.color = Color.Lerp(textColor, Color.clear, timer / animationDuration);

                introText.rectTransform.anchoredPosition += Vector2.up * (Time.deltaTime / animationDuration) * introText.rectTransform.rect.height;

                timer += Time.deltaTime;

                if (skipIntro)
                {
                    introImage.color = Color.clear;
                    introText.color = Color.clear;
                    break;
                }

                yield return null;
            }

            introImage.gameObject.SetActive(false);
            introText.gameObject.SetActive(false);
        }

        SceneManager.LoadScene("InitialMenu");
    }
}
