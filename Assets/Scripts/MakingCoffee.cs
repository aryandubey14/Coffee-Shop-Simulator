using System.Collections;
using TMPro;
using UnityEngine;

public class MakingCoffee : MonoBehaviour
{
    public static MakingCoffee Instance;
    public bool IsMakingCoffee = false;

    [Header("Timer")]
    public TMP_Text CoffeeTimer;

    [Header("Player Animator")] 
    public Animator player;

    [Header("Coffee Bean Box")] 
    public GameObject CoffeeBeanBoxEmpty;
    public GameObject CoffeeBeanBox;

    [Header("Cup")] 
    public GameObject CoffeeCup;

    [Header("Extras")] 
    public GameObject popUp;

    private float coffeeMakingTime;   // total time
    private float currentTime;        // countdown

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (IsMakingCoffee)
        {
            // countdown timer
            currentTime -= Time.deltaTime;

            if (currentTime < 0f) currentTime = 0f;

            CoffeeTimer.text = "Making Coffee : " + currentTime.ToString("F1");
            CoffeeTimer.gameObject.SetActive(true);
        }
    }

    public void StartMakingCoffee()
    {
        if (!IsMakingCoffee)
        {
            // Calculate total coffee time from sound lengths
            coffeeMakingTime =
                AudioManager.Instance.GetSound("CoffeeBeanCrush").clip.length +
                AudioManager.Instance.GetSound("CoffeeMachine").clip.length;

            currentTime = coffeeMakingTime;

            IsMakingCoffee = true;
            popUp.SetActive(false);
            // start animation
            player.SetBool("MakingCoffee", true);


            StartCoroutine(MakeCoffee());
        }
    }

    private IEnumerator MakeCoffee()
    {
        // play bean crush
        AudioManager.Instance.PlaySound("CoffeeBeanCrush");
        yield return new WaitForSeconds(AudioManager.Instance.GetSound("CoffeeBeanCrush").clip.length);

        // box toggle
        CoffeeBeanBox.SetActive(true);
        CoffeeBeanBoxEmpty.SetActive(false);

        // play machine
        AudioManager.Instance.PlaySound("CoffeeMachine");
        yield return new WaitForSeconds(AudioManager.Instance.GetSound("CoffeeMachine").clip.length);

        // restore box state
        CoffeeBeanBox.SetActive(false);
        CoffeeBeanBoxEmpty.SetActive(true);

        // picking cup
        player.SetBool("MakingCoffee", false);
        player.SetBool("PickingCup", true);
        yield return new WaitForSeconds(player.GetCurrentAnimatorStateInfo(0).length);

        // spawn cup
        CoffeeCup.SetActive(true);
        CoffeeDelivery.Instance.hasCoffee = true;

        // done
        IsMakingCoffee = false;
        CoffeeTimer.gameObject.SetActive(false);
    }
}
