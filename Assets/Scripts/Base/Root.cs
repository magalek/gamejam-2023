using System.Collections;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform maskTransform;
    [SerializeField] private float maxMaskY;

    private Coroutine rootCoroutine;

    public void Reset()
    {
        maskTransform.localScale = new Vector3(
            maskTransform.localScale.x
            ,0,
            maskTransform.localScale.z);
    }
    
    public void Show(float rootTime) => StartRootCoroutine(maxMaskY, rootTime);

    public void Hide(float rootTime) => StartRootCoroutine(0, rootTime);

    private void StartRootCoroutine(float desiredY, float rootTime)
    {
        if (rootCoroutine != null)
        {
            StopCoroutine(rootCoroutine);
            rootCoroutine = null;
        }

        rootCoroutine = StartCoroutine(RootCoroutine());
        
        IEnumerator RootCoroutine()
        {
            float time = 0;

            float currentY = maskTransform.localScale.y;
            
            while (time < rootTime)
            {
                time += Time.deltaTime;

                var interpolatedT = Mathf.Clamp01(time / rootTime);

                maskTransform.localScale = new Vector3(
                    maskTransform.localScale.x
                    ,Mathf.SmoothStep(currentY, desiredY, interpolatedT),
                    maskTransform.localScale.z);
                
                yield return null;
            }
            maskTransform.localScale = new Vector3(
                maskTransform.localScale.x
                ,desiredY,
                maskTransform.localScale.z);
        }
    }
}