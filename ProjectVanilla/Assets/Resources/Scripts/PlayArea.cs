namespace Assets.Resources.Scripts
{
    internal class PlayArea
    {
        //private Transform SwitchObjects()
        //{
        //    Transform newObject = null;
        //    int rightJoyStick = (int)Input.GetAxisRaw("RightJoyStickHorizontal");
        //    print("RIGHT JOYSTICK " + rightJoyStick);
        //    Transform switchObject = null;
        //    //if (Input.GetAxis("Trigger") < 0 || Input.GetKey(KeyCode.U))
        //    {
        //        print("Trigger Down ");

        //        //newObject = ClosestObjects(triggerObjects, arm);

        //        newObject = triggerObjects[indexTriggerObject];

        //        if (rightJoyStick == 1 || Input.GetKeyDown(KeyCode.L))
        //        {
        //            //switchList.Clear();
        //            if (axisInUse == false)
        //            {
        //                switchList.Clear();
        //                //StartCoroutine(PopulateList(newObject));
        //                foreach (Transform child in triggerObjects)
        //                {
        //                    if (newObject != null)
        //                    {
        //                        if (child.name != newObject.name)
        //                        {
        //                            switchList.Add(child);
        //                            //print(child.name);
        //                        }
        //                    }
        //                }

        //                switchObject = ClosestObjects(switchList, newObject);

        //                Vector3 right = arm.transform.TransformDirection(Vector3.right);

        //                Vector3 toOther = switchObject.transform.position - arm.transform.position;

        //                if (Vector3.Dot(right, toOther) > 0)
        //                {
        //                    //print("New Object right " + newObject.name);

        //                    newObject = switchObject;
        //                    indexTriggerObject = triggerObjects.IndexOf(a => a.name == switchObject.name);
        //                }
        //                axisInUse = true;
        //                Debug.Log("+1 Switch!");
        //            }
        //            //else if (Input.GetAxisRaw("RightJoyStickHorizontal") == -1)
        //            //{
        //            //    foreach (Transform child in triggerObjects)
        //            //    {
        //            //        if (child.name != newObject.name)
        //            //        {
        //            //            switchList.Add(child);
        //            //        }
        //            //    }
        //            //    //print("Closet Object to New Object : " + ClosestObjects(switchList, newObject));
        //            //    switchObject = ClosestObjects(switchList, newObject);

        //            //    Vector3 right = arm.transform.TransformDirection(Vector3.right);

        //            //    Vector3 toOther = switchObject.transform.position - arm.transform.position;

        //            //    if (Vector3.Dot(right, toOther) < 0)
        //            //    {
        //            //        switchObject = switchList.FirstOrDefault(x => x.name == newObject.name);
        //            //        newObject = switchObject;
        //            //    }
        //            //    else
        //            //    {
        //            //        newObject = switchObject;
        //            //    }
        //            //}
        //        }
        //    }

        //    return newObject;
        //}
    }
}