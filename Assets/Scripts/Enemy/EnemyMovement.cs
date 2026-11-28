using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Variables")]
    int playerHealth;
    Transform player;
    int enemyHealth;
    NavMeshAgent agent;


    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        playerHealth = player.GetComponent<PlayerHealth>().currentHealth;
        enemyHealth = GetComponent<EnemyHealth>().currentHealth;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update ()
    {

        if (enemyHealth > 0 && playerHealth > 0 && agent.isActiveAndEnabled)
        {
            agent.SetDestination (player.position);
        }
        else
        {
            agent.enabled = false;
        }
    }
}
