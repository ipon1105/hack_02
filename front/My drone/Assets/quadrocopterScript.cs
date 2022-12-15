using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class quadrocopterScript : MonoBehaviour
{

    //����������� ���������
    private double pitch; //������
    private double roll; //����
    private double yaw; //��������
    public double throttle; //���, ��� �� ������ �����, ������� �� public

    //��������� ���������
    public double targetPitch;
    public double targetRoll;
    public double targetYaw;

    //PID ����������, ������� ����� ��������������� ����
    //������� ���� ���� ���������, ����� PID ��������� ����
    //��������� ��������� �� ���� :) �������� ���� ��������
    private PID pitchPID = new PID(100, 0, 20);
    private PID rollPID = new PID(100, 0, 20);
    private PID yawPID = new PID(50, 0, 50);

    void readRotation()
    {

        //����������� ���������� ������ �������������,
        //� �������� ������������� ��� ������ ���������� ��������
        //�� �������������-���������-������������, ��� �� ��� ������ ��� ���
        //��������
        Vector3 rot = GameObject.Find("Frame").GetComponent<Transform>().rotation.eulerAngles;
        pitch = rot.x;
        yaw = rot.y;
        roll = rot.z;

    }

    //������� ������������ �������������
    //� ������� PID ����������� �� �����������
    //�������� ����� ������� ���, ����� ���� ������� ������ ��� ��������
    void stabilize()
    {

        //��� ���������� ��������� �������� ����� ��������� ����� � �������
        //��� �������� ������ ������ � ���������� [-180, 180] ����� ����������
        //���������� ������ PID �����������, ��� ��� ��� ������ ������������ �� 350
        //��������, ����� ����� ��������� �� -10

        double dPitch = targetPitch - pitch;
        double dRoll = targetRoll - roll;
        double dYaw = targetYaw - yaw;

        dPitch -= Math.Ceiling(Math.Floor(dPitch / 180.0) / 2.0) * 360.0;
        dRoll -= Math.Ceiling(Math.Floor(dRoll / 180.0) / 2.0) * 360.0;
        dYaw -= Math.Ceiling(Math.Floor(dYaw / 180.0) / 2.0) * 360.0;

        //1 � 2 ����� �������
        //3 � 4 ������ �����
        double motor1power = throttle;
        double motor2power = throttle;
        double motor3power = throttle;
        double motor4power = throttle;

        //������������ �� �������� ���������� �� ������
        double powerLimit = throttle > 20 ? 20 : throttle;

        //���������� ��������:
        //�� �������� ��������� ������ ���������� �� ����������
        //�� ������ ��������������� ����������
        double pitchForce = -pitchPID.calc(0, dPitch / 180.0);
        pitchForce = pitchForce > powerLimit ? powerLimit : pitchForce;
        pitchForce = pitchForce < -powerLimit ? -powerLimit : pitchForce;
        motor1power += pitchForce;
        motor2power += pitchForce;
        motor3power += -pitchForce;
        motor4power += -pitchForce;

        //���������� ������:
        //��������� �� �������� � ��������, ������ ���������� ������� ���������
        double rollForce = -rollPID.calc(0, dRoll / 180.0);
        rollForce = rollForce > powerLimit ? powerLimit : rollForce;
        rollForce = rollForce < -powerLimit ? -powerLimit : rollForce;
        motor1power += rollForce;
        motor2power += -rollForce;
        motor3power += -rollForce;
        motor4power += rollForce;

        //���������� ���������:
        double yawForce = yawPID.calc(0, dYaw / 180.0);
        yawForce = yawForce > powerLimit ? powerLimit : yawForce;
        yawForce = yawForce < -powerLimit ? -powerLimit : yawForce;
        motor1power += yawForce;
        motor2power += -yawForce;
        motor3power += yawForce;
        motor4power += -yawForce;

        GameObject.Find("Motor1").GetComponent<motorScript>().power = (float)motor1power;
        GameObject.Find("Motor2").GetComponent<motorScript>().power = (float)motor2power;
        GameObject.Find("Motor3").GetComponent<motorScript>().power = (float)motor3power;
        GameObject.Find("Motor4").GetComponent<motorScript>().power = (float)motor4power;
    }

    //��� �������� � ���� �� Unity ���������� �������� � FixedUpdate, � �� � Update
    void FixedUpdate()
    {
        readRotation();
        stabilize();
    }

}

public class PID
{

    private double P;
    private double I;
    private double D;

    private double prevErr;
    private double sumErr;

    public PID(double P, double I, double D)
    {
        this.P = P;
        this.I = I;
        this.D = D;
    }

    public double calc(double current, double target)
    {

        double dt = Time.fixedDeltaTime;

        double err = target - current;
        this.sumErr += err;

        double force = this.P * err + this.I * this.sumErr * dt + this.D * (err - this.prevErr) / dt;

        this.prevErr = err;
        return force;
    }

};