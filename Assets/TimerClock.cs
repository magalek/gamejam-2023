using TMPro;
using UnityEngine;

public class TimerClock : MonoBehaviour
{
    private TextMeshProUGUI label;

    private float timer;
    
    private void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float seconds = (int)timer % 60;
        float minutes = Mathf.FloorToInt(seconds / 60);
        
        label.text = $"{minutes:00}:{seconds:00}";
    }
}
