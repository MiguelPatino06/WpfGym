using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerClub.Bussiness.Model;
using PowerClub.Bussiness.Utils;
using System.Configuration;
using System.Data.SqlClient;

namespace PowerClub.Bussiness.Services
{
    public class ReportServices: BaseServices
    {

        public static readonly string ParamConnectionString = "ReportDataSource";
        //public static readonly string ConnectionString = ConfigurationManager.AppSettings[ParamConnectionString];
        
        public ReportServices()
        {
            
        }
        public DataTable GetDataTardanzas(string procedure, ParametersTardanzas criterios)
        {
            DataTable dt = new DataTable();
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ReportDataSource"];
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(procedure, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IDBRANCHOFICCE", SqlDbType.Int).Value = criterios.IdOffice;
                    cmd.Parameters.Add("@IDTRAINER", SqlDbType.Int).Value = criterios.IdTrainer;
                    cmd.Parameters.Add("@IDWORKOUT", SqlDbType.Int).Value = criterios.IdWorkout;
                    cmd.Parameters.Add("@DATESTART", SqlDbType.DateTime).Value = criterios.DateStart;
                    cmd.Parameters.Add("@DATEEND", SqlDbType.DateTime).Value = criterios.DateEnd;
                    cmd.Parameters.Add("@DELAY", SqlDbType.Int).Value = criterios.Delay;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return dt;
        }


        public DataTable GetDataSustitutos(string procedure, ParametersSustituciones criterios)
        {
            DataTable dt = new DataTable();
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ReportDataSource"];
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(procedure, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IDOFFICE", SqlDbType.Int).Value = criterios.IdOffice;
                    cmd.Parameters.Add("@IDWORKOUT", SqlDbType.Int).Value = criterios.IdWorkout;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = criterios.DateStart;
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = criterios.DateEnd;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return dt;
        }

        public DataTable GetClases(string procedure, ParametersClases criterios)
        {
            DataTable dt = new DataTable();
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ReportDataSource"];
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(procedure, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IDBRANCHOFICCE", SqlDbType.Int).Value = criterios.IdOffice;
                    cmd.Parameters.Add("@IDTRAINER", SqlDbType.Int).Value = criterios.IdTrainer;
                    cmd.Parameters.Add("@IDWORKOUT", SqlDbType.Int).Value = criterios.IdWorkout;
                    cmd.Parameters.Add("@IDSTAFF", SqlDbType.Int).Value = criterios.IdStaff;
                    cmd.Parameters.Add("@IDSTATUS", SqlDbType.Int).Value = criterios.IdStatus;
                    cmd.Parameters.Add("@DATESTART", SqlDbType.DateTime).Value = criterios.DateStart;
                    cmd.Parameters.Add("@DATEEND", SqlDbType.DateTime).Value = criterios.DateEnd;
                    cmd.Parameters.Add("@ISCHECKED", SqlDbType.Int).Value = criterios.Asistencia;


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return dt;
        }

    }
}
