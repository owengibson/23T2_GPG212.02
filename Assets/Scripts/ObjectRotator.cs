using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPG212_02
{
    public class ObjectRotator : MonoBehaviour
    {
        private Vector3 _startRotation;
        private float _sensitivity;
        private Vector3 _mouseReference;
        private Vector3 _mouseOffset;
        private Vector3 _rotation;
        private bool _isRotating;

        [SerializeField] private Camera camera;

        void Start()
        {
            _startRotation = transform.eulerAngles;

            _sensitivity = 0.4f;
            _rotation = Vector3.zero;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (DetectObjectWithRaycast() == gameObject)
                {
                    _isRotating = true;
                    _mouseReference = Input.mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isRotating = false;
            }
        }

        private void FixedUpdate()
        {
            if (_isRotating)
            {
                // offset
                _mouseOffset = (Input.mousePosition - _mouseReference);

                // apply rotation
                _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

                // rotate
                transform.Rotate(_rotation);

                // store mouse
                _mouseReference = Input.mousePosition;
            }
        }

        private GameObject DetectObjectWithRaycast()
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider.gameObject;
            }

            return null;
        }

        public void ResetRotation()
        {
            StartCoroutine(ChangeRotationOverTime(transform, transform.eulerAngles, _startRotation, 1.5f));
        }

        private IEnumerator ChangeRotationOverTime(Transform transform, Vector3 start, Vector3 end, float time)
        {
            float counter = 0f;

            while (counter < time)
            {
                counter += Time.deltaTime;
                transform.eulerAngles = Vector3.Lerp(start, end, counter / time);

                yield return null;
            }
        }
    }
}