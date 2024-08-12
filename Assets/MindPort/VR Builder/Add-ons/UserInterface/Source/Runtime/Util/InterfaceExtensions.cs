using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace VRBuilder.UI.Utils
{
    public static class InterfaceExtensions
    {
        public static IEnumerable<T> FindInterfaceOfType<T>()
        {
            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var rootGameObject in rootGameObjects)
            {
                var childrenInterfaces = rootGameObject.GetComponentsInChildren<T>();
                foreach (var childInterface in childrenInterfaces)
                {
                    yield return childInterface;
                }
            }
        }
    }
}
