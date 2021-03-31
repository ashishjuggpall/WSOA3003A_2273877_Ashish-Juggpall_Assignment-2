using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject Player;

    public int PlayerHealth = 20;
    public int PlayerHealthItem = 3;

    public int SpecialPower = 3;
    public GameObject SpecialPowerUI;

    public Text SpecialPowerDisplay;

    public GameObject PlayerHealUI;
    public Text PlayerHealthDisplay;
    public Text PlayerHealthItemDisplay;

    private bool IsPlayersTurn = true;

    public bool IsPlayerBlockActive = false;

    private bool BattleWon = false;
    private bool BattleLost = false;

    public GameObject PlayerBattleUI;
    public GameObject PlayerHealthUI;

    public GameObject BattleWonUI;
    public GameObject BattleLostUI;

    public PlayerShake playershake;

    public GameObject PlayerHealthEffect;

    public GameObject DamageEffect;
    public GameObject EnemyDamageEffect;
    public GameObject SpecialDamageEffect;
    public GameObject PlayerDamageEffect;

    public GameObject PlayerBlockEffect;

    public GameObject Enemy;

    public int EnemyHealth = 30;
    public int EnemyHealthItem = 1;
    public Text EnemyHealthDisplay;

    public float EnemyTimeRemaining = 3;
    public bool EnemyTimeRunning = false;

    private bool IsEnemyTurn = false;

    public bool IsEnemyBlockActive = false;

    public GameObject EnemyHealthUI;

    public EnemyShaker enemyShaker;

    public GameObject EnemyHealthEffect;
    public void Start()
    {
        playershake = GameObject.FindGameObjectWithTag("PlayerShake").GetComponent<PlayerShake>();
        enemyShaker = GameObject.FindGameObjectWithTag("EnemyShake").GetComponent<EnemyShaker>();

        Enemy.SetActive(true);
        IsEnemyTurn = true;
        EnemyHealthUI.SetActive(true);

        BattleWonUI.SetActive(false);
        BattleLostUI.SetActive(false);

        Player.SetActive(true);
        IsPlayersTurn = false;
        PlayerBattleUI.SetActive(false);

        BattleWon = false;
        BattleLost = false;

    }

    public void Update()
    {
        EnemyHealthDisplay.text = EnemyHealth.ToString();
        PlayerHealthDisplay.text = PlayerHealth.ToString();
        PlayerHealthItemDisplay.text = PlayerHealthItem.ToString();
        SpecialPowerDisplay.text = SpecialPower.ToString();

        if ((PlayerHealth > 0) && (EnemyHealth > 0))
        {
            if (IsEnemyTurn == true)
            {
                EnemyTimeRunning = true;
                PlayerBattleUI.SetActive(false);
                if (EnemyHealth <= 10)
                {
                    if (EnemyHealth < 5)
                    {
                        if (IsPlayerBlockActive == true)
                        {
                            if (EnemyTimeRunning)
                            {
                                if (EnemyTimeRemaining > 0)
                                {
                                    EnemyTimeRemaining -= Time.deltaTime;
                                }

                                else
                                {
                                    EnemyAttack();
                                    EnemyTimeRemaining = 3;
                                    EnemyTimeRunning = false;
                                    IsPlayerBlockActive = false;
                                    IsEnemyTurn = false;
                                    PlayerBattleUI.SetActive(true);
                                    IsPlayersTurn = true;
                                }

                            }
                        }

                        else
                        {

                            if (EnemyTimeRunning)
                            {
                                if (EnemyTimeRemaining > 0)
                                {
                                    EnemyTimeRemaining -= Time.deltaTime;
                                }

                                else
                                {
                                    EnemyTimeRemaining = 3;
                                    EnemyTimeRunning = false;
                                    EnemyPowerAttack();
                                }
                            }
                        }
                    }

                    if (EnemyHealthItem > 0)
                    {
                        if (EnemyTimeRunning)
                        {
                            if (EnemyTimeRemaining > 0)
                            {
                                EnemyTimeRemaining -= Time.deltaTime;
                            }

                            else
                            {
                                EnemyTimeRemaining = 3;
                                EnemyTimeRunning = false;
                                EnemyHeal();
                            }
                        }

                    }

                    else
                    {
                        if (IsPlayerBlockActive == true)
                        {
                            if (EnemyTimeRunning)
                            {
                                if (EnemyTimeRemaining > 0)
                                {
                                    EnemyTimeRemaining -= Time.deltaTime;
                                }

                                else
                                {
                                    EnemyTimeRemaining = 3;
                                    EnemyTimeRunning = false;
                                    IsPlayerBlockActive = false;
                                    IsEnemyTurn = false;
                                    PlayerBattleUI.SetActive(true);
                                    IsPlayersTurn = true;
                                }

                            }
                        }

                        else
                        {
                            if (EnemyTimeRunning)
                            {
                                if (EnemyTimeRemaining > 0)
                                {
                                    EnemyTimeRemaining -= Time.deltaTime;
                                }

                                else
                                {
                                    EnemyTimeRemaining = 3;
                                    EnemyTimeRunning = false;
                                    EnemyAttack();
                                }
                            }

                        }

                    }
                }

                else
                {
                    if (EnemyTimeRunning)
                    {
                        if (EnemyTimeRemaining > 0)
                        {
                            EnemyTimeRemaining -= Time.deltaTime;
                        }

                        else
                        {
                            EnemyTimeRemaining = 3;
                            EnemyTimeRunning = false;
                            EnemyAttack();
                        }
                    }
                }
            }

            if (IsPlayersTurn == true)
            {
                PlayerBattleUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.X))
                {
                    PlayerAttack();
                }

                if (PlayerHealthItem > 0)
                {
                    PlayerHealUI.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        PlayerHeal();
                    }

                }

                else if (PlayerHealthItem <=0 )
                {
                    PlayerHealUI.SetActive(false);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Instantiate(PlayerBlockEffect, transform.position, Quaternion.identity);
                    IsPlayerBlockActive = true;
                    IsPlayersTurn = false;
                    PlayerBattleUI.SetActive(false);
                    IsEnemyTurn = true;
                }

                if (SpecialPower > 0)
                {
                    SpecialPowerUI.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        PowerAttack();
                    }
                }

                else if (SpecialPower <=0)
                {
                    SpecialPowerUI.SetActive(false);
                }
            }
        }

        else
        {
            if (PlayerHealth <= 0)
            {
                WinGame();
            }

            if (EnemyHealth <= 0)
            {
                LoseGame();
            }
        }
    }


    public void PlayerAttack()
    {
        Instantiate(DamageEffect, new Vector3(5, 2, 0), Quaternion.identity);
        EnemyHealth -= 2;
        IsPlayersTurn = false;
        PlayerBattleUI.SetActive(false);
        IsEnemyTurn = true;
        enemyShaker.EnemyShake();
    }

    public void PowerAttack()
    {
        Instantiate(SpecialDamageEffect, new Vector3(5, 2, 0), Quaternion.identity);
        EnemyHealth -= 5;
        SpecialPower--;
        IsPlayersTurn = false;
        PlayerBattleUI.SetActive(false);
        IsEnemyTurn = true;
        enemyShaker.EnemyShake();
    }

    public void PlayerHeal()
    {
        Instantiate(PlayerHealthEffect, transform.position, Quaternion.identity);
        PlayerHealth += 6;
        PlayerHealthItem--;
        IsPlayersTurn = false;
        PlayerBattleUI.SetActive(false);
        IsEnemyTurn = true;
    }

    public void WinGame()
    {
        BattleWon = true;
        PlayerHealthUI.SetActive(false);
        EnemyHealthUI.SetActive(false);
        IsPlayersTurn = false;
        IsEnemyTurn = false;
        Enemy.SetActive(false);
    }

    public void LoseGame()
    {
        BattleLost = true;
        PlayerHealthUI.SetActive(false);
        EnemyHealthUI.SetActive(false);
        IsPlayersTurn = false;
        IsEnemyTurn = false;
        Enemy.SetActive(false);

    }

    public void EnemyAttack()
    {
        Instantiate(EnemyDamageEffect, transform.position, Quaternion.identity);
        PlayerHealth -=2;
        IsEnemyTurn = false;
        PlayerBattleUI.SetActive(true);
        IsPlayersTurn = true;
        playershake.PlayerShaker();
    }

    public void EnemyHeal()
    {
        Instantiate(EnemyHealthEffect, new Vector3(5, 2, 0), Quaternion.identity);
        EnemyHealth += 5;
        EnemyHealthItem--;
        IsEnemyTurn = false;
        PlayerBattleUI.SetActive(true);
        IsPlayersTurn = true;
    }

    public void EnemyPowerAttack()
    {
        Instantiate(PlayerDamageEffect, transform.position, Quaternion.identity);
        PlayerHealth -= 7;
        IsEnemyTurn = false;
        PlayerBattleUI.SetActive(true);
        IsPlayersTurn = true;
        playershake.PlayerShaker();
    }

    //for new position instantiate(effect, new vector3 (co-ordinates), Quaternion.identity);
    /// public void RandomGenerator()
    //RandomNumber = Random.Range(1, 4);
}
