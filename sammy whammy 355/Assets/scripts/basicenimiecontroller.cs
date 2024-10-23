using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;
using Vector3 = UnityEngine.Vector3;

public class basicenimiecontroller : MonoBehaviour
{
    public PlayerController player;
    public NavMeshAgent agent;
    public int DistMin = 10;
    public int DistMax = 15;
    public Transform Player;
    public AudioSource weaponSpeaker;
    private GameManager gm;

    [Header("enime stats")]
    public int health = 5;
    public int maxhealth = 5;
    public int damageGiven = 1;
    public int damageReceived = 1;
    public float pushBackForce = 100;
    public float corpesForce = 50;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gm = GameObject.Find("game manager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        //agent.destination = player.transform.position;
        weaponSpeaker = GetComponent<AudioSource>();
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        if (distanceToPlayer <= DistMax)
        {
            agent.destination = player.transform.position;
        }

        if (health <= 0)
        {
            gm.enimecounter--;
            weaponSpeaker.Play();
            Destroy(gameObject, 2f);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "shot")
        {
            health -= damageReceived;
            Destroy(collision.gameObject);
            Debug.Log(damageReceived);
        }

        Debug.Log(collision.gameObject.name);

    }

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(damage);
    }
}