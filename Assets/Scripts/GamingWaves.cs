
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameWaves", menuName = "Configuration/Create Waves", order = 1)]
public class GamingWaves : ScriptableObject
{

    [SerializeField, Tooltip("Легкий танк")]
    private int _liteTankCount;
    [SerializeField, Tooltip("колесный танк")]
    private int _wheeledTankCount;
    [SerializeField, Tooltip("средний танк")]
    private int _mediumTankCount;
    [SerializeField, Tooltip("тяжелый танк")]
    private int _heavyTankCount;

    private Dictionary<TypeTank, int> _countTanks = new Dictionary<TypeTank, int>();

    /// <summary>
    /// Получить количество танков разного вида
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
