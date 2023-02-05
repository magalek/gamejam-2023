using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

namespace Misc
{
    public class EnemyIndicator : MonoBehaviour
    {
        private const string ARROW_SPRITE_KEY = "arrow{0}";
        
        [SerializeField] private SpriteAtlas arrowAtlas;
        [SerializeField] private float flightTime;

        private SpriteRenderer renderer;

        private int spriteIndex = 0;

        private float heightDifference = 0.5f;

        private Vector2 initialPosition;
        private float t = 0.5f;
        private float normalizedT;
        private bool goingUp;

        private void Awake()
        {
            renderer = GetComponentInChildren<SpriteRenderer>();
            initialPosition = transform.position;
        }

        private void Start()
        {
            StartCoroutine(FlightCoroutine());
        }

        private IEnumerator SpriteCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                renderer.sprite = arrowAtlas.GetSprite(string.Format(ARROW_SPRITE_KEY, arrowAtlas.spriteCount - spriteIndex - 1));
                spriteIndex = (spriteIndex + 1) % arrowAtlas.spriteCount;
            }
        }

        private IEnumerator FlightCoroutine()
        {
            while (true)
            {
                t = Mathf.Clamp01(t + (goingUp ? Time.deltaTime : -Time.deltaTime));
                normalizedT = Mathf.SmoothStep(0, 1, t / flightTime);
                transform.position = Vector2.Lerp(
                    initialPosition - (Vector2.up * heightDifference), 
                    initialPosition + (Vector2.up * heightDifference),
                    normalizedT);
                if (Mathf.Approximately(t, 0) || Mathf.Approximately(t, 1)) goingUp = !goingUp;
                yield return null;
            }
        }
    }
}