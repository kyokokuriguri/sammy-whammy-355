using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody axle;
    Camera playerCam;

    Vector2 camRotation;

    public bool sprintMode = false;

    [Header("wepon stats")]
    public bool canFire = true;
    public Transform weponSlot;
    public int weponID = 1;
    public float fireRate = 0;
    public float maxAmmo = 0;
    public float currentAmmo = 0;
    public int Firemode = 1;
    public float reloadAmount = 0;
    public float clipSize = 0;
    public float currentClip = 0;



    [Header("player stats")]
    public int maxHealth = 5;
    public int health = 5;
    public int healthRestore = 1;
    [Header("Movement Settings")]
    public float speed = 10.0f;
    public float sprintMultiplier = 2.5f;
    public float jumpHeight = 5.0f;
    public float groundDetectDistance = 1f;

    [Header("User Settings")]
    public bool sprintToggleOption = false;
    public float mouseSensitivity = 2.0f;
    public float Xsensitivity = 2.0f;
    public float Ysensitivity = 2.0f;
    public float camRotationLimit = 90f;

    // Start is called before the first frame update
    void Start()
    {
        axle = GetComponent<Rigidbody>();
        playerCam = transform.GetChild(0).GetComponent<Camera>();

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

        playerCam.transform.localRotation = Quaternion.AngleAxis(camRotation.y, Vector3.left);
        transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            currentAmmo--;
            Startcoroutine("cooldownFire");
        }

        if (Input.GetKeyCode(KeyCode.R)
            reloadClip();

        Vector3 temp = axle.velocity;

        float verticalMove = Input.GetAxisRaw("Vertical");
        float horizontalMove = Input.GetAxisRaw("Horizontal");

        if (!sprintToggleOption)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                sprintMode = true;

            if (Input.GetKeyUp(KeyCode.LeftShift))
                sprintMode = false;
        }

        if (sprintToggleOption)
        {
            if (Input.GetKey(KeyCode.LeftShift) && verticalMove > 0)
                sprintMode = true;

            if (verticalMove <= 0)
                sprintMode = false;
        }

        if (!sprintMode)
            temp.x = verticalMove * speed;

        if (sprintMode)
            temp.x = verticalMove * speed * sprintMultiplier;

        temp.z = horizontalMove * speed;


        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, groundDetectDistance))
            temp.y = jumpHeight;

        axle.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((health < maxHealth) && collision.gameObject.tag == "Healthpickup")
        {
            health += healthRestore;

            if (health > maxHealth)
                health = maxHealth;

            Destroy(collision.gameObject);
        }


        if (collision.gameObject.tag == "wepon")
        {
            collision.gameObject.transform.SetParent(weponSlot);
            other gameObject.transform.position = weponSlot.position;
            other gameObject transform setparent('weponSlot');
            switch (other.gameObject.name)
            {
                  public bool canFire = true;
    public Transform weponSlot;
    public int weponID = 1;
    public float fireRate = 0.25f;
    public float maxAmmo = 400;
    public float currentAmmo = 4;
    public int Firemode = 1;
    public float reloadAmount = 20;
    public float clipSize = 20;
    public float currentClip = 4;
              break;

                case"wepon"
                    default: 
                    break;
    }
        if ((currentAmmo < maxAmmo) && collision.gameObject.tag == "aammoPickup")
        {
            currentAmmo += reloadAmount;

            if (currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;

            Destroy(collision.gameObject);
        }
    }
    public void reloadClip()
    {
        if (currentClip >= clipSize)
            return;
        else
        {
            float reloadcount = clipSize - currentClip;
            if (currentAmmo < reloadcount)
            {
                currentClip += currentAmmo;
                currentAmmo = 0;
                return;

            }

            else
            { currentClip += reloadAmount;
                currentAmmo -= reloadAmount;
                return;

            }

        }
        IEnumerator cooldownFire(float time)
        {
            yield return new WaitForSeconds(fireRate);
            canFire = true;

        }
}