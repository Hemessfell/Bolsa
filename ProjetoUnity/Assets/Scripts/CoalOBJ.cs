using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalOBJ : MonoBehaviour
{
    [SerializeField] private Sprite[] sps;
    private SpriteRenderer spr;

    public int quantity;

    private Coroutine timeToLoseQuantity;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        timeToLoseQuantity = StartCoroutine(TimeToLoseQuantity());
    } 

    public void UpdateQuantity(bool willIncrease)
    {
        if (willIncrease)
        {
            StopCoroutine(timeToLoseQuantity);
            timeToLoseQuantity = StartCoroutine(TimeToLoseQuantity());

            if (quantity == 0)
            {
                spr.sprite = sps[0];
            }
            else if (quantity == 1)
            {
                spr.sprite = sps[1];
            }
            else if (quantity == 2)
            {
                spr.sprite = sps[2];
            }
            else if (quantity == 3)
            {
                spr.sprite = sps[3];
            }
            else if (quantity == 4)
            {
                spr.sprite = sps[4];
            }

            quantity++;
        }
        else
        {
            if (quantity == 0)
            {
                FindObjectOfType<MachineManager>().StopTrainAnimation();
                Destroy(gameObject);
            }
            else if (quantity == 1)
            {
                spr.sprite = sps[0];
            }
            else if (quantity == 2)
            {
                spr.sprite = sps[1];
            }
            else if (quantity == 3)
            {
                spr.sprite = sps[2];
            }
            else if (quantity == 4)
            {
                spr.sprite = sps[3];
            }

            quantity--;
        }

        quantity = Mathf.Clamp(quantity, 0, 4);
    }

    private IEnumerator TimeToLoseQuantity()
    {
        yield return new WaitForSeconds(10);
        UpdateQuantity(false);
        timeToLoseQuantity = StartCoroutine(TimeToLoseQuantity());
    }
}