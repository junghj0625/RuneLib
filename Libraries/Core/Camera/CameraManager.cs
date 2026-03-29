using UnityEngine;



namespace Rune
{
    public class CameraManager : MonoPlusSingleton<CameraManager>
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _mainCameraInstance.OnChange.AddListener(OnChangeMainCameraInstance);
        }

        public override void Refresh()
        {
            base.Refresh();

            _mainCameraInstance.Refresh();
        }



        public static Camera MainCamera
        {
            get => SingletonInstance._mainCameraInstance.Value != null ? SingletonInstance._mainCameraInstance.Value : Camera.main;
        }



        public static LooseEvent<Camera> OnChangeMainCamera { get; } = new();



        private void OnChangeMainCameraInstance(Camera value)
        {
            OnChangeMainCamera.Invoke(value);
        }



        private readonly Attribute<Camera> _mainCameraInstance = new(null);
    }
}