using Autofac.Unity.Tests.Scripts;
using NUnit.Framework;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Autofac.Unity.Tests
{
    public class AutofacUnityTests
    {
        const string sceneName = "AutofacUnity";
        const string scenePath = "Assets/Package/Tests/AutofacUnity.unity";

        Player _player;

        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            DependencyInjection.Configure();

            var sceneLoad = EditorSceneManager.LoadSceneAsyncInPlayMode(scenePath, new LoadSceneParameters(LoadSceneMode.Additive));
            sceneLoad.completed += _ =>
            {
                _player = Object.FindObjectOfType<Player>();
            };

            while (!sceneLoad.isDone)
                yield return null;
        }

        [UnityTearDown]
        public IEnumerator UnloadScene()
        {
            yield return SceneManager.UnloadSceneAsync(sceneName);
        }

        [UnityTest]
        public IEnumerator PropertiesShouldBeInjected()
        {
            Assert.NotNull(_player.Inventory);
            yield return null;
        }
    }
}
