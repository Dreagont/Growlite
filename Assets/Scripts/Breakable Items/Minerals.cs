using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minerals : MonoBehaviour
{
    [SerializeField] private int Toughness;
    [SerializeField] private float spread = 0.7f;
    [SerializeField] private int DropCount = 5;
    [SerializeField] private List<GameObject> DropList = new List<GameObject>();

    public void Hit(int damage)
    {
        Toughness -= damage;
        if (Toughness <= 0)
        {
            Break();
        }
    }

    public void Break()
    {
        Destroy(gameObject);

        for (int i = 0; i < DropCount; i++)
        {
            foreach (var item in DropList)
            {
                Vector3 dropPosition = transform.position;

                // Randomize drop position within the spread
                dropPosition.x += spread * (Random.value - 0.5f);
                dropPosition.y += spread * (Random.value - 0.5f);

                GameObject drop = Instantiate(item, dropPosition, Quaternion.identity);
            }
        }
    }
}
