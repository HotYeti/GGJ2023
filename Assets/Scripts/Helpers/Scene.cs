using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
    public class Scene : MonoBehaviour
    {
        public void AnotherRound()
        {
            SceneManager.LoadScene(0);
        }
    }
}