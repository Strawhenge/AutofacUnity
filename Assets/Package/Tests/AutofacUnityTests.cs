﻿using Autofac.Unity.Tests.Scripts;
using Autofac.Unity.Tests.Services;
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
        const string sceneName = "TestScene";
        const string scenePath = "Assets/Package/Tests/TestScene.unity";

        Player _player;
        Item _playerItem;
        Enemy _enemy;
        HitPoint _enemyHitPoint;
        Vendor _vendor;

        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            DependencyInjection.Configure();

            var sceneLoad = EditorSceneManager.LoadSceneAsyncInPlayMode(scenePath, new LoadSceneParameters(LoadSceneMode.Additive));
            sceneLoad.completed += _ =>
            {
                _player = Object.FindObjectOfType<Player>();
                _playerItem = Object.FindObjectOfType<Item>();
                _enemy = Object.FindObjectOfType<Enemy>();
                _enemyHitPoint = _enemy.GetComponentInChildren<HitPoint>();
                _vendor = Object.FindObjectOfType<Vendor>();
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
            Assert.NotNull(_player.Health);
            Assert.NotNull(_player.TimeAccessor);

            Assert.NotNull(_playerItem.Health);

            Assert.NotNull(_enemy.Inventory);
            Assert.NotNull(_enemy.TimeAccessor);
            Assert.NotNull(_enemy.Health);

            Assert.NotNull(_enemyHitPoint.Health);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ScopedPropertiesShouldBeUniqueByScope()
        {
            Assert.False(
                ReferenceEquals(_player.Inventory, _enemy.Inventory));

            Assert.True(
                ReferenceEquals(_enemy.Health, _enemyHitPoint.Health));

            yield return null;
        }

        [UnityTest]
        public IEnumerator ScopedPropertiesShouldBeUniqueByNestedScope()
        {
            Assert.False(
                ReferenceEquals(_player.Health, _playerItem.Health));

            yield return null;
        }

        [UnityTest]
        public IEnumerator SingletonPropertiesShouldBeTheSame()
        {
            Assert.True(
                ReferenceEquals(_player.TimeAccessor, _enemy.TimeAccessor));

            yield return null;
        }

        [UnityTest]
        public IEnumerator PropertiesShouldBeInjectedInAccordanceToScopeConfiguration()
        {
            Assert.IsInstanceOf<Inventory>(_player.Inventory);
            Assert.IsInstanceOf<Inventory>(_enemy.Inventory);

            Assert.IsInstanceOf<VendorInventory>(_vendor.Inventory);

            yield return null;
        }
    }
}
