using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody myRB;
    Camera playerCam;

    Vector2 camRotation;

    public Transform weaponSlot;

    [Header("Player Stats")]
    public int maxHealth = 5;
    public int health = 5;
    public int healthRestore = 1;

    [Header("Weapon Stats")]
    public GameObject shot;
    public GameObject stabby;
    public float bulletLifespan = 0f;
    public float shotSpeed = 100f;
    public int weaponID = -1;
    public int fireMode = 10;
    public float fireRate = 20f;
    public float currentClip = 100;
    public float clipSize = 100;
    public float maxAmmo = 100;
    public float currentAmmo = 100;
    public float reloadAmt = 20;
    public bool canFire = true;

    [Header("Movement Settings")]
    public float speed = 10.0f;
    public float sprintMultiplier = 2.5f;
    public bool sprintMode = false;
    public float jumpHeight = 5.0f;
    public float groundDetectDistance = 1f;

    [Header("User Settings")]
    public bool sprintToggleOption = false;
    public float mouseSensitivity = 2.0f;
    public float Xsensitivity = 2.0f;
    public float Ysensitivity = 2.0f;
    public float camRotationLimit = 90f;
    private object weponSlot;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
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
       if (Input.GetMouseButtonDown(0) && canFire && weaponID == 0)
        {
            stabby.SetActive(true);
            StartCoroutine(cooldownFire(fireRate));
        }

        else if (Input.GetMouseButton(0) && canFire && currentClip > 0)
        {
            GameObject s = Instantiate(shot, weaponSlot.position, weaponSlot.transform.rotation);
            s.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * shotSpeed);
            Destroy(s, bulletLifespan);
            canFire = false;
            currentClip--;
            StartCoroutine(cooldownFire(fireRate));
        }

        if (Input.GetMouseButton(0) && canFire && currentClip > 0)
        {
            GameObject s = Instantiate(shot, weaponSlot.position, weaponSlot.transform.rotation);
            s.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * shotSpeed);
            Destroy(s, bulletLifespan);
            canFire = false;
            currentClip--;
            StartCoroutine(cooldownFire(fireRate));
        }

        if (Input.GetKeyDown(KeyCode.R))
            reloadClip();

        Vector3 temp = myRB.velocity;

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

        temp.x = verticalMove * speed;
        temp.z = horizontalMove * speed;

        if (sprintMode)
            temp.x *= sprintMultiplier;

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, groundDetectDistance))
            temp.y = jumpHeight;

        myRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            if (weaponSlot.GetChild(0) != null)
                weaponSlot.GetChild(0).SetParent(null);

            other.gameObject.transform.position = weaponSlot.position;
            other.gameObject.transform.rotation = weaponSlot.rotation;

            other.gameObject.transform.SetParent(weaponSlot);

            switch (other.gameObject.name)
            {
                case "wepon":
                    weaponID = 0;
                    shotSpeed = 10000;
                    fireMode = 0;
                    fireRate = 0.25f;
                    currentClip = 20;
                    clipSize = 20;
                    maxAmmo = 400;
                    currentAmmo = 200;
                    reloadAmt = 20;
                    bulletLifespan = 1;
                    break;

                case "fencing sword":
                    weaponID = 1;
                    shotSpeed = 1;
                    fireMode = 0;
                    fireRate = 1f;
                    currentClip = 0;
                    clipSize = 1;
                    maxAmmo = 1;
                    currentAmmo = 1;
                    reloadAmt = 1;
                    bulletLifespan = 0;
                    break;
                default:
                    break;
            }
        }
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

        if ((currentAmmo < maxAmmo) && collision.gameObject.tag == "ammoPickup")
        {
            currentAmmo += reloadAmt;

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
            float reloadCount = clipSize - currentClip;

            if (currentAmmo < reloadCount)
            {
                currentClip += currentAmmo;
                currentAmmo = 0;
                return;
            }

            else
            {
                currentClip += reloadCount;
                currentAmmo -= reloadCount;
                return;
            }
        }
    }

    IEnumerator cooldownFire(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }


}