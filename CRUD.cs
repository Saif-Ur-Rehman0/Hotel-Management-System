using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace db_connectivity.Models
{
 
    public class CRUD
    {
        public static string connectionString = "data source=DESKTOP-03NFORP; Initial Catalog=HotelManagement;Integrated Security=true";
        public static List<Staff> getAllStaff()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("viewStaff", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Staff> list = new List<Staff>();
                while (rdr.Read())
                {
                    Staff user = new Staff();
                    user.id= rdr["StaffId"].ToString();
                    user.name = rdr["StaffName"].ToString();
                    user.password = rdr["Pass_word"].ToString();
                    user.phoneno = rdr["Phone"].ToString();
                    user.gender = rdr["Gender"].ToString();
                    user.Cnic = rdr["CNIC"].ToString();
                    user.position = rdr["Position"].ToString();
                    user.salary = rdr["Salary"].ToString();
                    list.Add(user);
                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }

        public static int Signup(string name, string id,string CNIC, string password, string phoneno, string optradio)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("signup", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Char, 7).Value = id; 
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 9).Value = password;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 20).Value = name;
                cmd.Parameters.Add("@phone", SqlDbType.Char, 12).Value = phoneno;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar, 6).Value = optradio;
                cmd.Parameters.Add("@CNIC", SqlDbType.Char, 15).Value = CNIC;
                cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@status"].Value);
               


            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }

        public static int Login(string userId, string password)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("signin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Char, 7).Value = userId;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 9).Value = password;


                cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@status"].Value);



            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }

        public static int Check_staff(string userId, string password, string position)
        {
            //YAHA check karna hy k userid hy k nahi databse mein through procedure
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("checkstaff", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Char, 7).Value = userId;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 9).Value = password;
                cmd.Parameters.Add("@position", SqlDbType.VarChar, 13).Value = position;

                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);



            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public static int addstaff(string salary, string name, string id, string CNIC, string password, string phoneno, string optradio, string position)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("addstaff", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Char, 7).Value = id;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 9).Value = password;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 20).Value = name;
                cmd.Parameters.Add("@phone", SqlDbType.Char, 12).Value = phoneno;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar, 6).Value = optradio;
                cmd.Parameters.Add("@CNIC", SqlDbType.Char, 15).Value = CNIC;
                cmd.Parameters.Add("@position", SqlDbType.Char, 7).Value = position;
                cmd.Parameters.Add("@salary", SqlDbType.Int).Value = salary;
                cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@status"].Value);



            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }

        public static int Removestaff(string id, string position)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("Removestaff", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Staffid", SqlDbType.Char, 7).Value = id;
                cmd.Parameters.Add("@position", SqlDbType.Char, 7).Value = position;
                cmd.Parameters.Add("@out", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@out"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }

        public static int updatestaff(string salary, string name, string id, string CNIC, string password, string phoneno, string optradio, string position)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("updatestaff", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Char, 7).Value = id;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 9).Value = password;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 20).Value = name;
                cmd.Parameters.Add("@phone", SqlDbType.Char, 12).Value = phoneno;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar, 6).Value = optradio;
                cmd.Parameters.Add("@CNIC", SqlDbType.Char, 15).Value = CNIC;
                cmd.Parameters.Add("@position", SqlDbType.Char, 7).Value = position;
                cmd.Parameters.Add("@salary", SqlDbType.Int).Value = salary;
                cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@status"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }

        public static List<Roomers> getroomRecords(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("viewRoomRequests", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Char, 7).Value = id;
                SqlDataReader rdr = cmd.ExecuteReader();

                List<Roomers> list = new List<Roomers>();
                while (rdr.Read())
                {
                    Roomers user = new Roomers();
                    user.Custid = rdr["CustId"].ToString();
                    user.Staffid = rdr["StaffId"].ToString();
                    user.Roomtype = rdr["RType"].ToString();
                    user.stat = rdr["stat"].ToString();
                    user.Checkindate = rdr["RequestDate"].ToString();
                    user.checkoutdate = rdr["RequestDateout"].ToString();
                    user.noguests = rdr["Numguests"].ToString();
                    list.Add(user);
                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }

        public static List<Cookers> getCookRecords(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("viewFoodRequests", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Char, 7).Value = id;
                SqlDataReader rdr = cmd.ExecuteReader();

                List<Cookers> list = new List<Cookers>();
                while (rdr.Read())
                {
                    Cookers user = new Cookers();
                    user.cookid = rdr["StaffId"].ToString();
                    user.Custid = rdr["CustId"].ToString();
                    user.FoodName = rdr["FoodName"].ToString();
                    user.Foodtype = rdr["FType"].ToString();
                    user.Noofitems = rdr["NumItems"].ToString();
                    user.stat = rdr["stat"].ToString();
                    list.Add(user);
                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }

        }

        public static int savecookchoice(string name, string custid, string cookid, string choice)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("savecookchoice", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@custid", SqlDbType.Char, 7).Value = custid;
                cmd.Parameters.Add("@cookid", SqlDbType.VarChar, 9).Value = cookid;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 20).Value = name;
                cmd.Parameters.Add("@choice", SqlDbType.Char, 12).Value = choice;

                cmd.ExecuteNonQuery();
                result = 1;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }

        public static int FreeRoom(string RType)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("free_room", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@RType", SqlDbType.VarChar, 30).Value = RType;

                cmd.Parameters.Add("@Room_Id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@Room_Id"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }

        public static int bookroom(Roomers data)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("BookRoom", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@custid", SqlDbType.Char, 7).Value = data.Custid;
                cmd.Parameters.Add("@staffid", SqlDbType.Char, 7).Value = data.Staffid;
                cmd.Parameters.Add("@roomid", SqlDbType.Int).Value = data.RoomId;
                cmd.Parameters.Add("@choice", SqlDbType.Char, 1).Value = data.stat;
                cmd.Parameters.Add("@checkin", SqlDbType.Date).Value = data.Checkindate;
                cmd.Parameters.Add("@checkout", SqlDbType.Date).Value = data.checkoutdate;
                cmd.Parameters.Add("@numguests", SqlDbType.Int).Value = data.noguests;
                cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@status"].Value);
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }
        public static int EnterFeedack(string userId, string About, string comment, string rate)
        {
            
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("enter_feedback", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@CustId", SqlDbType.Char, 7).Value = userId;
                cmd.Parameters.Add("@About", SqlDbType.Char, 4).Value = About;
                cmd.Parameters.Add("@comment", SqlDbType.Char, 500).Value = comment;
                cmd.Parameters.Add("@ratings", SqlDbType.Char, 5).Value = rate;

                cmd.Parameters.Add("@out", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@out"].Value);



            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static int checkcustomer(string userId, string roomid)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("checkcustomer", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@CustId", SqlDbType.Char, 7).Value = userId;
                cmd.Parameters.Add("@RoomId", SqlDbType.Int).Value = roomid;
                cmd.Parameters.Add("@out", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@out"].Value);



            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static int Request_for_food(string userId, string Ftype,string quantity)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("request_for_food", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@CustId", SqlDbType.Char, 7).Value = userId;
                cmd.Parameters.Add("@FType", SqlDbType.VarChar, 30).Value = Ftype;
                cmd.Parameters.Add("@quantity", SqlDbType.VarChar, 7).Value = quantity;
                cmd.Parameters.Add("@out", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@out"].Value);



            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static int Request_for_room(string userId, string rtype, string requestdate, string requestdateout, string numguests)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("request_for_room", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@CustId", SqlDbType.Char, 7).Value = userId;
                cmd.Parameters.Add("@rtype", SqlDbType.VarChar, 30).Value = rtype;
                cmd.Parameters.Add("@requestdate", SqlDbType.Date).Value = requestdate;
                cmd.Parameters.Add("@requestdateout", SqlDbType.Date).Value = requestdateout;
                cmd.Parameters.Add("@numguests", SqlDbType.Int).Value = numguests;
                cmd.Parameters.Add("@out", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@out"].Value);



            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static int checkdate(string requestdate, string requestdateout)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("checkdate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@r1", SqlDbType.Date).Value = requestdate;
                cmd.Parameters.Add("@r2", SqlDbType.Date).Value = requestdateout;
                cmd.Parameters.Add("@out", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@out"].Value);



            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
    }
}