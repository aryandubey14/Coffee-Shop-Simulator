using UnityEngine;

public class CoffeeDelivery : MonoBehaviour
{
    public static CoffeeDelivery Instance;

    [Header("Player Animator")]
    public Animator anim;

    [Header("Delivery Settings")]
    public bool hasCoffee = false; // Set to true when coffee is made

    //Singleton
    void Awake()
    {
        Instance = this;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if collided with table
        if (hit.collider.CompareTag("Table") && hasCoffee)
        {
            DeliverCoffee();
        }
    }

    private void DeliverCoffee()
    {
        // Reset coffee state
        hasCoffee = false;
        MakingCoffee.Instance.CoffeeCup.SetActive(false);
        anim.SetBool("PickingCup",false);

        DeliveryReward.Instance.OnDeliveryComplete();
        CustomerAI.Instance.ReceiveCoffee();
    }
}