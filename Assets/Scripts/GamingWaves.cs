
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameWaves", menuName = "Configuration/Create Waves", order = 1)]
public class GamingWaves : ScriptableObject
{

    [SerializeField, Tooltip("������ ����")]
    private int _liteTankCount;
    [SerializeField, Tooltip("�������� ����")]
    private int _wheeledTankCount;
    [SerializeField, Tooltip("������� ����")]
    private int _mediumTankCount;
    [SerializeField, Tooltip("������� ����")]
    private int _heavyTankCount;

    private Dictionary<TypeTank, int> _countTanks = new Dictionary<TypeTank, int>();

    /// <summary>
    /// �������� ���������� ������ ������� ����
    /// </summary>
    public Dictionary<TypeTank, int> GetCountTanks { get => _countTanks; }


    private void Initialize()
    {
        _countTanks.Add(TypeTank.LiteTank, _liteTankCount);
        _countTanks.Add(TypeTank.WheeledTank, _wheeledTankCount);
        _countTanks.Add(TypeTank.MediumTank, _mediumTankCount);
        _countTanks.Add(TypeTank.HeavyTank, _heavyTankCount);

    }
    private void OnEnable()
    {
        Initialize();
    }

}
