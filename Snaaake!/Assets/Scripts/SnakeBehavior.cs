using System;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SnakeBehavior : MonoBehaviour
{
    [SerializeField] private KeyCode _upDirection;
    [SerializeField] private KeyCode _downDirection;
    [SerializeField] private KeyCode _leftDirection;
    [SerializeField] private KeyCode _rightDirection;
    
    private AudioSource[] _sources;
    [SerializeField] AudioClip lose;
    [SerializeField] AudioClip up;
    [SerializeField] AudioClip down;
    [SerializeField] AudioClip left;
    [SerializeField] AudioClip right;
    
    [Range(0.0167f, 0.14f)] public float moveInterval = 0.14f;

    public static List<GameObject> Body = new List<GameObject>();
    private List<Vector3> _previousPositions = new List<Vector3>();

    public GameObject bodyPrefab;
    public GameOverManager gameOverManager;

    private float moveTimer;
    private Vector2 _direction = Vector2.right;
    private Vector2 nextDirection;
    public bool isHead;
    private float lastDirectionChangeTime;

    private bool strobeCounter;
    private bool hasIncreasedSpeed;
    
    private void Start()
    {
        _sources = GetComponents<AudioSource>();

        if (Body.Count == 0)
        {
            isHead = true;
            Body.Add(gameObject);
            _previousPositions.Add(transform.position);
        }
        else
        {
            isHead = false;
        }
    }

    private void InitializeSnake()
    {
        _sources = GetComponents<AudioSource>();

        if (Body.Count == 0)
        {
            isHead = true;
            Body.Add(gameObject);
            _previousPositions.Add(transform.position);
        }
        else
        {
            isHead = false;
        }
    }
    private void Update()
    {
        if (!isHead)
        {
            return;
        }

        HandleInput();

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            Move();
            moveTimer = 0f;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(_upDirection))
        {
            nextDirection = Vector2.up;
        }

        if (Input.GetKeyDown(_downDirection))
        {
            nextDirection = Vector2.down;
        }

        if (Input.GetKeyDown(_leftDirection))
        {
            nextDirection = Vector2.left;
        }

        if (Input.GetKeyDown(_rightDirection))
        {
            nextDirection = Vector2.right;
        }
    }
    
    private void Move()
    {
        if (nextDirection == Vector2.up && _direction != Vector2.down)
        {
            if (_direction != Vector2.up)
                PlaySound(up);            
            _direction = Vector2.up;
        }
        if (nextDirection == Vector2.down && _direction != Vector2.up)
        {
            if (_direction != Vector2.down)
                PlaySound(down);            
            _direction = Vector2.down;
        }
        if (nextDirection == Vector2.left && _direction != Vector2.right)
        {
            if (_direction != Vector2.left)
                PlaySound(left);            
            _direction = Vector2.left;
        }
        if (nextDirection == Vector2.right && _direction != Vector2.left)
        {
            if (_direction != Vector2.right)
                PlaySound(right);            
            _direction = Vector2.right;
        }

        _previousPositions.Insert(0, transform.position);

        transform.position = new Vector3(
            transform.position.x + _direction.x,
            transform.position.y + _direction.y,
            1.0f
        );

        if (_previousPositions.Count > Body.Count)
        {
            _previousPositions.RemoveAt(_previousPositions.Count - 1);
        }

        for (int i = 1; i < Body.Count; i++)
        {
            if (i - 1 < _previousPositions.Count)
            {
                Body[i].transform.position = _previousPositions[i - 1];
            }
        }

        if (Body.Count % 10 == 0)
        {
            strobeCounter = !strobeCounter;

            for (int i = 0; i < Body.Count; i++)
            {
                if (strobeCounter)
                {
                    Body[i].GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
                }
                else
                {
                    Body[i].GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
                }
            }
        }
        else
        {
            for (int i = 0; i < Body.Count; i++)
                Body[i].GetComponent<SpriteRenderer>().color = new Color(0,1,0,1);
        }

        // Increase speed when the body count is one more than a multiple of 10
        if (Body.Count > 2 && Body.Count % 10 == 1 && !hasIncreasedSpeed)
        {
            IncreaseSpeed();
            hasIncreasedSpeed = true;
        }

        // Reset the flag if the body count is not one more than a multiple of 10
        if (Body.Count % 10 != 1)
        {
            hasIncreasedSpeed = false;
        }
    }

    public void Expand()
    {
        ScoreManager.Instance.AddScore(1);
        Vector3 newPosition = _previousPositions.Count > 0 ? _previousPositions[_previousPositions.Count - 1] : transform.position;
        GameObject newBodyPart = Instantiate(bodyPrefab, newPosition, Quaternion.identity, LoadGame.ObjectsParent);
        Body.Add(newBodyPart);

        if (_previousPositions.Count < Body.Count)
        {
            _previousPositions.Add(newPosition);
        }
    }

    private void IncreaseSpeed()
    {
        moveInterval *= 0.9f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food") && isHead)
        {
            Expand();
        }
        else if (other.CompareTag("Wall"))
        {
            GameOverManager.Instance.GameOver();
            PlaySound(lose);
        }
    }
    
    void PlaySound(AudioClip clip)
    {
        AudioSource source = GetAvailableAudioSource();
        if (source != null)
        {
            source.clip = clip;
            source.Play();
        }
    }

    AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in _sources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        Array.Resize(ref _sources, _sources.Length + 1);
        _sources[_sources.Length - 1] = newSource;
        return newSource;
    }
}
