using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class loadScene : MonoBehaviour
{
    public string sceneName; // Name of the scene to load
    [SerializeField] private float fadeDuration = 1.0f; // Duration of fade
    private Image fadePanel;
    private CanvasGroup canvasGroup;
    private Collider2D[] colliders;

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

        canvasGroup = fadeObj.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;
    }

    private void Start()
    {
        colliders = FindObjectsOfType<Collider2D>();
        StartCoroutine(FadeIn());
    }

    public void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(FadeOutAndLoad());
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
        canvasGroup.blocksRaycasts = false;
        foreach (var col in colliders) col.enabled = true;
    }

    private IEnumerator FadeOutAndLoad()
    {
        fadePanel.gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
        foreach (var col in colliders) col.enabled = false;

        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
        foreach (var col in colliders) col.enabled = true;
    }
}
