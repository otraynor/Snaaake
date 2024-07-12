using System;
using System.Collections;
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
    
    private AudioSource _source;
    [SerializeField] AudioClip lose;

    [Range(0.0167f, 0.09342f)] public float moveInterval = 0.09342f;
    public float directionChangeCooldown = 0.07f; // Minimum time between direction changes

    private static List<GameObject> _body = new List<GameObject>();
    private List<Vector3> _previousPositions = new List<Vector3>();

    public GameObject bodyPrefab;
    public GameOverManager gameOverManager; // Reference to the Game Over Manager

    private float moveTimer;
    private Vector2 _direction = Vector2.right;
    private bool isHead;
    private float lastDirectionChangeTime;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        
        if (_body.Count == 0)
        {
            isHead = true;
            _body.Add(gameObject);
            _previousPositions.Add(transform.position);
        }
        else
        {
            isHead = false;
        }

        // Ensure the GameOverManager is assigned
        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager is not assigned in the Inspector!");
        }
    }

    private void Update()
    {
        if (!isHead) return;

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
        if (Time.time - lastDirectionChangeTime >= directionChangeCooldown)
        {
            if (Input.GetKeyDown(_upDirection) && (_direction != Vector2.down))
            {
                _direction = Vector2.up;
                lastDirectionChangeTime = Time.time;
            }
            else if (Input.GetKeyDown(_downDirection) && (_direction != Vector2.up))
            {
                _direction = Vector2.down;
                lastDirectionChangeTime = Time.time;
            }
            else if (Input.GetKeyDown(_leftDirection) && (_direction != Vector2.right))
            {
                _direction = Vector2.left;
                lastDirectionChangeTime = Time.time;
            }
            else if (Input.GetKeyDown(_rightDirection) && (_direction != Vector2.left))
            {
                _direction = Vector2.right;
                lastDirectionChangeTime = Time.time;
            }
        }
    }

    private void Move()
    {
        _previousPositions.Insert(0, transform.position);

        transform.position = new Vector3(
            transform.position.x + _direction.x,
            transform.position.y + _direction.y,
            1.0f
        );

        if (_previousPositions.Count > _body.Count)
        {
            _previousPositions.RemoveAt(_previousPositions.Count - 1);
        }

        for (int i = 1; i < _body.Count; i++)
        {
            if (i - 1 < _previousPositions.Count)
            {
                _body[i].transform.position = _previousPositions[i - 1];
            }
        }
    }

    public void Expand()
    {
        ScoreManager.instance.AddScore(1);
        Vector3 newPosition = _previousPositions.Count > 0 ? _previousPositions[_previousPositions.Count - 1] : transform.position;
        GameObject newBodyPart = Instantiate(bodyPrefab, newPosition, UnityEngine.Quaternion.identity);
        _body.Add(newBodyPart);

        if (_previousPositions.Count < _body.Count)
        {
            _previousPositions.Add(newPosition);
        }

        IncreaseSpeed();
    }

    private void IncreaseSpeed()
    {
        moveInterval *= 0.98f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Snake collided with: {other.gameObject.name} tagged as: {other.tag}");

        if (other.CompareTag("Food") && isHead)
        {
            Expand();
        }
        else if (other.CompareTag("Wall"))
        {
            gameOverManager.GameOver();
            PlaySound(lose);
        }
    }
    void PlaySound(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }
}
