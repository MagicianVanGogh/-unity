using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useDice : MonoBehaviour
{
    //public playerController player;
    public Click nums;
    public Text num;
    public Text whom;
    public int[] steps,kills,dies;
    public List<Text> step;
    public List<Text> kill;
    public List<Text> die;
    private void Start()
    {
        steps = new int[4];
        kills = new int[4];
        dies = new int[4];
        for (int i = 0; i < 4; i++)
        {
            steps[i] = 0;
            kills[i] = 0;
            dies[i] = 0;
            step[i].text = steps[i].ToString("0");
        }

        whom.text = "轮到绿色";
    }
    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            kill[i].text = kills[i].ToString("0");
            die[i].text = dies[i].ToString("0");
        }
    }
    //public Click camera;
    public GameObject pl;
    public void usD()
    {
        if (!nums.isDice)
        {
            int point = Random.Range(1, 7);
            num.text = point.ToString("0");
            nums.point = point;
            nums.isDice = true;
            steps[nums.num] += point;
            step[nums.num].text = steps[nums.num].ToString("0");
        }

        /*//nums.clickOn();
        if (player!=null&&pl!=null)
        {
            player.cangop = false;
        }
        //pl = camera.clickOn();
        if (pl!=null )
        {
            player = pl.GetComponent<playerController>();
            int point = Random.Range(1, 7);
            player.canGo = point;
            if (point==6)
            {
                nums.num += 1;
            }
            player.ifgo = true;
            num.text = point.ToString("0");
            player.canFly = true;
            player.cangop = true;
        }
        pl = null;
        nums.num = nums.num % 2;
        
        if (nums.num==0)
        {
            whom.text = "轮到绿色";
        }
        else
        {
            whom.text = "轮到红色";
        }*/
        nums.clickOn();
    }
}
