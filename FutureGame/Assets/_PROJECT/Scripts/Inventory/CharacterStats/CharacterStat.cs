﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStat
{

    public float BaseValue = 100;

    public virtual float Value
    {
        get
        {
            if (isDirty || BaseValue != _lastBaseValue)
            {
                _lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    protected bool isDirty = true;
    protected float _value;
    protected float _lastBaseValue = float.MinValue;

    protected readonly List<StatModifier> _statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public CharacterStat()
    {
        _statModifiers = new List<StatModifier>();
        StatModifiers = _statModifiers.AsReadOnly();
    }

    public CharacterStat(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        _statModifiers.Add(mod);
        _statModifiers.Sort(CompareModifierOrder);
    }

    public virtual bool RemoveModifiers(StatModifier mod)
    {
        if (_statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {

        bool didRemove = false;

        for (int i = _statModifiers.Count - 1; i >= 0; i--)
        {
            if (_statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                _statModifiers.RemoveAt(i);
            }
        }

        return didRemove;
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
        {
            return -1;
        }
        else if (a.Order > b.Order)
        {
            return 1;
        }
        return 0;
    }

    protected float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers

        for (int i = 0; i < _statModifiers.Count; i++)
        {
            StatModifier mod = _statModifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
            {
                sumPercentAdd += mod.Value; // Start adding together all modifiers of this type

                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd / 100; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.Type == StatModType.PercentMult) // Percent renamed to PercentMult
            {
                finalValue *= 1 + mod.Value / 100;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }
}