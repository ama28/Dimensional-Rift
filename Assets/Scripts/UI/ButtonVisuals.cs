using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonVisuals : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [System.Serializable]
    struct FontInfo {
        public TMP_FontAsset font;
        public float size_percent;
    }
    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color onPressColor;
    [SerializeField] private List<FontInfo> fonts;
    [SerializeField] private List<Color> colors;
    private Image image;
    private float fontSize;
    private Color originalColor;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        fontSize = text.fontSize;
        originalColor = text.color;
    }

    public void OnPointerClick(PointerEventData data) 
    {
        StartCoroutine(ColorFade(onPressColor, 0.1f));
    }

    public void OnPointerEnter(PointerEventData data) 
    {
        StopAllCoroutines();
        StartCoroutine(FlickerText(0.6f));
    }

    IEnumerator FlickerText(float duration)
    {
        float timeElapsed = 0;
        FontInfo fontInfo;
        text.enableAutoSizing = false;

        float delayTime = 0.005f;
        float delayIncrease = 0.008f;

        while(timeElapsed < duration) {
            fontInfo = fonts[Random.Range(0, fonts.Count)];
            text.font = fontInfo.font;
            text.fontSize = fontSize * fontInfo.size_percent;
            text.color = colors[Random.Range(0, colors.Count)];
            delayTime += delayIncrease;
            timeElapsed += delayTime;
            yield return new WaitForSeconds(delayTime);
        }
        fontInfo = fonts[0];
        text.font = fontInfo.font;
        text.fontSize = fontSize;
        text.color = originalColor;
        text.enableAutoSizing = true;
    }

    IEnumerator ColorFade(Color color, float duration) {
        // float elapsed = 0;
        image.color = color;
        yield return new WaitForSeconds(duration);

        // while(elapsed < duration) {
        //     elapsed += Time.deltaTime;
        //     yield return null;
        // }
        image.color = Color.white;
    }

}
