using Autofac.Unity.Tests.Services;
using UnityEngine;

namespace Autofac.Unity.Tests.Scripts
{
    public class Player : MonoBehaviour
    {
        public Inventory Inventory { get; set; }

        public TimeAccessor TimeAccessor { get; set; }
    }
}
