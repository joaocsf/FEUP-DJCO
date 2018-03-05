using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyElevator : MonoBehaviour {

    private Transform doors;
    public int height = 0;
    public Vector3 doorStart;
    public float yOffset;
    public float doorTime = 0.1f;
    public float floorTime = 0.1f;
    private HashSet<PlayerStatus> playersInside = new HashSet<PlayerStatus>();
    private Vector3 startPos;
    private Transform oldParent;
    public LayerMask lm;

    private bool started = false;

	void Start () {
        doors = transform.GetChild(0);
        doors.transform.localPosition = doorStart; 
        startPos = transform.localPosition;
	}
	
	void Update () {
		
	}

    private void SetPlayersParent(Transform t)
    {
        foreach (PlayerStatus p in playersInside)
            p.transform.parent = t;
    }

    IEnumerator LerpObject(Transform t, float time, Vector3 startPos, Vector3 endPos)
    {
       float currTime = 0;
        while(currTime <= time){
            currTime += Time.fixedDeltaTime;
            float weight = Mathf.Clamp(currTime/time, 0f, 1f);            

            Vector3 pos = t.localPosition;
            pos = Vector3.Lerp(startPos, endPos, Mathf.Pow(weight, 2));
            t.localPosition = pos;

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator StartGame()
    {
        RaycastHit hit;

        GameController.State = GameController.GameState.Playing;

        Transform t = null;
        if (Physics.Raycast(transform.position - new Vector3(0,0,0.5f), Vector3.up, out hit, 10, lm))
            t = hit.collider.transform;

        yield return LerpObject(doors, doorTime, doorStart, Vector3.zero);
        StartCoroutine(LerpObject(t, doorTime, t.localPosition, t.localPosition + Vector3.up * 1.5f));

        //SetPlayersParent(transform);
        started = true;


        yield return LerpObject(transform, floorTime, startPos, startPos + Vector3.up*yOffset);

        yield return new WaitForSeconds(0.5f);

        SetPlayersParent(oldParent);

        yield return LerpObject(doors, doorTime, Vector3.zero, doorStart);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (started) return;
        PlayerStatus sts = other.GetComponent<PlayerStatus>();
        if (sts == null)
            return;

        oldParent = sts.transform.parent;

        playersInside.Add(sts);
        if (GameController.CheckBeginGame(playersInside))
            StartCoroutine(StartGame());
    }

    private void OnTriggerExit(Collider other)
    {
        if (started) return;
        PlayerStatus sts = other.GetComponent<PlayerStatus>();
        if (sts == null)
            return;
        playersInside.Remove(sts);
    }
}
