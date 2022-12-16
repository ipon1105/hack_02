using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStop : MonoBehaviour
{
    private float CheckPeriod = 1f;
    private void OnTriggerEnter(Collider collision)
    {  
        //Debug.Log(collision.name);
        if(collision.name == "Station")
        {
            GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetPitch = -6;
            StartCoroutine(SetNull());
        }
           
    }
    IEnumerator SetNull()
    {
        yield return new WaitForSeconds(4);
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetPitch = 0;
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().throttle = 18;
    }
}
