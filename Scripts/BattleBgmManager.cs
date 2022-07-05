using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class BattleBgmManager : MonoBehaviour
{
    public float duration;
    public void StartBattleBgm(int mapId)
    {
        switch ((mapId-1)/3)
        {
            case 0:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE01, duration, duration, 0.7f, 0, 1);
                break;
            case 1:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE02, duration, duration, 0.7f, 0, 1);
                break;
            case 2:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE03, duration, duration, 0.7f, 0, 1);
                break;
            case 3:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE04, duration, duration, 0.7f, 0, 1);
                break;
            case 4:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE05, duration, duration, 0.7f, 0, 1);
                break;
            case 5:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE06, duration, duration, 0.7f, 0, 1);
                break;
            case 6:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE07, duration, duration, 0.7f, 0, 1);
                break;
            case 7:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE08, duration, duration, 0.7f, 0, 1);
                break;
            case 8:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE09, duration, duration, 0.7f, 0, 1);
                break;
            case 9:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE10, duration, duration, 0.7f, 0, 1);
                break;
            case 10:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE10, duration, duration, 0.7f, 0, 1);
                break;
            case 11:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE12, duration, duration, 0.7f, 0, 1);
                break;
            case 12:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE13, duration, duration, 0.7f, 0, 1);
                break;
            case 13:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE14, duration, duration, 0.7f, 0, 1);
                break;
            default:
                break;
        }
    }
    public void StartBossBattleBgm(int mapId)
    {
        switch (mapId)
        {
            case 3:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS01, duration, duration, 0.7f, 0, 1);
                break;
            case 6:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS02, duration, duration, 0.7f, 0, 1);
                break;
            case 9:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS03, duration, duration, 0.7f, 0, 1);
                break;
            case 12:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS04, duration, duration, 0.7f, 0, 1);
                break;
            case 15:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS05, duration, duration, 0.7f, 0, 1);
                break;
            case 18:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS06, duration, duration, 0.7f, 0, 1);
                break;
            case 21:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS07, duration, duration, 0.7f, 0, 1);
                break;
            case 24:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS08, duration, duration, 0.7f, 0, 1);
                break;
            case 27:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS09, duration, duration, 0.7f, 0, 1);
                break;
            case 30:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS10, duration, duration, 0.7f, 0, 1);
                break;
            case 33:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS11, duration, duration, 0.7f, 0, 1);
                break;
            case 36:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS12, duration, duration, 0.7f, 0, 1);
                break;
            case 39:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS13, duration, duration, 0.7f, 0, 1);
                break;
            case 42:
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.STAGE_BOSS14, duration, duration, 0.7f, 0, 1);
                break;
            default:
                break;
        }
    }
}
