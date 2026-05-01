using UnityEngine;

[CreateAssetMenu(fileName = "MejorScore", menuName = "Herramientas/MejorScore", order = 0)]

public class MejorScore : ScorePersistente
{
    public int scoreActual = 0;
    public int mejorScore = 0;
}
