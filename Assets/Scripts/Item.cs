using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemInfo
{
    Potion,
    Speed
}
public class Item : MonoBehaviour
{
    ItemInfo itemInfo;


    // Start is called before the first frame update
    void Start()
    {
        SetItem();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void SetItem()
    {
        itemInfo = (ItemInfo)Random.Range(0, (int)ItemInfo.Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddBuff(other.GetComponent<Player>());
        }
    }

    void AddBuff(Player player)
    {
        switch (itemInfo)
        {
            case ItemInfo.Potion:
                {
                    player.ItemPotion();
                    Destroy(this.gameObject);
                }
                break;
            case ItemInfo.Speed:
                {
                    player.ItemSpeed();
                    Destroy(this.gameObject);
                }
                break;
        }
    }
}
