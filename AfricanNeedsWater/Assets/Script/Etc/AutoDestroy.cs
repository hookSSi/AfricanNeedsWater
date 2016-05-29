using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

    public float LimitTime;
    private float time = 0;

	void FixedUpdate ()
    {
        time += Time.deltaTime;

        if (time > LimitTime)
            Destroy(this.gameObject);
	}
}
