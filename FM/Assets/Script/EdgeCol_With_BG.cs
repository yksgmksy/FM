using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EdgeCol_With_BG : MonoBehaviour {

    private BansheeGz.BGSpline.Curve.BGCurve bc;
    private EdgeCollider2D ec2d;

    int lastIndex;
    Vector2 lastPosition;
	// Use this for initialization
	void Start () {
        bc = gameObject.GetComponent<BansheeGz.BGSpline.Curve.BGCurve>();
        ec2d = gameObject.GetComponent<EdgeCollider2D>();
        
        Vector2[] vtmp = ec2d.points;
        for (int c = 0; c < bc.PointsCount; c++)
        {
           
            vtmp[c].x = bc.Points[c].PositionLocal.x;
            vtmp[c].y = bc.Points[c].PositionLocal.y;
            lastIndex = c;
            lastPosition = vtmp[c];
        }
       

        //Debug.Log(lastIndex); 
        //Debug.Log(ec2d.pointCount);
        for ( int i = lastIndex; i< ec2d.pointCount; i++)
        {
            vtmp[i] = lastPosition;
        }

        ec2d.points = vtmp;
    }
}
