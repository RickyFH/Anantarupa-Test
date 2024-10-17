using UnityEngine;
using System.Collections.Generic;

namespace ProgrammerTest
{
	public class PlayerController : MonoBehaviour
	{
		    [SerializeField] private Transform playerView;
            private bool _facingRight = true;
            private const float Speed = 10f;
            private const int DIRECTION = 1;
            private List<ICollision> _detections;
            private const int InitialValue = 0;
            private void Awake() => _detections = new List<ICollision>();

            private void Update()
            {
                if (Input.GetKey(KeyCode.A))
                {
                    MovingCalculation(false);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    MovingCalculation(true);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    OnInputEnterDo();
                }
            }
        
            private void OnInputEnterDo()
            {
                if (_detections.Count != 0)
                {
                    _detections[InitialValue].OnTriggerDo(transform.position);
                }
            }
        		
            private void MovingCalculation(bool isRight)
            {
                MoveHorizontal(isRight ? Vector3.right : Vector3.left);
                if (_facingRight != isRight)
                {
                    _facingRight = !_facingRight;
                    UpdateFacing();
                }
            }
        
            private void MoveHorizontal(Vector3 direction) => transform.position += (direction * (Speed * Time.deltaTime));

        
            private void UpdateFacing() => playerView.localScale = new Vector3(_facingRight ? DIRECTION : -DIRECTION, DIRECTION, DIRECTION);

        
        
            private void OnTriggerEnter(Collider other)
            {
                if (!other.TryGetComponent(out ICollision detection)) return;
                _detections.Add(detection);
                detection.OnGettingCloseDo();
            }
        
            private void OnTriggerExit(Collider other)
            {
                if (!other.TryGetComponent(out ICollision detection)) return;
                _detections.Remove(detection);
                detection.OnExitDo();
            }
	}
}