using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
  [SerializeField] private GameObject _objectPrefab;
  [SerializeField] private int _initialPoolSize = 20;

  private Queue<GameObject> _pool = new Queue<GameObject>();

  private List<GameObject> _activeObjects = new List<GameObject>();

  private void Awake() {
    for (int i = 0; i < _initialPoolSize; i++) {
      CreateAndPoolObject();
    }
  }

  private GameObject CreateAndPoolObject() {
    GameObject newObj = Instantiate(_objectPrefab, transform);
    newObj.SetActive(false);
    _pool.Enqueue(newObj);
    return newObj;
  }

  public GameObject Get() {
    if (_pool.Count == 0) CreateAndPoolObject();

    GameObject obj = _pool.Dequeue();
    obj.SetActive(true);

    _activeObjects.Add(obj); 

    obj.GetComponent<IPoolableObject>()?.OnObjectSpawn();
    return obj;
  }

  public void Return(GameObject obj) {
    obj.GetComponent<IPoolableObject>()?.OnObjectReturn();
    obj.SetActive(false);

    _activeObjects.Remove(obj); 
    _pool.Enqueue(obj);
  }

  public void ReturnAll() {
    for (int i = _activeObjects.Count - 1; i >= 0; i--) {
      Return(_activeObjects[i]);
    }
  }
}