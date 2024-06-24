using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private GameObject attachPoint;
    [SerializeField] public bool handFull;
    [SerializeField] private GameObject itemInHand;
    [SerializeField] private List<GameObject> itemsAround;

    private void Update() 
    {
        handFull = attachPoint.transform.childCount > 0;
        InputMethod();

        if (itemInHand != null)
        {itemInHand.transform.position = attachPoint.transform.position;}
        
    }

    private void InputMethod()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!handFull && itemsAround.Count > 0)
            {
                AttachItem(itemsAround[0]);
                itemsAround.RemoveAt(0);
                
                if (itemInHand.GetComponent<Gun>() != null && itemInHand.GetComponent<Gun>().name != "Medkit")
                {
                    SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                    soundManager.PlaySound("Gun Equip");
                }
            }
            else
            {
                detachItem();
            }
        }
    }

    private void AttachItem(GameObject item)
    {
        item.transform.SetParent(attachPoint.transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        if (item.GetComponent<Rigidbody2D>() != null)
        {
            item.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        itemInHand = item;
    }

    private void detachItem()
    {
        GameObject item = attachPoint.transform.GetChild(0).gameObject;
        attachPoint.transform.DetachChildren();

        if (item.GetComponent<Rigidbody2D>() != null)
        {
            item.GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        itemInHand = null;
    }

    public GameObject GetItemInHand()
    {
        return itemInHand;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "item")
        {
            itemsAround.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "item")
        {
            itemsAround.Remove(other.gameObject);
        }
    }
}
