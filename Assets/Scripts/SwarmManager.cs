using System.Collections.Generic;
using Units.Enemies;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();
    public Transform target;

    private void Update()
    {
        _enemies.RemoveAll(a => a.Health <= 0); // assumes enemies stay dead for a while before perishing
        foreach (var enemy in _enemies)
        {
            if (enemy.Health <= 0)
                _enemies.Remove(enemy);
            else
                enemy.Step();
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        if (enemy != null && !_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);
            enemy.SetTarget(target);
        }
    }
}