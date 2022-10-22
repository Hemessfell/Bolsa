using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : MonoBehaviour
{
    [SerializeField] private GameObject coalOBJ, cloneCoal;

    public void InstantiateCoal()
    {
        GameObject coal = Instantiate(coalOBJ, transform.position, Quaternion.identity);
        coal.GetComponent<CoalOBJ>().UpdateQuantity(true);
        MachineManager.Instance.PlayTrainAnimation();
    }

    public void InstantiateClone()
    {
        Instantiate(cloneCoal, transform.position, Quaternion.identity);
    }
}
