using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using System.Data.SqlTypes;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace HashTestConsole.SQLliteScript
{
    //HashHagDB.db
    //MainTempTable
    public class Connection
    {

        public void FromTxtToDB()
        {
            SQLiteConnection connection;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            string currentPath = Directory.GetCurrentDirectory();

            string connectionString = "Data Source=HashHagDB.db;Cache=Shared;Mode=ReadWriteCreate;";
            connection = new SQLiteConnection();
            using (connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = connection.CreateCommand();
                command.Connection = connection;

                string delerteAllFromTable = "DELETE FROM MainTempTable";
                command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = delerteAllFromTable;
                command.ExecuteNonQuery();

                //update sqlite_sequence set seq = 0 where name='<tablename>'

                string updateId = "UPDATE sqlite_sequence SET seq = 0 WHERE name= 'MainTempTable'";
                command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = updateId;
                command.ExecuteNonQuery();

                using (StreamReader reader = File.OpenText(System.IO.Path.Combine(currentPath, "GenerationTags.txt")))
                {
                    string line;
                    int line_num;
                    //Read all lines from file
                    for (line_num = 0; (line = reader.ReadLine()) != null; ++line_num)
                    {
                        try
                        {
                            string sqlExpression = "INSERT INTO MainTempTable(Word) " +
                                        $"VALUES('{line}')";
                            command = new SQLiteCommand();
                            command.Connection = connection;
                            command.CommandText = sqlExpression;
                            command.ExecuteNonQuery();
                        }
                        catch { Console.WriteLine($"Ошибка в строке {line_num}"); }
                        Console.WriteLine($"Строка номер {line_num}");
                    }
                }
            }
        }


        public void FillAbkectiveTable(List<string> abjectiveList)
        {
            SQLiteConnection connection;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            string currentPath = Directory.GetCurrentDirectory();

            string connectionString = "Data Source=HashHagDB.db;Cache=Shared;Mode=ReadWriteCreate;";
            connection = new SQLiteConnection();
            using (connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = connection.CreateCommand();
                command.Connection = connection;

                string delerteAllFromTable = "DELETE FROM AbjectiveTable";
                command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = delerteAllFromTable;
                command.ExecuteNonQuery();

                //update sqlite_sequence set seq = 0 where name='<tablename>'

                string updateId = "UPDATE sqlite_sequence SET seq = 0 WHERE name= 'AbjectiveTable'";
                command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = updateId;
                command.ExecuteNonQuery();

               foreach(var abj in abjectiveList)
                {
                    
                        try
                        {
                            string sqlExpression = "INSERT INTO AbjectiveTable(Abjective) " +
                                        $"VALUES('{abj}')";
                            command = new SQLiteCommand();
                            command.Connection = connection;
                            command.CommandText = sqlExpression;
                            command.ExecuteNonQuery();
                        }
                        catch { Console.WriteLine($"Ошибка в строке {abj}"); }
                        
                    
                }
            }
        }

        public List<string> GenerationNewHashTags()
        {
            List<string> basicWords = new List<string>();
            SQLiteConnection connection;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            string currentPath = Directory.GetCurrentDirectory();

            string connectionString = "Data Source=HashHagDB.db;Cache=Shared;Mode=ReadWriteCreate;";

            using (connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = connection.CreateCommand();
                command.Connection = connection;
                string sqlExpression = "SELECT Word FROM GlossaryOfBasicWords";
                command = new SQLiteCommand(sqlExpression, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var word = reader.GetValue(0);
                            Console.WriteLine($"{word}");
                            basicWords.Add(word.ToString());
                        }
                    }
                }
                //Console.ReadLine();
                return basicWords;

            }
        }

        public void DeleteAllFromHashTagsTable()
        {
            SQLiteConnection connection;
            string currentPath = Directory.GetCurrentDirectory();

            string connectionString = "Data Source=HashHagDB.db;Cache=Shared;Mode=ReadWriteCreate;";
            connection = new SQLiteConnection();
            using (connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = connection.CreateCommand();
                command.Connection = connection;

                string delerteAllFromTable = "DELETE FROM HashTagsTable";
                command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = delerteAllFromTable;
                command.ExecuteNonQuery();

                //update sqlite_sequence set seq = 0 where name='<tablename>'

                string updateId = "UPDATE sqlite_sequence SET seq = 0 WHERE name= 'HashTagsTable'";
                command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = updateId;
                command.ExecuteNonQuery();
            }

        }

        public void FillHashTagsTable(string hashtag)
        {
            SQLiteConnection connection;
            string currentPath = Directory.GetCurrentDirectory();

            string connectionString = "Data Source=HashHagDB.db;Cache=Shared;Mode=ReadWriteCreate;";
            connection = new SQLiteConnection();
            using (connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = connection.CreateCommand();
                command.Connection = connection;
                
                        try
                        {
                            string sqlExpression = "INSERT INTO HashTagsTable(HashTag) " +
                                        $"VALUES('{hashtag}')";
                            command = new SQLiteCommand();
                            command.Connection = connection;
                            command.CommandText = sqlExpression;
                            command.ExecuteNonQuery();
                        }
                        catch 
                        { 
                            Console.WriteLine($"Этот хештег уже есть в базе"); 
                        }
                       
            }
        }



        public static List<string> TestConnectionToDatabase(string databaseName)
        {
            List<string> questions = new List<string>();

            string connectionString = $"Data Source={databaseName}.db;Cache=Shared;Mode=ReadWriteCreate;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            //connection.Open();
            //connection.Close();

            using (connection = new SQLiteConnection(connectionString))
            {
                string sql = "SELECT * FROM  Questions ";
                // Создаем объект DataAdapter
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
                // Создаем объект Dataset
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                // Отображаем данные
                // перебор всех таблиц
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var cells = row.ItemArray;
                       // Question question = new Question(Convert.ToInt32(cells.GetValue(0)), Convert.ToInt32(cells.GetValue(1)), Convert.ToString(cells.GetValue(2)));
                       // questions.Add(question);
                    }
                }
            }


            using (connection = new SQLiteConnection(connectionString))
            {
                string sql = "SELECT * FROM  ResponseToQuestion";
                // Создаем объект DataAdapter
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
                // Создаем объект Dataset
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                // Отображаем данные
                // перебор всех таблиц
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var cells = row.ItemArray;
                        string answerText = Convert.ToString(cells.GetValue(2));
                        bool isTrue = Convert.ToInt32(cells.GetValue(3)) == 1 ? true : false;
                       // Answer answer = new Answer(answerText, isTrue);
                        //questions.Add(answer);
                       // questions.First(p => p.Id == Convert.ToInt32(cells.GetValue(1))).AnswerList.Add(answer);
                    }
                }
            }
            return questions;
        }
    }
}
