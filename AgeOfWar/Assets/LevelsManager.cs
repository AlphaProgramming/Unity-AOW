using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private int stars;
    public GameObject[] close;
    public GameObject[] open;
    public int[] levelsFinished = new int[] { level1, level2, level3, level4, level5, level6, level7, level8, level9, level10 };

    public static int level1, level2, level3, level4, level5, level6, level7, level8, level9, level10;

    // Start is called before the first frame update
    void Start()
    {
        stars = PlayerPrefs.GetInt("stars");
        level1 = PlayerPrefs.GetInt("level1");
        level2 = PlayerPrefs.GetInt("level2");
        level3 = PlayerPrefs.GetInt("level3");
        level4 = PlayerPrefs.GetInt("level4");
        level5 = PlayerPrefs.GetInt("level5");
        level6 = PlayerPrefs.GetInt("level6");
        level7 = PlayerPrefs.GetInt("level7");
        level8 = PlayerPrefs.GetInt("level8");
        level9 = PlayerPrefs.GetInt("level9");
        level10 = PlayerPrefs.GetInt("level10");

        foreach(int i in levelsFinished)
        {
            if(i == 1)
            {
                SetLevelOpen(i);
            }
        }

        if (level1 == 1)
        {
            SetLevelOpen(1);
        }
        if (level2 == 1)
        {
            SetLevelOpen(2);
        }
        if (level3 == 1)
        {
            SetLevelOpen(3);
        }
        if (level4 == 1)
        {
            SetLevelOpen(4);
        }
        if (level5 == 1)
        {
            SetLevelOpen(5);
        }
        if (level6 == 1)
        {
            SetLevelOpen(6);
        }
        if (level7 == 1)
        {
            SetLevelOpen(7);
        }
        if (level3 == 1)
        {
            SetLevelOpen(3);
        }
        if (level4 == 1)
        {
            SetLevelOpen(4);
        }
        if (level5 == 1)
        {
            SetLevelOpen(5);
        }
        if (level6 == 1)
        {
            SetLevelOpen(6);
        }
    }
    private void SetLevelOpen(int i)
    {
        close[i].SetActive(false);
        open[i].SetActive(true);
    }
}
