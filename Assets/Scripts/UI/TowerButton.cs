using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private Sprite dragSprite;

    public GameObject TowerInfo => tower;
    public Sprite DragSprite => dragSprite;

    public bool CanBuy()
    {
        int price = tower.GetComponent<Tower>().Price;
        if (GameManager.HasInstance)
        {
            if (price <= GameManager.Instance.Gold)
            {
                return true;
            }
        }
        return false;
    }
}
