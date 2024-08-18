using System;
using UnityEngine;

public class AdenoProngRepeller : MonoBehaviour
{
    public event Action<GameObject> repelActivated;

    private void Start()
    {
        Player player = FindAnyObjectByType<Player>();
        repelActivated += player.PlayKnockBack;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            repelActivated?.Invoke(gameObject);
        }
    }
}
