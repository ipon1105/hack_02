using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public double triger_enter_throttle;
    public double triger_enter_pitch;
    public double triger_enter_roll;
    public double triger_enter_yaw;

    public double a;
    public double b;
    public double c;
    public double d;

    public double triger_exit_throttle;
    public double triger_exit_pitch;
    public double triger_exit_roll;
    public double triger_exit_yaw;


    private void OnTriggerEnter(Collider collision)
    {

        Debug.Log(collision.name);
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().throttle = triger_enter_throttle;
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetPitch = triger_enter_pitch;
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetRoll = triger_enter_roll;
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetYaw = triger_enter_yaw;



        /*
        GameObject.Find("Motor1").GetComponent<motorScript>().power = 100;
        GameObject.Find("Motor2").GetComponent<motorScript>().power = 100;
        GameObject.Find("Motor3").GetComponent<motorScript>().power = 100;
        GameObject.Find("Motor4").GetComponent<motorScript>().power = 100;
        */
    }

    /*
    private void OnTriggerStay(Collider collision)
    {
        Debug.Log(collision.name);
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().throttle = a;
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetPitch = b;
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetRoll = c;
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetYaw = d;
    }
    */


    private void OnTriggerExit(Collider collision)
    {
        Debug.Log(collision.name);
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().throttle = triger_exit_throttle;
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetPitch = triger_exit_pitch;
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetRoll = triger_exit_roll;
        GameObject.Find("Quadrocopter").GetComponent<quadrocopterScript>().targetYaw = triger_exit_yaw;

    }

}