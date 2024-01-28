using System;
using UnityEngine;

public class BallCollisionHandler : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public event Action CollisionEntered;

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody = collision.collider.attachedRigidbody;

        if (_rigidbody != null)
            if (_rigidbody.TryGetComponent(out Ball ball))
                CollisionEntered?.Invoke();
    }
}