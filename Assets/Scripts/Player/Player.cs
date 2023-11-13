using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;

    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;

    [SerializeField] public int Money { get; private set; }

    public event UnityAction<int, int> OnHealthChange;
    public event UnityAction<int> OnMoneyChange;

    private void Awake()
    {
        _currentHealth = _health;

        UpdateAvailableWeapons();

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)  && Time.timeScale > 0)
        {
            _currentWeapon.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.timeScale > 0)
        {
            PreviousWeapon();
        }

        if (Input.GetKeyDown(KeyCode.W) && Time.timeScale > 0)
        {
            NextWeapon();
        }
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        OnMoneyChange?.Invoke(Money);
        _weapons.Add(weapon);
        UpdateAvailableWeapons();
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        OnHealthChange?.Invoke(_currentHealth, _health);

        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    public void OnEmeyDied(int reward)
    {
        Money += reward;
        OnMoneyChange?.Invoke(Money);
    }

    private void NextWeapon()
    {
        if(_currentWeaponNumber == _weapons.Count - 1)
        {
            _currentWeaponNumber = 0;
        }
        else
        {
            _currentWeaponNumber++;
        }

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
        {
            _currentWeaponNumber = _weapons.Count - 1;
        }
        else
        {
            _currentWeaponNumber--;
        }

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        if(_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(false);
            _currentWeapon.enabled = false;
        }

        _currentWeapon = weapon;
        _currentWeapon.gameObject.SetActive(true);
        _currentWeapon.enabled = true;
    }

    private void UpdateAvailableWeapons()
    {
        Weapon[] weapons = GetComponentsInChildren<Weapon>(true);

        _weapons.Clear();

        foreach (Weapon weapon in weapons)
        {
            if (weapon.IsBuyed)
            {
                _weapons.Add(weapon);
            }
        }
    }

    private void Die()
    {

    }
}
