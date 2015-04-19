using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace MemoryKidz
{
    /// <summary>
    /// Database-Class by Marvin Karaschewski
    /// Reading and Writing to and from Database of integer values for scores and byte[] for Images
    /// </summary>
    public static class DatabaseConnection
    {
        private static MySqlConnection connection;

        /// <summary>
        /// Open Database-Connection
        /// </summary>
        private static void EstablishConnection()
        {
            // Provides the Connection-Information and establishes the link
            connection = new MySqlConnection("server=outcast-prophets.no-ip.org; Database = inf1c; user id=inf1c; password=admin;");
            connection.Open();
        }

        /// <summary>
        /// Close Database-Connection
        /// </summary>
        private static void CloseConnection()
        {
            // Closes the Connection
            connection.Close();
        }

        /// <summary>
        /// Translates a given integer-value between 1 and 3 to a string representing a difficulty
        /// This may be used to interpret the difficulty to identify a table within the database
        /// </summary>
        private static string TranslateToDifficultyString(int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    {
                        return "easy";
                    }
                case 2:
                    {
                        return "medium";
                    }
                case 3:
                    {
                        return "hard";
                    }
                default:
                    return "ERROR";
            }
        }

        /// <summary>
        /// Inserts values and pictures into the Database
        /// </summary>
        /// <param name="values"></param>
        /// <param name="images"></param>
        /// <param name="difficulty"></param>
        public static void InsertValues(int[] values, List<byte[]> images,int difficulty)
        {
            EstablishConnection();

            string tableName = TranslateToDifficultyString(difficulty); 

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    MySqlCommand command = new MySqlCommand("", connection);

                    command.CommandText = "INSERT INTO " + tableName + " (beeld, punten) VALUES (@image, @score)";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@image", images[i]);
                    command.Parameters.AddWithValue("@score", values[i]);
                    command.Parameters[1].MySqlDbType = MySqlDbType.MediumBlob;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("INSERT FAILED: " + ex.Message);
            }

            CloseConnection();
        }

        /// <summary>
        /// Returns all 10 values in a given table. This one does not return any images from the database
        /// </summary>
        public static List<int> ReadValuesOnly(int difficulty)
        {
            List<int> values = new List<int>();
            string tableName = TranslateToDifficultyString(difficulty);

            EstablishConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("", connection);

                command.CommandText = "SELECT * FROM " + tableName + ";";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int score = reader.GetInt32("punten");
                    values.Add(score);
                }
                reader.Close();
                CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("QUERY FAILED: " + ex.Message + "-- QUERY: " + ex.Source);
                CloseConnection();
            }

            CloseConnection();
            return values;
        }

        /// <summary>
        /// Accepts 10 KeyValuePairs of int, byte[] which are meant to replace all scores currently sitting in the database
        /// This is needed to always maintain integrity of the database
        /// </summary>
        public static void ReplaceHighscores(Dictionary<int, byte[]> newValues)
        {
            // Delete the old values to prepare rewriting
            ScrapTable();

            List<int> values = new List<int>();
            List<byte[]> images = new List<byte[]>();

            int index = 0;
            foreach (KeyValuePair<int, byte[]> kvp in newValues)
            {
                values.Add(kvp.Key);
                images.Add(kvp.Value);
                index++;
            }

            /// <summary>
            /// Writes a new set of 10 valuepairs to the table with a given difficulty
            /// </summary>
            InsertValues(values.ToArray(), images, GameSpecs.Difficulty);
            Session.LatestScoreBoard = values.ToArray();

        }

        /// <summary>
        /// Deletes all values in the database for a given difficulty-level
        /// </summary>
        private static void ScrapTable()
        {
            EstablishConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("", connection);

                string tableName = TranslateToDifficultyString(GameSpecs.Difficulty);

                command.CommandText = "TRUNCATE TABLE " + tableName + ";";
                command.ExecuteNonQuery();

                CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DELETION FAILED WITH ERROR: " + ex.Message);
                CloseConnection();
            }

            CloseConnection();
        }

        /// <summary>
        /// Reads a single image from the database, which is queried by score and difficulty
        /// </summary>
        public static MemoryStream ReadSingleImage(int score, int difficulty)
        {
            EstablishConnection();

            string tableName = TranslateToDifficultyString(difficulty);

            try
            {
                MySqlCommand command = new MySqlCommand("", connection);

                command.CommandText = "SELECT beeld FROM " + tableName + " WHERE punten = @score;";
                command.Parameters.AddWithValue("@score", score);
                List<byte> bytes = new List<byte>();

                /// Starts the reader with "sequential access" which is needed for querying byte[]
                MySqlDataReader dataReader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                while (dataReader.Read())
                {
                    int chunkSize = 1;
                    long bytesize = dataReader.GetBytes(0, 0, null, 0, 0);
                    long bytesread = 0;
                    int curpos = 0;
                    byte[] imageData = new byte[bytesize];

                    while (bytesread < bytesize)
                    {
                        bytesread += dataReader.GetBytes(0, curpos, imageData, curpos, chunkSize);
                        curpos += chunkSize;
                    }
                    bytes = imageData.ToList<byte>();
                }

                CloseConnection();
                MemoryStream m = new MemoryStream(bytes.ToArray());
                return m;  
            }
            catch (Exception ex)
            {
                MessageBox.Show("QUERY FAILED: " + ex.Message + "-- QUERY: " + ex.Source);
                CloseConnection();
                return null;
            }
        }
    }
}