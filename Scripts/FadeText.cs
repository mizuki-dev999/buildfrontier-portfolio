using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeText : MonoBehaviour
{
    public float speed = 1.0f;

    private TextMeshProUGUI startText;
    private float time;
    void Start()
    {
        startText = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.gameObject.SetActive(false);
        }
        startText.color = GetAlphaColor(startText.color);
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 3.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
