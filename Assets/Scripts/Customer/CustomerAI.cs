using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    public static CustomerAI Instance;
    public Transform Target; // table or counter
    public Transform Exit;

    private NavMeshAgent agent;
    private Animator anim;
    private bool hasArrived = false; 

    void Awake() { Instance = this; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (Target != null)
            agent.SetDestination(Target.position);
    }

    void Update()
    {
        // Walking / Idle animation control
        float speed = agent.velocity.magnitude;
        anim.SetBool("IsWalking", speed > 0.1f);

        // Arrive at table
        if (!hasArrived && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            hasArrived = true;
            AskForCoffee();
        }
    }

    public void AskForCoffee()
    {
        MakingCoffee.Instance.popUp.SetActive(true);
        anim.SetBool("IsWalking", false);
    }

    public void ReceiveCoffee()
    {
        // Stop moving
        agent.isStopped = true;
        anim.SetBool("IsWalking", false);

        // Turn 180 degrees
        transform.Rotate(0f, 180f, 0f);

        // Start drinking coroutine
        StartCoroutine(StartDrinking());
    }

    private IEnumerator StartDrinking()
    {
        // Trigger drink animation
        anim.SetBool("IsDrinking", true);

        // Wait a frame so Animator updates
        yield return null;

        // Wait for animation length
        float drinkLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(drinkLength);

        anim.SetBool("IsDrinking", false);

        // Resume walking to exit
        agent.isStopped = false;
        if (Exit != null)
        {
            agent.SetDestination(Exit.position);

            // Wait until agent reaches exit
            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);

            // Destroy customer when reached
            Destroy(gameObject);
        }
    }

}
