using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] private Image imagePanel;
    [SerializeField] private Image hitPanel;

    private void Start()
    {
        BaseStructure.Instance.Hit += OnHit;
        StartCoroutine(HitBarCoroutine());
    }

    private void OnHit(float percent)
    {
        imagePanel.fillAmount = percent;
    }

    private IEnumerator HitBarCoroutine()
    {
        while (true)
        {
            while (hitPanel.fillAmount > imagePanel.fillAmount)
            {
                var hit = hitPanel.fillAmount * 100;
                var hp = imagePanel.fillAmount * 100;
                
                hitPanel.fillAmount -= Time.deltaTime / (10 - (hit - hp));
                yield return null;
            }
            yield return null;
        }
    }
}