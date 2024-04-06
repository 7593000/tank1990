// Список перечислений используемых в игре

/// <summary>
/// Типы танка
/// </summary>
public enum TypeTank
{   /// <summary>
    /// Легкий танк
    /// </summary>
    LiteTank,
    /// <summary>
    /// Колесный танк
    /// </summary>
    WheeledTank,
    /// <summary>
    /// средний танк
    /// </summary>
    MediumTank,
    /// <summary>
    /// Тяжелый танк
    /// </summary>
    HeavyTank

}
/// <summary>
///Перечисление пунктов меню 
/// </summary>
public enum TypeSelectMenu
{
    None,
    /// <summary>
    /// Слайдер установки яркости
    /// </summary>
    Brightness,
    /// <summary>
    /// пункт: Старт игра
    /// </summary>
    Start,
    /// <summary>
    /// пункт: Настройки игры
    /// </summary>
    Setup,
    /// <summary>
    /// пункт: Выход из игры
    /// </summary>
    Exit,
    /// <summary>
    /// пункт: выход из настроек в главное меню
    /// </summary>
    Back,
    /// <summary>
    /// слайдер громкости музыки
    /// </summary>
    Volume,
    /// <summary>
    /// Количество жизней у игрока
    /// </summary>
    LivesPlayer,
    /// <summary>
    /// Стартовое количество активных танков 
    /// </summary>
    NumberBots,
    LiteTank,
    WheeledTank,
    MediumTank,
    HeavyTank

}

/// <summary>
/// Перечисление движения
/// </summary>
public enum DirectionType : byte
{

    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3


}

/// <summary>
/// перечисление принадлежности танков 
/// </summary>
public enum SideType : byte
{
    None,
    Player,
    Enemy


}
/// <summary>
/// Тип данных для сохранения
/// </summary>
public enum TypeSave
{
    StatusGame,
    Sound,
    Bright,
    LiveBot,
    LivePlayer,
    numberBots
}

/// <summary>
/// перечисление типов звуков  
/// </summary>
public enum Sound
{
    /// <summary>
    /// Движение 
    /// </summary>
    move = 0,
    /// <summary>
    /// Остановка 
    /// </summary>
    stand = 1,
    /// <summary>
    /// Выстрел
    /// </summary>
    shoot = 2,
    /// <summary>
    /// Взрыв
    /// </summary>
    explosion = 3,
    /// <summary>
    /// Старт игры
    /// </summary>
    startGame = 4,
    /// <summary>
    /// Попадание  в кирпич
    /// </summary>
    hittingBrick = 5,
    /// <summary>
    /// Попадание в непробиваемую поверхность
    /// </summary>
    hittingZoneGame = 6,
    none
}