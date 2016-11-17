using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RunTimeRopeCreator : MonoBehaviour {

	public SpriteRenderer segmentPrefab;
    private HingeJoint2D playerHingeJoint;

    // Use this for initialization
	void Start () {
        SetRopeSettings();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetRopeSettings()
    {
        playerHingeJoint = GameObject.FindGameObjectWithTag("Player").GetComponent<HingeJoint2D>();
        GameObject newRope = new GameObject();
        newRope.AddComponent<Rope>();

        Rope ropeComponent = newRope.GetComponent<Rope>();

        ropeComponent.transform.position = new Vector2(22.0f, 3.45f);
        ropeComponent.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -70.0f);
        ropeComponent.SegmentsPrefabs = new SpriteRenderer[1];
        ropeComponent.SegmentsPrefabs[0] = segmentPrefab;
        ropeComponent.HangFirstSegment = true;
        ropeComponent.useBendLimit = false;
        ropeComponent.overlapFactor = -0.17f;
        ropeComponent.WithPhysics = true;
        ropeComponent.FirstSegmentConnectionAnchor = new Vector2(-3.0f, 0.0f);

        ropeComponent.useBendLimit = true;
        ropeComponent.bendLimit = 90;

        UpdateRope(ropeComponent);

        //set drag to every of the segments
        Component[] rbComponentsInChildren;

        rbComponentsInChildren = ropeComponent.GetComponentsInChildren<Rigidbody2D>();
        int numberOfSegments = rbComponentsInChildren.Length;

        foreach (Rigidbody2D rb in rbComponentsInChildren)
        {
            rb.drag = 0.2f;
            rb.angularDrag = 0.6f;
        }
    }

    void UpdateRope(Rope rope)
    {
        DestroyChildren(rope);
        if (rope.SegmentsPrefabs == null || rope.SegmentsPrefabs.Length == 0)
        {
            Debug.LogWarning("Rope Segments Prefabs is Empty");
            return;
        }
        float segmentHeight = rope.SegmentsPrefabs[0].bounds.size.y * (1 + rope.overlapFactor);
        List<Vector3> nodes = rope.nodes;
        int currentSegPrefIndex = 0;
        Rigidbody2D previousSegment = null;
        float previousTheta = 0;
        int currentSegment = 0;
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            //construct line between nodes[i] and nodes[i+1]
            float theta = Mathf.Atan2(nodes[i + 1].y - nodes[i].y, nodes[i + 1].x - nodes[i].x);
            float dx = segmentHeight * Mathf.Cos(theta);
            float dy = segmentHeight * Mathf.Sin(theta);
            float startX = nodes[i].x + dx / 2;
            float startY = nodes[i].y + dy / 2;
            float lineLength = Vector2.Distance(nodes[i + 1], nodes[i]);
            int segmentCount = 0;
            switch (rope.OverflowMode)
            {
                case LineOverflowMode.Round:
                    segmentCount = Mathf.RoundToInt(lineLength / segmentHeight);
                    break;
                case LineOverflowMode.Shrink:
                    segmentCount = (int)(lineLength / segmentHeight);
                    break;
                case LineOverflowMode.Extend:
                    segmentCount = Mathf.CeilToInt(lineLength / segmentHeight);
                    break;
            }
            for (int j = 0; j < segmentCount; j++)
            {
                if (rope.SegmentsMode == SegmentSelectionMode.RoundRobin)
                {
                    currentSegPrefIndex++;
                    currentSegPrefIndex %= rope.SegmentsPrefabs.Length;
                }
                else if (rope.SegmentsMode == SegmentSelectionMode.Random)
                {
                    currentSegPrefIndex = Random.Range(0, rope.SegmentsPrefabs.Length);
                }
                GameObject segment = (Instantiate(rope.SegmentsPrefabs[currentSegPrefIndex]) as SpriteRenderer).gameObject;
                segment.name = "Segment_" + currentSegment;
                segment.transform.parent = rope.transform;
                segment.transform.localPosition = new Vector3(startX + dx * j, startY + dy * j);
                segment.transform.localRotation = Quaternion.Euler(0, 0, theta * Mathf.Rad2Deg - 90);
                if (rope.WithPhysics)
                {
                    Rigidbody2D segRigidbody = segment.GetComponent<Rigidbody2D>();
                    if (segRigidbody == null)
                        segRigidbody = segment.AddComponent<Rigidbody2D>();
                    //if not the first segment, make a joint
                    if (currentSegment != 0)
                    {
                        float dtheta = 0;
                        if (j == 0)
                        {
                            //first segment in the line
                            dtheta = (theta - previousTheta) * Mathf.Rad2Deg;
                            if (dtheta > 180) dtheta -= 360;
                            else if (dtheta < -180) dtheta += 360;
                        }
                        //add Hinge
                        AddJoint(rope, dtheta, segmentHeight, previousSegment, segment);
                    }
                    previousSegment = segRigidbody;
                }
                currentSegment++;
                if (j == segmentCount - 1)
                {
                    playerHingeJoint.enabled = true;
                    playerHingeJoint.connectedBody = segment.GetComponent<Rigidbody2D>(); ;
                }
            }
            previousTheta = theta;
        }
        UpdateEndsJoints(rope);
    }

    private void DestroyChildren(Rope rope)
    {
        while (rope.transform.childCount > 0)
            DestroyImmediate(rope.transform.GetChild(0).gameObject);
    }

    private void AddJoint(Rope rope, float dtheta, float segmentHeight, Rigidbody2D previousSegment, GameObject segment)
    {
        HingeJoint2D joint = segment.AddComponent<HingeJoint2D>();
        joint.connectedBody = previousSegment;
        joint.anchor = new Vector2(0, -segmentHeight / 2);
        joint.connectedAnchor = new Vector2(0, segmentHeight / 2);
        if (rope.useBendLimit)
        {
            joint.useLimits = true;
            joint.limits = new JointAngleLimits2D()
            {
                min = dtheta - rope.bendLimit,
                max = dtheta + rope.bendLimit
            };
        }

#if UNITY_5
        if (rope.BreakableJoints)
            joint.breakForce = rope.BreakForce;
#endif
    }

    private static void UpdateEndsJoints(Rope rope)
    {
        Transform firstSegment = rope.transform.GetChild(0);
        if (rope.WithPhysics &&
            rope.HangFirstSegment &&
            rope.transform.childCount > 0)
        {

            HingeJoint2D joint = firstSegment.gameObject.GetComponent<HingeJoint2D>();
            if (!joint)
                joint = firstSegment.gameObject.AddComponent<HingeJoint2D>();
            Vector2 hingePositionInWorldSpace = rope.transform.TransformPoint(rope.FirstSegmentConnectionAnchor);
            joint.connectedAnchor = hingePositionInWorldSpace;
            joint.anchor = firstSegment.transform.InverseTransformPoint(hingePositionInWorldSpace);
            joint.connectedBody = GetConnectedObject(hingePositionInWorldSpace, firstSegment.GetComponent<Rigidbody2D>());
            if (joint.connectedBody)
            {
                joint.connectedAnchor = joint.connectedBody.transform.InverseTransformPoint(hingePositionInWorldSpace);
            }
        }
        else
        {
            HingeJoint2D joint = firstSegment.gameObject.GetComponent<HingeJoint2D>();
            if (joint) DestroyImmediate(joint);
        }
        Transform lastSegment = rope.transform.GetChild(rope.transform.childCount - 1);
        if (rope.WithPhysics && rope.HangLastSegment)
        {
            HingeJoint2D[] joints = lastSegment.gameObject.GetComponents<HingeJoint2D>();
            HingeJoint2D joint = null;
            if (joints.Length > 1)
                joint = joints[1];
            else
                joint = lastSegment.gameObject.AddComponent<HingeJoint2D>();
            Vector2 hingePositionInWorldSpace = rope.transform.TransformPoint(rope.LastSegmentConnectionAnchor);
            joint.connectedAnchor = hingePositionInWorldSpace;
            joint.anchor = lastSegment.transform.InverseTransformPoint(hingePositionInWorldSpace);
            joint.connectedBody = GetConnectedObject(hingePositionInWorldSpace, lastSegment.GetComponent<Rigidbody2D>());
            if (joint.connectedBody)
            {
                joint.connectedAnchor = joint.connectedBody.transform.InverseTransformPoint(hingePositionInWorldSpace);
            }
        }
        else
        {
            HingeJoint2D[] joints = lastSegment.gameObject.GetComponents<HingeJoint2D>();
            if (joints.Length > 1)
                for (int i = 1; i < joints.Length; i++)
                    DestroyImmediate(joints[i]);
        }
    }

    static Rigidbody2D GetConnectedObject(Vector2 position, Rigidbody2D originalObj)
    {
        Rigidbody2D[] sceneRigidbodies = GameObject.FindObjectsOfType<Rigidbody2D>();
        for (int i = 0; i < sceneRigidbodies.Length; i++)
        {
            if (sceneRigidbodies[i].GetComponent<SpriteRenderer>())
            {
                if (originalObj != sceneRigidbodies[i] && sceneRigidbodies[i].GetComponent<SpriteRenderer>().bounds.Contains(position))
                {
                    return sceneRigidbodies[i];
                }
            }
        }
        return null;
    }
}
