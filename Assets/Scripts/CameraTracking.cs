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
    Vector3 dir;

    void Rotate()
    {
        transform.position = Player.Instance.transform.position + dir * 1f;

        float nowMouseValueX = Input.GetAxis("Mouse X");
        float nowMouseValueY = Input.GetAxis("Mouse Y");

        var newAngle = Vector3.zero;
        newAngle.x = ROTATE_SPEED.x * nowMouseValueX;
        newAngle.y = ROTATE_SPEED.y * nowMouseValueY;

        transform.RotateAround(Player.Instance.transform.position, Vector3.up, newAngle.x);
        transform.RotateAround(Player.Instance.transform.position, transform.right, -newAngle.y);
        AdjustCameraPos();

    }


    void AdjustCameraPos()
    {
        dir = transform.position - Player.Instance.transform.position;
        float dis   = Vector3.Distance(Player.Instance.transform.position, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(Player.Instance.transform.position, dir, out hit, dis))
        {
            Debug.Log("tag:" + hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "obj")
            {
                transform.position = hit.point;
            }

            Debug.DrawRay(Player.Instance.transform.position, dir * dis, Color.red);
        }
    }


    void Initialize()
    {
        dir = transform.position - Player.Instance.transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
}
