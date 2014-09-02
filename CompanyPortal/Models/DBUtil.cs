using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CompanyPortal.Models
{
    public class DBUtil
    {
        SqlConnection conn;
        public DBUtil()
        {
            try
            {
                string connectionString = "Data Source=(local);Initial Catalog=CompanyPortal;Integrated Security=True;";
                conn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                //Log error
            }

        }
        public void Login(ref User user)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_Login";
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter();

            parameter.ParameterName = "@UserName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.UserName;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Password";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.Password;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.UserID = Convert.ToInt32(reader[0]);
                        user.UserName = Convert.ToString(reader[1]);
                        user.FirstName = Convert.ToString(reader[2]);
                        user.LastName = Convert.ToString(reader[3]);
                        user.CompanyID = Convert.ToInt32(reader[4]);
                        break;
                    }
                    reader.Close();
                    conn.Close();

                    user.CompanyID = GetCompanyByUser(user.UserID);
                    GetUserGroupsByUser(ref user);
                }
                else
                {
                    conn.Close();
                    user.UserID = -1;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Log error
            }
        }

        public User Register(Company company, User user)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "ins_Register";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@CompanyName";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = company.CompanyName;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@UserName";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = user.UserName;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Password";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = user.Password;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@FirstName";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = user.FirstName;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@LastName";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = user.LastName;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Return";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.ReturnValue;
                parameter.Value = -1;
                command.Parameters.Add(parameter);

                conn.Open();
                command.ExecuteNonQuery();
                user.UserID = (int)parameter.Value;
                conn.Close();
                Login(ref user);
                return user;
            }
            catch(Exception ex)
            {
                //Log Error
                return null;
            }
        }

        public int GetCompanyByUser(int userID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_CompanyByUser";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = userID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Return";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = -1;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                return (int)parameter.Value;
            }
            catch(Exception ex)
            {
                //Log error
            }
            return -1;
        }

        public List<Post> GetPostsByUser(int userID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_PostsByUser";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = userID;
            command.Parameters.Add(parameter);

            List<Post> posts = new List<Post>();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Post p = new Post();
                        p.PostID = Convert.ToInt32(reader[0]);
                        p.PostText = Convert.ToString(reader[1]);
                        p.PostTitle = Convert.ToString(reader[2]);
                        p.PostDate = Convert.ToString(reader[3]);
                        p.UserName = Convert.ToString(reader[4]);
                        posts.Add(p);
                    }
                }
            }
            catch(Exception ex)
            {
                //Log error
            }
            conn.Close();
            return posts;
        }

        public List<Vote> GetVotesByUser(int userID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_TopCurrentVotesByUser";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = userID;
            command.Parameters.Add(parameter);

            List<Vote> votes = new List<Vote>();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Vote v = new Vote();
                        v.VoteID = Convert.ToInt32(reader[0]);
                        v.VoteName = Convert.ToString(reader[1]);
                        v.VoteQuestion = Convert.ToString(reader[2]);
                        v.VoteCreator = Convert.ToInt32(reader[3]);
                        v.VoteDate = Convert.ToString(reader[4]);
                        v.EndDate = Convert.ToString(reader[5]);
                        TimeSpan daysRemaining = Convert.ToDateTime(v.EndDate) - DateTime.Now;
                        v.DaysRemaining = daysRemaining.Days.ToString();
                        votes.Add(v);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
            }
            conn.Close();
            return votes;
        }

        public Vote GetVoteByID(int voteID, int userID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_VoteByID";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = userID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@VoteID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = voteID;
            command.Parameters.Add(parameter);

            Vote v = new Vote();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        v.VoteID = Convert.ToInt32(reader[0]);
                        v.VoteName = Convert.ToString(reader[1]);
                        v.VoteQuestion = Convert.ToString(reader[2]);
                        v.VoteCreator = Convert.ToInt32(reader[3]);
                        v.VoteDate = Convert.ToString(reader[4]);
                        conn.Close();
                        return v;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
            }
            conn.Close();
            return v;
        }

        public Vote GetVoteByOptionID(int voteOptionID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_VoteByOptionID";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@VoteOptionID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = voteOptionID;
            command.Parameters.Add(parameter);

            Vote v = new Vote();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        v.VoteID = Convert.ToInt32(reader[0]);
                        v.VoteName = Convert.ToString(reader[1]);
                        v.VoteQuestion = Convert.ToString(reader[2]);
                        v.VoteCreator = Convert.ToInt32(reader[3]);
                        v.VoteDate = Convert.ToString(reader[4]);
                        conn.Close();
                        return v;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
            }
            conn.Close();
            return v;
        }

        public List<VoteOption> GetVoteOptions(int voteID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_VoteOptions";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@VoteID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = voteID;
            command.Parameters.Add(parameter);

            List<VoteOption> v = new List<VoteOption>();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VoteOption vo = new VoteOption();
                        vo.VoteOptionID = Convert.ToInt32(reader[0]);
                        vo.OptionText = Convert.ToString(reader[1]);
                        v.Add(vo);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
            }
            conn.Close();
            return v;
        }

        public List<VoteResult> GetVoteResults(int voteID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_VoteResultsByID";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@VoteID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = voteID;
            command.Parameters.Add(parameter);

            List<VoteResult> v = new List<VoteResult>();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VoteResult vr = new VoteResult();
                        vr.OptionText = Convert.ToString(reader[0]);
                        vr.VoteCount = Convert.ToInt32(reader[1]);
                        v.Add(vr);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
            }
            conn.Close();
            return v;
        }

        public void GetUserGroupsByUser(ref User user)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_UserGroupsByUser";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.UserID;
            command.Parameters.Add(parameter);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Group group = new Group();
                        group.groupID = Convert.ToInt32(reader[0]);
                        group.groupName = Convert.ToString(reader[1]);
                        user.UserGroups.Add(group);
                    }
                }
            }
            catch(Exception ex)
            {
                //Log error
            }
            conn.Close();
        }

        public List<Group> GetUserGroups(int userID)
        {
            int companyID = GetCompanyByUser(userID);

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_UserGroups";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@CompanyID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = companyID;
            command.Parameters.Add(parameter);

            List<Group> userGroups = new List<Group>();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Group group = new Group();
                        group.groupID = Convert.ToInt32(reader[0]);
                        group.groupName = Convert.ToString(reader[1]);
                        userGroups.Add(group);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
            }
            conn.Close();
            return userGroups;
        }

        public Company GetCompanyInfo(int companyID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_CompanyInfo";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@CompanyID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = companyID;
            command.Parameters.Add(parameter);
            Company company = new Company();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                company.CompanyID = companyID;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        company.CompanyID = Convert.ToInt32(reader[0]);
                        company.CompanyName = Convert.ToString(reader[1]);
                        company.Address = Convert.ToString(reader[2]);
                        company.City = Convert.ToString(reader[3]);
                        company.Province = Convert.ToString(reader[4]);
                        company.PostalCode = Convert.ToString(reader[5]);
                        company.Telephone = Convert.ToString(reader[6]);
                        company.CreatedDate = Convert.ToDateTime(reader[7]);
                    }
                }
            }
            catch(Exception ex)
            {
                //Log error
            }
            conn.Close();
            return company;
        }

        public User GetUserInfo(int userID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_UserByID";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = userID;
            command.Parameters.Add(parameter);
            User user = new User();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                user.UserID = userID;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.UserID = Convert.ToInt32(reader[0]);
                        user.UserName = Convert.ToString(reader[1]);
                        user.FirstName = Convert.ToString(reader[2]);
                        user.LastName = Convert.ToString(reader[3]);
                        user.CompanyID = Convert.ToInt32(reader[4]);
                        user.Password = Convert.ToString(reader[5]);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
            }
            conn.Close();

            command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_UserGroupsByUser";
            command.CommandType = CommandType.StoredProcedure;

            parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = userID;
            command.Parameters.Add(parameter);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Group group = new Group();
                        group.groupID = Convert.ToInt32(reader[0]);
                        group.groupName = Convert.ToString(reader[1]);
                        user.UserGroups.Add(group);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
            }
            conn.Close();
            return user;
        }

        public List<User> GetUsersForCompany(int userID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_UsersByCompany";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = userID;
            command.Parameters.Add(parameter);

            List<User> users = new List<User>();
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.UserID = Convert.ToInt32(reader[0]);
                        user.UserName = Convert.ToString(reader[1]);
                        user.FirstName = Convert.ToString(reader[2]);
                        user.LastName = Convert.ToString(reader[3]);
                        user.CompanyID = Convert.ToInt32(reader[4]);
                        user.Password = Convert.ToString(reader[5]);
                        users.Add(user);
                    }
                }
            }
            catch(Exception ex)
            {
                //Log error
            }
            conn.Close();
            return users;
        }
        public bool UpdateCompanyInfo(Company company)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "upd_Company";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@CompanyID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = company.CompanyID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@CompanyName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = company.CompanyName;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Return";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.ReturnValue;
            parameter.Value = -1;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                if ((int)parameter.Value != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                //Log error
                return false;
            }
        }

        public int InsertUser(User user)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "ins_User";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@CompanyID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.CompanyID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@UserName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.UserName;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@FirstName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.FirstName;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@LastName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.LastName;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Password";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.Password;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Return";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = -1;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                if ((int)parameter.Value != -1)
                {
                    return Convert.ToInt32(parameter.Value);
                }
                else
                {
                    return Convert.ToInt32(parameter.Value);
                }
            }
            catch(Exception ex)
            {
                //Log error
                return -1;
            }
        }

        public bool InsertUserVote(Vote v, int voteOptionID, int UserID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "ins_UserVote";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@VoteID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = v.VoteID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@VoteOptionID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = voteOptionID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = UserID;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                if ((int)parameter.Value != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //Log error
                return false;
            }
        }

        public bool UpdateUserInfo(User user)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "upd_User";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@CompanyID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.CompanyID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.UserID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@UserName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.UserName;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@FirstName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.FirstName;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@LastName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.LastName;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Password";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = user.Password;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Return";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.ReturnValue;
            parameter.Value = -1;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                if ((int)parameter.Value != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                //Log error
                return false;
            }
        }
        public bool ChangePassword(int UserID, string UserName, string Password)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "upd_Password";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = UserID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@UserName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = UserName;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Password";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Password;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Return";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.ReturnValue;
            parameter.Value = -1;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                if ((int)parameter.Value != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                //Log error
                return false;
            }
        }

        public bool CheckAdmin(int UserID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_CheckAdmin";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = UserID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Return";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = -1;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                if ((int)parameter.Value != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                //Log error
                return false;
            }
        }

        public int AddVote(Vote vote)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "ins_Vote";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@VoteName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = vote.VoteName;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@VoteQuestion";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = vote.VoteQuestion;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@VoteDate";
            parameter.SqlDbType = SqlDbType.DateTime;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = vote.VoteDate;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@EndDate";
            parameter.SqlDbType = SqlDbType.DateTime;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = vote.EndDate;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@VoteCreator";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = vote.VoteCreator;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Return";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = -1;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                if ((int)parameter.Value != -1)
                {
                    return (int)parameter.Value;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                //Log error
                return -1;
            }
        }

        public bool InsertUserUserGroups(AddUserPage page)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "del_UserUserGroups";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = page.userID;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                //Log error
                return false;
            }

            foreach (Group g in page.groups)
            {
                if (g.isChecked)
                {
                    command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandText = "ins_UserUserGroup";
                    command.CommandType = CommandType.StoredProcedure;

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@UserGroupID";
                    parameter.SqlDbType = SqlDbType.Int;
                    parameter.Direction = ParameterDirection.Input;
                    parameter.Value = g.groupID;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@UserID";
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Direction = ParameterDirection.Input;
                    parameter.Value = page.userID;
                    command.Parameters.Add(parameter);

                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        //Log error
                        return false;
                    }
                }
            }
            return true;
        }
        public bool InsertVoteOptions(List<VoteOption> options, int voteID)
        {
            foreach (VoteOption v in options)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "ins_VoteOption";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@VoteID";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = voteID;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@VoteOption";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = v.OptionText;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Return";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Output;
                parameter.Value = -1;
                command.Parameters.Add(parameter);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                    if ((int)parameter.Value == -1)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Log error
                    return false;
                }
            }
            return true;
        }
        public bool InsertVoteUserGroups(AddVoteGroupsPage page)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "del_VoteUserGroups";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@VoteID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = page.voteID;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                //Log error
                return false;
            }

            foreach (Group g in page.groups)
            {
                if (g.isChecked)
                {
                    command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandText = "ins_VoteUserGroup";
                    command.CommandType = CommandType.StoredProcedure;

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@UserGroupID";
                    parameter.SqlDbType = SqlDbType.Int;
                    parameter.Direction = ParameterDirection.Input;
                    parameter.Value = g.groupID;
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter();
                    parameter.ParameterName = "@VoteID";
                    parameter.SqlDbType = SqlDbType.VarChar;
                    parameter.Direction = ParameterDirection.Input;
                    parameter.Value = page.voteID;
                    command.Parameters.Add(parameter);

                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        //Log error
                        return false;
                    }
                }
            }
            return true;
        }
        public bool DeleteVote(int voteID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "del_Vote";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@VoteID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = voteID;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                //Log error
                return false;
            }
            return true;
        }
        public bool CheckUserVote(int voteID, int userID)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "sel_CheckUserVote";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@VoteID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = voteID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = userID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@Return";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = -1;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                if ((int)parameter.Value != -1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Log error
                return true;
            }
            return false;
        }
        public bool InsertUserError(int userID, string errorMessage, string errorPage)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "ins_UserError";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = userID;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@ErrorMessage";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = errorMessage;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@ErrorPage";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = errorPage;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                //Log error
                return false;
            }
            return true;
        }
        public bool InsertUserError(string errorMessage, string errorPage)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = "ins_UserError";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@UserID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = null;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@ErrorMessage";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = errorMessage;
            command.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@ErrorPage";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = errorPage;
            command.Parameters.Add(parameter);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                //Log error
                return false;
            }
            return true;
        }
    }
}