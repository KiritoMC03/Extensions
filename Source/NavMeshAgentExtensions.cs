using System;
using System.Collections;
using JoyKirito.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace JoyKirito.Extensions
{
    public static class NavMeshAgentExtensions
    {
        public static void SetDestination(this NavMeshAgent agent, Vector3 target, Action onComplete)
        {
            agent.SetDestination(target);
            agent.InvokeOnComplete(onComplete);
        }
        
        public static void InvokeOnComplete(this NavMeshAgent agent, Action action)
        {
            if (agent.IsNull() || action.IsNull()) return;
            if (!agent.TryGetComponent(out EmptyMonoBehaviour emptyMonoBehaviour))
                emptyMonoBehaviour = agent.gameObject.AddComponent<EmptyMonoBehaviour>();
            emptyMonoBehaviour.StartCoroutine(WaitCompletionRoutine(agent, action, emptyMonoBehaviour));
        }

        private static IEnumerator WaitCompletionRoutine(NavMeshAgent agent, Action action,
            EmptyMonoBehaviour emptyMonoBehaviour)
        {
            Transform transform = agent.transform;
            Vector3 targetPose = agent.destination;
            while (agent.velocity.sqrMagnitude > 0.01f || 
                   Vector3.Distance(transform.position, targetPose) > 0.1f)
                yield return null;
            UnityEngine.Object.DestroyImmediate(emptyMonoBehaviour);
            action?.Invoke();
        }
    }
}
