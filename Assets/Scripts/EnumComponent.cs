// ������ ������������ ������������ � ����

/// <summary>
/// ���� �����
/// </summary>
public enum TypeTank
{   /// <summary>
    /// ������ ����
    /// </summary>
    LiteTank,
    /// <summary>
    /// �������� ����
    /// </summary>
    WheeledTank,
    /// <summary>
    /// ������� ����
    /// </summary>
    MediumTank,
    /// <summary>
    /// ������� ����
    /// </summary>
    HeavyTank

}
/// <summary>
///������������ ������� ���� 
/// </summary>
public enum TypeSelectMenu
{
    None,
    /// <summary>
    /// ������� ��������� �������
    /// </summary>
    Brightness,
    /// <summary>
    /// �����: ����� ����
    /// </summary>
    Start,
    /// <summary>
    /// �����: ��������� ����
    /// </summary>
    Setup,
    /// <summary>
    /// �����: ����� �� ����
    /// </summary>
    Exit,
    /// <summary>
    /// �����: ����� �� �������� � ������� ����
    /// </summary>
    Back,
    /// <summary>
    /// ������� ��������� ������
    /// </summary>
    Volume,
    /// <summary>
    /// ���������� ������ � ������
    /// </summary>
    LivesPlayer,
    /// <summary>
    /// ��������� ���������� �������� ������ 
    /// </summary>
    NumberBots,
    LiteTank,
    WheeledTank,
    MediumTank,
    HeavyTank

}

/// <summary>
/// ������������ ��������
/// </summary>
public enum DirectionType : byte
{

    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3


}

/// <summary>
/// ������������ �������������� ������ 
/// </summary>
public enum SideType : byte
{
    None,
    Player,
    Enemy


}
/// <summary>
/// ��� ������ ��� ����������
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
/// ������������ ����� ������  
/// </summary>
public enum Sound
{
    /// <summary>
    /// �������� 
    /// </summary>
    move = 0,
    /// <summary>
    /// ��������� 
    /// </summary>
    stand = 1,
    /// <summary>
    /// �������
    /// </summary>
    shoot = 2,
    /// <summary>
    /// �����
    /// </summary>
    explosion = 3,
    /// <summary>
    /// ����� ����
    /// </summary>
    startGame = 4,
    /// <summary>
    /// ���������  � ������
    /// </summary>
    hittingBrick = 5,
    /// <summary>
    /// ��������� � ������������� �����������
    /// </summary>
    hittingZoneGame = 6,
    none
}