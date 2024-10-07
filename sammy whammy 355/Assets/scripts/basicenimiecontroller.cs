using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class basicenimiecontroller : MonoBehaviour
{
    public PlayerController player;
    public NavMeshAgent agent;
    public GameObject Corpes;
    public float corpesLifespan = 10;

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
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject corpes = Instantiate(Corpes, transform.position, transform.rotation);
            corpes.GetComponent<Rigidbody>().AddForce(-transform.forward * corpesForce);
            Destroy(corpes, corpesLifespan); 
        }


    }

    private void OnTriggerEnter(Collision collistion)
    {
        if(collistion.gameObject.tag == "shot")
        {
            health -= damageReceived;
            Destroy(collistion.gameObject);
            
        }

    }
}