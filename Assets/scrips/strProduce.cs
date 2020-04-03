using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strProduce : MonoBehaviour
{
    
    public GameObject pointPrefab;
    public enum Color
    {
        RED=0,
        BLUE,
        GREEN,
        YELLOW
    };
    public Color startColor;
    public Material[] usingColor;
    private GameObject[] points;
    private void Start()
    {
        points=new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            points[i] = Instantiate(pointPrefab, new Vector3(transform.position.x, transform.position.y , transform.position.z+ i), Quaternion.identity);
            points[i].GetComponent<Renderer>().material = usingColor[(int)startColor];
            points[i].GetComponent<strPointController>().color = (int)startColor;
            
            //if (startColor==Color.YELLOW)
            //{
             //   startColor -= 4;
            //}            
            //startColor++;
        }
        for (int i = 0; i < 4; i++)
        {
            points[i].GetComponent<strPointController>().nextP = points[i + 1];
            
        }
    }
}
