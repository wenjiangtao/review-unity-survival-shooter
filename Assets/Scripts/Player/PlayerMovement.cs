using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    
    private Vector3 _movement;
    private Animator _animator;
    private Rigidbody _playerRigidbody;
    private int _floorMask;
    private float _camRayLength = 100f;
    
    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _animator = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Move(h, v);
        Turning();
        Animating(h, v);
    }
    
    void Move(float h, float v)
    {
        _movement.Set(h, 0f, v);
        _movement = _movement.normalized * speed * Time.deltaTime;
        _playerRigidbody.MovePosition(transform.position + _movement);
    }
    
    void Turning()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(cameraRay, out floorHit, _camRayLength, _floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            
            Quaternion rotation = Quaternion.LookRotation(playerToMouse);
            _playerRigidbody.MoveRotation(rotation);
        }
    }
    
    void Animating(float h, float v)
    {
        bool isWalking = h != 0f || v != 0f;
        _animator.SetBool("IsWalking", isWalking);
    }
    
    
}
