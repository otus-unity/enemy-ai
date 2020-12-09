
using UnityEngine;
using TMPro;

public class HealthIndicator : MonoBehaviour
{
    public Health health;
    int displayedHealth = 0;
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = $"{displayedHealth}";
    }

    void Update()
    {
        if (health.value != displayedHealth) {
            text.text = $"{health.value}";
            displayedHealth = health.value;
        }
    }
}
