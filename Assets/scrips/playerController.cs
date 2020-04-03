using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public GameObject winT;
    public useDice useDice;
    public GameObject startPoint;

    

    //public bool use;
    public bool cangop;
    public bool isFly;
    public bool canFly;
    //RED = 0,
    ///BLUE=1,
    //GREEN=2,
    //YELLOW=3
    public int color;
    public float speed;
    private float sp;
    public GameObject nowStand;
    //bool finished;
    public int canGo;
    public bool ifgo;

    public GameObject buttomDice;
    public bool buttomdown;
    private void Start()
    {
        Time.timeScale = 6;
        //use = false;
        cangop = false;
        //nowStand = startPoint;
        isFly = false;
         sp= speed;
        //finished = false;
        //StartCoroutine(Fly());
        canGo = 0;
        ifgo = false;
        canFly = true;
        buttomdown = false;
    }
    private void Update()
    {
        //speed -= Time.deltaTime;
        
        if(ifgo&&nowStand.GetComponent<strPointController>().color!=6)
        {
            isFly = true;
            StartCoroutine(Fly());
            ifgo = false;
            nowStand.GetComponent<strPointController>().onThis.Remove(transform.gameObject);
        }
        else if(ifgo)
        {
            isFly = true;
            StartCoroutine(FlyOut());
            ifgo = false;
            nowStand.GetComponent<strPointController>().onThis.Remove(transform.gameObject);
        }
        if (cangop)
        {
            if (isFly)
            {
                buttomDice.SetActive(false);
            }

            if (!canFly && !isFly)
            {
                buttomDice.SetActive(false);

            }
            if (!buttomdown && !isFly)
            {
                buttomDice.SetActive(true);
                
                    //buttomDice.GetComponent<useDice>().pl = null;
                    //buttomDice.GetComponent<useDice>().player = null;
                
                
            }
        }
        
        if (!isFly)
        {
            if (color==nowStand.GetComponent<strPointController>().color&& canFly)
            {
                StartCoroutine(FlyFar());
                isFly = false;
                canFly = false;
                nowStand.GetComponent<strPointController>().onThis.Remove(transform.gameObject);
            }
            else if (nowStand.GetComponent<strPointController>().color == 5)
            {
                //winT.SetActive(true);
                Destroy(transform.gameObject);
                //Time.timeScale = 0;
            }
        }
    }

    IEnumerator Fly()
    {
        GameObject target=null;
            float ti = 0.8f;
        if (nowStand.GetComponent<strPointController>().colorP != null&&color==nowStand.GetComponent<strPointController>().color)
        {
            target = nowStand.GetComponent<strPointController>().colorP;
        }
        else if (nowStand.GetComponent<strPointController>().nextP!=null)
        {
            target = nowStand.GetComponent<strPointController>().nextP;
        }
        else
        {
            yield return StartCoroutine(FlyBack());
        }
        if (target!=null)
        {
            
            while (ti >= 0)
            {
                ti -= Time.deltaTime;

                Vector3 dir = (target.transform.position - transform.position);//向目标行动 
                                                                               //transform.Translate(dir.normalized * Time.deltaTime * speed);
                dir.y = dir.y + 1;
                transform.position = Vector3.MoveTowards(transform.position, dir + transform.position, Time.deltaTime * 1.5f);
                Quaternion tagRot = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = tagRot;

                yield return new WaitForSeconds(0);
            }
            yield return new WaitForSeconds(0);
            nowStand = target;
            canGo--;
            if (canGo > 0)
            {
                yield return StartCoroutine(Fly());
            }
            else
            {
                isFly = false;
                nowStand.GetComponent<strPointController>().onThis.Add(transform.gameObject);
                //int i = 0;
                bool isdone = false;
                while (!isdone)
                {
                    for (int i = 0; i < nowStand.GetComponent<strPointController>().onThis.Count; i++)
                    {
                        if (nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().color != color)
                        {
                            useDice.kills[color]++;
                            nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().useDice.dies[nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().color]++;
                            nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().flyback();
                            break;
                        }
                        isdone = true;
                    }
                }
                
            }
        }
            
    }
    IEnumerator FlyFar()
    {
        
        buttomdown = true;
        if (nowStand.GetComponent<strPointController>().flyTo != null)
        {
            float ti = 1f;
            GameObject target = nowStand.GetComponent<strPointController>().flyTo;
            while (ti >= 0)
            {
                ti -= Time.deltaTime;

                Vector3 dir = (target.transform.position - transform.position);//向目标行动 
                                                                               //transform.Translate(dir.normalized * Time.deltaTime * speed);
                dir.y = dir.y + 1;
                transform.position = Vector3.MoveTowards(transform.position, dir + transform.position, Time.deltaTime * 6f);
                Quaternion tagRot = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = tagRot;

                yield return new WaitForSeconds(0);
            }
            yield return new WaitForSeconds(0);
            nowStand = target;
            nowStand.GetComponent<strPointController>().onThis.Add(transform.gameObject);
            bool isdone = false;
            while (!isdone)
            {
                for (int i = 0; i < nowStand.GetComponent<strPointController>().onThis.Count; i++)
                {
                    if (nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().color != color)
                    {
                        useDice.kills[color]++;
                        nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().useDice.dies[nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().color]++;
                        nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().flyback();
                        break;
                    }
                    isdone = true;
                }
            }
        }
        yield return new WaitForSeconds(0);
        buttomdown = false;
    }
    IEnumerator FlyBack()
    {
        GameObject target;
        float ti = 0.8f;
        
            target = nowStand.GetComponent<strPointController>().lastP;
        
        
        while (ti >= 0)
        {
            ti -= Time.deltaTime;

            Vector3 dir = (target.transform.position - transform.position);//向目标行动 
                                                                           //transform.Translate(dir.normalized * Time.deltaTime * speed);
            dir.y = dir.y + 1;
            transform.position = Vector3.MoveTowards(transform.position, dir + transform.position, Time.deltaTime * 1.5f);
            Quaternion tagRot = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = tagRot;

            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSeconds(0);
        nowStand = target;

        canGo--;
        if (canGo > 0)
        {
            yield return StartCoroutine(FlyBack());
        }
        else
        {
            isFly = false;
            nowStand.GetComponent<strPointController>().onThis.Add(transform.gameObject);
            bool isdone = false;
            while (!isdone)
            {
                for (int i = 0; i < nowStand.GetComponent<strPointController>().onThis.Count; i++)
                {
                    if (nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().color != color)
                    {
                        useDice.kills[color]++;
                        nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().useDice.dies[nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().color]++;
                        nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().flyback();
                        break;
                    }
                    isdone = true;
                }
            }
        }
    }
    IEnumerator FlyOut()
    {
        if (canGo>=4)
        {
            GameObject target = null;
            float ti = 0.8f;

            target = nowStand.GetComponent<strPointController>().nextP;

            if (target != null)
            {
                while (ti >= 0)
                {
                    ti -= Time.deltaTime;

                    Vector3 dir = (target.transform.position - transform.position);//向目标行动 
                                                                                   //transform.Translate(dir.normalized * Time.deltaTime * speed);
                    dir.y = dir.y + 1;
                    transform.position = Vector3.MoveTowards(transform.position, dir + transform.position, Time.deltaTime * 6f);
                    Quaternion tagRot = Quaternion.LookRotation(dir, Vector3.up);
                    transform.rotation = tagRot;

                    yield return new WaitForSeconds(0);
                }
                yield return new WaitForSeconds(0);
                nowStand = target;
                nowStand.GetComponent<strPointController>().onThis.Add(transform.gameObject);
                bool isdone = false;
                while (!isdone)
                {
                    for (int i = 0; i < nowStand.GetComponent<strPointController>().onThis.Count; i++)
                    {
                        if (nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().color != color)
                        {
                            useDice.kills[color]++;
                            nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().useDice.dies[nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().color]++;
                            nowStand.GetComponent<strPointController>().onThis[i].GetComponent<playerController>().flyback();
                            break;
                        }
                        isdone = true;
                    }
                }

                isFly = false;

            }
        }
        yield return new WaitForSeconds(0);
        isFly = false;
    }
    public void flyback()
    {
        StartCoroutine(GoBack());
    }
    IEnumerator GoBack()
    {
        
        GameObject target = null;
        float ti = 1f;
        //GameObject remove = this.gameObject;
        
            target = startPoint;
        nowStand.GetComponent<strPointController>().onThis.Remove(transform.gameObject);


        if (target != null )
        {
            while (ti >= 0)
            {
                ti -= Time.deltaTime;

                Vector3 dir = (target.transform.position - transform.position);//向目标行动 
                                                                                         //transform.Translate(dir.normalized * Time.deltaTime * speed);
                dir.y = dir.y+1;
                transform.position = Vector3.MoveTowards(transform.position, dir + transform.position, Time.deltaTime * 30f);
                Quaternion tagRot = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = tagRot;

                yield return new WaitForSeconds(0);
            }
            yield return new WaitForSeconds(0);
            nowStand = target;
            nowStand.GetComponent<strPointController>().onThis.Add(transform.gameObject);
            //onThis.Remove(remove);
            //isFly = false;

        }
        yield return new WaitForSeconds(0);
        
    }
}
