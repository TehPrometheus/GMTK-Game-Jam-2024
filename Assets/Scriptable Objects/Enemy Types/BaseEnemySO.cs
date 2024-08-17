using UnityEngine;


[CreateAssetMenu(fileName = "Enemy_", menuName = "Scriptable Objects/Enemy")]
public class BaseEnemySO : ScriptableObject
{
    #region Header BASIC PARAMETERS
    [Space(10)]
    [Header("BASIC PARAMETERS")]
    #endregion
    #region Tooltip
    [Tooltip("The basic parameters of the enemy")]
    #endregion Tooltip
    public float speed;

}
