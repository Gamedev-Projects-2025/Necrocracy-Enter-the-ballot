using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class loadScene : MonoBehaviour
{
    public string sceneName; // Name of the scene to load
    [SerializeField] private float fadeDuration = 0.5f; // Duration of fade
    private Image fadePanel;
    [SerializeField] private bool skip = false;
    private void Awake()
    {
        // Create the fade panel at runtime
        GameObject fadeObj = new GameObject("FadePanel");
        fadeObj.transform.SetParent(transform, false);

        Canvas canvas = fadeObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        fadePanel = fadeObj.AddComponent<Image>();
        fadePanel.color = new Color(0, 0, 0, 1);
        fadePanel.rectTransform.anchorMin = Vector2.zero;
        fadePanel.rectTransform.anchorMax = Vector2.one;
        fadePanel.rectTransform.sizeDelta = Vector2.zero;
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            if (skip)
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                StartCoroutine(FadeOutAndLoad());
            }
        }
    }

    private IEnumerator FadeIn()
    {
        float timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float alpha = timer / fadeDuration;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadePanel.color = new Color(0, 0, 0, 0);
        fadePanel.gameObject.SetActive(false);
    }

    private IEnumerator FadeOutAndLoad()
    {
        fadePanel.gameObject.SetActive(true);
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
}
