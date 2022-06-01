using System.Collections;

public class CsvReader : IEnumerator, IEnumerable
{
    public string[][] Grid { get; private set; }

    public CsvReader(string pathToCsv, char separator = ',') =>
        Grid = File.ReadLines(pathToCsv).Select(x => x.Split(separator)).ToArray(); // O(n^2) but we'll only do this once

    public string this[int rowIndex, int columnIndex] =>
        Grid[rowIndex][columnIndex]; // O(1)

    public void Set(int rowIndex, int columnIndex, string value) =>
        Grid[rowIndex][columnIndex] = value; // O(1)

    public string[] GetRowAt(int rowIndex) =>
        Grid[rowIndex]; // O(1)

    public string[] GetColumnAt(int columnIndex) =>
        (from rowIndex in Enumerable.Range(0, GetLength(0)) select Grid[rowIndex][columnIndex]).ToArray(); // O(n)

    public IEnumerable<string[]> Rows =>
        from row in Grid select row;

    public IEnumerable<string[]> Columns =>
        from colIndex in Enumerable.Range(0, GetLength(1)) select GetColumnAt(colIndex);

    public int GetLength(int index) => // O(1)
        index switch
        {
            0 => Grid.Length,
            1 => Grid[0].Length,
            _ => throw new IndexOutOfRangeException(),
        };

    #region IEnumerable and IEnumerator methods
    private int Index = -1;

    private int RowIndex => Index / GetLength(1);

    private int ColumnIndex => Index % GetLength(1);

    //IEnumerable
    public object Current => Grid[RowIndex][ColumnIndex];

    //IEnumerator
    public bool MoveNext() => ++Index < GetLength(0) * GetLength(1);

    //IEnumerable
    public void Reset() => Index = -1;

    //IEnumerator and IEnumerable
    public IEnumerator GetEnumerator() => this;
    #endregion
}