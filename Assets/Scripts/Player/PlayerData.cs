[System.Serializable]
public class PlayerData
{
    public int BestScore { get; private set; }

	public PlayerData(Player player)
	{
		int lastBestScore = GetLastBestScore();

		if (player.Score > lastBestScore)
			BestScore = player.Score;
		else
			BestScore = lastBestScore;
	}

	private int GetLastBestScore()
	{
		PlayerData lastPlayerData = SaveSystem.LoadPlayer();

		if (lastPlayerData != null)
			return lastPlayerData.BestScore;
		else
			return 0;
	}
}