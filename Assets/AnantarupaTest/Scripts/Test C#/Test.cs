using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    public void Start()
    {
        var configurator = new Configurator<WeaponId, WeaponStats>();
        configurator.Add(ObjectId<WeaponId>.Any, weapon => weapon.AttackDamage += 200000);
        configurator.Add(ObjectId<WeaponId>.Any, weapon => weapon.AttackDamage += 30000);


        configurator.Add(WeaponId.Sword001, weapon => weapon.AttackDamage += 4000);
        configurator.Add(WeaponId.Sword001, weapon => weapon.AttackDamage += 500);

        configurator.Add(WeaponId.Sword002, weapon => weapon.AttackDamage += 60);
        
        configurator.Add(WeaponId.Sword003, weapon => weapon.AttackDamage += 7);

        configurator.Add(ObjectId<WeaponId>.Any, SetZeroAttackDamage);
        configurator.Remove(ObjectId<WeaponId>.Any, SetZeroAttackDamage);

        var weapon1 = new WeaponStats()
        {
            Id = WeaponId.Sword001,
            AttackDamage = 1000000
        };

        var weapon2 = new WeaponStats()
        {
            Id = WeaponId.Sword003,
            AttackDamage = 1000000
        };

        configurator.Configure(weapon1);
        configurator.Configure(weapon2);

        Console.WriteLine($"Weapon 1 attack damage = {weapon1.AttackDamage}");
        Console.WriteLine($"Weapon 2 attack damage = {weapon2.AttackDamage}");

        // The output should be:
        // Weapon 1 attack damage = 1234500
        // Weapon 2 attack damage = 1230007
    }
    
    private static void SetZeroAttackDamage(WeaponStats weapon)
    {
        weapon.AttackDamage = 0;
    }
}
public enum WeaponId
{
    Sword001,
    Sword002,
    Sword003
}

public class WeaponStats
{
    public WeaponId Id { get; set; }
    public int AttackDamage { get; set; }
}

public class Configurator<TWeaponID,TWeaponStats>
{
    private readonly Dictionary<ObjectId<TWeaponID>, List<Action<TWeaponStats>>> _configurations = new Dictionary<ObjectId<TWeaponID>, List<Action<TWeaponStats>>>();

    public void Add(ObjectId<TWeaponID> key, Action<TWeaponStats> configAction)
    {
        if (!_configurations.ContainsKey(key))
        {
            _configurations[key] = new List<Action<TWeaponStats>>();
        }
        _configurations[key].Add(configAction);
    }
    
    public void Add(TWeaponID key, Action<TWeaponStats> configAction)
    {
        var data = new ObjectId<TWeaponID>(key);
        if (!_configurations.ContainsKey(data))
        {
            _configurations[data] = new List<Action<TWeaponStats>>();
        }
        _configurations[data].Add(configAction);
    }

    public void Remove(ObjectId<TWeaponID> key, Action<TWeaponStats> configAction)
    {
        if (_configurations.ContainsKey(key))
        {
            _configurations[key].Remove(configAction);
            if (_configurations[key].Count == 0)
            {
                _configurations.Remove(key);
            }
        }
    }

    // Apply all configurations to the given data based on its key (Id)
    public void Configure(TWeaponStats data)
    {
        var key = new ObjectId<TWeaponID>((TWeaponID)data.GetType().GetProperty("Id").GetValue(data));
        
        // Apply configurations for the "Any" key
        if (_configurations.ContainsKey(ObjectId<TWeaponID>.Any))
        {
            foreach (var config in _configurations[ObjectId<TWeaponID>.Any])
            {
                config(data);
            }
        }

        // Apply configurations for the specific key
        if (_configurations.ContainsKey(key))
        {
            foreach (var config in _configurations[key])
            {
                config(data);
            }
        }
    }
}

public class ObjectId<T>
{
    public static ObjectId<T> Any { get; } = new ObjectId<T>(default(T), true);

    public T Id { get; }
    public bool IsAny { get; }

    public ObjectId(T id, bool isAny = false)
    {
        Id = id;
        IsAny = isAny;
    }
}