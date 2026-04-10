using System.Collections;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour {
  [SerializeField] private ObjectPool _columnPool;

  [SerializeField] private float _spawnInterval = 2.0f;
  [SerializeField] private float _moveDuration = 7.0f;

  [SerializeField] private float _spawnY = 15f;
  [SerializeField] private float _despawnY = -15f;
  [SerializeField] private Vector2 _horizontalRange = new Vector2(-4f, 4f);

  private Coroutine _spawnCoroutine;

  private void OnEnable() {
    _columnPool.ReturnAll();

    EventBus.Subscribe<FirstTapEvent>(OnFirstTap); 
    EventBus.Subscribe<GameOverEvent>(OnGameOver);
  }

  private void OnDisable() {
    EventBus.Unsubscribe<FirstTapEvent>(OnFirstTap);
    EventBus.Unsubscribe<GameOverEvent>(OnGameOver);

    StopSpawning();
    _columnPool.ReturnAll();
  }

  private void OnFirstTap(FirstTapEvent e) {
    StartSpawning();
  }

  private void OnGameOver(GameOverEvent e) {
    StopSpawning();
  }

  private void StartSpawning() {
    StopSpawning();
    _spawnCoroutine = StartCoroutine(SpawnLoop());
  }

  private void StopSpawning() {
    if (_spawnCoroutine != null) {
      StopCoroutine(_spawnCoroutine);
      _spawnCoroutine = null;
    }
  }

  private IEnumerator SpawnLoop() {
    while (true) {
      GameObject columnGO = _columnPool.Get();

      float randomX = Random.Range(_horizontalRange.x, _horizontalRange.y);
      Vector3 startPos = new Vector3(randomX, _spawnY, 0);
      Vector3 endPos = new Vector3(randomX, _despawnY, 0);

      columnGO.transform.position = startPos;
      ColumnPair pair = columnGO.GetComponent<ColumnPair>();

      pair.StartMovement(endPos, _moveDuration);

      yield return new WaitForSeconds(_spawnInterval);
    }
  }
}