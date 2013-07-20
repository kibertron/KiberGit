
public class PlayerDataClass {
	public int networkPlayer;
	public string playerName;
	public int playerScore;
	public int playerID;
	public PlayerDataClass Constructor ()
	{
		PlayerDataClass capture = new PlayerDataClass();	
		capture.networkPlayer = networkPlayer;
		capture.playerName = playerName;
		capture.playerScore = playerScore;
		capture.playerID = playerID;
		return capture;
	}
}