using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private static int _health = 100, _timeTravelJuice = 100;

    public static List<Transform> TriggerObjects = new List<Transform>();

    private GameObject[] _allGameObjects;

    private Vector3 _forward, _right, _up;
    private float _gameObjectOffset;
    private int _indexTriggerObject = -1;

    private bool _jump;

    private readonly float _jumpHeight = 3.0f;

    private bool _leftTriggerDown;

    [SerializeField] private float _moveSpeed = 4.0f;

    private RaycastHit _objectHit;

    private GameObject _raycastObject;

    private Rigidbody _rigidbody;

    private bool _stickDownLastRight, _stickDownLastLeft, _lockOn, _rotating, _isDeviceTraveling, _armToggle = true;

    public Transform ActiveGameObject;

    public float anglex, angley, anglez;

    public Transform Arm, Line;

    public Vector3 CenterPt;

    public Transform Device;

    private JoystickDirection joystickDirection;

    private List<Transform> leftList = new List<Transform>();

    private Transform leftObject = null;

    private List<Transform> leftRightList = new List<Transform>();
    private Transform newObject;

    public Vector3 Offset = new Vector3(0f, 0, 10f);
    public float Radius;
    private List<Transform> rightList = new List<Transform>();

    private Transform rightObject = null;
    public float RotateSpeed;
    public List<Transform> SwitchList = new List<Transform>();

    public static int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public static int TimeTravelJuice
    {
        get { return _timeTravelJuice; }
        set { _timeTravelJuice = value; }
    }

    private void Awake()
    {
        _allGameObjects = FindObjectsOfType<GameObject>();
    }

    private void Start()
    {
        _forward = Camera.main.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);

        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;

        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !_jump)
        {
            StartCoroutine(Jump());
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
                _moveSpeed = 10.0f;
            else if (Input.GetKeyUp(KeyCode.LeftShift)) _moveSpeed = 4.0f;
            Move();
        }

        var forwardDRAW = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forwardDRAW, Color.red);

        if (Input.GetKey(KeyCode.K)) Arm.Rotate(RotateSpeed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.L)) Arm.Rotate(-RotateSpeed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.I)) Arm.Rotate(0, RotateSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.O)) Arm.Rotate(0, -RotateSpeed * Time.deltaTime, 0);
        if (_armToggle) CheckForHit();
        if (Input.GetButton("AButton"))
        {
            print("a");
            Device.transform.localPosition = new Vector3(0.15f, 0.313f, 1.424f);
        }

        if (Input.GetButton("BButton"))
        {
            print("b");
            Arm.gameObject.SetActive(false);
            _armToggle = false;
        }

        if (Input.GetButton("XButton"))
        {
            print("x");
            Arm.gameObject.SetActive(true);
            _armToggle = true;
        }

        if (Input.GetButton("YButton")) print("y");
        if (Input.GetButtonDown("LeftBumper"))
        {
        }

        if (Input.GetButtonDown("RightBumper"))
        {
        }

        if (Input.GetAxis("Trigger") > 0.001)
        {
        }

        if (Input.GetAxis("Trigger") > 0.001 && Input.GetButton("BButton"))
        {
        }

        if (Input.GetAxis("Trigger") < 0 && Input.GetButton("XButton")) SetGlobalRewind();

        xBoxButtons();
        RotateArm();
        //Left(newObject, TriggerObjects);
        //Right(newObject, TriggerObjects);
        //LeftRight(newObject, TriggerObjects);

        if (SwitchObject() != null)
            Arm.LookAt(SwitchObject());

        if (Input.GetAxisRaw("RightJoyStickHorizontal") != 1 || Input.GetAxisRaw("RightJoyStickHorizontal") != -1)
        {
        }
    }

    private List<Transform> CheckObjectLeft(Transform objectCheck, List<Transform> listToCheck)
    {
        leftList.Clear();
        var sortLeftList = new List<Transform>();

        foreach (var child in listToCheck)
        {
            if (child == objectCheck) continue;
            var right = child.transform.TransformDirection(Vector3.right);

            var toOther = objectCheck.transform.position - child.transform.position;

            if (Vector3.Dot(right, toOther) > 0)
                if (!leftList.Contains(child))
                    leftList.Add(child);
            sortLeftList = leftList.OrderByDescending(x => x.position.x).ToList();
        }

        return sortLeftList;
    }

    private List<Transform> CheckObjectRight(Transform objectCheck, List<Transform> listToCheck)
    {
        rightList.Clear();
        var sortRightList = new List<Transform>();

        foreach (var child in listToCheck)
        {
            if (child == objectCheck) continue;
            var right = child.transform.TransformDirection(Vector3.right);

            var toOther = objectCheck.transform.position - child.transform.position;

            if (Vector3.Dot(right, toOther) < 0) rightList.Add(child);
            sortRightList = rightList.OrderByDescending(x => x.position.x).ToList();
        }

        return sortRightList;
    }

    private List<Transform> Left(Transform objectCheck, List<Transform> triggerObjectList)
    {
        if (objectCheck == null || triggerObjectList.Count == 0) return null;
        leftList.Clear();
        rightList.Clear();
        foreach (var child in triggerObjectList)
        {
            var relativePoint = objectCheck.InverseTransformPoint(child.position);
            if (relativePoint.x > 0.0)
                if (!leftList.Contains(child) || child == objectCheck)
                {
                    print("Object is to the left " + child);
                    leftList.Add(child);
                }

            //else if (relativePoint.x < 0.0)
            //{
            //    if (!rightList.Contains(child))
            //    {
            //        print("Object is to the right " + child);
            //        rightList.Add(child);
            //    }
            //}
        }

        leftList = leftList.OrderByDescending(x => x.position.x).ToList();
        foreach (var transform1 in leftList) print(transform1);
        return leftList;
        //rightList = rightList.OrderBy(x => x.position.x).ToList();
    }

    private List<Transform> Right(Transform objectCheck, List<Transform> triggerObjectList)
    {
        if (objectCheck == null || triggerObjectList.Count == 0) return null;
        leftList.Clear();
        rightList.Clear();
        foreach (var child in triggerObjectList)
        {
            var relativePoint = objectCheck.InverseTransformPoint(child.position);
            if (relativePoint.x < 0.0)
                if (!rightList.Contains(child) || child == objectCheck)
                {
                    print("Object is to the right " + child);
                    rightList.Add(child);
                }
        }

        rightList = rightList.OrderByDescending(x => x.position.x).ToList();
        foreach (var transform1 in rightList) print(transform1);
        return rightList;
    }

    private Transform SwitchObject()
    {
        var rightJoyStick = (int) Input.GetAxisRaw("RightJoyStickHorizontal");

        if (Input.GetAxis("Trigger") < 0 || Input.GetKey(KeyCode.LeftShift))
        {
            if (TriggerObjects.Count == 0)
            {
                print("No Objects in Area ");
                return null;
            }

            //newObject = ClosestObjects(TriggerObjects, Arm);

            //if (_indexTriggerObject == -1)
            //{
            //    newObject = ObjectFound(TriggerObjects, Arm, JoystickDirection.noDirection);
            //}
            //else
            //{
            //    newObject = TriggerObjects[_indexTriggerObject];
            //}
            //_indexTriggerObject = TriggerObjects.IndexOf(ClosestObjects(TriggerObjects, Arm));
            //if (_indexTriggerObject == -1)
            //{
            //    newObject = ClosestObjects(TriggerObjects, Arm);
            //}
            //else
            //{
            //    newObject = TriggerObjects[_indexTriggerObject];
            //}

            if (rightJoyStick == 1 || Input.GetKey(KeyCode.L))
            {
                if (!_stickDownLastRight)
                    if (_indexTriggerObject < TriggerObjects.Count - 1)
                    {
                        var currentObject = newObject;
                        //currentObject = CheckObjectLeft(currentObject, TriggerObjects)[0];
                        //currentObject = Right(currentObject, TriggerObjects)[0];
                        //_indexTriggerObject = TriggerObjects.IndexOf(currentObject);
                        currentObject = ObjectFound(TriggerObjects, currentObject, JoystickDirection.right);
                        _indexTriggerObject = TriggerObjects.IndexOf(currentObject);
                        _stickDownLastRight = true;
                    }

                //Debug.Log("+1 Switch!");
            }
            else
            {
                _stickDownLastRight = false;
            }

            if (rightJoyStick == -1 || Input.GetKey(KeyCode.K))
            {
                if (!_stickDownLastLeft)
                    if (_indexTriggerObject >= 0)
                    {
                        var currentObject = newObject;
                        //currentObject = CheckObjectRight(currentObject, TriggerObjects)[0];
                        //currentObject = Left(currentObject, TriggerObjects)[0];
                        //_indexTriggerObject = TriggerObjects.IndexOf(currentObject);
                        currentObject = ObjectFound(TriggerObjects, currentObject, JoystickDirection.left);
                        _indexTriggerObject = TriggerObjects.IndexOf(currentObject);
                        _stickDownLastLeft = true;
                        //print("Current Object " + currentObject);
                    }

                //Debug.Log("-1 Switch!");
            }
            else
            {
                _stickDownLastLeft = false;
            }
        }
        else
        {
            _leftTriggerDown = false;
            _indexTriggerObject = -1;
            newObject = null;
        }

        return newObject;
    }

    private Transform ClosestObjects(List<Transform> triggerObjects, Transform player)
    {
        Transform tMin = null;
        var minDist = Mathf.Infinity;
        var currentPos = player.position;
        foreach (var t in triggerObjects)
        {
            var dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }

        if (tMin.gameObject.GetComponent<SphereCollider>() == null) tMin.gameObject.AddComponent<SphereCollider>().radius = 3;
        return tMin;
    }

    private Transform ObjectFound(List<Transform> triggerObjects, Transform originCheck, JoystickDirection direction)
    {
        //Transform startingObject = null;
        Transform finalObject = null;

        switch (direction.ToString())
        {
            case "left":
                leftList.Clear();
                foreach (var child in triggerObjects)
                {
                    var relativePoint = originCheck.InverseTransformPoint(child.position);
                    if (relativePoint.x > 0.0)
                        if (!leftList.Contains(child) || child == originCheck)
                            leftList.Add(child);
                }

                leftList = leftList.OrderByDescending(x => x.position.x).ToList();

                finalObject = leftList[0];
                print("Final Object " + finalObject);
                break;

            case "right":
                rightList.Clear();
                foreach (var child in triggerObjects)
                {
                    var relativePoint = originCheck.InverseTransformPoint(child.position);
                    if (relativePoint.x < 0.0)
                        if (!rightList.Contains(child) || child == originCheck)
                            rightList.Add(child);
                }

                rightList = rightList.OrderByDescending(x => x.position.x).ToList();
                finalObject = rightList[0];
                print("Final Object " + finalObject);
                break;

            case "noDirection":
                finalObject = ClosestObjects(triggerObjects, originCheck);
                break;
        }

        return finalObject;
    }

    private void CheckForHit()
    {
        if (_raycastObject == null) return;

        var fwd = _raycastObject.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(_raycastObject.transform.position, fwd * 15, Color.green);
        if (Physics.Raycast(_raycastObject.transform.position, fwd, out _objectHit, 15))
            if (_objectHit.transform.GetComponent<RewindTime>() != null)
            {
                _objectHit.transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                _lockOn = true;
            }

        if (_raycastObject == null) _lockOn = false;
        if (_objectHit.transform != null)
            if (_lockOn && _objectHit.transform.GetComponent<RewindTime>() != null)
                StartCoroutine(DeviceTravel());

        if (_lockOn) Arm.LookAt(_objectHit.transform);
    }

    private IEnumerator DeviceTravel()
    {
        MoveDevice(Device, _objectHit.transform);
        while (_isDeviceTraveling == false) yield return null;

        SetDeviceUp(Device, _objectHit.transform);

        var rewindTime = _objectHit.transform.GetComponent<RewindTime>();
        rewindTime.isActive = true;
        print("start rewind ");
    }

    private void MoveDevice(Transform device, Transform destinationObject)
    {
        if (_isDeviceTraveling == false) device.Translate(0, 0, 5 * Time.deltaTime);

        if ((destinationObject.transform.position - device.position).magnitude < 3.5f) _isDeviceTraveling = true;
    }

    private Vector3 RandomVector()
    {
        return new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
    }

    private float RandomFloat()
    {
        float newFloat = Random.Range(100, 500);
        return newFloat;
    }

    private void SetDeviceUp(Transform device, Transform destinationObject)
    {
        if (_rotating == false)
        {
            var offsetNew = -destinationObject.transform.forward;
            _gameObjectOffset = destinationObject.GetComponent<Collider>().bounds.size.z / 2;
            offsetNew *= _gameObjectOffset + 2f;

            device.position = destinationObject.transform.position + offsetNew;
            device.rotation = Quaternion.Euler(destinationObject.transform.rotation.eulerAngles);
            _rotating = true;
        }
        else
        {
            //print("Random Vector " + RandomVector().x + " , " + RandomVector().y + " , " + RandomVector().z);
            device.transform.RotateAround(destinationObject.position, Vector3.up, 100 * Time.deltaTime);
            // device.transform.RotateAround(destinationObject.position, RandomVector(), RandomFloat() * Time.deltaTime);
        }
    }

    private void Move()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (direction.magnitude > 0.1f)
        {
            var rightMovement = _right * _moveSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
            var upMovement = _forward * _moveSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
            var heading = Vector3.Normalize(rightMovement + upMovement);
            var headingAngle = Quaternion.LookRotation(_forward).eulerAngles.y;

            transform.forward = heading;

            transform.position += rightMovement;
            transform.position += upMovement;
        }
    }

    private void xBoxButtons()
    {
        var direction = new Vector3(Input.GetAxisRaw("LeftJoyStickHorizontal"), 0, Input.GetAxisRaw("LeftJoyStickVertical"));

        if (direction.magnitude > 0.1f)
        {
            var rightMovement = _right * _moveSpeed * Time.deltaTime * Input.GetAxisRaw("LeftJoyStickHorizontal");
            var upMovement = _forward * _moveSpeed * Time.deltaTime * Input.GetAxisRaw("LeftJoyStickVertical");
            var heading = Vector3.Normalize(rightMovement + upMovement);
            var headingAngle = Quaternion.LookRotation(_forward).eulerAngles.y;

            transform.forward = heading;

            transform.position += rightMovement;
            transform.position += upMovement;

            //this.GetComponent<Rigidbody>().AddForce(movement * moveSpeed);
        }
    }

    private void RotateArm()
    {
        if (_leftTriggerDown == false)
        {
            var angH = Input.GetAxis("RightJoyStickHorizontal") * 30;
            var angV = Input.GetAxis("RightJoyStickVertical") * 30;
            Arm.localEulerAngles = new Vector3(angV, angH, 0);
        }
    }

    private IEnumerator Jump()
    {
        var originalHeight = transform.position.y;

        _jump = true;
        yield return null;

        _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);

        while (transform.position.y > originalHeight) yield return null;
        _jump = false;

        yield return null;
    }

    private void SetGlobalRewind()
    {
        foreach (var rewindGameObject in _allGameObjects)
            if (rewindGameObject != null)
                if (rewindGameObject.GetComponent<RewindTime>() != null)
                    rewindGameObject.GetComponent<RewindTime>().isActive = true;
    }

    private enum JoystickDirection
    {
        left,
        right,
        noDirection
    }
}