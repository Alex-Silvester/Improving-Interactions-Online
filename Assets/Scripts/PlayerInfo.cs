using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private int playerNumber = 0;

    public void setPlayerNumber(int num)
    {
        this.playerNumber = num; 
    }

    public int getPlayerNumber()
    {
        return this.playerNumber; 
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
