﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] private Image imagePanel;
    [SerializeField] private Image hitPanel;

    private void Start()
    {
        BaseStructure.Instance.HealthChanged += OnHealthChanged;
        StartCoroutine(HitBarCoroutine());
    }

    private void OnHealthChanged(float percent)
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
                
                hitPanel.fillAmount -= Time.deltaTime / (10 - Mathf.Clamp((hit - hp), 0, 5));
                yield return null;
            }
            yield return null;
        }
    }
}