using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerPanel : MonoBehaviour
{
    [SerializeField] private Tower[] previousTower;
    [SerializeField] private Tower upgradeTower;

    public void Upgrade()
    {
        if (GameManager.HasInstance)
        {
            if (upgradeTower.Price <= GameManager.Instance.Gold)
            {
                GameManager.Instance.SpendGold(upgradeTower.Price);
                Tower nextTower = Instantiate(upgradeTower);
                nextTower.transform.position = transform.position;
                nextTower.location = gameObject.GetComponent<Tower>().location;
                Destroy(gameObject);
            }
        }
    }

    public void Sell()
    {
        if (GameManager.HasInstance)
        {
            gameObject.GetComponent<Tower>().location.SetActive(true);
            gameObject.GetComponent<Tower>().location = null;
            GameManager.Instance.ReceiveGold(SellPrice());
            Destroy(gameObject);
        }
    }

    private int SellPrice()
    {
        int coin = 0;
        coin += gameObject.GetComponent<Tower>().Price;
        if (previousTower.Length != 0)
        {
            foreach (var item in previousTower)
            {
                coin += item.Price;
            }
        }
        return Mathf.RoundToInt(coin * 0.7f);
    }
}
