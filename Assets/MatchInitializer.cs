using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class MatchInitializer : MonoBehaviour
    {
        public List<TankGraphics> Tanks;
        
        public Transform RedSpawn, BlueSpawn;

        public List<PlayerController> PlayerControllers;

        [FormerlySerializedAs("playerPrefab")] public PlayerView BlueCharacterPrefab;
        [FormerlySerializedAs("playerPrefab")] public PlayerView RedCharacterPrefab;

        public GameObject InitScreen;
        public EndScreen EndScreen;

        private void Start()
        {
            SoundManager.Instance.PlayBGM("bgm_gameplay");
            
            PlayerControllers = new List<PlayerController>();
            for (int i = 0; i < 2; i++)
            {
                var player = new PlayerController();
                
                player.playerTeam = i == 0 ? Team.Blue : Team.Red;

                var characterPrefab = player.playerTeam == Team.Blue ? BlueCharacterPrefab : RedCharacterPrefab;
                
                var view = Instantiate(characterPrefab, transform.parent);
                var spawnPoint = i == 0 ? BlueSpawn : RedSpawn;
                view.transform.position = spawnPoint.position;
                player.view = view;
                view.playerController = player;
                player.ID = i + 1;
                PlayerControllers.Add(player);
            }

            var tank1 = Tanks[0];
            var tank2 = Tanks[1];
            tank1.enemyTank = tank2;
            tank2.enemyTank = tank1;

            StartCoroutine(IntroRoutine());
            StartCoroutine(MatchRoutine());
        }

        IEnumerator IntroRoutine()
        {
            yield return new WaitForSeconds(GameConstants.BEGIN_INTRO_DELAY);

            GameInfo.Instance.IsRunning = false;
            var tank1 = Tanks[0];
            var tank2 = Tanks[1];
            tank1.ExecuteShot(true);
            tank2.ExecuteShot(true);
            
            //Destroy tanks
            
            yield return new WaitForSeconds(GameConstants.INTRO_LENGTH);
            
            GameInfo.Instance.IsRunning = true;
        }

        private IEnumerator MatchRoutine()
        {
            var tank1 = Tanks[0];
            var tank2 = Tanks[1];
            while (tank1.tankController.IsAlive() && tank2.tankController.IsAlive())
            {
                yield return null;
            }
            
            EndScreen.gameObject.SetActive(true);
            SoundManager.Instance.PlaySFX("sfx_win_congratulations", false);
            EndScreen.VictoryLabel.text =
                $"{(tank1.tankController.IsAlive() ? "<color=blue>BLUE</color>" : "<color=red>RED</color>")} TEAM WINS!";
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
            if (!GameInfo.Instance.IsRunning)
                return;
            
            var horizontal = Input.GetAxis(GameInput.GetInput(playerController.ID, "Horizontal")); 
            var vertical = Input.GetAxis(GameInput.GetInput(playerController.ID, "Vertical"));

            playerController.ApplyHorizontalMovement(horizontal);
            
            if(horizontal== 0 && vertical == 0)
                playerController.DisableTarget();
            else
                playerController.TargetProp(new Vector2(horizontal, -vertical));

            if (Input.GetButtonDown(GameInput.GetInput(playerController.ID, "Jump")))
            {
                playerController.Jump();
            }

            if (Input.GetButtonDown(GameInput.GetInput(playerController.ID, "Grab")))
            {
                if (playerController.IsGrabbing)
                {
                    playerController.Throw(new Vector2(horizontal, -vertical));
                }
                else
                {
                    playerController.Grab(new Vector2(horizontal, -vertical));
                }
            }
        }
    }
}