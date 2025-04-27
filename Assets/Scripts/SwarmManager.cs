using System.Collections.Generic;
using Units.Enemies;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();
    public Transform target;

    private void Update()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy.Health <= 0)
            {
                ScoreManager.Instance.AddScore(enemy.ScoreValue);
            }
            else
                enemy.Step();
        }
        _enemies.RemoveAll(a => a.Health <= 0); // assumes enemies stay dead for a while before perishing
    }

    public void RegisterEnemy(Enemy enemy)
    {
        if (enemy is null || _enemies.Contains(enemy))
            return;
        
        _enemies.Add(enemy);
        enemy.SetTarget(target);
    }
}