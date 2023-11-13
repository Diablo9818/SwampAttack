using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;

    public event UnityAction<Enemy> Dying;

    public Player GetTarget() { return _target; }

    public int GetReward() { return _reward; }

    private Player _target;

    public void Init(Player target)
    {
        _target = target;   
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        if(_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Dying?.Invoke(this);
        Destroy(gameObject);
    }
}
