using InvoiceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManagementSystem.Controllers
{
    public class TeacherAttandenceController : Controller
    {
        clsCommon objCommon = new clsCommon();

        // GET: TeacherAttandence
        public ActionResult TeacherAttandence()
        {
            if (objCommon.getUserIdFromSession() != 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]

        public ActionResult InsertTeacherAttandence(TeacherAttandenceModel model)
        {
            model = model.addTeacherAttandence(model);
            return Json(model.Response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTeacherAttandence(TeacherAttandenceModel cls)
        {
            try
            {
                int TotalEntries = 0;
                int showingEntries = 0;
                int startentries = 0;
                List<TeacherAttandenceModel> lstTeacherAttandenceList = new List<TeacherAttandenceModel>();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetTeacherAttandenceList", conn);
                cmd.Parameters.AddWithValue("@PageSize", cls.PageSize);
                cmd.Parameters.AddWithValue("@PageIndex", cls.PageIndex);
                cmd.Parameters.AddWithValue("@Search", cls.SearchText);
                cmd.Parameters.AddWithValue("@intActive", cls.intActive); 
                cmd.Parameters.AddWithValue("@TeacherId", objCommon.getTeacherIdFromSession());
                cmd.Parameters.AddWithValue("@UserId", objCommon.getUserIdFromSession());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                conn.Close();


                if (dt != null && dt.Rows.Count > 0)
                {

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        TeacherAttandenceModel obj = new TeacherAttandenceModel();
                        obj.Id = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.TeacherId = Convert.ToInt32(dt.Rows[i]["TeacherId"] == null || dt.Rows[i]["TeacherId"].ToString().Trim() == "" ? null : dt.Rows[i]["TeacherId"].ToString());
                        obj.TeacherName = dt.Rows[i]["FullName"] == null || dt.Rows[i]["FullName"].ToString().Trim() == "" ? null : dt.Rows[i]["FullName"].ToString();
                        obj.Status = Convert.ToBoolean(dt.Rows[i]["Status"] == null || dt.Rows[i]["Status"].ToString().Trim() == "" ? null : dt.Rows[i]["Status"].ToString());
                        obj.Date = dt.Rows[i]["Date"] == null || dt.Rows[i]["Date"].ToString().Trim() == "" ? null : Convert.ToDateTime(dt.Rows[i]["Date"]).ToString("dd/MM/yyyy");
                        obj.LeaveType = Convert.ToInt32(dt.Rows[i]["LeaveType"] == null || dt.Rows[i]["LeaveType"].ToString().Trim() == "" ? null : dt.Rows[i]["LeaveType"].ToString());
                        obj.Reason = dt.Rows[i]["Reason"] == null || dt.Rows[i]["Reason"].ToString().Trim() == "" ? null : dt.Rows[i]["Reason"].ToString();
                        obj.ROWNUMBER = Convert.ToInt32(dt.Rows[i]["ROWNUMBER"] == null || dt.Rows[i]["ROWNUMBER"].ToString().Trim() == "" ? null : dt.Rows[i]["ROWNUMBER"].ToString());
                        obj.PageCount = Convert.ToInt32(dt.Rows[i]["PageCount"] == null || dt.Rows[i]["PageCount"].ToString().Trim() == "" ? null : dt.Rows[i]["PageCount"].ToString());
                        obj.PageSize = Convert.ToInt32(dt.Rows[i]["PageSize"] == null || dt.Rows[i]["PageSize"].ToString().Trim() == "" ? null : dt.Rows[i]["PageSize"].ToString());
                        obj.PageIndex = Convert.ToInt32(dt.Rows[i]["PageIndex"] == null || dt.Rows[i]["PageIndex"].ToString().Trim() == "" ? null : dt.Rows[i]["PageIndex"].ToString());
                        obj.TotalRecord = Convert.ToInt32(dt.Rows[i]["TotalRecord"] == null || dt.Rows[i]["TotalRecord"].ToString().Trim() == "" ? null : dt.Rows[i]["TotalRecord"].ToString());
                        lstTeacherAttandenceList.Add(obj);
                    }
                }
                cls.LSTTeacherAttandenceList = lstTeacherAttandenceList;
                if (cls.LSTTeacherAttandenceList.Count > 0)
                {
                    var pager = new Models.Pager((int)cls.LSTTeacherAttandenceList[0].TotalRecord, cls.PageIndex, (int)cls.PageSize);

                    cls.Pager = pager;
                }
                cls.TotalEntries = TotalEntries;
                cls.ShowingEntries = showingEntries;
                cls.fromEntries = startentries;
                cls.LSTTeacherAttandenceList = lstTeacherAttandenceList;

                return PartialView("_TeacherAttandenceListPartial", cls);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public ActionResult GetTeacherAttandenceee(TeacherAttandenceModel cls)
        {
            try
            {
                int TotalEntries = 0;
                int showingEntries = 0;
                int startentries = 0;
                List<TeacherAttandenceModel> lstTeacherAttandenceList = new List<TeacherAttandenceModel>();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_FilterGetTeacherAttandenceList", conn);
                cmd.Parameters.AddWithValue("@PageSize", cls.PageSize);
                cmd.Parameters.AddWithValue("@PageIndex", cls.PageIndex);
                cmd.Parameters.AddWithValue("@Search", cls.SearchText);
                cmd.Parameters.AddWithValue("@TeacherId", objCommon.getTeacherIdFromSession());
                cmd.Parameters.AddWithValue("@UserId", objCommon.getUserIdFromSession());
                cmd.Parameters.AddWithValue("@Date",cls.Date);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                conn.Close();


                if (dt != null && dt.Rows.Count > 0)
                {

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        TeacherAttandenceModel obj = new TeacherAttandenceModel();
                        obj.Id = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.TeacherId = Convert.ToInt32(dt.Rows[i]["TeacherId"] == null || dt.Rows[i]["TeacherId"].ToString().Trim() == "" ? null : dt.Rows[i]["TeacherId"].ToString());
                        obj.TeacherName = dt.Rows[i]["FullName"] == null || dt.Rows[i]["FullName"].ToString().Trim() == "" ? null : dt.Rows[i]["FullName"].ToString();
                        obj.Status = Convert.ToBoolean(dt.Rows[i]["Status"] == null || dt.Rows[i]["Status"].ToString().Trim() == "" ? null : dt.Rows[i]["Status"].ToString());
                        obj.Date = dt.Rows[i]["Date"] == null || dt.Rows[i]["Date"].ToString().Trim() == "" ? null : Convert.ToDateTime(dt.Rows[i]["Date"]).ToString("dd/MM/yyyy");
                        obj.LeaveType = Convert.ToInt32(dt.Rows[i]["LeaveType"] == null || dt.Rows[i]["LeaveType"].ToString().Trim() == "" ? null : dt.Rows[i]["LeaveType"].ToString());
                        obj.Reason = dt.Rows[i]["Reason"] == null || dt.Rows[i]["Reason"].ToString().Trim() == "" ? null : dt.Rows[i]["Reason"].ToString();
                        obj.ROWNUMBER = Convert.ToInt32(dt.Rows[i]["ROWNUMBER"] == null || dt.Rows[i]["ROWNUMBER"].ToString().Trim() == "" ? null : dt.Rows[i]["ROWNUMBER"].ToString());
                        obj.PageCount = Convert.ToInt32(dt.Rows[i]["PageCount"] == null || dt.Rows[i]["PageCount"].ToString().Trim() == "" ? null : dt.Rows[i]["PageCount"].ToString());
                        obj.PageSize = Convert.ToInt32(dt.Rows[i]["PageSize"] == null || dt.Rows[i]["PageSize"].ToString().Trim() == "" ? null : dt.Rows[i]["PageSize"].ToString());
                        obj.PageIndex = Convert.ToInt32(dt.Rows[i]["PageIndex"] == null || dt.Rows[i]["PageIndex"].ToString().Trim() == "" ? null : dt.Rows[i]["PageIndex"].ToString());
                        obj.TotalRecord = Convert.ToInt32(dt.Rows[i]["TotalRecord"] == null || dt.Rows[i]["TotalRecord"].ToString().Trim() == "" ? null : dt.Rows[i]["TotalRecord"].ToString());
                        lstTeacherAttandenceList.Add(obj);
                    }
                }
                cls.LSTTeacherAttandenceList = lstTeacherAttandenceList;
                if (cls.LSTTeacherAttandenceList.Count > 0)
                {
                    var pager = new Models.Pager((int)cls.LSTTeacherAttandenceList[0].TotalRecord, cls.PageIndex, (int)cls.PageSize);

                    cls.Pager = pager;
                }
                cls.TotalEntries = TotalEntries;
                cls.ShowingEntries = showingEntries;
                cls.fromEntries = startentries;
                cls.LSTTeacherAttandenceList = lstTeacherAttandenceList;

                return PartialView("_FilterTeacherAttandenceListPartial", cls);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult GetSingleTeacherAttandenceData(TeacherAttandenceModel cls)
        {
            try
            {
                cls = cls.GetTeacherAttandence(cls);
                return Json(cls, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult deleteTeacherAttandence(TeacherAttandenceModel cls)
        {
            try
            {
                cls = cls.deleteTeacherAttandence(cls);
                return Json(cls, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetTeacher(TeacherModel cls)
        {
            try
            {
                List<TeacherModel> lstClientList = new List<TeacherModel>();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetTeacherList", conn);
                cmd.Parameters.AddWithValue("@UserId", objCommon.getUserIdFromSession());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                conn.Close();


                if (dt != null && dt.Rows.Count > 0)
                {

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        TeacherModel obj = new TeacherModel();

                        obj.Id = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.FullName = dt.Rows[i]["FullName"] == null || dt.Rows[i]["FullName"].ToString().Trim() == "" ? null : dt.Rows[i]["FullName"].ToString();

                        lstClientList.Add(obj);
                    }
                }
                cls.LSTTeacherList = lstClientList;

                return Json(cls, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ActionResult GetMonth(MonthModel cls)
        {
            try
            {
                List<MonthModel> lstClientList = new List<MonthModel>();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetMonthList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                conn.Close();


                if (dt != null && dt.Rows.Count > 0)
                {

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        MonthModel obj = new MonthModel();

                        obj.Id = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.MonthName = dt.Rows[i]["MonthName"] == null || dt.Rows[i]["MonthName"].ToString().Trim() == "" ? null : dt.Rows[i]["MonthName"].ToString();

                        lstClientList.Add(obj);
                    }
                }
                cls.LSTMonthList = lstClientList;

                return Json(cls, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ActionResult GetYear(YearModel cls)
        {
            try
            {
                List<YearModel> lstClientList = new List<YearModel>();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetYearList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                conn.Close();


                if (dt != null && dt.Rows.Count > 0)
                {

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        YearModel obj = new YearModel();

                        obj.Id = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.Year = dt.Rows[i]["Year"] == null || dt.Rows[i]["Year"].ToString().Trim() == "" ? null : dt.Rows[i]["Year"].ToString();

                        lstClientList.Add(obj);
                    }
                }
                cls.LSTYearList = lstClientList;

                return Json(cls, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ClientBulkUpload(TeacherAttandenceModel cls)
        {
            try
            {
                if (objCommon.getComapanyUserIdFromSession() != 0)
                {
                    string filePath = string.Empty;
                    //clsClient cls = new clsClient();
                    if (cls.file != null)
                    {
                        string path = Server.MapPath("ManageClient/Data/BulkClient/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        filePath = path + Path.GetFileName(cls.file[0].FileName);
                        string extension = Path.GetExtension(cls.file[0].FileName);
                        cls.file[0].SaveAs(filePath);

                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                break;
                            case ".xlsx": //Excel 07 and above.
                                conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                break;
                        }

                        DataTable dt = new DataTable();
                        conString = string.Format(conString, filePath, "NO");


                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    cls.LSTTeacherAttandenceList = validateData(dt);
                                    if (cls.LSTTeacherAttandenceList[0].strerrorMessage == "")
                                    {
                                        cls.LSTTeacherAttandenceList[0].strerrorMessage = "Client Uploaded Successfully";
                                        InsertBulkIntoDatabase(dt);
                                    }
                                    else
                                    {

                                    }
                                    connExcel.Close();
                                }
                            }
                        }
                    }
                }
                //else
                //{
                //    return Redirect(objCommon.RedirectToLogin(2));
                //}
                var jsonResult = Json(cls.LSTTeacherAttandenceList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        int rowNo = 0;
        string errorMessage = "";

        public List<TeacherAttandenceModel> validateData(DataTable dt)
        {
            bool? status = true;
            TeacherAttandenceModel obj = new TeacherAttandenceModel();
            List<TeacherAttandenceModel> lst = new List<TeacherAttandenceModel>();
            try
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    errorMessage = "";
                    rowNo = i + 1;
                    TeacherAttandenceModel cls = new TeacherAttandenceModel();

                    //cls.intUserType = 3;
                    //cls.strCompanyName = dt.Rows[i][0].ToString();
                    //if (cls.strCompanyName == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>Company name should not be blank.";
                    //}
                    //cls.strFirstName = dt.Rows[i][1].ToString();

                    //if (cls.strFirstName == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>First name should not be blank.";
                    //}

                    //cls.strMiddleName = dt.Rows[i][2].ToString();
                    //if (cls.strMiddleName == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>Middle name should not be blank.";
                    //}

                    //cls.strLastName = dt.Rows[i][3].ToString();
                    //if (cls.strLastName == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>Last name should not be blank.";
                    //}

                    //cls.strMobileno = dt.Rows[i][5].ToString();
                    //if (cls.strMobileno == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>mobile number should not be blank.";
                    //}
                    //else if (cls.strMobileno.Length != 10)
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>mobile number should be in 10 Digit.";
                    //}

                    //cls.strPhoneno = dt.Rows[i][6].ToString();
                    //if (cls.strPhoneno == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>phone number should not be blank.";
                    //}
                    //else if (cls.strPhoneno.Length != 10)
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>phone number should be in 10 Digit.";
                    //}

                    //cls.strAddress = dt.Rows[i][7].ToString();
                    //cls.strCityName = dt.Rows[i][8].ToString();
                    //if (cls.strCityName == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>city name should not be blank.";
                    //}

                    //cls.strAreaName = dt.Rows[i][9].ToString();
                    //if (cls.strAreaName == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>area name should not be blank.";
                    //}

                    //cls.strClientTypeName = dt.Rows[i][10].ToString();
                    //if (cls.strClientTypeName == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>client type should not be blank.";
                    //}

                    //cls.strServiceProvider = dt.Rows[i][11].ToString();
                    //if (cls.strServiceProvider == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>service provider should not be blank.";
                    //}

                    //cls.strEntity = dt.Rows[i][12].ToString();
                    //if (cls.strEntity == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>entity name should not be blank.";
                    //}
                    //cls.strEmail = dt.Rows[i][4].ToString();
                    //if (cls.strEmail == "")
                    //{
                    //    status = false;
                    //    errorMessage = errorMessage + "<br>Email should not be blank.";
                    //}
                    //else if (cls.strEmail != "")
                    //{
                    //    status = cls.CheckEmailInBulkUpdate(cls.strEmail);
                    //    if (status == false)
                    //    {
                    //        errorMessage = errorMessage + "<br>Email already exists.";
                    //    }
                    //}
                    cls.TeacherName = dt.Rows[i][1].ToString();
                    cls.Status = Convert.ToBoolean(dt.Rows[i][2].ToString());
                    cls.Date = dt.Rows[i][3].ToString();
                    cls.LeaveType = Convert.ToInt32(dt.Rows[i][4].ToString());
                    cls.Reason = dt.Rows[i][5].ToString();
                    cls.strerrorMessage = errorMessage;
                    lst.Add(cls);
                }
                obj.LSTTeacherAttandenceList = lst;
            }
            catch (Exception ex)
            {
                throw ex;
                errorMessage = errorMessage + "|" + rowNo;
                status = false;
                return obj.LSTTeacherAttandenceList;
            }


            return obj.LSTTeacherAttandenceList;
        }

        public void InsertBulkIntoDatabase(DataTable dt)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    TeacherAttandenceModel cls = new TeacherAttandenceModel();
                    Random rnd = new Random();
                    int RandomNo = rnd.Next(1000, 10000);
                    //cls.strClientUniqueId = "Client_" + RandomNo.ToString();//Client_1123

                    //string cityName = dt.Rows[i][8].ToString();
                    //int city = 0;
                    //city = cls.CheckCityForBulkUpdate(cityName);

                    //if (city != 0)
                    //{
                    //    cls.intCityId = city;
                    //}
                    //else
                    //{
                    //    cls.intCityId = cls.addCity(cityName);
                    //}
                    //// Area Logic
                    //string areaName = dt.Rows[i][9].ToString();
                    //int area = 0;
                    //area = cls.CheckAreaForBulkUpdate(areaName, (int)cls.intCityId);

                    //if (area != 0)
                    //{
                    //    cls.intAreaId = area;
                    //}
                    //else
                    //{
                    //    cls.intAreaId = cls.addArea(areaName, (int)cls.intCityId);
                    //}
                    //// ClientName
                    //string clientName = dt.Rows[i][10].ToString();
                    //int client = 0;
                    //client = cls.CheckClientTypeForBulkUpdate(clientName);
                    //if (client != 0)
                    //{
                    //    cls.intClientTypeId = client;
                    //}
                    //else
                    //{
                    //    cls.intClientTypeId = cls.addClientType(clientName);
                    //}
                    //// ServiceProvider Logic
                    //string spName = dt.Rows[i][11].ToString();
                    //int serviceprovider = 0;
                    //serviceprovider = cls.CheckServiceProviderForBulkUpdate(spName);

                    //if (serviceprovider != 0)
                    //{
                    //    cls.intServiceProviderId = serviceprovider;
                    //}
                    //else
                    //{
                    //    cls.intServiceProviderId = cls.addServiceProvider(spName);
                    //}
                    //// Entity Logic
                    //string entityName = dt.Rows[i][12].ToString();
                    //int entity = 0;
                    //entity = cls.CheckEntityForBulkUpdate(entityName);

                    //if (entity != 0)
                    //{
                    //    cls.intEntityId = entity;
                    //}
                    //else
                    //{
                    //    cls.intEntityId = cls.addEntity(entityName);
                    //}
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertBulkClientData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Teacher", SqlDbType.VarChar).Value = dt.Rows[i][1].ToString();
                    cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = dt.Rows[i][2].ToString();
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = dt.Rows[i][3].ToString();
                    cmd.Parameters.Add("@LeaveType", SqlDbType.Int).Value = dt.Rows[i][4].ToString();
                    cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = dt.Rows[i][5].ToString();
                    //cmd.Parameters.Add("@strEmail", SqlDbType.VarChar).Value = dt.Rows[i][5].ToString();
                    //cmd.Parameters.Add("@strMobileno", SqlDbType.VarChar).Value = dt.Rows[i][5].ToString();
                    //cmd.Parameters.Add("@strPhoneno", SqlDbType.VarChar).Value = dt.Rows[i][6].ToString();
                    //cmd.Parameters.Add("@strAddress", SqlDbType.VarChar).Value = dt.Rows[i][7].ToString();
                    //cmd.Parameters.Add("@intCompanyId", SqlDbType.Int).Value = objCommon.getCompanyIdFromSessionForCompany();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.CommandTimeout = 0;
                    da.ReturnProviderSpecificTypes = true;
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}