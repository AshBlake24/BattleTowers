using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Heart _heartTemplate;

    private List<Heart> _hearts = new List<Heart>();

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        if (_hearts.Count < health)
        {
            int iterations = health - _hearts.Count;

            for (int i = 0; i < iterations; i++)
            {
                CreateHeart();
            }
        }
        else if (_hearts.Count > health)
        {
            int iterations = _hearts.Count - health;

            for (int i = 0; i < iterations; i++)
            {
                RemoveHeart(_hearts[_hearts.Count - i - 1]);
            }
        }
    }

    private void CreateHeart()
    {
        Heart heart = Instantiate(_heartTemplate, transform);

        _hearts.Add(heart);
    }

    private void RemoveHeart(Heart heart)
    {
        _hearts.Remove(heart);
        Destroy(heart.gameObject);
    }
}