using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodBehavior : MonoBehaviour
{
    private AudioSource _source;
    [SerializeField] AudioClip eat;
    
    public BoxCollider2D boundary;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        
        if (boundary == null)
        {
            //Debug.LogError("Boundary is not assigned in the Inspector!");
            return;
        }
        RandomizePosition();
    }
    private void RandomizePosition()
    {
        Bounds bounds = this.boundary.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           PlaySound(eat); 
           RandomizePosition();           
        }
    }

    void PlaySound(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }
}
