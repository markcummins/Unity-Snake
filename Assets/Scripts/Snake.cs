using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private bool _can_move = true;
    private int _speed_max = 4;
    private int _speed = 12;
    private List<Transform> _segments;

    public Transform segmentPrefab;

    void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    void Update()
    {
        if (_direction != Vector2.down && Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }

        if (_direction != Vector2.right && Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }

        if (_direction != Vector2.up && Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }

        if (_direction != Vector2.left && Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }

        if (Time.frameCount % _speed == 0)
        {
            _can_move = true;
        }
    }

    void FixedUpdate()
    {
        if (_can_move == false)
        {
            return;
        }

        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
        _can_move = false;
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);

        if (_speed > _speed_max && _segments.Count % 4 == 0)
        {
            _speed = _speed - 2;
        }
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            Vector3 headLoc = this.transform.localPosition;
            Vector3 objectLoc = other.transform.localPosition;

            if (headLoc != objectLoc)
            {
                ResetState();
            }
        }

        else if (other.tag == "Food")
        {
            Grow();
        }
    }
}
