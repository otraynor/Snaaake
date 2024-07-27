using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodBehavior : MonoBehaviour
{
    private AudioSource _source;
    [SerializeField] private AudioClip eat;
    public BoxCollider2D boundary;
    private SpriteRenderer spriteRenderer;
    private bool isStrobing = false;
    private float strobeTimer = 0f;
    private float strobeInterval = 0.1f; // Adjust this value to change strobe speed
    
    private void Start()
    {
        _source = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (boundary == null)
        {
            return;
        }
        RandomizePosition();
    }

    private void StrobeCheck()
    {
        if (ScoreManager.instance.GetScore() % 10 == 8)
        {
            if (!isStrobing)
            {
                // Start strobing
                isStrobing = true;
                strobeTimer = 0f;
            }

            // Handle strobe effect
            strobeTimer += Time.deltaTime;
            if (strobeTimer >= strobeInterval)
            {
                strobeTimer = 0f;
                // Toggle color between black and white
                if (spriteRenderer.color == Color.black)
                {
                    spriteRenderer.color = Color.white;
                }
                else
                {
                    spriteRenderer.color = Color.black;
                }
            }
        }
        else
        {
            if (isStrobing)
            {
                // Stop strobing and set color to red
                isStrobing = false;
                spriteRenderer.color = Color.red;
            }
        }
    }
    private void Update()
    {
        StrobeCheck();
    }

    
    private void RandomizePosition()
    {
        Bounds bounds = boundary.bounds;
        Vector3 newPosition;
        Collider2D[] colliders;
        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            newPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 1f);

            // Check for any colliders at the new position
            colliders = Physics2D.OverlapBoxAll(newPosition, Vector2.one * 0.5f, 0f);
        } while (colliders.Length > 0 && colliders.Any(collider => collider.CompareTag("Wall")));

        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlaySound(eat);
            StrobeCheck();
            RandomizePosition();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }
}
