using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask Load(string name)
        {
            if (SceneManager.GetActiveScene().name == name)
                return;
      
            await SceneManager.LoadSceneAsync(name);
        }
    }
}