using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedDB_Form
{
    //DEADLOCK COUNTER WORKS WELL ON SERIALIZABLE LEVEL BUT HAS BUGS ON OTHER LEVELS
    public partial class Form1 : Form
    {
        int transactionNumber = 5; // Set low for testing purposes
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new object[] { "Read Uncommitted", "Read Committed", "Repeatable Read", "Serializable" });
            comboBox1.SelectedIndex = 0; // Default Read Uncommitted
        }

        #region Start Simulation Button
        private async void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("***\nSimulation started...\n***\n");

            string connectionString = "Data Source=NISA-PC;Initial Catalog=AdventureWorks2022;Integrated Security=True;";
            int selectedIsolationLevel = comboBox1.SelectedIndex;
            IsolationLevel isolationLevel = GetIsolationLevel(selectedIsolationLevel);

            int numberOfTypeAUsers = (int)numericUpDown1.Value; // number of type A users
            int numberOfTypeBUsers = (int)numericUpDown2.Value; // number of type B users

            int deadlockCountA = 0;
            int deadlockCountB = 0;

            var tasks = new Task[numberOfTypeAUsers + numberOfTypeBUsers];

            var beginTime = DateTime.Now;

            for (int i = 0; i < numberOfTypeAUsers; i++)
            {
                tasks[i] = Task.Run(() => TypeAUserTransaction(connectionString, isolationLevel, ref deadlockCountA));
            }

            for (int i = numberOfTypeAUsers; i < numberOfTypeAUsers + numberOfTypeBUsers; i++)
            {
                tasks[i] = Task.Run(() => TypeBUserTransaction(connectionString, isolationLevel, ref deadlockCountB));
            }

            await Task.WhenAll(tasks);

            var endTime = DateTime.Now;
            var elapsed = endTime - beginTime;

            MessageBox.Show($"Elapsed time: {elapsed.TotalSeconds} seconds\nDeadlocks (Type A): {deadlockCountA}\nDeadlocks (Type B): {deadlockCountB}", "Simulation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Console.WriteLine("\n***\nSimulation complete.\n***\n");
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

        #region User Transaction Methods
        private void TypeAUserTransaction(string connectionString, IsolationLevel isolationLevel, ref int deadlockCountA)
        {
            var rand = new Random();
            var beginTime = DateTime.Now;

            for (int i = 0; i < transactionNumber; i++) // Perform 100 transactions
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction(isolationLevel))
                    {
                        try
                        {
                            if (rand.NextDouble() < 0.5)
                                RunUpdateQuery(connection, transaction, "20110101", "20111231");
                            if (rand.NextDouble() < 0.5)
                                RunUpdateQuery(connection, transaction, "20120101", "20121231");
                            if (rand.NextDouble() < 0.5)
                                RunUpdateQuery(connection, transaction, "20130101", "20131231");
                            if (rand.NextDouble() < 0.5)
                                RunUpdateQuery(connection, transaction, "20140101", "20141231");
                            if (rand.NextDouble() < 0.5)
                                RunUpdateQuery(connection, transaction, "20150101", "20151231");

                            transaction.Commit();
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 1205) // Deadlock error number
                            {
                                // Deadlock occurred, increment deadlock count for Type A
                                deadlockCountA++;
                                Console.WriteLine("Deadlock occurred for Type A. Continuing gracefully.");
                            }
                            else
                            {
                                Console.WriteLine("Transaction failed: " + ex.Message);
                                transaction.Rollback();
                            }
                        }
                    }
                }
            }

            var endTime = DateTime.Now;
            var elapsed = endTime - beginTime;
            Console.WriteLine($"Elapsed time for Type A user: {elapsed.TotalSeconds} seconds");
        }


        private void TypeBUserTransaction(string connectionString, IsolationLevel isolationLevel, ref int deadlockCountB)
        {
            var rand = new Random();
            var beginTime = DateTime.Now;

            for (int i = 0; i < transactionNumber; i++) // Perform 100 transactions
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction(isolationLevel))
                    {
                        try
                        {
                            if (rand.NextDouble() < 0.5)
                                RunSelectQuery(connection, transaction, "20110101", "20111231");
                            if (rand.NextDouble() < 0.5)
                                RunSelectQuery(connection, transaction, "20120101", "20121231");
                            if (rand.NextDouble() < 0.5)
                                RunSelectQuery(connection, transaction, "20130101", "20131231");
                            if (rand.NextDouble() < 0.5)
                                RunSelectQuery(connection, transaction, "20140101", "20141231");
                            if (rand.NextDouble() < 0.5)
                                RunSelectQuery(connection, transaction, "20150101", "20151231");

                            transaction.Commit();
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 1205) // Deadlock error number
                            {
                                // Deadlock occurred, increment deadlock count for Type B
                                deadlockCountB++;
                                Console.WriteLine("Deadlock occurred for Type B. Continuing gracefully.");
                            }
                            else
                            {
                                Console.WriteLine("Transaction failed: " + ex.Message);
                                transaction.Rollback();
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
                command.Parameters.AddWithValue("@BeginDate", beginDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                command.ExecuteScalar();
            }
        }
        #endregion

        #region Necessary methods
        // IDK why but without this methods the program won't run
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
