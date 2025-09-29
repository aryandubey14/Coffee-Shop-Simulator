using System.Collections;
using UnityEngine;
using TMPro;

public class DeliveryReward : MonoBehaviour
{
    public static DeliveryReward Instance;

    [Header("References")]
    [SerializeField] private GameObject coinPrefab3D;  // 3D coin prefab (world object to spawn on table)
    [SerializeField] private GameObject coinPrefabUI;  // UI coin prefab (Image under canvas)
    [SerializeField] private Transform coinSpawn;      // Where coins spawn on the table
    [SerializeField] private RectTransform scoreUI;    // Target UI element (coin icon/score text)
    [SerializeField] private Canvas canvas;            // Main UI canvas

    [Header("Settings")]
    [SerializeField] private int rewardAmount = 10;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int coinCount = 3;

    void Awake()
    {
        Instance = this;
    }

    public void OnDeliveryComplete()
    {
        for (int i = 0; i < coinCount; i++)
        {
            StartCoroutine(SpawnWorldCoin(i * 0.2f));
        }
    }

    private IEnumerator SpawnWorldCoin(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 1. Spawn coin in world (on table)
        GameObject coin3D = Instantiate(coinPrefab3D, coinSpawn.position, Quaternion.identity);

        // 2. Wait for a short time so players see it
        yield return new WaitForSeconds(0.5f);

        // 3. Convert world -> screen -> UI space
        Vector3 screenStart = Camera.main.WorldToScreenPoint(coin3D.transform.position);
        Destroy(coin3D); // remove world coin, replace with UI version

        // Spawn UI coin
        GameObject coinUI = Instantiate(coinPrefabUI, canvas.transform);
        RectTransform coinRect = coinUI.GetComponent<RectTransform>();

        // Convert screen pos -> UI local pos
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, screenStart, canvas.worldCamera, out Vector2 localStart);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, scoreUI.position, canvas.worldCamera, out Vector2 localTarget);

        coinRect.localPosition = localStart;

        // 4. Animate movement
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            coinRect.localPosition = Vector2.Lerp(localStart, localTarget, t);
            yield return null;
        }

        Destroy(coinUI);

        // 5. Update score
        GameManager.Instance.Score += rewardAmount;
        GameManager.Instance.UpdateScore();
    }
}
