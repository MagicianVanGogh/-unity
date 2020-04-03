using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chessLeft : MonoBehaviour
{
    public List<GameObject> chess;
    public Click li;
    public Text whom;
    public int grade;
    private bool haveBeen;
    private bool gradeBeen;
    private void Start()
    {
        haveBeen = false;
        gradeBeen = false;
    }
    private void Update()
    {       
            checkNum();
            
        
    }
    public void checkNum()
    {
        for (int i = 0; i < chess.Count; i++)
        {
            if (chess[i]==null)
            {
                chess.Remove(chess[i]);
            }
        }
        if (chess.Count==0&&!gradeBeen)
        {
            grade = li.grade;
            li.grade++;
            gradeBeen = true;
        }
        if (chess.Count==0&&li.point==6&&!haveBeen)
        {                        
            li.num++;
            if (li.num == 0)
            {
                whom.text = "轮到红色";
            }
            else if (li.num == 1)
            {
                whom.text = "轮到蓝色";
            }
            else if (li.num == 2)
            {
                whom.text = "轮到绿色";
            }
            else if (li.num == 3)
            {
                whom.text = "轮到黄色";
            }
            li.num--;
            haveBeen = true;
        }
    }
}
