using System;
using UnityEngine;

[RequireComponent(typeof(AvailableCells))]
public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private MedicalKit _medKitPrefab;

    private AvailableCells _availableCells;
    private int _countCorrectCells;
    private int _numberCoins = 10;
    private int _numberMedKit = 3;

    private void Awake()
    {
        _availableCells = GetComponent<AvailableCells>();
    }

    private void Start()
    {
        CorrectCellsCount();
        SpawnEnvironment(_numberCoins, _coinPrefab);
        SpawnEnvironment(_numberMedKit, _medKitPrefab);
    }

    private void CorrectCellsCount()
    {
        _countCorrectCells = _availableCells.GetCountCorrectCells();
    }

    private void SpawnEnvironment(int numberSurroundingObjects, object surroundingObjects)
    {
        Vector3 positionCorrectCell;
        int index;
        int nextIndex;
        int previousIndex;
        int initialIndex = 0;
        int elementNearIndex = 1; 
        int minValue = 1;
        int countDeleteCells = 3;

        for (int i = 0; i < numberSurroundingObjects; i++)
        {
            index = UnityEngine.Random.Range(minValue, _countCorrectCells);

            nextIndex = index + elementNearIndex;
            previousIndex = index - elementNearIndex;

            if (previousIndex > initialIndex && nextIndex < _countCorrectCells)
            {
                positionCorrectCell = _availableCells.GetCoordinateCorrectCell(index);

                if (surroundingObjects is Coin coin)
                {
                    coin = Instantiate(_coinPrefab, positionCorrectCell, Quaternion.identity);

                    coin.Collected += RemoveCoin;

                    RemoveNearestCells(nextIndex, previousIndex, index);

                    _countCorrectCells -= countDeleteCells;
                }

                if (surroundingObjects is MedicalKit medKit)
                {
                    medKit = Instantiate(_medKitPrefab, positionCorrectCell, Quaternion.identity);

                    medKit.Collected += RemoveMedKit;

                    RemoveNearestCells(nextIndex, previousIndex, index);

                    _countCorrectCells -= countDeleteCells;
                }
            }
        }
    }

    private void RemoveCoin(Coin coin)
    {
        Destroy(coin.gameObject);

        coin.Collected -= RemoveCoin;
    }

    private void RemoveMedKit(MedicalKit medKit)
    {
        Destroy(medKit.gameObject);

        medKit.Collected -= RemoveMedKit;
    }

    private void RemoveNearestCells(int nextIndex, int previousIndex, int index)
    {
        _availableCells.RemoveCorrectCells(nextIndex);
        _availableCells.RemoveCorrectCells(index);
        _availableCells.RemoveCorrectCells(previousIndex);
    }
}
