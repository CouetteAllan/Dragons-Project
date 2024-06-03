using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovements),typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovements _movements;
    private PlayerInputs _inputs;

    private void Awake()
    {
        _movements= GetComponent<PlayerMovements>();
        _inputs= GetComponent<PlayerInputs>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _movements.UpdateMovement(_inputs.Dir.normalized);
    }
}
