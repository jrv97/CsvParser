namespace Tests
{
    public class Tests
    {
        [Fact]
        public void TestEnumeration()
        {
            var pathToCsv = @"C:\Users\j.delavega\source\repos\CsvParser\Tests\email-password-recovery-code.csv";
            CsvReader csv = new(pathToCsv);

            // test Grid structure
            List<string[]> lines = new();
            string line;
            using (StreamReader reader = new(pathToCsv))
            {
                while ((line = reader.ReadLine()) != null)
                    lines.Add(line.Split(','));
            }
            // test enumerating and indexing whole csv one item at a time
            for (int i = 0; i < csv.GetLength(0); i++)
                for (int j = 0; j < csv.GetLength(1); j++)
                    csv[i, j].Should().BeEquivalentTo(lines.ElementAt(i)[j]);
            // test get rows methods
            for (int i = 0; i < lines.Count; i++)
                lines.ElementAt(i).Should().BeEquivalentTo(csv.GetRowAt(i));
            foreach (var rows in Enumerable.Zip(csv.Rows, lines))
                rows.First.Should().BeEquivalentTo(rows.Second);
            // test get cols methods
            for (int j = 0; j < lines.ElementAt(0).Length; j++)
                lines.Select(line => line[j]).ToArray().Should().BeEquivalentTo(csv.GetColumnAt(j));
            foreach (var cols in Enumerable.Zip(csv.Columns, from j in Enumerable.Range(0, csv.GetLength(1)) select lines.Select(line => line[j])))
                cols.First.Should().BeEquivalentTo(cols.Second);
        }

        void PrintEnumerator<T>(IEnumerable<T> list) =>
            Console.WriteLine($"[{string.Join(", ", list.Select(x => x.ToString()))}]");

    }
}