using UnityEngine;
using Zenject;

namespace TapToKill {
    public class GameSceneInstaller : MonoInstaller {

        public Camera inputCam;
        public SceneRenderConfig renderConfig;

        public GameObject positivePopupPooler;
        public GameObject negativePopupPooler;

        public Transform gameField;

        public override void InstallBindings() {
            Container.BindInstance(inputCam).WithId(CameraType.InputCamera).AsSingle();
            Container.BindInstance(renderConfig).AsSingle();
            Container.Bind<IObjectSelector>().To<Raycast3DSelector>().AsSingle();
            Container.BindInstance(positivePopupPooler.GetComponent<IObjectPooler>())
                .WithId(PopupType.PositivePopup);
            Container.BindInstance(negativePopupPooler.GetComponent<IObjectPooler>())
                .WithId(PopupType.NegativePopup);
            Container.BindInstance(gameField).AsSingle();
        }
    }
}
