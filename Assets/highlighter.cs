using UnityEngine;
using UnityEngine.UI;

public class highlighter : MonoBehaviour
{
    public Image redCircle;
    private float alphaValue;
    // Start is called before the first frame update
    void Start()
    {
        redCircle = GetComponent<Image>();
        Color curColor = redCircle.color;
        curColor.a = 0.5f;
        redCircle.color = curColor;
    }

    // Update is called once per frame
    void Update()
    {
        Color curColor = redCircle.color;
        alphaValue += Time.deltaTime / 2f;
        if (alphaValue > 1f)
        {
            alphaValue = 0.5f;
        }
        curColor.a = alphaValue;
        redCircle.color = curColor;
    }
}
