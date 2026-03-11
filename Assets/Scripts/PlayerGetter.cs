using UnityEngine;

public class PlayerGetter : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    public GameObject getPlayer(int idx)
    {
        if (idx == 1) return player1;
        if (idx == 2) return player2;
        else return new GameObject();
    }
}

