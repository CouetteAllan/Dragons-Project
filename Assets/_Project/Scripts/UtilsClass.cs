using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rayqdr.Utils
{
    public static class UtilsClass
    {
        public static Camera MainCamera
        {
            get
            {
                if(_camera == null)
                {
                    _camera = Camera.main;
                }
                return _camera;
            }
        }
        private static Camera _camera;

        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, MainCamera);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, MainCamera);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        public static Vector3 GetDirToMouse(Vector3 fromPosition)
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            return (mouseWorldPosition - fromPosition).normalized;
        }

    }
}

