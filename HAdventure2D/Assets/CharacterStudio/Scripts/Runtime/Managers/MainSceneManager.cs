using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class MainSceneManager : MonoSingleton<MainSceneManager>
    {
        [SerializeField] CSPresettingLayout _presettingLayout;
        protected override bool Awake()
        {
            DontDestroyOnLoad(gameObject);
            return base.Awake();
        }
        void Start()
        {
            DataManager.Instance.Load();
            if (string.IsNullOrEmpty(DataManager.Instance.SaveData.DataFolderPath))
            {
                _presettingLayout.gameObject.SetActive(true);
                _presettingLayout.Setup();
                return;
            }
            DataManager.Instance.InitConfigs();
            EventBus.Instance.Publish(new TransitionArg("CharacterStudio"));
        }
    }
}
