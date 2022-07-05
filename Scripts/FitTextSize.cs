using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FitTextSize : MonoBehaviour
{
    public TextMeshProUGUI gearInfoText;
    public RectTransform gearBG;
    public void FitSize()
    {
        gearInfoText.GetComponent<ContentSizeFitter>().enabled = true;
        gearBG.sizeDelta = new Vector2(gearInfoText.preferredWidth + 40, gearInfoText.preferredHeight + 20);
    }

    public void FitOutSize()
    {
        gearInfoText.GetComponent<ContentSizeFitter>().enabled = false;
        gearBG.sizeDelta = new Vector2(100, 100);
        gearInfoText.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 100);
    }
}
