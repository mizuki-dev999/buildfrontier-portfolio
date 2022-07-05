using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;

public class TitleManager : MonoBehaviour
{
    private int clickCheck = 0;

    private void Start()
    {
        BGMManager.Instance.Play(BGMPath.TITLE02);
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && clickCheck == 0)
        {
            clickCheck++;
            SEManager.Instance.Play(SEPath.TITLE_TAP_SE);
            //BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME,1,1,0.7f,0,1);
            FadeManager.Instance.LoadScene("Home", 1.0f);
        }
    }
}
