using System;
using System.Collections;
using UnityEngine;

namespace CharacterStudio
{
    public class SceneTransitionManager : MonoSingleton<SceneTransitionManager>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] float _transitionTime = 1f;
        EventSubscription<TransitionArg> _transitionSubscription;
        protected override bool Awake()
        {
            _transitionSubscription = EventBus.Instance.Subscribe<TransitionArg>(OnTransition);
            return base.Awake();
        }
        protected override void OnDestroy()
        {
            EventBus.Instance.Unsubscribe(_transitionSubscription);
            base.OnDestroy();
        }
        private void OnTransition(TransitionArg arg)
        {
            StartCoroutine(Cor_Transition(arg));
        }
        IEnumerator Cor_Transition(TransitionArg arg)
        {
            _animator.SetTrigger("In");
            yield return new WaitForSeconds(_transitionTime);
            UnityEngine.SceneManagement.SceneManager.LoadScene(arg.SceneName);
            _animator.SetTrigger("Out");
        }

    }

    public class TransitionArg : EventArgs
    {
        public string SceneName { get; private set; }
        public TransitionArg(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}
