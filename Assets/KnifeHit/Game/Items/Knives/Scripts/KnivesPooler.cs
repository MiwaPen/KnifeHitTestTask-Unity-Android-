using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class KnivesPooler : MonoBehaviour
{
    [Inject] DiContainer diContainer;
    [Inject] private LevelBehaviour levelBehaviour;
    [System.Serializable]
    public class KnivesPool
    {
        public KnifeBehaviourScript prefab;
        public int knifeCount;
    }

    public KnivesPool knifePool;
    Queue<GameObject> objectsPool;

    public void Initialize()
    {
        objectsPool = new Queue<GameObject>();

        for (int i = 0; i < knifePool.knifeCount; i++)
        {
            GameObject obj = diContainer
                .InstantiatePrefab(knifePool.prefab.gameObject);
            obj.GetComponent<KnifeBehaviourScript>()
                .onKnifeHitKnife += levelBehaviour.LoseGame;
            obj.SetActive(false);
            objectsPool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool(Vector3 pos)
    {
        if (objectsPool.Count == 0) return null;

        GameObject obj = objectsPool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);

        IPooledObject objInterface;
        obj.TryGetComponent<IPooledObject>(out objInterface);

        if (objInterface != null)
            objInterface.GetFromPool();

        objectsPool.Enqueue(obj);
        return obj;
    }

    public void  DisableAllItems()
    {
        int length = objectsPool.Count;
        for (int i = 0; i < length; i++)
        {
            GameObject obj = objectsPool.Dequeue();
            obj.GetComponent<KnifeBehaviourScript>()
                .onKnifeHitKnife -= levelBehaviour.LoseGame;
            Destroy(obj);
        }
        objectsPool.Clear();
    }
}
