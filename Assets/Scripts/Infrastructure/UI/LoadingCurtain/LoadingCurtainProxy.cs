using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.UI.LoadingCurtain
{
    public class LoadingCurtainProxy : ILoadingCurtain
    {
        private readonly LoadingCurtain.Factory _factory;
        private ILoadingCurtain _impl;
        private int _value;
        
        public LoadingCurtainProxy(LoadingCurtain.Factory factory) => 
            _factory = factory;

        public async UniTask InitializeAsync() => 
            _impl = await _factory.Create(InfrastructureAssetPath.CurtainPath);

        public void Show() => _impl.Show();

        public void Hide() => _impl.Hide();
    }
}