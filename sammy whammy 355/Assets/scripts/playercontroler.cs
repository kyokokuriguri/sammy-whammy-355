using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroler : MonoBehaviour
{
    Rigidbody axle;
    Camera PlayerCam;
    Vector2 camRotation;
    public float speed = 10f;
    public float jump = 5f;
    public float mouseSensitivity = 2.0f;
    [Header("user settings")]
    public bool sprintToggleOption = false; 
    public float Xsensitivity = 2.0f;
    public float Ysensitivity = 2.0f;
    public float camRotationLimit = 2.0f;
    public float sprintMultiplier = 2.5f;
        public bool sprintMode = false;
    //public float groundDetectionDistance = 1;
    // Start is called before the first frame update
    void Start()
    {
        axle = GetComponent<Rigidbody>();
        PlayerCam = transform.GetChild(0).GetComponent<Camera>();
        camRotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit);
        PlayerCam.transform.localRotation = Quaternion.AngleAxis(camRotation.y, Vector3.left);
        transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);

        Vector3 temp = axle.velocity;
        temp.x = Input.GetAxisRaw("Vertical") * speed;
        temp.z = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetKeyDown(KeyCode.Space) && Physics. Raycast(transform.position, -transform.up,1))//can use groundDetectDistance too
            temp.y = jump;
        axle.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);
        if(sprintToggleOption)
            {
            if (Input.GetKey(KeyCode.LeftShift))
                sprintMode = true;
            if (Input.GetKey(KeyCode.LeftShift))
                sprintMode = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && temp.x > 0)
        {
            sprintMode = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
            sprintMode = true;
        if(sprintMode)
                temp.x = Input.GetAxisRaw("Vertical") * speed;
        if (sprintMode)
                temp.x = Input.GetAxisRaw("Vertical") * speed;
        if (Input.GetKey(KeyCode.LeftShift))
            sprintMode = false;

    }
}
