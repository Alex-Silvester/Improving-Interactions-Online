using UnityEngine;

public class PlayerGetter : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    public GameObject getPlayer(int idx)
    {
        if (idx == 1) { return player1; }
        else return player2;
    }
}

