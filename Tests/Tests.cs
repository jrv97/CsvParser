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
            // test get rows
            for (int i = 0; i < lines.Count; i++)
                lines.ElementAt(i).Should().BeEquivalentTo(csv.GetRowAt(i));
            // test get cols
            for (int j = 0; j < lines.ElementAt(0).Length; j++)
                lines.Select(line => line[j]).ToArray().Should().BeEquivalentTo(csv.GetColumnAt(j));
        }
    }
}