using com.cyborgAssets.inspectorButtonPro;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class FilterMaterialUpdator : MonoBehaviour
{
    [SerializeField] private Material p1Material;
    [SerializeField] private Material p2Material;

    BoxCollider bc;
    Renderer rend;

    public void updateStuff()
    {
        bc = GetComponent<BoxCollider>();
        rend = GetComponent<Renderer>();
    }

    [ProButton]
    public void Player1()
    {
        updateStuff();
        rend.material = p1Material;
        bc.excludeLayers = LayerMask.GetMask("Player 1");
    }


    [ProButton]
    public void Player2()
    {
        updateStuff();
        rend.material = p2Material;
        bc.excludeLayers = LayerMask.GetMask("Player 2");
    }
}
