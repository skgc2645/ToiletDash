using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraTracking : MonoBehaviour
{

    [SerializeField] GameObject obj;

    //member
    
    //COEF
    Vector2 ROTATE_SPEED = new Vector2(4f,4f);    //âÒì]ë¨ìx(å„ÅXUIÇ©ÇÁê›íËÇ≈Ç´ÇÈÇÊÇ§Ç…ÇµÇΩÇ¢)
    const float BOT_VERT_LIMIT = -58f;
    const float TOP_VERT_LIMIT = 85f;

    Vector3 dir;
    float _vertAngle = 0;


    void Rotate()
    {
        transform.position = Player.Instance.PlayerHipPos + dir * 1f;

        float nowMouseValueX = Input.GetAxis("Mouse X");
        float nowMouseValueY = Input.GetAxis("Mouse Y");

        var newAngle = Vector3.zero;
        newAngle.x = ROTATE_SPEED.x * nowMouseValueX;
        transform.RotateAround(Player.Instance.PlayerHipPos, Vector3.up, newAngle.x);

        newAngle.y = ROTATE_SPEED.y * nowMouseValueY;
        _vertAngle += newAngle.y;
        if (_vertAngle < TOP_VERT_LIMIT && _vertAngle > BOT_VERT_LIMIT)
        {
            transform.RotateAround(Player.Instance.PlayerHipPos, transform.right, -newAngle.y);
        }
            AdjustCameraPos();
    }


    void AdjustCameraPos()
    {
        dir = transform.position - Player.Instance.PlayerHipPos;
        float dis   = Vector3.Distance(Player.Instance.PlayerHipPos, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(Player.Instance.PlayerHipPos, dir, out hit, dis))
        {
            transform.position = hit.point;
        }
    }


    void Initialize()
    {
        dir = transform.position - Player.Instance.PlayerHipPos;
    }


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance._goal.Value && GameManager.Instance._isGame.Value)
        {
            Rotate();
        }
        else
        {

        }
    }
}
