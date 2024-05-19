using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace AdvancedDB2
{
    public partial class Form1 : Form
    {
        const String DBIndexed = "Data Source=ERMAN;Initial Catalog=AdventureWorks2022WithoutIndexes;Integrated Security=True;";

        const String DBUnindexed = "Data Source=ERMAN;Initial Catalog=AdventureWorks2022WithIndexes;Integrated Security=True;";

        String connectionString;

        private bool isStarted = false;

        private readonly int transactionNumber = 100; //Set low for testing purposes
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
        private void StartSim_Click(object sender, EventArgs e)
        {
            //Info console message
            Console.WriteLine("***\nSimulation started...\n***\n");
            isStarted = true;

            int selectedIsolationLevel = IsoLvl_Dropdown.SelectedIndex;
            IsolationLevel isolationLevel = GetIsolationLevel(selectedIsolationLevel);

            int selectedDBIndex = DBDropdown.SelectedIndex;
            connectionString = SelectedDB(selectedDBIndex);


            int numberOfTypeAUsers = (int)TypeADropDown.Value;
            int numberOfTypeBUsers = (int)TypeBDropDown.Value;

            var threads = new Thread[numberOfTypeAUsers + numberOfTypeBUsers];

            var beginTime = DateTime.Now;

            for (int i = 0; i < numberOfTypeAUsers; i++)
            {
                int index = i + 1; // Thread index starts from 1
                threads[i] = new Thread(() => TypeAUserTransaction(connectionString, isolationLevel, index));
                threads[i].Start();
            }

            for (int i = 0; i < numberOfTypeBUsers; i++)
            {
                int index = i + 1; // Thread index starts from 1
                threads[numberOfTypeAUsers + i] = new Thread(() => TypeBUserTransaction(connectionString, isolationLevel, index));
                threads[numberOfTypeAUsers + i].Start();
            }

            // Wait for all threads to finish
            foreach (var thread in threads)
            {
                thread.Join();
            }

            var endTime = DateTime.Now;
            var elapsed = endTime - beginTime;

            //Message Box
            MessageBox.Show($"Isolation level: {isolationLevel} \nElapsed time: {elapsed.TotalSeconds} seconds\nDeadlocks (Type A): {deadlockCountA}\nDeadlocks (Type B): {deadlockCountB}", "Simulation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Info console message
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
        private String SelectedDB(int selectedDBIndex)
        {

            switch (selectedDBIndex)
            {
                case 0:
                    return connectionString = DBIndexed;
                case 1:
                    return connectionString = DBUnindexed;
                default:
                    return connectionString = DBUnindexed; // Default to AdventureWorks2022
            }
        }
        #endregion

        #region User Transaction Methods
        private void TypeAUserTransaction(string connectionString, IsolationLevel isolationLevel, int whichUser)
        {
            var rand = new Random();
            var beginTime = DateTime.Now;
            var timeoutA = 0;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                for (int i = 0; i < transactionNumber; i++) // Perform 10 transactions
                {
                    Debug.WriteLine("Type A For içi: " + i);
                    using (var transaction = connection.BeginTransaction(isolationLevel))
                    {
                        try
                        {
                            bool isUpdated = false;
                            var begindate = "20110101";
                            var enddate = "20111231";

                            if (rand.NextDouble() < 0.5)
                            {
                                begindate = "20110101";
                                enddate = "20111231";
                                isUpdated = true;
                                RunUpdateQuery(connection, transaction, begindate, enddate);
                            }
                            if (rand.NextDouble() < 0.5)
                            {
                                begindate = "20120101";
                                enddate = "20121231";
                                isUpdated = true;
                                RunUpdateQuery(connection, transaction, begindate, enddate);
                            }
                            if (rand.NextDouble() < 0.5)
                            {
                                begindate = "20130101";
                                enddate = "20131231";
                                isUpdated = true;
                                RunUpdateQuery(connection, transaction, begindate, enddate);
                            }
                            if (rand.NextDouble() < 0.5)
                            {
                                begindate = "20140101";
                                enddate = "20141231";
                                isUpdated = true;
                                RunUpdateQuery(connection, transaction, begindate, enddate);
                            }
                            if (rand.NextDouble() < 0.5)
                            {
                                begindate = "20150101";
                                enddate = "20151231";
                                isUpdated = true;
                                RunUpdateQuery(connection, transaction, begindate, enddate);
                            }

                            if (isUpdated)
                            {
                                Debug.WriteLine("Type A before commit.");
                                transaction.Commit();
                                Debug.WriteLine("Type A after commit.");
                            }
                            else
                            {
                                transaction.Rollback();
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 1205 || ex.Number == 3609) // Deadlock error number
                            {
                                // Deadlock occurred, increment deadlock count for Type A
                                Interlocked.Increment(ref deadlockCountA);
                                Console.WriteLine("Deadlock occurred for Type A. Continuing gracefully.");
                                // Wait for a short period and retry
                                Thread.Sleep(rand.Next(100, 500));
                                i--; // Retry the current transaction
                            }
                            else
                            {
                                Debug.WriteLine("Transaction failed for Type A: " + ex.Message);
                                Debug.WriteLine(ex.Number); // For observation purposes
                                timeoutA++;
                                i--;
                                try
                                {
                                    Debug.WriteLine("Rollback Started.");
                                    transaction.Rollback();
                                    Debug.WriteLine("Rollback Ended.");
                                }
                                catch (Exception rollbackEx)
                                {
                                    Debug.WriteLine("Rollback failed: " + rollbackEx.Message);
                                }
                            }
                        }
                    }
                }
            }

            var endTime = DateTime.Now;
            var elapsed = endTime - beginTime;
            Console.WriteLine($"Elapsed time for Type A user {whichUser}: {elapsed.TotalSeconds} seconds. Timeout A: {timeoutA}. Normal Time for A: {elapsed.TotalSeconds - timeoutA}");
        }

        private void TypeBUserTransaction(string connectionString, IsolationLevel isolationLevel, int whichUser)
        {
            var rand = new Random();
            var beginTime = DateTime.Now;
            var timeoutB = 0;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                for (int i = 0; i < transactionNumber; i++) // Perform 10 transactions
                {
                    Debug.WriteLine("Type B For içi: " + i);

                    using (var transaction = connection.BeginTransaction(isolationLevel))
                    {
                        try
                        {
                            bool isSelected = false;
                            var begindate = "20110101";
                            var enddate = "20111231";

                            if (rand.NextDouble() < 0.5)
                            {
                                begindate = "20110101";
                                enddate = "20111231";
                                isSelected = true;
                                RunSelectQuery(connection, transaction, begindate, enddate);
                            }
                            if (rand.NextDouble() < 0.5)
                            {
                                begindate = "20120101";
                                enddate = "20121231";
                                isSelected = true;
                                RunSelectQuery(connection, transaction, begindate, enddate);
                            }
                            if (rand.NextDouble() < 0.5)
                            {
                                begindate = "20130101";
                                enddate = "20131231";
                                isSelected = true;
                                RunSelectQuery(connection, transaction, begindate, enddate);
                            }
                            if (rand.NextDouble() < 0.5)
                            {
                                begindate = "20140101";
                                enddate = "20141231";
                                isSelected = true;
                                RunSelectQuery(connection, transaction, begindate, enddate);
                            }
                            if (rand.NextDouble() < 0.5)
                            {
                                begindate = "20150101";
                                enddate = "20151231";
                                isSelected = true;
                                RunSelectQuery(connection, transaction, begindate, enddate);
                            }


                            if (isSelected)
                            {
                                Debug.WriteLine("Type B before commit.");
                                transaction.Commit();
                                Debug.WriteLine("Type B after commit.");
                            }
                            else
                            {
                                transaction.Rollback();
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 1205 || ex.Number == 3609) // Deadlock error number
                            {
                                // Deadlock occurred, increment deadlock count for Type B
                                Interlocked.Increment(ref deadlockCountB);
                                Debug.WriteLine("Deadlock occurred for Type B. Continuing gracefully.");
                            }
                            else
                            {
                                Debug.WriteLine("Transaction failed for Type B: " + ex.Message);
                                Debug.WriteLine(ex.Number); // For observation purposes
                                timeoutB++;
                                i--;
                                try
                                {
                                    transaction.Rollback();
                                }
                                catch (Exception rollbackEx)
                                {
                                    Debug.WriteLine("Rollback failed: " + rollbackEx.Message);
                                }
                            }
                        }
                    }
                }
            }

            var endTime = DateTime.Now;
            var elapsed = endTime - beginTime;

            Console.WriteLine($"Elapsed time for Type B user {whichUser}: {elapsed.TotalSeconds} seconds. Timeout B: {timeoutB}. Normal Time for B: {elapsed.TotalSeconds - timeoutB}");
        }

        #region SQL Queries
        static void RunUpdateQuery(SqlConnection connection, SqlTransaction transaction, string beginDate, string endDate)
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
                command.CommandTimeout = 5;
                command.Parameters.AddWithValue("@BeginDate", beginDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                command.ExecuteNonQuery();
            }
        }

        static void RunSelectQuery(SqlConnection connection, SqlTransaction transaction, string beginDate, string endDate)
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
                command.CommandTimeout = 5;
                command.Parameters.AddWithValue("@BeginDate", beginDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                command.ExecuteScalar();
            }
        }

        #endregion
        #endregion

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DBDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
