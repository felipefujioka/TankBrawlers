using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class MatchInitializer : MonoBehaviour
    {
        public List<Transform> SpawnPoints;

        public List<PlayerController> PlayerControllers;

        [FormerlySerializedAs("playerPrefab")] public PlayerView PlayerPrefab;

        private void Start()
        {
            PlayerControllers = new List<PlayerController>();
            for (int i = 0; i < 2; i++)
            {
                var player = new PlayerController();
                var view = Instantiate(PlayerPrefab, transform.parent);
                var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)];
                view.transform.position = spawnPoint.position;
                player.view = view;
                player.ID = i + 1;
                
                PlayerControllers.Add(player);
            }
        }

        private void Update()
        {
            foreach (var playerController in PlayerControllers)
            {
                GetAndApplyInput(playerController);
            }
        }

        private void GetAndApplyInput(PlayerController playerController)
        {
            var horizontal = Input.GetAxis(GameInput.GetInput(playerController.ID, "Horizontal")); 
            var vertical = Input.GetAxis(GameInput.GetInput(playerController.ID, "Vertical"));

            playerController.ApplyHorizontalMovement(horizontal);

            if (Input.GetButtonDown(GameInput.GetInput(playerController.ID, "Jump")))
            {
                playerController.Jump();
            }

            if (Input.GetButtonDown(GameInput.GetInput(playerController.ID, "Grab")))
            {
                if (playerController.IsGrabbing)
                {
                    playerController.Throw(new Vector2(horizontal, vertical));
                }
                else
                {
                    playerController.Grab();
                }
            }
        }
    }
}