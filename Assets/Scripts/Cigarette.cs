using UnityEngine;
using System.Collections;

public class Cigarette : MonoBehaviour
{
    public ParticleSystem smoke;

    public float smokeDuration = 2f;
    public float moveSpeed = 4f;

    public float downAmount = 0.15f; 
    public float upAmount = 0.10f;

    private bool isBusy = false;

    private Vector3 currentOffset = Vector3.zero; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isBusy)
        {
            StartCoroutine(SmokeRoutine());
        }

        // постійно застосовуємо офсет
        transform.localPosition += currentOffset;
    }

    IEnumerator SmokeRoutine()
    {
        isBusy = true;

        // 1 — опустити 
        yield return MoveOffset(new Vector3(0, -downAmount, 0));

        // 2 — дим
        smoke?.Play();
        yield return new WaitForSeconds(smokeDuration);
        smoke?.Stop();

        // 3 — підняти
        yield return MoveOffset(new Vector3(0, upAmount, 0));

        // 4 — повернути офсет до нуля щоб не наростав
        currentOffset = Vector3.zero;

        isBusy = false;
    }

    IEnumerator MoveOffset(Vector3 target)
    {
        Vector3 start = currentOffset;
        Vector3 end = target;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            currentOffset = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }
}
