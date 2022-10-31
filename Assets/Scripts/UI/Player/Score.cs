using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text _currentScore;
    [SerializeField] private TMP_Text _bestScore;

    private PlayerData _playerData;

    private void OnEnable()
    {
        UpdatePlayerData();

        _currentScore.text = player.Score.ToString();
        _bestScore.text = $"Best: {_playerData.BestScore}";
    }

    private void UpdatePlayerData()
    {
        SaveSystem.SavePlayer(player);
        _playerData = SaveSystem.LoadPlayer();
    }
}