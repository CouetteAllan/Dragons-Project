using System;
using UnityEngine;
namespace UtilsClass
{
    public static class Utils
    {
        public static Camera MainCamera
        {
            get
            {
                if(_mainCamera == null)
                    _mainCamera = Camera.main;
                return _mainCamera;
            }
        }

        private static Camera _mainCamera;
    }
}
