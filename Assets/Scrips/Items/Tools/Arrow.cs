using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Arrow : Tool
{
    [SerializeField]
    public float baseSpeed = 60;

    private Bow _bow;
    private bool _isMove;
    private static readonly Vector3 Gravity = Vector3.down * 9.8f;

    private Vector3 _initialVelocity;
    private Vector3 _initialPosition;
    private float _travelTime;

    // void Start()
    // {
    //     
    // }

    private void Update()
    {
        if (_isMove)
        {
            _travelTime += Time.deltaTime;
            var projectileTransform = transform;

            var position = projectileTransform.position;
            var oldPos = position;
            position = GetPosition();
            projectileTransform.position = position;
            projectileTransform.up = position - oldPos;
            var hit = GetHit(oldPos, position);
            if (hit)
            {
                _isMove = false;
                // Some some = collider.GetComponent<Some>();
            }
        }
    }

    private Collider GetHit(Vector3 start, Vector3 end)
    {
        var ray = new Ray
        {
            origin = start,
            direction = end - start
        };
        var distance = ray.direction.magnitude;
        if (Physics.Raycast(ray, out var hit, distance))
        {
            return hit.collider;
        }

        return null;
    }

    public void Shoot(Bow shoutedBow, Vector3 destination, float force = 1f)
    {
        _bow = shoutedBow;
        _isMove = true;
        var projectileTransform = transform;
        projectileTransform.parent = null;
        var position = projectileTransform.position;
        _initialVelocity = (destination - position).normalized * (baseSpeed * force);
        
        Debug.DrawLine(position, destination, Color.green, 2);
        _initialPosition = position;
    }

    [SuppressMessage("ReSharper", "ArrangeRedundantParentheses")]
    private Vector3 GetPosition()
    {
        return (_initialPosition) + (_initialVelocity * _travelTime) + (Gravity * (0.5f * _travelTime * _travelTime));
    }
}
