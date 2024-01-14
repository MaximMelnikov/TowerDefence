using Cysharp.Threading.Tasks;

namespace Core.SceneLoader
{
    public interface ISceneLoader
    {
        public UniTask Load(string name);
    }
}