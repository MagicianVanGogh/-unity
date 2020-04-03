using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public playerController player;
    public GameOver gameover;
    public List<chessLeft> chesses;
    public Camera camera;
    public int point;
    public Text numed;
    public Click nums;
    public useDice use;
    public int grade;
    //RED = 0,
    ///BLUE=1,
    //GREEN=2,
    //YELLOW=3
    public int num;
    private string[] chess;
    public Text whom;
    public GameObject pl;
    public bool isDice;
    private void Start()
    {
        grade = 1;
        isDice = false;
        chess = new string[4];
        chess[0] = "chessR";
        chess[1] = "chessB";
        chess[2] = "chessG";
        chess[3] = "chessY";
        num = 0;
        whom.text = "轮到红色";
    }
    public void clickOn()
    {
        if (isDice)
        {                
            StartCoroutine(ClickChess());
            isDice = true;
        }
        
         
    }
    IEnumerator ClickChess()
    {
        if (chesses[num].chess.Count ==0)
        {
            num++;
        }
        while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    num = num % 4;
                    bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask(chess[num]));
                    if (isCollider)
                    {
                        Debug.DrawLine(ray.origin, hit.point);
                        pl = hit.collider.gameObject;

                        num++;
                        break;
                    }
                }
                yield return new WaitForSeconds(0);
            }
            yield return new WaitForSeconds(0);
            if (player != null && pl != null)
            {
                player.cangop = false;
            }
            if (pl != null)
            {
                player = pl.GetComponent<playerController>();
                //point = Random.Range(1, 7);
                player.canGo = point;
                if (point == 6)
                {
                    nums.num = nums.num % 4;
                    
                        nums.num += 3;
                    

                }
                player.ifgo = true;
                numed.text = point.ToString("0");
                player.canFly = true;
                player.cangop = true;
            }
            pl = null;
            nums.num = nums.num % 4;
        while (true)
        {
            num = num % 4;
            chesses[num].checkNum();
            if (chesses[num].chess.Count > 0)
            {
                if (chesses[num].grade == 0 && chesses[num].chess.Count == 0)
                {
                    chesses[num].grade = grade;
                    grade++;
                }
                /*if (chesses[0].chess.Count == 0 && chesses[1].chess.Count == 0 && chesses[2].chess.Count == 0 && chesses[3].chess.Count == 0)
                {
                    gameover.Over();
                }*/
                break;
            }
            else
            {
                num++;
                num = num % 4;
            }

        }
        /*if (chesses[num].chess.Count == 0)
        {
            num++;
        }*/
        if (nums.num == 0)
            {
                whom.text = "轮到红色";
            }
            else if (nums.num == 1)
            {
                whom.text = "轮到蓝色";
            }
            else if (nums.num == 2)
            {
                whom.text = "轮到绿色";
            }
            else if (nums.num == 3)
            {
                whom.text = "轮到黄色";
            }
        isDice = false;
        

        /*if (chesses[0].chess.Count == 0 && chesses[1].chess.Count == 0 && chesses[2].chess.Count == 0 && chesses[3].chess.Count == 0)
        {
            gameover.Over();
        }*/
        //break;

    }
}
