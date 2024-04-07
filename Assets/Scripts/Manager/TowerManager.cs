using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : BaseManager<TowerManager>
{
    private TowerButton towerBtn;
    private SpriteRenderer sR;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sR.enabled)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Space"))
            {
                PlaceTower(hit);
            }
        }
        if (sR.enabled)
        {
            FollowMouse();
            if (Input.GetMouseButtonDown(1))
            {
                DisableDrag();
            }
        } 
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtn != null)
        {
            GameObject newTower = Instantiate(towerBtn.TowerInfo);
            newTower.transform.position = hit.transform.position;
            if (GameManager.HasInstance)
            {
                int pay = newTower.GetComponent<Tower>().Price;
                newTower.GetComponent<Tower>().location = hit.collider.gameObject;
                newTower.GetComponent<Tower>().location.SetActive(false);
                GameManager.Instance.SpendGold(pay);
            }
            DisableDrag();
        }
    }

    public void SelectTower(TowerButton towerSelected)
    {
        towerBtn = towerSelected;
        if (towerBtn.CanBuy())
        {
            EnableDrag(towerBtn.DragSprite);
        }  
    }

    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnableDrag(Sprite sprite)
    {
        sR.enabled = true;
        sR.sprite = sprite;
    }

    public void DisableDrag()
    {
        sR.enabled = false;
    }
}
