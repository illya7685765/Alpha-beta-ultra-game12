using UnityEngine;

public class PeeController : MonoBehaviour
{
    [Header("Input")]
    public KeyCode peeKey = KeyCode.E;
    public bool holdToPee = true;

    [Header("Pee Particle")]
    public ParticleSystem peeParticle;
    public Transform peeSpawnPoint; // Порожній об'єкт, де починається струя

    private bool isPeeing = false;

    void Update()
    {
        HandleInput();
        // Більше нічого не змінюємо в Update
    }

    void HandleInput()
    {
        if (holdToPee)
        {
            if (Input.GetKeyDown(peeKey)) StartPee();
            if (Input.GetKeyUp(peeKey)) StopPee();
        }
        else
        {
            if (Input.GetKeyDown(peeKey))
            {
                if (isPeeing) StopPee();
                else StartPee();
            }
        }
    }

    void StartPee()
    {
        if (peeParticle)
        {
            var main = peeParticle.main;
            main.gravityModifier = 1f;

            var velocity = peeParticle.velocityOverLifetime;
            velocity.enabled = true;
            velocity.y = -2f;

            // Переміщати частинки в Update не потрібно, вони вже прикріплені до peeSpawnPoint
            peeParticle.Play();
            isPeeing = true;
        }
    }

    void StopPee()
    {
        if (peeParticle)
        {
            peeParticle.Stop();
            isPeeing = false;
        }
    }
}
