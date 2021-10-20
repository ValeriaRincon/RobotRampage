using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    GameObject missilePrefab;
    [SerializeField]
    private string robotType;

    public int health;
    public int range;
    public float fireRate;
    public Animator robot;

    public Transform missileFireSpot;
    UnityEngine.AI.NavMeshAgent agent;

    private Transform player;
    private float timeLastFired;

    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        transform.LookAt(player);
        agent.SetDestination(player.position);
        if(Vector3.Distance(transform.position, player.position) < range && Time.time - timeLastFired > fireRate)
        {
            timeLastFired = Time.time;
            fire();
        }
    }

    private void fire()
    {
        GameObject missile = Instantiate(missilePrefab);
        missile.transform.position = missileFireSpot.transform.position;
        missile.transform.rotation = missileFireSpot.transform.rotation;
        robot.Play("Fire");
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }

        health -= amount;

        if (health <= 0)
        {
            isDead = true;
            robot.Play("Die");
            StartCoroutine("DestroyRobot");
        }
    }

    IEnumerator DestroyRobot()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}