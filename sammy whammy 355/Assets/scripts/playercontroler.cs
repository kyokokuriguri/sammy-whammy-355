using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroler : MonoBehaviour
{
    Rigidbody axle;
    public float speed = 10f;
    publick float;jumpheight=5f;

    // Start is called before the first frame update
    void Start()
    {
        axle = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = axle.velocity;
        temp.x = Input.GetAxisRaw("vertical") * speed;
        temp.z = Input.GetAxisRaw("horazontal") * speed;
        axle.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (tmep .y+ransform.up);
        if (Input.GetKeyDown(KeyCode.Space))
            temp.y = jumpheight;
    }
}
