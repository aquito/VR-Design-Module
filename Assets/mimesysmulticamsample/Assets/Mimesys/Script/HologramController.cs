using Mimesys.Unity.Multicam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramController : MonoBehaviour
{
    public Transform HologramPosition;
    private MimesysCameraClient mimesysClient = null;
    private List<int> newlyConnectedPlayers = new List<int>();

    // Use this for initialization
    void Start()
    {
        mimesysClient = FindObjectOfType(typeof(MimesysCameraClient)) as MimesysCameraClient;
    }

    // Update is called once per frame
    void Update()
    {       
        if (newlyConnectedPlayers.Count > 0)
        {
            foreach (int playerId in newlyConnectedPlayers)
            {
                MimesysPlayer player = mimesysClient.GetPlayer(playerId);
                if (player != null)
                {
                    if ((player.HologramRenderer != null) || (player.PlayerHead == null))
                    {
                        if (HologramPosition != null)
                        {
                            // This is a hologram or audio only, place it in the room.
                            player.gameObject.transform.position = HologramPosition.position + new Vector3(0, 1f, 0);
                            player.gameObject.transform.rotation = HologramPosition.rotation;                            
                        }                       
                    }
                }
            }
            newlyConnectedPlayers.Clear();
        }
    }

    public void HandlePlayerConnected(int playerId)
    {
        newlyConnectedPlayers.Add(playerId);
    }
}
