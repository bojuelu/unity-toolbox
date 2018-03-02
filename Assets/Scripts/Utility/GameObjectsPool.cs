using UnityEngine;
using System.Collections.Generic;

namespace UnityToolbox
{
    public class GameObjectsPool : MonoBehaviour
    {
        public GameObject itemSource;
        public int initCount = 100;

        private Queue<GameObject> itemsPool = new Queue<GameObject>();
        public int itemsCount { get { return itemsPool.Count; } }

        void Start()
        {
            for (int i = 0; i < initCount; i++)
            {
                GameObject go = GameObject.Instantiate(itemSource);
                Put(go);
            }
        }

        public GameObject Get()
        {
            if (itemsPool.Count <= 0)
            {
                GameObject newItem = GameObject.Instantiate(itemSource);
                itemsPool.Enqueue(newItem);
                return newItem;
            }

            GameObject oldItem = itemsPool.Dequeue();
            oldItem.transform.SetParent(null);
            oldItem.SetActive(true);
            return oldItem;
        }

        public void Put(GameObject item)
        {
            item.SetActive(false);
            item.transform.SetParent(transform);
            itemsPool.Enqueue(item);
        }

        public void Clear(string goName)
        {
            while (itemsPool.Count > 0)
            {
                GameObject item = itemsPool.Dequeue();
                GameObject.Destroy(item);
            }
        }
    }
}
