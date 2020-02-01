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

                player.axisOffsetX = Input.GetAxis(GameInput.GetInput(player.ID, "Horizontal"));


                PlayerControllers.Add(player);
            }
        }

        private void FixedUpdate()
        {
            foreach (var playerController in PlayerControllers)
            {
                GetAndApplyInput(playerController);
            }
        }

        private void GetAndApplyInput(PlayerController playerController)
        {
            var horizontal = Input.GetAxis(GameInput.GetInput(playerController.ID, "Horizontal")) -
                             playerController.axisOffsetX;
            var vertical = Input.GetAxis(GameInput.GetInput(playerController.ID, "Vertical"));

            playerController.ApplyHorizontalMovement(Mathf.Abs(horizontal) > 0.2f ? Mathf.Sign(horizontal) : 0);

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