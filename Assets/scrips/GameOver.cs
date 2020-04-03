using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public List<chessLeft> chess;
    public GameObject backGround;
    private bool isdone;
    public List<Text> num;
    private void Start()
    {
        isdone = false;
    }
    private void Update()
    {
        if ( !isdone&&chess[0].chess.Count == 0 && chess[1].chess.Count == 0 && chess[2].chess.Count == 0 && chess[3].chess.Count == 0)
        {
            Over();
            isdone = true;
        }
    }
    public void Over()
    {
        backGround.SetActive(true);
        num[chess[0].grade - 1].text = "红色";
        num[chess[1].grade - 1].text = "蓝色";
        num[chess[2].grade - 1].text = "绿色";
        num[chess[3].grade - 1].text = "黄色";
        Time.timeScale = 0;
    }
}
