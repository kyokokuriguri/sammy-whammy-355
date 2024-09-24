using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class basicenimiecontroller : MonoBehaviour
{
    public PlayerController player;
    public NavMeshAgent agent;

    [Header("enime stats")]
    public int health = 5;
    public int maxhealth = 5;
    public int damageGiven = 1;
    public int damageReceived = 1;
    public float pushBackForce = 100;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;
        if(health<= 0)
           Destroy(gameObject)

    }

    private void onCollistionEnter(collistion collistion)
    {
        if(collistion.gameObject.tag=="bullet")
        {
            health -= damageReceived;
            Destroy(collistion.gameObject);
        }
)
    }
}