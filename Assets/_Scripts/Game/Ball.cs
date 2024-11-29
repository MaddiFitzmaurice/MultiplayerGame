using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : NetworkBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void OnNetworkSpawn()
    {
        Vector2 dir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        _rb.AddForce(dir * 1000000f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (TryGetComponent(out Player player))
        {
            Debug.Log("HIT");
        }
    }
}
