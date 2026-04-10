using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus {
  private static readonly Dictionary<Type, Delegate> _events = new Dictionary<Type, Delegate>();

  private static readonly object _lock = new object();

  public static void Subscribe<T>(Action<T> listener) {
    Type eventType = typeof(T);
    lock (_lock) {
      if (!_events.ContainsKey(eventType)) {
        _events[eventType] = null;
      }
      _events[eventType] = Delegate.Combine(_events[eventType], listener);
    }
  }

  public static void Unsubscribe<T>(Action<T> listener) {
    Type eventType = typeof(T);
    lock (_lock) {
      if (_events.ContainsKey(eventType)) {
        _events[eventType] = Delegate.Remove(_events[eventType], listener);

        if (_events[eventType] == null) {
          _events.Remove(eventType);
        }
      }
    }
  }

  public static void Raise<T>(T eventData) {
    Type eventType = typeof(T);
    Delegate listeners;

    lock (_lock) {
      if (!_events.TryGetValue(eventType, out listeners)) {
        return;
      }
    }

    try {
      (listeners as Action<T>)?.Invoke(eventData);
    }
    catch (Exception ex) {
      Debug.LogError($"{eventType.Name}: {ex.Message}");
      Debug.LogException(ex);
    }
  }
}