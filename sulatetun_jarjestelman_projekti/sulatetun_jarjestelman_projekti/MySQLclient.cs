using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace sulatetun_jarjestelman_projekti
{
    class MySQLClient
    {
        private const String Server = "mysli.oamk.fi";
        private const String DataBase = "opisk_t7koja01";
        private const String UID = "t7koja01";
        private const String Password = "SulJarR2";
        private static MySqlConnection dbConnection;

        private int LastUsedID = 0;
        int repeat = 0;
        bool hasLoad = false;

        public int id_send { get; private set; }
        public int speedControllX_send { get; private set; }
        public int turnControllY_send { get; private set; }
        public int controllerButton_send { get; private set; }

        int[] tempSend = new int[3] { 0, 0, 0 };

        public MySQLClient(int id, int scX, int tcY, int cb)
        {
            id_send = id;
            speedControllX_send = scX;
            turnControllY_send = tcY;
            controllerButton_send = cb;
        }
        public MySQLClient()
        {

        }

        public static void InitDB()
        {
            MySqlConnectionStringBuilder sqlBuilder = new MySqlConnectionStringBuilder();
            sqlBuilder.Server = Server;
            sqlBuilder.Database = DataBase;
            sqlBuilder.UserID = UID;
            sqlBuilder.Password = Password;

            String sConnection = sqlBuilder.ToString();
            sqlBuilder = null;

            dbConnection = new MySqlConnection(sConnection);
        }

        public void loadMySql(int speedX, int turnY, int cButton)
        {
            this.speedControllX_send = speedX;
            this.turnControllY_send = turnY;
            this.controllerButton_send = cButton;
            if (hasLoad == true)
            {
                if (tempSend[0] == speedX && (tempSend[1] == turnY || tempSend[1] == -10 || tempSend[1] == 0) && tempSend[2] == cButton)
                {
                    repeat += 1;
                }
                else
                {
                    repeat = 0;
                }

                //pc db rb db pc check
                bool check = checkPreviousCheck();
                if (check == true)
                {
                    if (repeat == 0)
                    {
                        insertToDatabase();
                    }
                    else
                    {
                        updateRepeat();
                    }
                }
                else
                {

                }
            }
            else
            {
                insertToDatabase();
                hasLoad = true;
            }

            tempSend[0] = speedX;
            tempSend[1] = turnY;
            tempSend[2] = cButton;
        }

        private bool checkPreviousCheck()
        {
            MySqlCommand scm = new MySqlCommand();
            dbConnection.Open();
            scm.Connection = dbConnection;

            scm.CommandText = "SELECT connectionCheck FROM pcOut WHERE idpcOut=@lastindex";
            scm.Prepare();
            scm.Parameters.AddWithValue("@lastindex", LastUsedID);

            int tempVal = scm.ExecuteNonQuery();
            dbConnection.Close();

            if (tempVal == 1)
            {
                return true;
            }
            else
            {
                return false; //FALSE
            }
        }

        //Insert Data To Database
        private void insertToDatabase()
        {
            //Insert Data to DataBase From Arduino Controller
            MySqlCommand scm = new MySqlCommand();
            dbConnection.Open();
            scm.Connection = dbConnection;

            scm.CommandText = "INSERT INTO pcOut(speedControl, turnControl, button) VALUES (@speed,@turn,@cb)";

            scm.Prepare();
            scm.Parameters.AddWithValue("@speed", speedControllX_send);
            scm.Parameters.AddWithValue("@turn", turnControllY_send);
            scm.Parameters.AddWithValue("@cb", controllerButton_send);

            scm.ExecuteNonQuery();
            LastUsedID = (int)scm.LastInsertedId;

            dbConnection.Close();
        }

        private void updateRepeat()
        {
            MySqlCommand scm = new MySqlCommand();
            dbConnection.Open();
            scm.Connection = dbConnection;

            scm.CommandText = "UPDATE pcOut SET repeatControl=@value WHERE idpcOut=@Lastid";

            scm.Prepare();
            scm.Parameters.AddWithValue("@value", repeat);
            scm.Parameters.AddWithValue("@Lastid", LastUsedID);

            scm.ExecuteNonQuery();
            //LastUsedID = (int)scm.LastInsertedId;

            dbConnection.Close();
        }
        public int getRepeats()
        {
            return repeat;
        }
    }
}
