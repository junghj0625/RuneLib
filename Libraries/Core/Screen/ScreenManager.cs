using System.Collections.Generic;
using UnityEngine;



namespace Rune
{
    public class ScreenManager : MonoPlusSingleton<ScreenManager>
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _resolution.OnChange.AddListener(OnChangeResolution);
            _screenMode.OnChange.AddListener(OnChangeScreenMode);
        }

        public override void Refresh()
        {
            base.Refresh();

            _resolution.Refresh();
            _screenMode.Refresh();
        }

        public override void Update()
        {
            base.Update();

            CheckScreenChange();
        }


        public static ResolutionType GetBestResolutionType()
        {
            int screenWidth = Screen.currentResolution.width;
            int screenHeight = Screen.currentResolution.height;

            ResolutionType bestType = ResolutionType._1280;

            foreach (var (type, res) in _resolutionLookup)
            {
                if (screenWidth >= res.x && screenHeight >= res.y)
                {
                    if (res.x > _resolutionLookup[bestType].x)
                    {
                        bestType = type;
                    }
                }
            }

            return bestType;
        }

        public static FullScreenMode GetBestScreenMode()
        {
            return FullScreenMode.FullScreenWindow;
        }



        private void RefreshScreen()
        {
            Vector2Int resolution = _resolutionLookup[_resolution.Value];

            var screenMode = _screenMode.Value;

            Screen.SetResolution(resolution.x, resolution.y, screenMode);
        }

        private void CheckScreenChange()
        {
            var res = _resolutionLookup[Resolution];

            if (res.x != Screen.width || res.y != Screen.height || ScreenMode != Screen.fullScreenMode)
            {
                RefreshResolutionAndScreenModeWithoutNotify();
            }
        }

        private void RefreshResolutionAndScreenModeWithoutNotify()
        {
            _screenMode.SetValueWithoutNotify(Screen.fullScreenMode);


            int width = Screen.width;
            int height = Screen.height;

            foreach (var (type, res) in _resolutionLookup)
            {
                if (res.x == width && res.y == height)
                {
                    _resolution.SetValueWithoutNotify(type);

                    break;
                }
            }
        }


        private void OnChangeResolution(ResolutionType value)
        {
            RefreshScreen();
        }

        private void OnChangeScreenMode(FullScreenMode value)
        {
            RefreshScreen();
        }


        
        private readonly Attribute<ResolutionType> _resolution = new(ResolutionType._1920);
        public static ResolutionType Resolution { get => SingletonInstance._resolution.Value; set => SingletonInstance._resolution.Value = value; }

        private readonly Attribute<FullScreenMode> _screenMode = new(FullScreenMode.FullScreenWindow);
        public static FullScreenMode ScreenMode { get => SingletonInstance._screenMode.Value; set => SingletonInstance._screenMode.Value = value; }



        public static readonly Dictionary<ResolutionType, Vector2Int> _resolutionLookup = new()
        {
            [ResolutionType._1280] = new Vector2Int(1280, 720),
            [ResolutionType._1920] = new Vector2Int(1920, 1080),
            [ResolutionType._2560] = new Vector2Int(2560, 1440),
            [ResolutionType._3840] = new Vector2Int(3840, 2160),
        };



        public enum ResolutionType
        {
            _1280,
            _1920,
            _2560,
            _3840,
        }
    }
}