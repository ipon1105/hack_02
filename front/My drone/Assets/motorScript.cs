using UnityEngine;
using System.Collections;

public class motorScript : MonoBehaviour
{

    public float power = 0.0f;

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddRelativeForce(0, power, 0);
    }
}