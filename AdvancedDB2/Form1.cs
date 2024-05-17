using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedDB2
{
    public partial class Form1 : Form
    {
        const string DBIndexed = "Data Source=NISA-PC;Initial Catalog=AdventureWorks2022;Integrated Security=True;Pooling=true;Min Pool Size=10;Max Pool Size=250";
        const string DBUnindexed = "Data Source=NISA-PC;Initial Catalog=AdventureWorks2022-INDEXED;Integrated Security=True;Pooling=true;Min Pool Size=10;Max Pool Size=250";
        string connectionString;

        private bool isStarted = false;

        private readonly int transactionNumber = 100; // Set low for testing purposes
        private int deadlockCountA = 0;
        private int deadlockCountB = 0;

        public Form1()
        {
            InitializeComponent();

            IsoLvl_Dropdown.Items.AddRange(new object[] { "Read Uncommitted", "Read Committed", "Repeatable Read", "Serializable" });
            IsoLvl_Dropdown.SelectedIndex = 0; // Default Read Uncommitted

            DBDropdown.Items.AddRange(new object[] { "AdventureWorks2022", "AdventureWorks2022-INDEXED" });
            DBDropdown.SelectedIndex = 0; // Default AdventureWorks2022

            if (isStarted)
            {
                StartSimButton.Enabled = false;
            }
            else
            {
                StartSimButton.Enabled = true;
            }
        }

        #region Start Simulation Button
        private async void StartSim_Click(object sender, EventArgs e)
        {
            // Info console message
            Console.WriteLine("***\nSimulation started...\n***\n");
            isStarted = true;

            int selectedIsolationLevel = IsoLvl_Dropdown.SelectedIndex;
            IsolationLevel isolationLevel = GetIsolationLevel(selectedIsolationLevel);

            int selectedDBIndex = DBDropdown.SelectedIndex;
            connectionString = SelectedDB(selectedDBIndex);

            int numberOfTypeAUsers = (int)TypeADropDown.Value;
            int numberOfTypeBUsers = (int)TypeBDropDown.Value;

            var tasks = new Task[numberOfTypeAUsers + numberOfTypeBUsers];

            var beginTime = DateTime.Now;

            for (int i = 0; i < numberOfTypeAUsers; i++)
            {
                tasks[i] = Task.Run(() => TypeAUserTransactionAsync(connectionString, isolationLevel));
            }

            for (int i = numberOfTypeAUsers; i < numberOfTypeAUsers + numberOfTypeBUsers; i++)
            {
                tasks[i] = Task.Run(() => TypeBUserTransactionAsync(connectionString, isolationLevel));
            }

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            var endTime = DateTime.Now;
            var elapsed = endTime - beginTime;

            // Message Box
            MessageBox.Show($"Isolation level: {isolationLevel} \nElapsed time: {elapsed.TotalSeconds} seconds\nDeadlocks (Type A): {deadlockCountA}\nDeadlocks (Type B): {deadlockCountB}", "Simulation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Info console message
            Console.WriteLine("\n***\nSimulation complete.\n***\n");
            isStarted = false;
        }
        #endregion

        #region Isolation Level
        private IsolationLevel GetIsolationLevel(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return IsolationLevel.ReadUncommitted;
                case 1:
                    return IsolationLevel.ReadCommitted;
                case 2:
                    return IsolationLevel.RepeatableRead;
                case 3:
                    return IsolationLevel.Serializable;
                default:
                    return IsolationLevel.ReadUncommitted; // Default to Read Uncommitted
            }
        }
        #endregion

        #region Select Database
        private string SelectedDB(int selectedDBIndex)
        {
            switch (selectedDBIndex)
            {
                case 0:
                    return DBIndexed;
                case 1:
                    return DBUnindexed;
                default:
                    return DBUnindexed; // Default to AdventureWorks2022
            }
        }
        #endregion

        #region User Transaction Methods
        private async Task TypeAUserTransactionAsync(string connectionString, IsolationLevel isolationLevel)
        {
            var rand = new Random();
            var beginTime = DateTime.Now;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                for (int i = 0; i < transactionNumber; i++) // Perform 100 transactions
                {
                    bool success = false;
                    int retryCount = 0;
                    while (!success && retryCount < 3)
                    {
                        using (var transaction = connection.BeginTransaction(isolationLevel))
                        {
                            try
                            {
                                if (rand.NextDouble() < 0.5)
                                    await RunUpdateQueryAsync(connection, transaction, "20110101", "20111231");
                                if (rand.NextDouble() < 0.5)
                                    await RunUpdateQueryAsync(connection, transaction, "20120101", "20121231");
                                if (rand.NextDouble() < 0.5)
                                    await RunUpdateQueryAsync(connection, transaction, "20130101", "20131231");
                                if (rand.NextDouble() < 0.5)
                                    await RunUpdateQueryAsync(connection, transaction, "20140101", "20141231");
                                if (rand.NextDouble() < 0.5)
                                    await RunUpdateQueryAsync(connection, transaction, "20150101", "20151231");

                                transaction.Commit();
                                success = true;
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number == 1205 || ex.Number == 3609) // Deadlock error number
                                {
                                    Interlocked.Increment(ref deadlockCountA);
                                    Console.WriteLine("Deadlock occurred for Type A. Retrying...");
                                    transaction.Rollback();
                                    await Task.Delay(100); // Backoff strategy
                                    retryCount++;
                                }
                                else
                                {
                                    Console.WriteLine("Transaction failed for Type A: " + ex.Message);
                                    transaction.Rollback();
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            var endTime = DateTime.Now;
            var elapsed = endTime - beginTime;
            Console.WriteLine($"Elapsed time for Type A user: {elapsed.TotalSeconds} seconds");
        }

        private async Task TypeBUserTransactionAsync(string connectionString, IsolationLevel isolationLevel)
        {
            var rand = new Random();
            var beginTime = DateTime.Now;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                for (int i = 0; i < transactionNumber; i++) // Perform 100 transactions
                {
                    bool success = false;
                    int retryCount = 0;
                    while (!success && retryCount < 3)
                    {
                        using (var transaction = connection.BeginTransaction(isolationLevel))
                        {
                            try
                            {
                                if (rand.NextDouble() < 0.5)
                                    await RunSelectQueryAsync(connection, transaction, "20110101", "20111231");
                                if (rand.NextDouble() < 0.5)
                                    await RunSelectQueryAsync(connection, transaction, "20120101", "20121231");
                                if (rand.NextDouble() < 0.5)
                                    await RunSelectQueryAsync(connection, transaction, "20130101", "20131231");
                                if (rand.NextDouble() < 0.5)
                                    await RunSelectQueryAsync(connection, transaction, "20140101", "20141231");
                                if (rand.NextDouble() < 0.5)
                                    await RunSelectQueryAsync(connection, transaction, "20150101", "20151231");

                                transaction.Commit();
                                success = true;
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number == 1205 || ex.Number == 3609) // Deadlock error number
                                {
                                    Interlocked.Increment(ref deadlockCountB);
                                    Console.WriteLine("Deadlock occurred for Type B. Retrying...");
                                    transaction.Rollback();
                                    await Task.Delay(100); // Backoff strategy
                                    retryCount++;
                                }
                                else
                                {
                                    Console.WriteLine("Transaction failed for Type B: " + ex.Message);
                                    transaction.Rollback();
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            var endTime = DateTime.Now;
            var elapsed = endTime - beginTime;
            Console.WriteLine($"Elapsed time for Type B user: {elapsed.TotalSeconds} seconds");
        }
        #endregion

        #region SQL Queries
        static async Task RunUpdateQueryAsync(SqlConnection connection, SqlTransaction transaction, string beginDate, string endDate)
        {
            string updateQuery = @"
                UPDATE Sales.SalesOrderDetail
                SET UnitPrice = UnitPrice * 10.0 / 10.0
                WHERE UnitPrice > 100
                AND EXISTS (
                    SELECT * FROM Sales.SalesOrderHeader
                    WHERE Sales.SalesOrderHeader.SalesOrderID = Sales.SalesOrderDetail.SalesOrderID
                    AND Sales.SalesOrderHeader.OrderDate BETWEEN @BeginDate AND @EndDate
                    AND Sales.SalesOrderHeader.OnlineOrderFlag = 1
                )";

            using (var command = new SqlCommand(updateQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@BeginDate", beginDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                command.CommandTimeout = 120;
                await command.ExecuteNonQueryAsync();
            }
        }

        static async Task RunSelectQueryAsync(SqlConnection connection, SqlTransaction transaction, string beginDate, string endDate)
        {
            string selectQuery = @"
                SELECT SUM(Sales.SalesOrderDetail.OrderQty)
                FROM Sales.SalesOrderDetail
                WHERE UnitPrice > 100
                AND EXISTS (
                    SELECT * FROM Sales.SalesOrderHeader
                    WHERE Sales.SalesOrderHeader.SalesOrderID = Sales.SalesOrderDetail.SalesOrderID
                    AND Sales.SalesOrderHeader.OrderDate BETWEEN @BeginDate AND @EndDate
                    AND Sales.SalesOrderHeader.OnlineOrderFlag = 1
                )";

            using (var command = new SqlCommand(selectQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@BeginDate", beginDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                command.CommandTimeout = 120;
                await command.ExecuteScalarAsync();
            }
        }
        #endregion

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
